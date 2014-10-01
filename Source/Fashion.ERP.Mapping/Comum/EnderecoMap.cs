using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class EnderecoMap : FashionClassMap<Endereco>
    {
        public EnderecoMap()
            : base("endereco", 10)
        {
            Map(x => x.TipoEndereco).Not.Nullable();
            Map(x => x.Logradouro).Length(100).Not.Nullable();
            Map(x => x.Numero).Length(10);
            Map(x => x.Complemento).Length(100);
            Map(x => x.Bairro).Length(100).Not.Nullable();
            Map(x => x.Cep).Length(9).Not.Nullable();

            References(x => x.Pessoa);
            References(x => x.Cidade).Not.Nullable();
        }
    }
}