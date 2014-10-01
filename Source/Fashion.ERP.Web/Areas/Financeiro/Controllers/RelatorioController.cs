using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Financeiro;
using Fashion.ERP.Reporting.Financeiro;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Financeiro.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Areas.Financeiro.Controllers
{
    public partial class RelatorioController : BaseController
    {
        #region Variaveis
        private readonly IRepository<ChequeRecebido> _chequeRecebidoRepository;
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly ILogger _logger;
        private readonly Dictionary<string, string> _colunas;
        #endregion

        #region Construtores
        public RelatorioController(ILogger logger, IRepository<ChequeRecebido> chequeRecebidoRepository,
            IRepository<Pessoa> pessoaRepository)
        {
            _chequeRecebidoRepository = chequeRecebidoRepository;
            _pessoaRepository = pessoaRepository;
            _logger = logger;

            _colunas = new Dictionary<string, string>
                           {
                               {"Número", "NumeroCheque"},
                               {"Banco", "Banco.Nome"},
                               {"Agência", "Agencia"},
                               {"Conta", "Conta"},
                               {"Emissão", "DataEmissao"},
                               {"Vencimento", "DataVencimento"},
                               {"Cliente", "Cliente.Nome"},
                               {"Emitente", "Emitente.Nome1"},
                               {"Compensado", "Compensado"},
                               {"Valor", "Valor"},
                               {"Saldo", "Saldo"},
                           };
        }
        #endregion

        #region Views

        #region ListaChequeRecebido
        public virtual ActionResult ListaChequeRecebido()
        {
            ViewBag.Group = new SelectList(_colunas, "value", "key");
            ViewBag.Sort = new SelectList(_colunas, "value", "key");
            ViewBag.Unidade = new SelectList(_pessoaRepository.Find(p => p.Unidade != null && p.Unidade.Ativo), "Id", "Nome");

            return View(new ListaChequeRecebidoModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult ListaChequeRecebido(ListaChequeRecebidoModel model)
        {
            var query = _chequeRecebidoRepository.Find();
            var filtros = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(model.Agencia))
            {
                query = query.Where(p => p.Agencia.Contains(model.Agencia));
                filtros.AppendFormat("Agência: {0}, ", model.Agencia);
            }

            if (model.Banco.HasValue)
            {
                query = query.Where(p => p.Banco.Codigo == model.Banco);
                filtros.AppendFormat("Banco: {0}, ", model.Banco);
            }

            if (model.Cliente.HasValue)
            {
                query = query.Where(p => p.Cliente.Cliente.Codigo == model.Cliente);
                filtros.AppendFormat("Cliente: {0}, ", model.Cliente.Value);
            }

            if (model.Compensado.HasValue)
            {
                query = query.Where(p => p.Compensado == model.Compensado.Value);
                filtros.AppendFormat("Compensado: {0}, ", model.Compensado.Value.ToSimNao());
            }

            if (!string.IsNullOrWhiteSpace(model.Conta))
            {
                query = query.Where(p => p.Conta == model.Conta);
                filtros.AppendFormat("Conta: {0}, ", model.Conta);
            }

            if (model.DataEmissaoInicio.HasValue)
            {
                query = query.Where(p => p.DataEmissao.Date >= model.DataEmissaoInicio.Value);
                filtros.AppendFormat("Data emissão apartir de: {0:dd/MM/yyyy}, ", model.DataEmissaoInicio.Value);
            }

            if (model.DataEmissaoFim.HasValue)
            {
                query = query.Where(p => p.DataEmissao.Date <= model.DataEmissaoFim.Value);
                filtros.AppendFormat("Data emissão até: {0:dd/MM/yyyy}, ", model.DataEmissaoFim.Value);
            }

            if (model.DataVencimentoInicio.HasValue)
            {
                query = query.Where(p => p.DataVencimento.Date >= model.DataVencimentoInicio.Value);
                filtros.AppendFormat("Data vencimento apartir de: {0:dd/MM/yyyy}, ", model.DataVencimentoInicio.Value);
            }

            if (model.DataVencimentoFim.HasValue)
            {
                query = query.Where(p => p.DataVencimento.Date <= model.DataVencimentoFim.Value);
                filtros.AppendFormat("Data vencimento até: {0:dd/MM/yyyy}, ", model.DataVencimentoFim.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.Emitente))
            {
                query = query.Where(p => p.Emitente.Nome1.Contains(model.Emitente) || p.Emitente.Nome2.Contains(model.Emitente));
                filtros.AppendFormat("Emitente: {0}, ", model.Emitente);
            }

            if (!string.IsNullOrWhiteSpace(model.Nominal))
            {
                query = query.Where(p => p.Nominal.Contains(model.Nominal));
                filtros.AppendFormat("Nominal: {0}, ", model.Nominal);
            }

            if (!string.IsNullOrWhiteSpace(model.NumeroCheque))
            {
                query = query.Where(p => p.NumeroCheque.Equals(model.NumeroCheque));
                filtros.AppendFormat("NumeroCheque: {0}, ", model.NumeroCheque);
            }

            if (model.Unidade.HasValue)
            {
                query = query.Where(p => p.Unidade.Id == model.Unidade);
                filtros.AppendFormat("Unidade: {0}, ", model.Unidade.Value);
            }

            if (model.Valor.HasValue)
            {
                query = query.Where(p => p.Valor == model.Valor.Value);
                filtros.AppendFormat("Valor: {0}, ", model.Valor.Value);
            }

            var result = query.ToList();

            if (!result.Any())
                return Json(new { Error = "Nenhum cheque recebido foi encontrado." });

            var report = new ListaChequeRecebidoReport { DataSource = result };

            if (filtros.Length > 2)
                report.Filtros.Value = filtros.ToString().Substring(0, filtros.Length - 2);

            var grupo = report.Groups.First(p => p.Name.Equals("Grupo"));

            if (model.Group != null)
            {
                grupo.Groupings.Add("=Fields." + model.Group);

                var key = _colunas.First(p => p.Value == model.Group).Key;
                var titulo = string.Format("= \"{0}: \" + Fields.{1}", key, model.Group);
                grupo.GroupHeader.GetTextBox("Titulo").Value = titulo;
            }
            else
            {
                report.Groups.Remove(grupo);
            }

            if (model.Sort != null)
                report.Sortings.Add("=Fields." + model.Sort, model.SortDirection == "asc" ? SortDirection.Asc : SortDirection.Desc);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #endregion
    }
}