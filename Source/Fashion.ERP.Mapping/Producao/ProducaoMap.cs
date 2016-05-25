using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProducaoMap : EmpresaClassMap<Domain.Producao.Producao>
    {
        public ProducaoMap() : base("producao", 0)
        {
            Map(x => x.Lote);
            Map(x => x.Ano);
            Map(x => x.Data);
            Map(x => x.DataAlteracao);
            Map(x => x.Descricao);
            Map(x => x.Observacao).Length(4000).Nullable();
            Map(x => x.SituacaoProducao);
            Map(x => x.TipoProducao);

            References(x => x.ProducaoProgramacao);
            References(x => x.RemessaProducao);
            References(x => x.Funcionario);
            
            HasMany(x => x.ProducaoItens)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}