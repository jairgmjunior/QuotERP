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
using FluentNHibernate.Conventions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using NHibernate.Linq;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.EngenhariaProduto.Controllers
{
    public partial class MaterialComposicaoModeloController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Modelo> _modeloRepository;
        private readonly IRepository<Cor> _corRepository;
        private readonly IRepository<DepartamentoProducao> _departamentoProducaoRepository;
        private readonly IRepository<SequenciaProducao> _sequenciaProducaoRepository;
        private readonly IRepository<Tamanho> _tamanhoRepository;
        private readonly IRepository<UnidadeMedida> _unidadeMedidaRepository;
        private readonly IRepository<Variacao> _variacaoRepository;
        private readonly IRepository<SetorProducao> _setorProducaoRepository;
        private readonly IRepository<Material> _materialRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public MaterialComposicaoModeloController(ILogger logger, IRepository<Modelo> modeloRepository, 
            IRepository<Cor> corRepository, IRepository<DepartamentoProducao> departamentoProducaoRepository,
            IRepository<Tamanho> tamanhoRepository, IRepository<UnidadeMedida> unidadeMedidaRepository,
            IRepository<Variacao> variacaoRepository, IRepository<SetorProducao> setorProducaoRepository,
            IRepository<Material> materialRepository,
            IRepository<SequenciaProducao> sequenciaRepository)
        {
            _modeloRepository = modeloRepository;
            _corRepository = corRepository;
            _departamentoProducaoRepository = departamentoProducaoRepository;
            _tamanhoRepository = tamanhoRepository;
            _unidadeMedidaRepository = unidadeMedidaRepository;
            _variacaoRepository = variacaoRepository;
            _setorProducaoRepository = setorProducaoRepository;
            _materialRepository = materialRepository;
            _sequenciaProducaoRepository = sequenciaRepository;
            _logger = logger;
        }
        #endregion

        #region MaterialComposicao
        [PopulateViewData("PopulaComposicaoViewData")]
        public virtual ActionResult MaterialComposicaoModelo(long modeloId)
        {
            var modelo = _modeloRepository.Get(modeloId);

            var model = new MaterialComposicaoModeloModel
            {
                ModeloId = modelo.Id ?? 0,
                ModeloReferencia = modelo.Referencia,
                ModeloDescricao = modelo.Descricao,
                ModeloEstilistaNome = modelo.Estilista.Nome,
                ModeloDataCriacao = modelo.DataCriacao,
                Grid = new List<GridMaterialComposicaoModel>()
            };

            var sequencias = modelo.SequenciaProducoes.OrderBy(o => o.DepartamentoProducao.Id);
            foreach (var sequenciaProducao in sequencias)
            {
                foreach (var composicao in sequenciaProducao.MaterialComposicaoModelos)
                {
                    model.Grid.Add(new GridMaterialComposicaoModel
                    {
                        Id = composicao.Id,
                        Departamento = sequenciaProducao.DepartamentoProducao.Id ?? 0,
                        IdSetor = sequenciaProducao.SetorProducao != null ? sequenciaProducao.SetorProducao.Id : null,
                        Variacao = composicao.Variacao != null ? composicao.Variacao.Id : null,
                        Cor = composicao.Cor != null ? composicao.Cor.Id : null,
                        Tamanho = composicao.Tamanho != null ? composicao.Tamanho.Id : null,
                        Referencia = composicao.Material.Id ?? 0,
                        UnidadeMedida = composicao.UnidadeMedida.Id ?? 0,
                        Quantidade = composicao.Quantidade
                    });
                }
            }

            return View(model);
        }

        [HttpPost, PopulateViewData("PopulaComposicaoViewData")]
        public virtual ActionResult MaterialComposicaoModelo(MaterialComposicaoModeloModel model)
        {
            if (model.Grid.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Adicione pelo menos uma composição.");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                try
                {
                    // Exclui todas as composições
                    var modelo = _modeloRepository.Get(model.ModeloId);

                    var sequenciasProducao = modelo.SequenciaProducoes;

                    sequenciasProducao.ForEach(sp => sp.MaterialComposicaoModelos.Clear());

                    if (ExisteMaterialComposicaoComValoresIguais(model.Grid))
                    {
                        this.AddErrorMessage("Não é possível cadastrar materiais de composição com os parâmetros (departamento, setor, variação, cor e referência) iguais.");
                        return View(model);
                    }

                    // Inclui as composições da tela
                    for (int i = 0; i < model.Grid.Count; i++)
                    {
                        var materialModel = model.Grid[i];

                        var sequenciaProducao =
                            sequenciasProducao.SingleOrDefault(
                                s =>
                                {
                                    if (s.SetorProducao == null)
                                    {
                                        if (s.DepartamentoProducao.Id == materialModel.Departamento)
                                            return true;
                                    }
                                    else
                                    {
                                        if (s.DepartamentoProducao.Id == materialModel.Departamento &&
                                            s.SetorProducao.Id == materialModel.IdSetor)
                                            return true;
                                    }
                                    return false;
                                });

                        var composicao = new MaterialComposicaoModelo
                        {
                            Variacao = materialModel.Variacao != null ? _variacaoRepository.Load(materialModel.Variacao) : null,
                            Cor = materialModel.Cor != null ? _corRepository.Load(materialModel.Cor) : null,
                            Tamanho = materialModel.Tamanho != null ? _tamanhoRepository.Load(materialModel.Tamanho) : null,
                            Material = _materialRepository.Load(materialModel.Referencia),
                            UnidadeMedida = _unidadeMedidaRepository.Load(materialModel.UnidadeMedida),
                            Quantidade = materialModel.Quantidade
                        };

                        sequenciaProducao.MaterialComposicaoModelos.Add(composicao);
                    }

                    modelo.DataAlteracao = DateTime.Now;
                    _modeloRepository.SaveOrUpdate(modelo);

                    this.AddSuccessMessage("Materiais de composição do modelo atualizados com sucesso.");
                    return RedirectToAction("Detalhar", "Modelo", new { modeloId = model.ModeloId });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Ocorreu um erro ao salvar a composição do modelo. Confira se os dados foram informados corretamente: " +
                        exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }
            return View(model);
        }

        private bool ExisteMaterialComposicaoComValoresIguais(IEnumerable<GridMaterialComposicaoModel> materialModel)
        {
            var duplicados = materialModel.Select(m => new { m.Departamento, m.IdSetor, m.Variacao, m.Cor, m.Referencia })
                .GroupBy(item => item).SelectMany(grp => grp.Skip(1));
            return !duplicados.IsEmpty();
        }

        #region PopulaComposicaoViewData
        protected void PopulaComposicaoViewData(MaterialComposicaoModeloModel model)
        {
            // DepartamentoProducao
            var departamentos = _modeloRepository.Get(model.ModeloId)
                            .SequenciaProducoes.Select(p => p.DepartamentoProducao).ToList();

            ViewBag.Departamento = new SelectList(departamentos.Distinct(), "Id", "Nome");
            ViewBag.DepartamentosDicionario = departamentos.Distinct().ToDictionary(k => k.Id, v => v.Nome);

            // SetorProducao
            var setores = _setorProducaoRepository.Find()
                .Select(s => new { s.Id, s.Nome })
                .OrderBy(o => o.Nome).ToList();
            ViewBag.Setor = new SelectList(Enumerable.Empty<string>());
            ViewBag.SetoresDicionario = setores.ToDictionary(k => k.Id, v => v.Nome);

            // Variacao
            var variacoes = _modeloRepository.Load(model.ModeloId).Variacoes
                .Select(s => new { s.Id, s.Nome })
                .OrderBy(o => o.Nome);
            ViewBag.Variacao = new SelectList(variacoes, "Id", "Nome");
            ViewBag.VariacoesDicionario = variacoes.ToDictionary(k => k.Id, v => v.Nome);

            // Cor
            var cores = _corRepository.Find()
                .Select(s => new { s.Id, s.Nome })
                .OrderBy(o => o.Nome).ToList();
            ViewBag.Cor = new SelectList(Enumerable.Empty<string>());
            ViewBag.CoresDicionario = cores.ToDictionary(k => k.Id, v => v.Nome);

            // Tamanho
            var tamanhosModelo = _modeloRepository.Get(model.ModeloId).Grade.Tamanhos.Select(t => t.Key);
            ViewBag.Tamanho = new SelectList(tamanhosModelo, "Id", "Sigla");

            var tamanhos = _tamanhoRepository.Find()
                .Select(s => new { s.Id, s.Sigla, s.Ativo })
                .OrderBy(o => o.Sigla).ToList();
            ViewBag.TamanhosDicionario = tamanhos.ToDictionary(k => k.Id, v => v.Sigla);

            // Material
            var materiais = _materialRepository.Find()
                .Select(s => new { s.Id, s.Referencia })
                .OrderBy(o => o.Referencia);
            ViewBag.MateriaisDicionario = materiais.ToDictionary(k => k.Id, v => v.Referencia);

            // UnidadeMedida
            var unidadeMedidas = _unidadeMedidaRepository.Find()
                .Select(s => new { s.Id, s.Sigla, s.Ativo })
                .OrderBy(o => o.Sigla).ToList();
            ViewBag.UnidadeMedida = new SelectList(unidadeMedidas.Where(p => p.Ativo), "Id", "Sigla");
            ViewBag.UnidadeMedidasDicionario = unidadeMedidas.ToDictionary(k => k.Id, v => v.Sigla);
        }
        #endregion

        #region CoresVariacao
        [OutputCache(Duration = 60, VaryByParam = "id"), AjaxOnly]
        public virtual JsonResult CoresVariacao(long id /* Id da variacao*/)
        {
            var variacao = _variacaoRepository.Get(id);
            var cores = variacao.Cores.Select(s => new { s.Id, s.Nome }).OrderBy(o => o.Nome);

            return Json(cores, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
        
        #region SequenciaProducao

        [PopulateViewData("PopulaDepartamentoViewData")]
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