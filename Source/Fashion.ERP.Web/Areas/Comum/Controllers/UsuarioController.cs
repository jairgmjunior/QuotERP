using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Common.Utils;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class UsuarioController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly IRepository<Permissao> _permissaoRepository;
        private readonly IRepository<PerfilDeAcesso> _perfilDeAcessoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public UsuarioController(ILogger logger, IRepository<Usuario> usuarioRepository, 
            IRepository<Permissao> permissaoRepository, IRepository<PerfilDeAcesso> perfilDeAcessoRepository,
            IRepository<Pessoa> pessoaRepository)
        {
            _logger = logger;
            _usuarioRepository = usuarioRepository;
            _permissaoRepository = permissaoRepository;
            _perfilDeAcessoRepository = perfilDeAcessoRepository;
            _pessoaRepository = pessoaRepository;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var model = _usuarioRepository.Find().ToList();
            return View(model);
        }
        #endregion

        #region Novo

        [PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo()
        {
            var model = new NovoUsuarioModel();
            model.Populate();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Novo(NovoUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = Mapper.Unflat<Usuario>(model);
                domain.CriptografarSenha(model.Senha);

                domain.ClearPerfisDeAcesso();
                var perfisDeAcesso = _perfilDeAcessoRepository.Find(x => model.PerfisDeAcesso.Contains(x.Id)).ToList();
                domain.AddRangePerfisDeAcesso(perfisDeAcesso);

                domain.ClearPermissoes();
                var permissoes = _permissaoRepository.Find(x => model.Permissoes.Contains(x.Id)).ToList();
                domain.AddRangePermissoes(permissoes);

                _usuarioRepository.Save(domain);

                this.AddSuccessMessage("Usuário cadastrado com sucesso!");
                return RedirectToAction("Index");
            }

            model.Populate();
            return View(model);
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData]
        public virtual ActionResult Editar(long id)
        {
            var domain = _usuarioRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<EditarUsuarioModel>(domain);
                model.Permissoes = domain.Permissoes.Select(x => x.Id).ToArray();
                model.PerfisDeAcesso = domain.PerfisDeAcesso.Select(x => x.Id).ToArray();
                model.Populate();
                return View("Editar", model);
            }

            this.AddErrorMessage("Não é possível editar o usuário. Confira se os dados foram informados corretamente.");

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(EditarUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuarioRepository.Get(model.Id);
                var domain = Mapper.Unflat(model, usuario);

                domain.ClearPerfisDeAcesso();
                var perfisDeAcesso = _perfilDeAcessoRepository.Find(x => model.PerfisDeAcesso.Contains(x.Id)).ToList();
                domain.AddRangePerfisDeAcesso(perfisDeAcesso);

                domain.ClearPermissoes();
                var permissoes = _permissaoRepository.Find(x => model.Permissoes.Contains(x.Id)).ToList();
                domain.AddRangePermissoes(permissoes);

                if (model.ResetarSenha)
                    domain.CriptografarSenha(model.NovaSenha);

                _usuarioRepository.Merge(domain);

                PermissaoHelper.SetRemoveUserPermissionCache(model.Login);

                this.AddSuccessMessage("Usuário alterado com sucesso.");
                return RedirectToAction("Index");
            }

            model.Populate();
            return View(model);
        }
        #endregion

        #region Excluir

        [HttpPost, ExportModelStateToTempData]
        public virtual ActionResult Excluir(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _usuarioRepository.Get(id);
                    _usuarioRepository.Delete(domain);

                    this.AddSuccessMessage("Usuário excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o usuário: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #region AlterarSenha

        public virtual ActionResult AlterarSenha()
        {
            var usuario = PermissaoHelper.BuscaUsuario();

            var model = new AlterarSenhaUsuarioModel
                            {
                                Id = usuario.Id.GetValueOrDefault(),
                                Nome = usuario.Nome
                            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult AlterarSenha(AlterarSenhaUsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuarioRepository.Get(model.Id);

                // Validar a senha do usuário
                if (!SimpleHash.VerifyHash(model.SenhaAtual, HashAlgorithmType.Sha512, usuario.Senha))
                    ModelState.AddModelError("SenhaAtual", "A Senha atual está incorreta.");

                if (ModelState.IsValid)
                {
                    try
                    {
                        usuario.CriptografarSenha(model.Senha);
                        _usuarioRepository.Update(usuario);

                        this.AddSuccessMessage("Senha alterada com sucesso.");
                        return Redirect("/");
                    }
                    catch (Exception exception)
                    {
                        ModelState.AddModelError(string.Empty, exception);
                    }
                }
            }

            return View(model);
        }
        #endregion

        #endregion

        #region Métodos

        #region PopulateViewData
        protected void PopulateViewData(UsuarioModel model)
        {
            var funcionarios = _pessoaRepository.Find(p => p.Funcionario != null && p.Funcionario.Ativo).ToList();
            ViewData["Funcionario"] = funcionarios.ToSelectList("Nome");
        }
        #endregion

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var usuario = model as UsuarioModel;

        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
        }
        #endregion

        #endregion
    }
}