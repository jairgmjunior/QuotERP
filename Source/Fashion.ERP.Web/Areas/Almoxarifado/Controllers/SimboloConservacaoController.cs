using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Extensions;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Fashion.Framework.UnitOfWork.DinamicFilter;
using NHibernate.Linq;
using Ninject.Extensions.Logging;
using System.Text;


namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class SimboloConservacaoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<SimboloConservacao> _simboloRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public SimboloConservacaoController(ILogger logger, IRepository<SimboloConservacao> simboloRepository)
        {
            _simboloRepository = simboloRepository;
            _logger = logger;
        }
        #endregion



        #region Index
        public virtual ActionResult Index()
        {
            var simbolos = _simboloRepository.Find();
            var model = new PesquisaSimboloConservacaoModel{ModoConsulta = "Listar"};

            model.Grid = simbolos.Select(s => new GridSimboloConservacaoModel
            {
                Id = s.Id.GetValueOrDefault(),
                Descricao = s.Descricao,
                CategoriaConservacao = s.CategoriaConservacao.ToString(),
                Foto = (s.Foto != null ? s.Foto.Nome.GetFileUrl() : string.Empty)
            }
                ).OrderBy(s => s.Descricao).ToList();

            return View(model);
        }

        //[HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataPesquisa")]
        //public virtual ActionResult Index(PesquisaSimboloConservacaoModel model)
        //{
        //    var simbolosConservacao = _simboloRepository.Find();

        //    try
        //    {
        //        #region Filtros

        //        var filtros = new StringBuilder();

        //        if (model.Descricao != String.Empty)
        //        {
        //            simbolosConservacao = simbolosConservacao.Where(s => s.Descricao.Contains(model.Descricao));
        //            filtros.AppendFormat("Descrição: {0}, ",
        //                                 model.Descricao);
        //        }

        //        if (model.CategoriaConservacao.HasValue)
        //        {
        //            simbolosConservacao = simbolosConservacao.Where(p => p.CategoriaConservacao == model.CategoriaConservacao.Value);
        //            filtros.AppendFormat("Categoria: {0}, ", model.CategoriaConservacao.Value);
        //        }


        //        #endregion

        //        // Verifica se é uma listagem
        //        if (model.ModoConsulta == "Listar")
        //        {
        //            if (model.OrdenarPor != null)
        //                simbolosConservacao = model.OrdenarEm == "asc"
        //                                    ? simbolosConservacao.OrderBy(model.OrdenarPor)
        //                                    : simbolosConservacao.OrderByDescending(model.OrdenarPor);

        //            model.Grid = simbolosConservacao.Select(p => new GridSimboloConservacaoModel
        //            {
        //                Id = p.Id.GetValueOrDefault(),
        //                Descricao = p.Descricao,
        //                CategoriaConservacao = p.CategoriaConservacao.ToString(),
        //                Foto = (p.Foto != null ? p.Foto.Nome.GetFileUrl() : string.Empty)
        //            }).ToList();

        //            return View(model);
        //        }

        //        // Se não é uma listagem, gerar o relatório
        //        var result = simbolosConservacao.Fetch(s => s.Descricao).Fetch(s => s.CategoriaConservacao).ToList();

        //        if (!result.Any())
        //            return Json(new { Error = "Nenhum item encontrado." });

        //        #region Montar Relatório

        //        var report = new ListaPedidoCompraReport { DataSource = result };

        //        if (filtros.Length > 2)
        //            report.ReportParameters["Filtros"].Value = filtros.ToString().Substring(0, filtros.Length - 2);

        //        var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

        //        if (model.AgruparPor != null)
        //        {
        //            grupo.Groupings.Add("=Fields." + model.AgruparPor);

        //            var key = ColunasPesquisaPedidoCompra.First(p => p.Value == model.AgruparPor).Key;
        //            var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.AgruparPor);
        //            grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
        //        }
        //        else
        //        {
        //            report.Groups.Remove(grupo);
        //        }

        //        if (model.OrdenarPor != null)
        //            report.Sortings.Add("=Fields." + model.OrdenarPor,
        //                                model.OrdenarEm == "asc" ? SortDirection.Asc : SortDirection.Desc);

        //        #endregion

        //        var filename = report.ToByteStream().SaveFile(".pdf");

        //        return Json(new { Url = filename });
        //    }
        //    catch (Exception exception)
        //    {
        //        var message = exception.GetMessage();
        //        _logger.Info(message);

        //        if (HttpContext.Request.IsAjaxRequest())
        //            return Json(new { Error = message });

        //        ModelState.AddModelError(string.Empty, message);
        //        return View(model);
        //    }
        //}
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new SimboloConservacaoModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(SimboloConservacaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<SimboloConservacao>(model);
                    domain.Foto = !string.IsNullOrWhiteSpace(domain.Foto.Nome)
                        ? ArquivoController.SalvarArquivo(model.FotoNome, model.Descricao)
                        : null;

                    _simboloRepository.Save(domain);

                    this.AddSuccessMessage("Simbolo cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar um símbolo! Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Editar

        [ImportModelStateFromTempData]
        public virtual ActionResult Editar(long id)
        {
            var domain = _simboloRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<SimboloConservacaoModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o símbolo.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(SimboloConservacaoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _simboloRepository.Get(model.Id));

                    _simboloRepository.Update(domain);

                    this.AddSuccessMessage("Simbolo atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o símbolo. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Excluir

        [HttpPost, ValidateAntiForgeryToken, ExportModelStateToTempData]
        public virtual ActionResult Excluir(long? id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _simboloRepository.Get(id);
                    _simboloRepository.Delete(domain);

                    this.AddSuccessMessage("Símbool excluído com sucesso");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError("", "Ocorreu um erro ao excluir o símbolo: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return RedirectToAction("Editar", new { id });
        }
        #endregion


        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            //var categoria = model as CategoriaModel;

            //// Verificar duplicado
            //if (_simboloRepository.Find(p => p.Nome == categoria.Nome && p.Id != model.Id).Any())
            //    ModelState.AddModelError("Nome", "Já existe uma categoria cadastrada com este nome.");
        }
        #endregion

        #region ValidaExcluir
        protected override void ValidaExcluir(long id)
        {
            //var domain = _simboloRepository.Get(id);

            //// Verificar se existe uma subcategoria com esta categoria
            //if (_subcategoriaRepository.Find().Any(p => p.Categoria == domain))
            //    ModelState.AddModelError("", "Não é possível excluir esta categoria, pois existe(m) subcateria(s) associadas a ela.");
        }
        #endregion

        #endregion
    }
}