using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class PrazoMap : FashionClassMap<Prazo>
    {
        public PrazoMap()
            : base("prazo", 0)
        {
            Map(x => x.Descricao).Not.Nullable().Length(100);
            Map(x => x.AVista).Not.Nullable();
            Map(x => x.QuantidadeParcelas).Not.Nullable();
            Map(x => x.PrazoPrimeiraParcela).Not.Nullable();
            Map(x => x.Intervalo);
            Map(x => x.Ativo).Not.Nullable();
            Map(x => x.Padrao).Not.Nullable();
        }
    }
}