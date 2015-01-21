using System;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class RequisicaoMaterialCancelamentoController : BaseController
    {
        #region Variaveis

        private readonly IRepository<RequisicaoMaterial> _requisicaoMaterialRepository;
        private readonly IRepository<RequisicaoMaterialItem> _requisicaoMaterialItemRepository;
        private readonly ILogger _logger;             

        #endregion

        #region Construtores

        public RequisicaoMaterialCancelamentoController(ILogger logger, IRepository<RequisicaoMaterial> requisicaoMaterialRepository,
                                      IRepository<RequisicaoMaterialItem> requisicaoMaterialItemRepository)
        {
            _requisicaoMaterialRepository = requisicaoMaterialRepository;
            _requisicaoMaterialItemRepository = requisicaoMaterialItemRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewDataItemCancelado")]
        public virtual ActionResult Cancelar(long id)
        {
            var domain = _requisicaoMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<RequisicaoMaterialCancelamentoModel>(domain);
                model.SituacaoRequisicaoMaterialDescricao = domain.SituacaoRequisicaoMaterial.EnumToString();
                
                model.GridItemCancelado = domain.RequisicaoMaterialItems.Select(p => new GridRequisicaoMaterialItemCanceladoModel()
                {
                    Id = p.Id.GetValueOrDefault(),
                    Referencia = p.Material.Referencia,
                    Descricao = p.Material.Descricao,
                    UND = p.Material.UnidadeMedida.Sigla,
                    QtdeAtendida = p.QuantidadeAtendida,
                    QtdePendente = p.QuantidadePendente,
                    Check = false,
                    SituacaoRequisicaoMaterialDescricao = p.SituacaoRequisicaoMaterial.EnumToString()
                }).ToList();

                return View(model);
            }

            this.AddErrorMessage("Não foi possível encontrar o requisição de material.");
            return RedirectToAction("Index", "RequisicaoMaterial");
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewDataItemCancelado")]
        public virtual ActionResult Cancelar(RequisicaoMaterialCancelamentoModel model)
        {
           if (ModelState.IsValid)
           {
                try
                {
                    foreach (var modelGrid in model.GridItemCancelado)
                    {
                        var requisicaoMaterialItemCancelado = new RequisicaoMaterialItemCancelado();

                        if (modelGrid.Check)
                        {
                            var domain = _requisicaoMaterialItemRepository.Get(modelGrid.Id);
                            
                            requisicaoMaterialItemCancelado.Observacao = model.ObservacaoCancelamento;

                            requisicaoMaterialItemCancelado.Data = DateTime.Now;
                          
                            requisicaoMaterialItemCancelado.CalculeQuantidade(domain);

                            domain.AtualizeSituacao();

                            domain.RequisicaoMaterialItemCancelado = requisicaoMaterialItemCancelado;
                            _requisicaoMaterialItemRepository.Update(domain);
                        }  
                    }

                    var requisicaoMaterial = _requisicaoMaterialRepository.Get(model.Id);
                    requisicaoMaterial.AtualizeSituacao();
                    _requisicaoMaterialRepository.SaveOrUpdate(requisicaoMaterial);
                    
                    this.AddSuccessMessage("Item cancelados com sucesso.");
                    return RedirectToAction("Index", "RequisicaoMaterial");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Não é possível salvar o cancelamento da requisição material. Confira se os dados foram informados corretamente: " +
                        exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }
           
            return View(model);
        }

        #endregion

        #region PopulateViewData
        protected void PopulateViewDataItemCancelado(RequisicaoMaterialCancelamentoModel model)
        {
        }
        #endregion
    }
}


