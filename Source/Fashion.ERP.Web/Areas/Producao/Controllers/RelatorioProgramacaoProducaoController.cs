using System.Linq;
using System.Web.Mvc;
using Fashion.ERP.Domain.Almoxarifado;
using Fashion.ERP.Domain.Comum;
using Fashion.ERP.Domain.Producao;
using Fashion.ERP.Reporting.Helpers;
using Fashion.ERP.Reporting.Producao;
using Fashion.ERP.Web.Controllers;
using Fashion.ERP.Web.Helpers.Extensions;
using Fashion.Framework.Repository;
using Ninject.Extensions.Logging;

namespace Fashion.ERP.Web.Areas.Producao.Controllers
{
    public partial class RelatorioProgramacaoProducaoController : BaseController
    {
        #region Variaveis
        private readonly ILogger _logger;
        private readonly IRepository<ProgramacaoProducao> _programacaoProducaoRepository;
        
        #endregion
        
        #region Construtores
        public RelatorioProgramacaoProducaoController(ILogger logger, IRepository<ProgramacaoProducao> programacaoProducaoRepository)
        {
            _logger = logger;
            _programacaoProducaoRepository = programacaoProducaoRepository;
        }
        #endregion

        #region FichaTecnicaEstimativaCusto
        public virtual ActionResult Imprimir(long id)
        {
            var programacaoProducao = _programacaoProducaoRepository.Get(id);
            
            if (programacaoProducao == null)
                return Json(new { Error = "Não foi encontrada nenhuma programação de produção com o id " + id });

            var programacaoProducao1 = programacaoProducao.ProgramacaoProducaoItems.Select(x => new
            {
                programacaoProducao.DataProgramada,
                Responsavel = programacaoProducao.Funcionario.Nome,
                QuantidadeProgramada = programacaoProducao.Quantidade,
                LoteAno = programacaoProducao.Lote.ToString() + "/" + programacaoProducao.Ano.ToString(),
                Tecidos = programacaoProducao.ProgramacaoProducaoMateriais.Where(m => 
                    m.Material.Subcategoria.Categoria.GeneroCategoria == GeneroCategoria.Tecido &&
                    (x.FichaTecnica.MateriaisConsumo.Any(mc => mc.Material.Id == m.Material.Id) 
                        || x.FichaTecnica.MateriaisConsumoVariacao.Any(mc => mc.Material.Id == m.Material.Id))).Select(y => new
                {
                    y.Material.Referencia,
                    y.Material.Descricao,
                    y.Material.UnidadeMedida.Sigla,
                    y.Quantidade
                }),
                Situacao = programacaoProducao.SituacaoProgramacaoProducao.EnumToString(),
                x.FichaTecnica.Tag,
                x.FichaTecnica.Ano,
                x.FichaTecnica.Referencia,
                x.FichaTecnica.Descricao,
                x.FichaTecnica.Catalogo,
                x.FichaTecnica.Silk,
                x.FichaTecnica.Pedraria,
                Modelagem = x.FichaTecnica.FichaTecnicaModelagem != null ? x.FichaTecnica.FichaTecnicaModelagem.Descricao : null,
                Dificuldade = x.FichaTecnica.ClassificacaoDificuldade.Descricao,
                x.FichaTecnica.Observacao,
                x.FichaTecnica.Bordado,
                PrimeiraFoto = ObtenhaUrlPrimeiraFoto(x.FichaTecnica),
                SegundaFoto = ObtenhaUrlSegundaFoto(x.FichaTecnica),
                Medidas = x.FichaTecnica.FichaTecnicaModelagem == null ? null : x.FichaTecnica.FichaTecnicaModelagem.Medidas.SelectMany(z => z.Itens.Select(y => new
                {
                    Descricao = z.DescricaoMedida,
                    Tamanho = y.Tamanho.Descricao,
                    y.Medida
                })).OrderBy(z => z.Descricao).ThenBy(z => z.Tamanho),
                TipoEnfesto = x.ProgramacaoProducaoMatrizCorte.TipoEnfestoTecido.EnumToString(),
                MatrizCorte = x.ProgramacaoProducaoMatrizCorte.ProgramacaoProducaoMatrizCorteItens.Select(z => new
                {
                    Tamanho = z.Tamanho.Descricao,
                    NumeroVezes = z.QuantidadeVezes,
                    z.Quantidade
                })
            });

            //var programacaoProducao1 = new
            //{
            //    FichasTecnicas = fichasTecnicas,
            //    programacaoProducao.DataProgramada,
            //    Responsavel = programacaoProducao.Funcionario.Nome,
            //    QuantidadeProgramada = programacaoProducao.Quantidade,
            //    LoteAno = programacaoProducao.Lote + '/' + programacaoProducao.Ano
            //};

            var report = new ProgramacaoProducaoReport() { DataSource = programacaoProducao1 };
            
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