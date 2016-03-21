using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class RemessaProducaoMap : EmpresaClassMap<RemessaProducao>
    {
        public RemessaProducaoMap(): base("remessaproducao", 0)
        {
            Map(x => x.Descricao).Length(100);
            Map(x => x.DataInicio);
            Map(x => x.DataAlteracao);
            Map(x => x.DataLimite);
            Map(x => x.Ano);
            Map(x => x.Numero);
            Map(x => x.Observacao).Length(4000).Nullable();
            
            References(x => x.Colecao);

            HasMany(x => x.RemessasProducaoCapacidadesProdutivas)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}