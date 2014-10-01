using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using FluentNHibernate.Conventions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class SequenciaProducaoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<SequenciaProducao> _sequenciaProducaoRepository;
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public SequenciaProducaoController(ILogger logger, IRepository<Modelo> modeloRepository, 
            IRepository<SetorProducao> setorProducaoRepository,
            IRepository<SequenciaProducao> sequenciaRepository,
            IRepository<DepartamentoProducao> departamentoRepository)
        {
            _modeloRepository = modeloRepository;
            _setorProducaoRepository = setorProducaoRepository;
            _sequenciaProducaoRepository = sequenciaRepository;
            _departamentoProducaoRepository = departamentoRepository;
            _logger = logger;
        }
        #endregion
        
        #region SequenciaProducao

        [HttpGet, PopulateViewData("PopulaDepartamentoViewData")]
        public virtual ActionResult SequenciaProducao(long modeloId)
        {
            var modelo = _modeloRepository.Get(modeloId);

            var sequenciaProducoes = modelo.SequenciaProducoes.Select(p => new SequenciaProducaoModel()
            {
                NomeDepartamento = p.DepartamentoProducao.Nome,
                DepartamentoId = p.DepartamentoProducao.Id,
                NomeSetor = p.SetorProducao != null ? p.SetorProducao.Nome : null,
                SetorId = p.SetorProducao != null ? p.SetorProducao.Id : null,
                ModeloId = modelo.Id,
                Id = p.Id
            }).ToList();

            var model = new SequenciaProducaoModeloModel
            {
                ModeloId = modelo.Id ?? 0,
                ModeloReferencia = modelo.Referencia,
                ModeloDescricao = modelo.Descricao,
                ModeloEstilistaNome = modelo.Estilista.Nome,
                ModeloDataCriacao = modelo.DataCriacao,
                SequenciasProducao = sequenciaProducoes
            };
            
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult SalveSequenciaProducao([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SequenciaProducaoModel> sequenciaProducoes)
        {
            var results = new List<SequenciaProducaoModel>();

            if (sequenciaProducoes != null && ModelState.IsValid)
            {
                try
                {
                    var modelo = _modeloRepository.Get(sequenciaProducoes.First().ModeloId);
                    
                    if (ExisteSequenciasProducaoDuplicadas(sequenciaProducoes))
                    {
                        var msg = "Não é possível existir sequências de produção com os mesmos valores.";
                        ModelState.AddModelError(string.Empty, msg);
                        //this.AddErrorMessage(msg);
                        return Json(results.ToDataSourceResult(request, ModelState));
                    }

                    if (ExisteMaterialComposicaoAssociado(sequenciaProducoes, modelo.SequenciaProducoes))
                    {
                        var msg = "Não é possível excluir uma sequência de produção com material de composição associado a ela.";
                        ModelState.AddModelError(string.Empty, msg);
                        //this.AddErrorMessage(msg);
                        return Json(results.ToDataSourceResult(request, ModelState));
                    }

                    modelo.ClearSequenciaProducao();
                    foreach (var sequenciaProducaoModel in sequenciaProducoes)
                    {
                        SequenciaProducao sequenciaProducao = null;
                        if (sequenciaProducaoModel.Id != null)
                        {
                            //atualiza existente
                            sequenciaProducao = ObtenhaSequenciaProducaoAtualizada(sequenciaProducaoModel);
                        }
                        else
                        {
                            //cria novo
                            sequenciaProducao = ObtenhaSequenciaProducao(sequenciaProducaoModel);
                        }
                        modelo.AddSequenciaProducao(sequenciaProducao);
                    }

                    modelo.DataAlteracao = DateTime.Now;
                    _modeloRepository.Update(modelo);

                    this.AddSuccessMessage("Sequências de produção atualizadas com sucesso.");
                    //return RedirectToAction("Detalhar", "Modelo", new { modeloId = sequenciaProducoes.First().ModeloId });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao salvar as sequências de produção. Confira se os dados foram informados corretamente.");
                    _logger.Info(exception.GetMessage());
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        private bool ExisteSequenciasProducaoDuplicadas(IEnumerable<SequenciaProducaoModel> sequenciaProducoes)
        {
            var duplicados = sequenciaProducoes.Select(s => new { s.NomeDepartamento, s.NomeSetor }).GroupBy(item => item)
               .SelectMany(grp => grp.Skip(1));
            return !duplicados.IsEmpty();
        }

        private SequenciaProducao ObtenhaSequenciaProducao(SequenciaProducaoModel sequenciaProducaoModel)
        {
            return new SequenciaProducao
            {
                DepartamentoProducao =
                    _departamentoProducaoRepository.Find(
                        departamentoProducao =>
                            departamentoProducao.Nome == sequenciaProducaoModel.NomeDepartamento)
                        .FirstOrDefault(),
                SetorProducao = _setorProducaoRepository.Find(
                    setorProducao =>
                        setorProducao.Nome == sequenciaProducaoModel.NomeSetor &&
                        setorProducao.DepartamentoProducao.Nome == sequenciaProducaoModel.NomeDepartamento)
                    .FirstOrDefault()
            };
        }

        private SequenciaProducao ObtenhaSequenciaProducaoAtualizada(SequenciaProducaoModel sequenciaProducaoModel)
        {
            SequenciaProducao sequenciaProducao;
            sequenciaProducao = _sequenciaProducaoRepository.Get(sequenciaProducaoModel.Id);
            if (sequenciaProducao.DepartamentoProducao.Nome != sequenciaProducaoModel.NomeDepartamento)
            {
                sequenciaProducao.DepartamentoProducao = _departamentoProducaoRepository.Find(
                    departamentoProducao =>
                        departamentoProducao.Nome == sequenciaProducaoModel.NomeDepartamento)
                    .FirstOrDefault();
            }

            if (sequenciaProducaoModel.NomeSetor == null)
            {
                sequenciaProducao.SetorProducao = null;
            }
            else
            {
                sequenciaProducao.SetorProducao = _setorProducaoRepository.Find(
                    setorProducao =>
                        setorProducao.Nome == sequenciaProducaoModel.NomeSetor &&
                        setorProducao.DepartamentoProducao.Nome ==
                        sequenciaProducaoModel.NomeDepartamento)
                    .FirstOrDefault();
            }
            return sequenciaProducao;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult AtualizeSequenciaProducao([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SequenciaProducaoModel> sequenciaProducoes)
        {
            //não faz nada, necessário porque a grid do kendo obriga a existência do método para batch updates.
            return Json(sequenciaProducoes.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public virtual ActionResult ExcluaSequenciaProducao([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")]IEnumerable<SequenciaProducaoModel> sequenciaProducoes)
        {
            //não faz nada, necessário porque a grid do kendo obriga a existência do método para batch updates.
            return Json(sequenciaProducoes.ToDataSourceResult(request, ModelState));
        }

        private bool ExisteMaterialComposicaoAssociado(IEnumerable<SequenciaProducaoModel> sequenciasProducaoModel, IEnumerable<SequenciaProducao> sequenciasProducao)
        {
            foreach (var sequenciaDomain in sequenciasProducao)
            {
                SequenciaProducaoModel sequenciaModel = null;

                if (sequenciaDomain.SetorProducao == null)
                {
                    sequenciaModel = sequenciasProducaoModel.SingleOrDefault(
                        model =>
                            model.NomeDepartamento == sequenciaDomain.DepartamentoProducao.Nome &&
                            model.NomeSetor == null);
                }
                else
                {
                    sequenciaModel = sequenciasProducaoModel.SingleOrDefault(
                        model =>
                            model.NomeDepartamento == sequenciaDomain.DepartamentoProducao.Nome &&
                            model.NomeSetor == sequenciaDomain.SetorProducao.Nome);
                }

                if (sequenciaModel == null)
                    return !sequenciaDomain.MaterialComposicaoModelos.IsEmpty();
            }

            return false;
        }

        #region LerSequenciasProducao
        [AjaxOnly, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public virtual ActionResult LerSequenciasProducao([DataSourceRequest]DataSourceRequest request)
        {
            return null;
        }
        #endregion

        #region PopulaDepartamentoViewData
        protected void PopulaDepartamentoViewData()
        {
            // DepartamentoProducao
            var departamentos = _departamentoProducaoRepository.Find().OrderBy(p => p.Nome).ToList();
            ViewBag.Departamento = departamentos.Where(p => p.Ativo).Select(s => new { s.Id, s.Nome });
            ViewBag.DepartamentosDicionario = departamentos.ToDictionary(k => k.Id, v => v.Nome);

            // SetorProducao
            var setores = _setorProducaoRepository.Find().OrderBy(p => p.Nome).ToList();
            ViewBag.Setor = new SelectList(Enumerable.Empty<string>());
            ViewBag.SetoresDicionario = setores.ToDictionary(k => k.Id, v => v.Nome);
        }
        #endregion

        #endregion
    }
}