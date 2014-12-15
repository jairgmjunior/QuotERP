using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Reporting.Comum;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Areas.Comum.Models;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Comum.Controllers
{
    public partial class RelatorioController : BaseController
    {
        #region Variaveis
        private readonly IRepository<Pessoa> _pessoaRepository;
        private readonly IRepository<Profissao> _profissaoRepository;
        private readonly IRepository<AreaInteresse> _areaInteresseRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public RelatorioController(ILogger logger, IRepository<Pessoa> pessoaRepository,
            IRepository<Profissao> profissaoRepository, IRepository<AreaInteresse> areaInteresseRepository)
        {
            _pessoaRepository = pessoaRepository;
            _profissaoRepository = profissaoRepository;
            _areaInteresseRepository = areaInteresseRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region FichaCliente
        public virtual ActionResult FichaCliente()
        {
            ViewData["Profissao"] = new SelectList(_profissaoRepository.Find(), "Id", "Nome");
            ViewData["AreaInteresse"] = new SelectList(_areaInteresseRepository.Find(), "Id", "Nome");

            return View(new FichaClienteModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult FichaCliente(FichaClienteModel model)
        {
            var query = _pessoaRepository.Find(p => p.Cliente != null);

            if (model.AreaInteresse.HasValue)
                query = query.Where(p => p.Cliente.AreaInteresse.Id == model.AreaInteresse);

            if (!string.IsNullOrWhiteSpace(model.Cnpj))
                query = query.Where(p => p.CpfCnpj == model.Cnpj);

            if (model.Codigo.HasValue)
                query = query.Where(p => p.Cliente.Codigo == model.Codigo);

            if (!string.IsNullOrWhiteSpace(model.Cpf))
                query = query.Where(p => p.CpfCnpj == model.Cpf);

            if (model.DataNascimento.HasValue)
                query = query.Where(p => p.DataNascimento == model.DataNascimento);

            if (!string.IsNullOrWhiteSpace(model.DocumentoIdentidade))
                query = query.Where(p => p.DocumentoIdentidade == model.DocumentoIdentidade);

            if (model.EstadoCivil.HasValue)
                query = query.Where(p => p.Cliente.EstadoCivil == model.EstadoCivil);

            if (!string.IsNullOrWhiteSpace(model.InscricaoEstadual))
                query = query.Where(p => p.InscricaoEstadual == model.InscricaoEstadual);

            if (!string.IsNullOrWhiteSpace(model.InscricaoMunicipal))
                query = query.Where(p => p.InscricaoMunicipal == model.InscricaoMunicipal);

            if (!string.IsNullOrWhiteSpace(model.InscricaoSuframa))
                query = query.Where(p => p.InscricaoSuframa == model.InscricaoSuframa);

            if (!string.IsNullOrWhiteSpace(model.Nome))
                query = query.Where(p => p.Nome.Contains(model.Nome));

            if (!string.IsNullOrWhiteSpace(model.NomeFantasia))
                query = query.Where(p => p.NomeFantasia.Contains(model.NomeFantasia));

            if (!string.IsNullOrWhiteSpace(model.OrgaoExpedidor))
                query = query.Where(p => p.OrgaoExpedidor == model.OrgaoExpedidor);

            if (model.Profissao.HasValue)
                query = query.Where(p => p.Cliente.Profissao.Id == model.Profissao);

            if (model.Sexo.HasValue)
                query = query.Where(p => p.Cliente.Sexo == model.Sexo);

            if (model.TipoPessoa.HasValue)
                query = query.Where(p => p.TipoPessoa == model.TipoPessoa);

            var result = query.ToList();

            if (!result.Any())
                return Json(new { Error = "Nenhum cliente foi encontrado." });

            var report = new FichaClienteReport { DataSource = result };
            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #region ListaCliente
        public virtual ActionResult ListaCliente()
        {
            return View(new ListaClienteModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult ListaCliente(ListaClienteModel model)
        {
            var query = _pessoaRepository.Find(p => p.Cliente != null);
            var filtros = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(model.Cnpj))
            {
                query = query.Where(p => p.CpfCnpj == model.Cnpj);
                filtros.AppendFormat("CNPJ: {0}, ", model.Cnpj);
            }

            if (model.Codigo.HasValue)
            {
                query = query.Where(p => p.Cliente.Codigo == model.Codigo);
                filtros.AppendFormat("Código: {0}, ", model.Codigo);
            }

            if (!string.IsNullOrWhiteSpace(model.Cpf))
            {
                query = query.Where(p => p.CpfCnpj == model.Cpf);
                filtros.AppendFormat("CPF: {0}, ", model.Cpf);
            }

            if (!string.IsNullOrWhiteSpace(model.Nome))
            {
                query = query.Where(p => p.Nome.Contains(model.Nome));
                filtros.AppendFormat("Nome: {0}, ", model.Nome);
            }

            if (model.TipoPessoa.HasValue)
            {
                query = query.Where(p => p.TipoPessoa == model.TipoPessoa);
                filtros.AppendFormat("Pessoa: {0}, ", model.TipoPessoa.Value.EnumToString());
            }

            var result = query.ToList();

            if (!result.Any())
                return Json(new { Error = "Nenhum cliente foi encontrado." });

            var report = new ListaClienteReport { DataSource = result };

            if (filtros.Length > 2)
                report.Filtros.Value = filtros.ToString().Substring(0, filtros.Length - 2);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #region FichaFornecedor
        public virtual ActionResult FichaFornecedor()
        {
            return View(new FichaFornecedorModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult FichaFornecedor(FichaFornecedorModel model)
        {
            var query = _pessoaRepository.Find(p => p.Fornecedor != null);

            if (!string.IsNullOrWhiteSpace(model.Cnpj))
                query = query.Where(p => p.CpfCnpj == model.Cnpj);

            if (model.Codigo.HasValue)
                query = query.Where(p => p.Fornecedor.Codigo == model.Codigo);

            if (!string.IsNullOrWhiteSpace(model.Cpf))
                query = query.Where(p => p.CpfCnpj == model.Cpf);

            if (model.DataNascimento.HasValue)
                query = query.Where(p => p.DataNascimento == model.DataNascimento);

            if (!string.IsNullOrWhiteSpace(model.DocumentoIdentidade))
                query = query.Where(p => p.DocumentoIdentidade == model.DocumentoIdentidade);

            if (!string.IsNullOrWhiteSpace(model.InscricaoEstadual))
                query = query.Where(p => p.InscricaoEstadual == model.InscricaoEstadual);

            if (!string.IsNullOrWhiteSpace(model.InscricaoMunicipal))
                query = query.Where(p => p.InscricaoMunicipal == model.InscricaoMunicipal);

            if (!string.IsNullOrWhiteSpace(model.InscricaoSuframa))
                query = query.Where(p => p.InscricaoSuframa == model.InscricaoSuframa);

            if (!string.IsNullOrWhiteSpace(model.Nome))
                query = query.Where(p => p.Nome.Contains(model.Nome));

            if (!string.IsNullOrWhiteSpace(model.NomeFantasia))
                query = query.Where(p => p.NomeFantasia.Contains(model.NomeFantasia));

            if (!string.IsNullOrWhiteSpace(model.OrgaoExpedidor))
                query = query.Where(p => p.OrgaoExpedidor == model.OrgaoExpedidor);

            if (model.TipoPessoa.HasValue)
                query = query.Where(p => p.TipoPessoa == model.TipoPessoa);

            var result = query.ToList();

            if (!result.Any())
                return Json(new { Error = "Nenhum fornecedor foi encontrado." });

            var report = new FichaFornecedorReport { DataSource = result };
            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #region ListaFornecedor
        public virtual ActionResult ListaFornecedor()
        {
            return View(new ListaFornecedorModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult ListaFornecedor(ListaFornecedorModel model)
        {
            var query = _pessoaRepository.Find(p => p.Fornecedor != null);
            var filtros = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(model.Cnpj))
            {
                query = query.Where(p => p.CpfCnpj == model.Cnpj);
                filtros.AppendFormat("CNPJ: {0}, ", model.Cnpj);
            }

            if (model.Codigo.HasValue)
            {
                query = query.Where(p => p.Fornecedor.Codigo == model.Codigo);
                filtros.AppendFormat("Código: {0}, ", model.Codigo);
            }

            if (!string.IsNullOrWhiteSpace(model.Cpf))
            {
                query = query.Where(p => p.CpfCnpj == model.Cpf);
                filtros.AppendFormat("CPF: {0}, ", model.Cpf);
            }

            if (!string.IsNullOrWhiteSpace(model.Nome))
            {
                query = query.Where(p => p.Nome.Contains(model.Nome));
                filtros.AppendFormat("Nome: {0}, ", model.Nome);
            }

            if (model.TipoPessoa.HasValue)
            {
                query = query.Where(p => p.TipoPessoa == model.TipoPessoa);
                filtros.AppendFormat("Pessoa: {0}, ", model.TipoPessoa.Value.EnumToString());
            }

            var result = query.ToList();

            if (!result.Any())
                return Json(new { Error = "Nenhum fornecedor foi encontrado." });

            var report = new ListaFornecedorReport { DataSource = result };

            if (filtros.Length > 2)
                report.Filtros.Value = filtros.ToString().Substring(0, filtros.Length - 2);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #region FichaFuncionario
        public virtual ActionResult FichaFuncionario()
        {
            return View(new FichaFuncionarioModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult FichaFuncionario(FichaFuncionarioModel model)
        {
            try
            {
                var query = _pessoaRepository.Find(p => p.Funcionario != null);

                if (model.Codigo.HasValue)
                    query = query.Where(p => p.Funcionario.Codigo == model.Codigo);

                if (!string.IsNullOrWhiteSpace(model.Cpf))
                    query = query.Where(p => p.CpfCnpj == model.Cpf);

                if (model.DataNascimento.HasValue)
                    query = query.Where(p => p.DataNascimento == model.DataNascimento);

                if (!string.IsNullOrWhiteSpace(model.DocumentoIdentidade))
                    query = query.Where(p => p.DocumentoIdentidade == model.DocumentoIdentidade);

                if (!string.IsNullOrWhiteSpace(model.InscricaoEstadual))
                    query = query.Where(p => p.InscricaoEstadual == model.InscricaoEstadual);

                if (!string.IsNullOrWhiteSpace(model.Nome))
                    query = query.Where(p => p.Nome.Contains(model.Nome));

                if (!string.IsNullOrWhiteSpace(model.OrgaoExpedidor))
                    query = query.Where(p => p.OrgaoExpedidor == model.OrgaoExpedidor);

                if (model.FuncaoFuncionario.HasValue)
                    query = query.Where(p => p.Funcionario.FuncaoFuncionario == model.FuncaoFuncionario);

                if (model.TipoPessoa.HasValue)
                    query = query.Where(p => p.TipoPessoa == model.TipoPessoa);

                var result = query.ToList();

                if (!result.Any())
                    return Json(new { Error = "Nenhum funcionário foi encontrado." });

                var report = new FichaFuncionarioReport { DataSource = result };
                var filename = report.ToByteStream().SaveFile(".pdf");

                return Json(new { Url = filename });
            }
            catch (Exception exception)
            {
                var message = exception.GetMessage();
                _logger.Info(message);
                return Json(new { Error = message });
            }
        }
        #endregion

        #region ListaFuncionario
        public virtual ActionResult ListaFuncionario()
        {
            return View(new ListaFuncionarioModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult ListaFuncionario(ListaFuncionarioModel model)
        {
            var query = _pessoaRepository.Find(p => p.Funcionario != null);
            var filtros = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(model.Cnpj))
            {
                query = query.Where(p => p.CpfCnpj == model.Cnpj);
                filtros.AppendFormat("CNPJ: {0}, ", model.Cnpj);
            }

            if (model.Codigo.HasValue)
            {
                query = query.Where(p => p.Funcionario.Codigo == model.Codigo);
                filtros.AppendFormat("Código: {0}, ", model.Codigo);
            }

            if (!string.IsNullOrWhiteSpace(model.Cpf))
            {
                query = query.Where(p => p.CpfCnpj == model.Cpf);
                filtros.AppendFormat("CPF: {0}, ", model.Cpf);
            }

            if (!string.IsNullOrWhiteSpace(model.Nome))
            {
                query = query.Where(p => p.Nome.Contains(model.Nome));
                filtros.AppendFormat("Nome: {0}, ", model.Nome);
            }

            if (model.TipoPessoa.HasValue)
            {
                query = query.Where(p => p.TipoPessoa == model.TipoPessoa);
                filtros.AppendFormat("Pessoa: {0}, ", model.TipoPessoa.Value.EnumToString());
            }

            var result = query.ToList();

            if (!result.Any())
                return Json(new { Error = "Nenhum funcionário foi encontrado." });

            var report = new ListaFuncionarioReport { DataSource = result };

            if (filtros.Length > 2)
                report.Filtros.Value = filtros.ToString().Substring(0, filtros.Length - 2);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #region FichaPrestadorServico
        public virtual ActionResult FichaPrestadorServico()
        {
            return View(new FichaPrestadorServicoModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult FichaPrestadorServico(FichaPrestadorServicoModel model)
        {
            var query = _pessoaRepository.Find(p => p.PrestadorServico != null);

            if (!string.IsNullOrWhiteSpace(model.Cnpj))
                query = query.Where(p => p.CpfCnpj == model.Cnpj);

            if (model.Codigo.HasValue)
                query = query.Where(p => p.PrestadorServico.Codigo == model.Codigo);

            if (!string.IsNullOrWhiteSpace(model.Cpf))
                query = query.Where(p => p.CpfCnpj == model.Cpf);

            if (model.DataNascimento.HasValue)
                query = query.Where(p => p.DataNascimento == model.DataNascimento);

            if (!string.IsNullOrWhiteSpace(model.DocumentoIdentidade))
                query = query.Where(p => p.DocumentoIdentidade == model.DocumentoIdentidade);

            if (!string.IsNullOrWhiteSpace(model.InscricaoEstadual))
                query = query.Where(p => p.InscricaoEstadual == model.InscricaoEstadual);

            if (!string.IsNullOrWhiteSpace(model.InscricaoMunicipal))
                query = query.Where(p => p.InscricaoMunicipal == model.InscricaoMunicipal);

            if (!string.IsNullOrWhiteSpace(model.InscricaoSuframa))
                query = query.Where(p => p.InscricaoSuframa == model.InscricaoSuframa);

            if (!string.IsNullOrWhiteSpace(model.Nome))
                query = query.Where(p => p.Nome.Contains(model.Nome));

            if (!string.IsNullOrWhiteSpace(model.NomeFantasia))
                query = query.Where(p => p.NomeFantasia.Contains(model.NomeFantasia));

            if (!string.IsNullOrWhiteSpace(model.OrgaoExpedidor))
                query = query.Where(p => p.OrgaoExpedidor == model.OrgaoExpedidor);

            if (model.TipoPrestadorServicos != null && model.TipoPrestadorServicos.Any())
                query = query.Where(p => p.PrestadorServico.TipoPrestadorServicos.Any(tipo => model.TipoPrestadorServicos.Contains(tipo)));

            if (model.TipoPessoa.HasValue)
                query = query.Where(p => p.TipoPessoa == model.TipoPessoa);

            var result = query.ToList();

            if (!result.Any())
                return Json(new { Error = "Nenhum prestador de serviço foi encontrado." });

            var report = new FichaPrestadorServicoReport { DataSource = result };
            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #region ListaPrestadorServico
        public virtual ActionResult ListaPrestadorServico()
        {
            return View(new ListaPrestadorServicoModel());
        }

        [HttpPost, AjaxOnly]
        public virtual JsonResult ListaPrestadorServico(ListaPrestadorServicoModel model)
        {
            var query = _pessoaRepository.Find(p => p.PrestadorServico != null);
            var filtros = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(model.Cnpj))
            {
                query = query.Where(p => p.CpfCnpj == model.Cnpj);
                filtros.AppendFormat("CNPJ: {0}, ", model.Cnpj);
            }

            if (model.Codigo.HasValue)
            {
                query = query.Where(p => p.PrestadorServico.Codigo == model.Codigo);
                filtros.AppendFormat("Código: {0}, ", model.Codigo);
            }

            if (!string.IsNullOrWhiteSpace(model.Cpf))
            {
                query = query.Where(p => p.CpfCnpj == model.Cpf);
                filtros.AppendFormat("CPF: {0}, ", model.Cpf);
            }

            if (!string.IsNullOrWhiteSpace(model.Nome))
            {
                query = query.Where(p => p.Nome.Contains(model.Nome));
                filtros.AppendFormat("Nome: {0}, ", model.Nome);
            }

            if (model.TipoPessoa.HasValue)
            {
                query = query.Where(p => p.TipoPessoa == model.TipoPessoa);
                filtros.AppendFormat("Tipo: {0}, ", model.TipoPessoa.Value.EnumToString());
            }

            var result = query.ToList();

            if (!result.Any())
                return Json(new { Error = "Nenhum prestador de serviço foi encontrado." });

            var report = new ListaPrestadorServicoReport { DataSource = result };

            if (filtros.Length > 2)
                report.Filtros.Value = filtros.ToString().Substring(0, filtros.Length - 2);

            var filename = report.ToByteStream().SaveFile(".pdf");

            return Json(new { Url = filename });
        }
        #endregion

        #region ListaFornecedor
        
        #endregion

        #endregion
    }
}