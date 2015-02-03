using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Web.Areas.Almoxarifado.Models;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Compras.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Mvc.Security;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Almoxarifado.Controllers
{
    public partial class RequisicaoMaterialBaixaController : BaseController
    {
        #region Variaveis

        private readonly IRepository<RequisicaoMaterial> _requisicaoMaterialRepository;
        private readonly IRepository<ReservaEstoqueMaterial> _reservaEstoqueMaterialRepository;
        private readonly IRepository<EstoqueMaterial> _estoqueMaterialRepository;
        private readonly IRepository<DepositoMaterial> _depositoMaterialRepository;
        private readonly IRepository<Usuario> _usuarioRepository;

        private readonly ILogger _logger;             

        #endregion

        #region Construtores

        public RequisicaoMaterialBaixaController(ILogger logger, IRepository<RequisicaoMaterial> requisicaoMaterialRepository,
                                      IRepository<ReservaEstoqueMaterial> reservaEstoqueMaterialRepository,
                                      IRepository<EstoqueMaterial> estoqueMaterialRepository,
                                      IRepository<DepositoMaterial> depositoMaterialRepository,
                                      IRepository<Usuario> usuarioRepository)
        {
            _requisicaoMaterialRepository = requisicaoMaterialRepository;
            _reservaEstoqueMaterialRepository = reservaEstoqueMaterialRepository;
            _estoqueMaterialRepository = estoqueMaterialRepository;
            _depositoMaterialRepository = depositoMaterialRepository;
            _usuarioRepository = usuarioRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Baixar

        [ImportModelStateFromTempData, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Baixar(long id)
        {
            var domain = _requisicaoMaterialRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<RequisicaoMaterialBaixaModel>(domain);
                model.SituacaoRequisicaoMaterialDescricao = domain.SituacaoRequisicaoMaterial.EnumToString();

                var depositoMaterial = ObtenhaDepositoMaterial(model);
                
                model.GridItems = ObtenhaRequisicaoItens(domain, depositoMaterial);

                return View("Baixar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar a requisição de material.");
            return RedirectToAction("Baixar");
        }

        private IList<GridRequisicaoMaterialItemBaixaModel> ObtenhaRequisicaoItens(RequisicaoMaterial domain, long? depositoMaterial)
        {
            return domain.RequisicaoMaterialItems.Select(x => new GridRequisicaoMaterialItemBaixaModel
            {
                Id = x.Id,
                Descricao = x.Material.Descricao,
                UND = x.Material.UnidadeMedida.Sigla,
                Referencia = x.Material.Referencia,
                QtdeAtendida = x.QuantidadeAtendida,
                QtdePendente = x.QuantidadePendente,
                Check = false,
                QtdeEstoque = QuantidadeEstoque(x.Material, depositoMaterial),
                SituacaoRequisicaoMaterialDescricao = x.SituacaoRequisicaoMaterial.EnumToString()
            }).ToList();
        }

        private double QuantidadeEstoque(Material material, long? depositoMaterial)
        {
            var estoqueMaterial =
                _estoqueMaterialRepository.Find(
                    x => x.DepositoMaterial.Id == depositoMaterial && x.Material.Referencia == material.Referencia).FirstOrDefault();
            return estoqueMaterial != null ? estoqueMaterial.Quantidade : 0;
        }

        [HttpPost, ValidateAntiForgeryToken, PopulateViewData("PopulateViewData")]
        public virtual ActionResult Baixar(RequisicaoMaterialBaixaModel model)
        {
           if (ModelState.IsValid)
           {
                try
                {
                    var requisicaoMaterial = _requisicaoMaterialRepository.Get(model.Id);
                    var depositoMaterial = _depositoMaterialRepository.Get(model.DepositoMaterial);

                    if(!BaixeRequisicaoMaterialItens(requisicaoMaterial, model, depositoMaterial))
                    {
                        return RedirectToAction("Baixar", new { model.Id });
                    }
                    
                    requisicaoMaterial.AtualizeSituacao();

                    _requisicaoMaterialRepository.SaveOrUpdate(requisicaoMaterial);

                    this.AddSuccessMessage("Item baixados com sucesso.");
                    return RedirectToAction("Index", "RequisicaoMaterial");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty,
                        "Não é possível baixar a requisição de material. Confira se os dados foram informados corretamente: " +
                        exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }
           
            return View("Baixar", model);
        }

        private bool BaixeRequisicaoMaterialItens(RequisicaoMaterial requisicaoMaterial, RequisicaoMaterialBaixaModel model, DepositoMaterial depositoMaterial)
        {
            var saidaMaterial = new SaidaMaterial
            {
                DataSaida = DateTime.Now,
                CentroCusto = requisicaoMaterial.CentroCusto,
                DepositoMaterialOrigem = depositoMaterial
            };

            foreach (var itemModel in model.GridItems)
            {
                if (!itemModel.Check)
                {
                    continue;
                }
                
                var requisicaoMaterialItem = requisicaoMaterial.RequisicaoMaterialItems.FirstOrDefault(y => itemModel.Id == y.Id);

                requisicaoMaterialItem.QuantidadeAtendida += itemModel.QtdeBaixa;

                var material = requisicaoMaterialItem.Material;
                

                if (itemModel.QtdeEstoque < itemModel.QtdeBaixa)
                {
                    this.AddErrorMessage("Não foi possível baixar o item, recarregue a página e tente novamente.");
                    return false;
                }

                saidaMaterial.CrieSaidaItemMaterial(_estoqueMaterialRepository, material, itemModel.QtdeBaixa);
                requisicaoMaterial.BaixeReservaMaterial(_reservaEstoqueMaterialRepository, material, itemModel.QtdeBaixa);
            }
            

            requisicaoMaterial.SaidaMaterials.Add(saidaMaterial);
            requisicaoMaterial.ReservaMaterial.AtualizeSituacao();

            return true;
        }

        #endregion

        #endregion

        #region ObtenhaRequisicaoMaterialItens
        [HttpGet, AjaxOnly]
        public virtual ActionResult ObtenhaRequisicaoMaterialItens(long idRequisicaoMaterial, long idDepositoMaterial)
        {
            var requisicaoMaterial = _requisicaoMaterialRepository.Get(idRequisicaoMaterial);
            var retorno = ObtenhaRequisicaoItens(requisicaoMaterial, idDepositoMaterial);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        private IList<DepositoMaterial> ObtenhaDepositoMaterials(long unidadeRequisitada)
        {
            var userId = FashionSecurity.GetLoggedUserId();
            var usuario = _usuarioRepository.Get(userId);
            var funcionarioId = usuario.Funcionario != null ? usuario.Funcionario.Id : null;

            var depositoMaterials = _depositoMaterialRepository.Find(x => x.Unidade.Id == unidadeRequisitada
                    && x.Funcionarios.Any(y => y.Id == funcionarioId)).ToList();

            return depositoMaterials;
        }

        private long? ObtenhaDepositoMaterial(RequisicaoMaterialBaixaModel model)
        {
            var depositoMaterials = ObtenhaDepositoMaterials(model.UnidadeRequisitada.Value);

            if (depositoMaterials.Count() == 1)
            {
                model.DepositoMaterial = depositoMaterials.First().Id;
            }

            return model.DepositoMaterial;
        }

        #region PopulateViewData
        protected void PopulateViewData(RequisicaoMaterialBaixaModel model)
        {
            var depositoMaterials = ObtenhaDepositoMaterials(model.UnidadeRequisitada.Value);

            if (depositoMaterials.Count() == 1)
            {
                model.DepositoMaterial = depositoMaterials.First().Id;
                ViewData["DepositoMaterial"] = depositoMaterials.ToSelectList("Nome", depositoMaterials.First().Id);
            }
            else
            {
                ViewData["DepositoMaterial"] = depositoMaterials.ToSelectList("Nome");
            }
        }
        #endregion
    }
}


