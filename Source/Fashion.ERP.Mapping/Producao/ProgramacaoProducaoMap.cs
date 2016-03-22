using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProgramacaoProducaoMap : EmpresaClassMap<ProgramacaoProducao>
    {
        public ProgramacaoProducaoMap()
            : base("programacaoproducao", 0)
        {
            Map(x => x.Lote);
            Map(x => x.Ano);
            Map(x => x.Data);
            Map(x => x.DataProgramada);
            Map(x => x.DataAlteracao);
            Map(x => x.Observacao).Length(4000).Nullable();
            Map(x => x.Quantidade);
            Map(x => x.SituacaoProgramacaoProducao).Not.Nullable();

            References(x => x.Funcionario);
            References(x => x.RemessaProducao);

            HasMany(x => x.ProgramacaoProducaoMateriais)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.ProgramacaoProducaoItems)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}