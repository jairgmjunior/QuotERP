using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class ClassificacaoDificuldadeMap : FashionClassMap<ClassificacaoDificuldade>
    {
        public ClassificacaoDificuldadeMap()
            : base("classificacaodificuldade", 0)
        {
            Map(x => x.Descricao).Length(50).Not.Nullable();
            Map(x => x.Criacao).Not.Nullable();
            Map(x => x.Producao).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}