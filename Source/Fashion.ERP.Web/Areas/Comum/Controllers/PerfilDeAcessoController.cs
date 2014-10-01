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
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class PerfilDeAcessoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Permissao> _permissaoRepository;
        private readonly IRepository<PerfilDeAcesso> _perfilDeAcessoRepository;
        private readonly IRepository<Usuario> _usuarioRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public PerfilDeAcessoController(ILogger logger, IRepository<Permissao> permissaoRepository,
            IRepository<PerfilDeAcesso> perfilDeAcessoRepository, IRepository<Usuario> usuarioRepository)
        {
            _logger = logger;
            _permissaoRepository = permissaoRepository;
            _perfilDeAcessoRepository = perfilDeAcessoRepository;
            _usuarioRepository = usuarioRepository;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var model = _perfilDeAcessoRepository.Find();
            return View(model);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            var model = new PerfilDeAcessoModel();
            model.Populate();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(PerfilDeAcessoModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = Mapper.Unflat<PerfilDeAcesso>(model);

                domain.ClearPermissoes();
                var permissoes = _permissaoRepository.Find(x => model.Permissoes.Contains(x.Id)).ToList();
                domain.AddRangePermissoes(permissoes);

                if (model.Id.HasValue)
                    _perfilDeAcessoRepository.Merge(domain);
                else
                    _perfilDeAcessoRepository.Save(domain);

                // Marca todos os usuários com esse perfil de acesso para ter o cache de permissões limpo.
                var usuarios = _usuarioRepository.Find(p => p.PerfisDeAcesso.Contains(domain));
                foreach (var usuario in usuarios)
                {
                    PermissaoHelper.SetRemoveUserPermissionCache(usuario.Login);
                }

                this.AddSuccessMessage("Perfil de acesso cadastrado com sucesso!");
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
            var domain = _perfilDeAcessoRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<PerfilDeAcessoModel>(domain);
                model.Permissoes = domain.Permissoes.Select(x => x.Id).ToArray();
                model.Populate();
                return View("Novo", model);
            }

            ModelState.AddModelError("", "Não é possível editar o perfil de acesso. Confira se os dados foram informados corretamente.");

            return RedirectToAction("Index");
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
                    var domain = _perfilDeAcessoRepository.Get(id);
                    _perfilDeAcessoRepository.Delete(domain);

                    this.AddSuccessMessage("Perfil de acesso excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o perfil de acesso: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var perfilDeAcesso = model as PerfilDeAcessoModel;

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
