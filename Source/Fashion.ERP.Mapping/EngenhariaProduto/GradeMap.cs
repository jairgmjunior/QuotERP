using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class GradeMap : FashionClassMap<Grade>
    {
        public GradeMap()
            : base("grade", 0)
        {
            Map(x => x.Descricao).Length(60).Not.Nullable();
            Map(x => x.DataCriacao).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();

            HasMany(x => x.Tamanhos)
                .Table("gradetamanho")
                .AsEntityMap("tamanho_id")
                .Element("ordem", part => part.Type<int>());
        }
    }
}