using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fashion.ERP.Domain;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Models;
using Fashion.ERP.Web.Helpers;
using Fashion.Framework.Common.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;
using Telerik.Reporting;

namespace Fashion.ERP.Web.Controllers
{
    public partial class RelatorioController : BaseController
    {
		#region Variaveis
        private readonly IRepository<Relatorio> _relatorioRepository;
        private readonly IRepository<Arquivo> _arquivoRepository;
        private readonly ILogger _logger;
        #endregion

        #region Construtores
        public RelatorioController(ILogger logger, IRepository<Relatorio> relatorioRepository,
            IRepository<Arquivo> arquivoRepository)
        {
            _relatorioRepository = relatorioRepository;
            _arquivoRepository = arquivoRepository;
            _logger = logger;
        }
        #endregion

        #region Views

        #region Index
        public virtual ActionResult Index()
        {
            var relatorios = _relatorioRepository.Find();

            var list = relatorios.Select(p => new GridRelatorioModel
            {
                Id = p.Id.GetValueOrDefault(),
                Nome = p.Nome,
            }).ToList();

            return View(list);
        }
        #endregion

        #region Novo

        public virtual ActionResult Novo()
        {
            return View(new RelatorioModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Novo(RelatorioModel model, HttpPostedFileBase arquivoUpload)
        {
            // Verificar se o arquivo é '.trdx'
            if (Path.GetExtension(arquivoUpload.FileName) != ".trdx")
            {
                ModelState.AddModelError("arquivoUpload", "Arquivo inválido. Verifique se o arquivo escolhido possui a extensão '.trdx'.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat<Relatorio>(model);

                    if (model.NomeParametro != null)
                    {
                        for (int i = 0; i < model.NomeParametro.Count; i++)
                        {
                            domain.AddRelatorioParametro(new RelatorioParametro
                            {
                                Nome = model.NomeParametro[i],
                                TipoRelatorioParametro = model.TipoParametro[i]
                            });
                        }
                    }

                    // Salvar o arquivo
                    domain.Arquivo = SalvarArquivo(arquivoUpload);
                    
                    _relatorioRepository.Save(domain);

                    this.AddSuccessMessage("Relatório cadastrado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Não é possível salvar o relatório. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Editar

        public virtual ActionResult Editar(long id)
        {
            var domain = _relatorioRepository.Get(id);

            if (domain != null)
            {
                var model = Mapper.Flat<RelatorioModel>(domain);
                return View("Editar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o relatório.");
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Editar(RelatorioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = Mapper.Unflat(model, _relatorioRepository.Get(model.Id));

                    _relatorioRepository.Update(domain);

                    this.AddSuccessMessage("Relatório atualizado com sucesso.");
                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar o relatório. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Visualizar

        public virtual ActionResult Visualizar(long id)
        {
            var domain = _relatorioRepository.Get(id);

            if (domain != null)
            {
                var model = new VisualizarRelatorioModel
                                {
                                    Id = domain.Id,
                                    Nome = domain.Nome,
                                    NomeParametro = domain.RelatorioParametros.Select(p => p.Nome).ToList(),
                                    TipoParametro = domain.RelatorioParametros.Select(p => p.TipoRelatorioParametro).ToList()
                                };
                return View("Visualizar", model);
            }

            this.AddErrorMessage("Não foi possível encontrar o relatório.");
            return RedirectToAction("Index");
        }

        [HttpPost, AjaxOnly]
        public virtual ActionResult Visualizar(VisualizarRelatorioModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var domain = _relatorioRepository.Get(model.Id);

                    var uriReportSource = new UriReportSource { Uri = domain.Arquivo.Nome };

                    var filename = uriReportSource.ToByteStream().SaveFile(".pdf");

                    //var contentType = MimeMapping.GetMimeMapping(filename);
                    return Json(new { Url = filename });
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao visualizar o relatório. Confira se os dados foram informados corretamente: " + exception.Message);
                    _logger.Info(exception.GetMessage());
                }
            }

            return View(model);
        }
        #endregion

        #region Excluir

        [HttpPost, ValidateAntiForgeryToken]
        public virtual ActionResult Excluir(long? id)
        {
            try
            {
                var domain = _relatorioRepository.Get(id);

                if (domain != null)
                {
                    this.AddSuccessMessage("Relatório excluído com sucesso");
                    _relatorioRepository.Delete(domain);
                }
                else
                {
                    this.AddErrorMessage("Não foi possível excluir o relatório. O registro informado não foi encontrado na base de dados.");
                }
            }
            catch (Exception exception)
            {
                this.AddErrorMessage("Ocorreu um erro ao excluir o relatório: " + exception.Message);
                _logger.Info(exception.GetMessage());
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region SalvarArquivo
        private Arquivo SalvarArquivo(HttpPostedFileBase arquivoUpload)
        {
            var filename = Upload.GenerateTempFilename("trdx").GetFilePath();
            arquivoUpload.SaveAs(filename);

            var fileInfo = new FileInfo(filename);
            var arquivo = new Arquivo
            {
                Nome = filename,
                Titulo = string.Empty,
                Data = fileInfo.CreationTime,
                Extensao = fileInfo.Extension,
                Tamanho = fileInfo.Length
            };

            return _arquivoRepository.Save(arquivo);
        }
        #endregion

        #endregion
    }
}