using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RelatorioFichaTecnicaController : BaseController
    {
        #region Variaveis
        private readonly ILogger _logger;
        private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        
        #endregion
        
        #region Construtores
        public RelatorioFichaTecnicaController(ILogger logger, IRepository<FichaTecnica> fichaTecnicaRepository)
        {
            _logger = logger;
            _fichaTecnicaRepository = fichaTecnicaRepository;
        }
        #endregion

        #region FichaTecnicaEstimativaCusto
        public virtual ActionResult FichaTecnica(long id)
        {
            var fichaTecnica = _fichaTecnicaRepository.Get(id);
            
            if (fichaTecnica == null)
                return Json(new { Error = "Não foi encontrada nenhuma ficha técnica com o id " + id });

            var sequenciasOperacionais =
                fichaTecnica.FichaTecnicaSequenciaOperacionals.ToList()
                    .GroupBy(y => new { y.DepartamentoProducao.Nome }).Select(x => new {x.Key.Nome});
            
            var variacoes = fichaTecnica.FichaTecnicaMatriz.FichaTecnicaVariacaoMatrizs.SelectMany(x => x.Cores.Select(y => new
            {
                Cor = y.Nome,
                Variacao = x.Variacao.Nome
            }));

            var medidas = fichaTecnica.FichaTecnicaModelagem == null ? null : fichaTecnica.FichaTecnicaModelagem.Medidas.SelectMany(x => x.Itens.Select(y => new
            {
                Descricao = x.DescricaoMedida,
                Tamanho = y.Tamanho.Descricao,
                y.Medida
            })).OrderBy(x => x.Descricao).ThenBy(x => x.Tamanho);

            var fichaTecnicaDynamic = new
            {
                FichaTecnica = fichaTecnica,
                SequenciasOperacionais = sequenciasOperacionais,
                Variacoes = variacoes,
                Medidas = medidas,
                PrimeiraFoto = ObtenhaUrlPrimeiraFoto(fichaTecnica),
                SegundaFoto = ObtenhaUrlSegundaFoto(fichaTecnica),
            };
            
            var report = new FichaTecnicaReport() { DataSource = fichaTecnicaDynamic };
            
            var filename = report.ToByteStream().SaveFile(".pdf");

            return File(filename);
        }

        public string ObtenhaUrlPrimeiraFoto(FichaTecnica fichaTecnica)
        {
            if (fichaTecnica.FichaTecnicaFotos.Any(x => x.Impressao))
            {
                return fichaTecnica.FichaTecnicaFotos.Where(x => x.Impressao).ElementAt(0).Arquivo.Nome.GetFileUrl();
            }

            return null;
        }

        public string ObtenhaUrlSegundaFoto(FichaTecnica fichaTecnica)
        {
            if (fichaTecnica.FichaTecnicaFotos.Count(x => x.Impressao) > 1)
            {
                return fichaTecnica.FichaTecnicaFotos.Where(x => x.Impressao).ElementAt(1).Arquivo.Nome.GetFileUrl();
            }

            return null;
        }

        #endregion
      }
}