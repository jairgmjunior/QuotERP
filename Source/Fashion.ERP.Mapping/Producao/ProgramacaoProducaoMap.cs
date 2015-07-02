using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProgramacaoProducaoMap : EmpresaClassMap<ProgramacaoProducao>
    {
        public ProgramacaoProducaoMap()
            : base("programacaoproducao", 0)
        {
            Map(x => x.Numero);
            Map(x => x.Data);
            Map(x => x.DataProgramada);
            Map(x => x.DataAlteracao);
            Map(x => x.Observacao).Length(4000).Nullable();
            Map(x => x.Quantidade);

            References(x => x.Funcionario);
            References(x => x.Colecao);
            References(x => x.FichaTecnica);

            References(x => x.ProgramacaoProducaoMatrizCorte).Cascade.All();
        }
    }
}