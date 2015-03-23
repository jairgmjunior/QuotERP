using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloMap : FashionClassMap<Modelo>
    {
        public ModeloMap()
            : base("modelo", 0)
        {
            Map(x => x.Referencia).Length(50).Not.Nullable();
            Map(x => x.Descricao).Length(100).Not.Nullable();
            Map(x => x.Tecido).Length(60);
            Map(x => x.Detalhamento).Not.Nullable().Length(200);
            Map(x => x.Complemento).Length(50);
            Map(x => x.DataCriacao).Not.Nullable();
            Map(x => x.DataAlteracao).Not.Nullable();
            Map(x => x.Aprovado).Nullable();
            Map(x => x.Lavada).Length(200);
            Map(x => x.DataModelagem).Nullable();
            Map(x => x.Observacao).Length(4000);
            Map(x => x.Cos);
            Map(x => x.Passante);
            Map(x => x.Entrepernas);
            Map(x => x.Boca);
            Map(x => x.Localizacao).Length(100);
            Map(x => x.TamanhoPadrao).Length(10);
            Map(x => x.LinhaCasa).Length(100);
            Map(x => x.Modelagem).Length(100);
            Map(x => x.EtiquetaMarca).Length(100);
            Map(x => x.EtiquetaComposicao).Length(100);
            Map(x => x.TecidoComplementar).Length(100);
            Map(x => x.Forro).Length(100);
            Map(x => x.ZiperBraguilha).Length(100);
            Map(x => x.ZiperDetalhe).Length(100);
            Map(x => x.Dificuldade).Length(100);
            Map(x => x.DataRemessaProducao).Nullable();
            Map(x => x.ChaveExterna).Length(5);
            
            References(x => x.Grade).Not.Nullable();
            References(x => x.Colecao).Not.Nullable();
            References(x => x.Classificacao).Not.Nullable();
            References(x => x.Segmento);
            References(x => x.Natureza).Not.Nullable();
            References(x => x.Barra);
            References(x => x.Comprimento);
            References(x => x.Marca).Not.Nullable();
            References(x => x.ProdutoBase);
            References(x => x.Artigo).Not.Nullable();
            References(x => x.Estilista).Not.Nullable();
            References(x => x.Modelista);
            References(x => x.Tamanho);
            References(x => x.ModeloAprovado).Cascade.All();

            HasMany(x => x.LinhasTravete)
                .Table("modelolinhatravete")
                .Element("nome")
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.LinhasBordado)
                .Table("modelolinhabordado")
                .Element("nome")
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.LinhasPesponto)
                .Table("modelolinhapesponto")
                .Element("nome")
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.Fotos)
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.VariacaoModelos)
                .Not.Inverse()
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.SequenciaProducoes)
                //.Not.Inverse()
                .AsList(part => part.Column("ordem"))
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.ProgramacaoBordados)
                //.Not.Inverse()
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}