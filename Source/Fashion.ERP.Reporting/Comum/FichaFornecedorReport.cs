using Fashion.ERP.Domain.Comum;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Reporting;

namespace Fashion.ERP.Reporting.Comum
{
    public partial class FichaFornecedorReport : Report
    {
        public FichaFornecedorReport()
        {
            InitializeComponent();

            #region ContatoSubreport
            ContatoSubreport.NeedDataSource += (s, e) =>
            {
                var subreport = s as Telerik.Reporting.Processing.SubReport;

                if (subreport != null)
                {
                    var pessoaId = Convert.ToInt64(subreport.InnerReport.Parameters["PessoaId"].Value);
                    var pessoa = ((IList<Pessoa>)DataSource).FirstOrDefault(p => p.Id == pessoaId);

                    if (pessoa != null)
                    {
                        subreport.InnerReport.DataSource = pessoa.Contatos;
                        subreport.InnerReport.Visible = pessoa.Contatos.Any();
                    }
                }
            };
            #endregion

            #region EnderecoSubreport
            EnderecoSubreport.NeedDataSource += (s, e) =>
            {
                var subreport = s as Telerik.Reporting.Processing.SubReport;

                if (subreport != null)
                {
                    var pessoaId = Convert.ToInt64(subreport.InnerReport.Parameters["PessoaId"].Value);
                    var pessoa = ((IList<Pessoa>)DataSource).FirstOrDefault(p => p.Id == pessoaId);

                    if (pessoa != null)
                    {
                        subreport.InnerReport.DataSource = pessoa.Enderecos;
                        subreport.InnerReport.Visible = pessoa.Enderecos.Any();
                    }
                }
            };
            #endregion

            #region InfBancariaSubreport
            InfBancariaSubreport.NeedDataSource += (s, e) =>
            {
                var subreport = s as Telerik.Reporting.Processing.SubReport;

                if (subreport != null)
                {
                    var pessoaId = Convert.ToInt64(subreport.InnerReport.Parameters["PessoaId"].Value);
                    var pessoa = ((IList<Pessoa>)DataSource).FirstOrDefault(p => p.Id == pessoaId);

                    if (pessoa != null)
                    {
                        subreport.InnerReport.DataSource = pessoa.InformacaoBancarias;
                        subreport.InnerReport.Visible = pessoa.InformacaoBancarias.Any();
                    }
                }
            };
            #endregion
        }
    }
}