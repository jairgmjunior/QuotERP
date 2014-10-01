using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloFotoMap : FashionClassMap<ModeloFoto>
    {
        public ModeloFotoMap()
            : base("modelofoto", 0)
        {
            Map(x => x.Impressao).Not.Nullable();
            Map(x => x.Padrao).Not.Nullable();

            References(x => x.Foto).Cascade.Delete().Not.Nullable();
            References(x => x.Modelo);
        }
    }
}