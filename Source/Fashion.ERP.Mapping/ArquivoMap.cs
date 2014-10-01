using Fashion.ERP.Domain;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping
{
    public class ArquivoMap : FashionClassMap<Arquivo>
    {
        public ArquivoMap()
            : base("arquivo", 0)
        {
            Map(x => x.Nome).Not.Nullable().Length(260);
            Map(x => x.Titulo).Not.Nullable().Length(100);
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Extensao).Not.Nullable().Length(10);
            Map(x => x.Tamanho).Not.Nullable();
        }
    }
}