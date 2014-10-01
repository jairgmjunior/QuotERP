using System;
using System.Collections.Generic;
using System.Linq;
using Fashion.ERP.Domain.Comum;

namespace Fashion.ERP.Reporting.Comum
{
    public partial class FichaClienteReport : Telerik.Reporting.Report
    {
        public FichaClienteReport()
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

            #region ReferenciaSubreport
            ReferenciaSubreport.NeedDataSource += (s, e) =>
                {
                    var subreport = s as Telerik.Reporting.Processing.SubReport;

                    if (subreport != null)
                    {
                        var clienteId = Convert.ToInt64(subreport.InnerReport.Parameters["ClienteId"].Value);
                        var pessoa = ((IList<Pessoa>)DataSource).FirstOrDefault(p => p.Cliente.Id == clienteId);

                        if (pessoa != null && pessoa.Cliente != null)
                        {
                            subreport.InnerReport.DataSource = pessoa.Cliente.Referencias;
                            subreport.InnerReport.Visible = pessoa.Cliente.Referencias.Any();
                        }
                    }
                };
            #endregion

            #region DependenteSubreport
            DependenteSubreport.NeedDataSource += (s, e) =>
                {
                    var subreport = s as Telerik.Reporting.Processing.SubReport;

                    if (subreport != null)
                    {
                        var clienteId = Convert.ToInt64(subreport.InnerReport.Parameters["ClienteId"].Value);
                        var pessoa = ((IList<Pessoa>)DataSource).FirstOrDefault(p => p.Cliente.Id == clienteId);

                        if (pessoa != null && pessoa.Cliente != null)
                        {
                            subreport.InnerReport.DataSource = pessoa.Cliente.Dependentes;
                            subreport.InnerReport.Visible = pessoa.Cliente.Dependentes.Any();
                        }
                    }
                };
            #endregion
        }
    }
}