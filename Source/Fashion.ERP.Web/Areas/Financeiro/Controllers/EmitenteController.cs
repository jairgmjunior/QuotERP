using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Financeiro.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.ERP.Web.Models;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class EmitenteController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Emitente> _emitenteRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public EmitenteController(ILogger logger, IRepository<Emitente> emitenteRepository)
        {
            _emitenteRepository = emitenteRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Detalhe
        [HttpGet]
        public virtual ActionResult Detalhe(long? banco, string agencia, string conta)
        {
            var model = new List<GridEmitenteModel>();

            if (banco.HasValue && !string.IsNullOrEmpty(agencia) && !string.IsNullOrEmpty(conta))
            {
                var domain = _emitenteRepository.Find().FirstOrDefault(p =>
                                                                       p.Banco.Id == banco.Value &&
                                                                       p.Agencia == agencia &&
                                                                       p.Conta == conta);

                if (domain != null)
                {
                    ViewBag.Id = domain.Id;

                    if (!string.IsNullOrWhiteSpace(domain.CpfCnpj1))
                    {
                        model.Add(new GridEmitenteModel
                        {
                            CpfCnpj = domain.CpfCnpj1,
                            Nome = domain.Nome1,
                            Documento = domain.Documento1,
                            OrgaoExpedidor = domain.OrgaoExpedidor1
                        });
                    }

                    if (!string.IsNullOrWhiteSpace(domain.CpfCnpj2))
                    {
                        model.Add(new GridEmitenteModel
                        {
                            CpfCnpj = domain.CpfCnpj2,
                            Nome = domain.Nome2,
                            Documento = domain.Documento2,
                            OrgaoExpedidor = domain.OrgaoExpedidor2
                        });
                    }
                }
            }

            return PartialView(model);
        }
        #endregion

        #region NovoOuEditar

        [HttpGet]
        public virtual ActionResult NovoOuEditar(long? banco, string agencia, string conta)
        {
            Emitente domain = null;

            if (banco.HasValue && !string.IsNullOrEmpty(agencia) && !string.IsNullOrEmpty(conta))
            {
                // Busca o emitente pelo banco/agencia/conta
                domain = _emitenteRepository.Find().FirstOrDefault(p =>
                                                                       p.Banco.Id == banco.Value &&
                                                                       p.Agencia == agencia &&
                                                                       p.Conta == conta);
            }

            if (domain != null)
            {
                var model = Mapper.Flat<EmitenteModel>(domain);

                if (domain.CpfCnpj1.Length == 14)
                    model.Cpf1 = domain.CpfCnpj1;
                else
                    model.Cnpj1 = domain.CpfCnpj1;

                if (domain.CpfCnpj2 != null)
                {

                    if (domain.CpfCnpj2.Length == 14)
                        model.Cpf2 = domain.CpfCnpj2;
                    else
                        model.Cnpj2 = domain.CpfCnpj2;
                }

                return PartialView(model);
            }

            return PartialView(new EmitenteModel { Banco = banco, Agencia = agencia, Conta = conta });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult NovoOuEditar(EmitenteModel model)
        {
            if (ModelState.IsValid)
            {
                var domain = Mapper.Unflat<Emitente>(model);

                domain.CpfCnpj1 = string.IsNullOrWhiteSpace(model.Cpf1) ? model.Cnpj1 : model.Cpf1;
                domain.CpfCnpj2 = string.IsNullOrWhiteSpace(model.Cpf2) ? model.Cnpj2 : model.Cpf2;
                domain.Ativo = true;

                domain = _emitenteRepository.SaveOrUpdate(domain);
                model.Id = domain.Id;

                return Json(model);
            }

            return PartialView(model);
        }
        #endregion

        #endregion

        #region Métodos

        #region ValidaNovoOuEditar
        protected override void ValidaNovoOuEditar(IModel model, string actionName)
        {
            var emitente = model as EmitenteModel;

        }
        #endregion

        #endregion
    }
}