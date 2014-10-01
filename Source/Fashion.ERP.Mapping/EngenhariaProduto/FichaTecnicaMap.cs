using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class FichaTecnicaMap : FashionClassMap<FichaTecnica>
    {
        public FichaTecnicaMap()
            : base("fichatecnica", 0)
        {
            Map(x => x.Referencia).Length(50).Not.Nullable();
            Map(x => x.Descricao).Length(100).Not.Nullable();
            Map(x => x.Detalhamento).Length(200);
            Map(x => x.Sequencia);
            Map(x => x.ProgramacaoProducao);
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.Modelagem).Length(100);
            Map(x => x.QuantidadeProducao).Not.Nullable();

            References(x => x.Modelo).Not.Nullable();
            References(x => x.Marca).Not.Nullable();
            References(x => x.Colecao).Not.Nullable();
            References(x => x.Barra);
            References(x => x.Segmento);
            References(x => x.ProdutoBase);
            References(x => x.Comprimento);
            References(x => x.Natureza).Not.Nullable();
            References(x => x.ClassificacaoDificuldade);
            References(x => x.Grade).Not.Nullable();
        }
    }
}