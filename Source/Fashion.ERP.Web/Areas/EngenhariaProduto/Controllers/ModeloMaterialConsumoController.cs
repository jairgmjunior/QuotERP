using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.EngenhariaProduto.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using WebGrease.Css.Extensions;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class ModeloMaterialConsumoController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Cor> _corRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<SequenciaProducao> _sequenciaProducaoRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<VariacaoModelo> _variacaoModeloRepository;
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public ModeloMaterialConsumoController(ILogger logger, IRepository<Modelo> modeloRepository, 
            IRepository<Cor> corRepository, IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<Tamanho> tamanhoRepository, IRepository<UnidadeMedida> unidadeMedidaRepository,
            IRepository<VariacaoModelo> variacaoModeloRepository, IRepository<SetorProducao> setorProducaoRepository,
            IRepository<Material> materialRepository,
            IRepository<SequenciaProducao> sequenciaRepository)
        {
            _modeloRepository = modeloRepository;
            _corRepository = corRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _tamanhoRepository = tamanhoRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _variacaoModeloRepository = variacaoModeloRepository;
            _setorProducaoRepository = setorProducaoRepository;
            _materialRepository = materialRepository;
            _sequenciaProducaoRepository = sequenciaRepository;
            _logger = logger;
        }
        #endregion

        #region MaterialComposicao
        [PopulateViewData("PopulaComposicaoViewData")]
        public virtual ActionResult ModeloMaterialConsumo(long modeloId)
        {
            var modelo = _modeloRepository.Get(modeloId);

            var model = new ModeloMaterialConsumoModel
            {
                ModeloId = modelo.Id ?? 0,
                ModeloReferencia = modelo.Referencia,
                ModeloDescricao = modelo.Descricao,
                ModeloEstilistaNome = modelo.Estilista.Nome,
                ModeloDataCriacao = modelo.DataCriacao,
                GridItens = new List<GridModeloMaterialConsumoModel>()
            };

            var materiaisConsumo = modelo.MateriaisConsumo.OrderBy(o => o.DepartamentoProducao.Id);
            foreach (var materialComsumo in materiaisConsumo)
            {
                model.GridItens.Add(new GridModeloMaterialConsumoModel
                {
                    Id = materialComsumo.Id,
                    DepartamentoProducao = materialComsumo.DepartamentoProducao.Nome,
                    Referencia = materialComsumo.Material.Referencia,
                    Descricao = materialComsumo.Material.Descricao,
                    UnidadeMedida = materialComsumo.UnidadeMedida.Sigla,
                    Quantidade = materialComsumo.Quantidade
                });
            }

            return View(model);
        }

        [HttpPost, PopulateViewData("PopulaComposicaoViewData")]
        public virtual ActionResult ModeloMaterialConsumo(ModeloMaterialConsumoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var modelo = _modeloRepository.Get(model.ModeloId);

                    model.GridItens.ForEach(materialModel =>
                    {
                        if (materialModel.Id != null && materialModel.Id != 0)
                        {
                            var materialConsumo = modelo.MateriaisConsumo.First(x => x.Id == materialModel.Id);

                            materialConsumo.DepartamentoProducao =
                                _departamentoProducaoRepository.Get(x => x.Nome == materialModel.DepartamentoProducao);
                            materialConsumo.Quantidade = materialModel.Quantidade;
                        }
                        else
                        {
                            var modeloMaterialConsumo = new ModeloMaterialConsumo
                            {
                                DepartamentoProducao =
                                    _departamentoProducaoRepository.Get(x => x.Nome == materialModel.DepartamentoProducao),
                                Material = _materialRepository.Get(x => x.Referencia == materialModel.Referencia),
                                Quantidade = materialModel.Quantidade,
                                UnidadeMedida = _unidadeMedidaRepository.Get(x => x.Sigla == materialModel.UnidadeMedida)
                            };
                            modelo.MateriaisConsumo.Add(modeloMaterialConsumo);    
                        }
                    });

                    var listaExcluir = new List<ModeloMaterialConsumo>();

                    modelo.MateriaisConsumo.ForEach(materialConsumo =>
                    {
                        if (model.GridItens.All(x => x.Referencia != materialConsumo.Material.Referencia))
                        {
                            listaExcluir.Add(materialConsumo);
                        }
                    });

                    foreach (var materialConsumo in listaExcluir)
                    {
                        modelo.MateriaisConsumo.Remove(materialConsumo);
                    }

                    _modeloRepository.SaveOrUpdate(modelo);

                    this.AddSuccessMessage("Materiais de consumo do modelo atualizados com sucesso.");
                    return RedirectToAction("Detalhar", "Modelo", new { modeloId = model.ModeloId });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao salvar os materiais de consumo do modelo. Confira se os dados foram informados corretamente: " +
                        exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }
            return View(model);
        }

        #region PopulaComposicaoViewData
        protected void PopulaComposicaoViewData(ModeloMaterialConsumoModel model)
        {
            // DepartamentoProducao
            var departamentos = _modeloRepository.Get(model.ModeloId).SequenciaProducoes.Select(p => p.DepartamentoProducao).ToList();
            ViewBag.DepartamentoProducaos = departamentos.Select(s => new { Id = s.Nome, s.Nome }).OrderBy(x => x.Nome);
        }
        #endregion

        #endregion
    }
}