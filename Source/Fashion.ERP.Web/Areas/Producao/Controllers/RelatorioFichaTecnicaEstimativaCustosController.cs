using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Attributes;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.ERP.Web.Helpers.ValueInjecter;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RelatorioFichaTecnicaEstimativaCustoController : BaseController
    {
        #region Variaveis
        private readonly ILogger _logger;
        private readonly IRepository<FichaTecnica> _fichaTecnicaRepository;
        
        #endregion
        
        #region Construtores
        public RelatorioFichaTecnicaEstimativaCustoController(ILogger logger, IRepository<FichaTecnica> fichaTecnicaRepository)
        {
            _logger = logger;
            _fichaTecnicaRepository = fichaTecnicaRepository;
        }
        #endregion

        #region FichaTecnicaEstimativaCusto
        public virtual ActionResult FichaTecnicaEstimativaCusto(long id)
        {
            var fichaTecnica = _fichaTecnicaRepository.Get(id);
            
            if (fichaTecnica == null)
                return Json(new { Error = "Não foi encontrada nenhuma ficha técnica com o id " + id });
            
            var sequenciasOperacionais =
                fichaTecnica.FichaTecnicaSequenciaOperacionals.ToList()
                    .GroupBy(y => new {y.DepartamentoProducao.Nome},
                        (chave, grupo) =>
                            new
                            {
                                DepartamentoProducao = chave.Nome,
                                Custo = grupo.Sum(z => z.Custo),
                                Tempo = grupo.Sum(z => z.Tempo)
                            });

            var fichaTecnicaDynamic = new
            {
                FichaTecnica = fichaTecnica,
                SequenciasOperacionais = sequenciasOperacionais,
                MateriaisConsumo = fichaTecnica.MateriaisConsumo.OrderBy(x => x.Material.Descricao),
                MateriaisConsumoVariacao = fichaTecnica.MateriaisConsumoVariacao.OrderBy(x => x.Material.Descricao),
                MateriaisComposicaoCusto = fichaTecnica.MateriaisComposicaoCusto.OrderBy(x => x.Material.Descricao),
                TotalSequenciasOperacionais = sequenciasOperacionais.Sum(x => x.Custo),
                TotalMateriaisConsumo = fichaTecnica.MateriaisConsumo.Sum(x => x.Custo * x.Quantidade),
                TotalMateriaisConsumoVariacao = fichaTecnica.MateriaisConsumoVariacao.Sum(x => x.Custo * x.Quantidade),
                TotalMateriaisComposicaoCusto = fichaTecnica.MateriaisComposicaoCusto.Sum(x => x.Custo)
            };
            
            var report = new FichaTecnicaEstimativaCustosReport() { DataSource = fichaTecnicaDynamic };
            
            var filename = report.ToByteStream().SaveFile(".pdf");

            return File(filename);
        }

        #endregion
      }
}