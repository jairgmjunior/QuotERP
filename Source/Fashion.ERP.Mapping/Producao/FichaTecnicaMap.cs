using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaMap : EmpresaClassMap<FichaTecnica>
    {
        public FichaTecnicaMap()
            : base("fichatecnica", 0)
        {
            DiscriminateSubClassesOnColumn("Tipo").Not.Nullable();

            Map(x => x.Tag).Length(100);
            Map(x => x.Ano);
            Map(x => x.Descricao).Length(100);
            Map(x => x.Detalhamento).Length(200).Nullable();
            Map(x => x.Observacao).Length(4000).Nullable();
            Map(x => x.Silk).Length(200).Nullable();
            Map(x => x.Bordado).Length(200).Nullable();
            Map(x => x.Pedraria).Length(200).Nullable();
            Map(x => x.QuantidadeProducaoAprovada).Nullable();
            Map(x => x.DataCadastro);
            Map(x => x.DataAlteracao);
            Map(x => x.Referencia);
            
            References(x => x.Artigo);
            References(x => x.Colecao);
            References(x => x.Marca).Nullable();
            References(x => x.Segmento);
            References(x => x.Natureza);
            References(x => x.Classificacao);
            References(x => x.ClassificacaoDificuldade);
            References(x => x.FichaTecnicaMatriz).Cascade.All();
            References(x => x.Estilista).Nullable();
            References(x => x.FichaTecnicaModelagem).Nullable().Cascade.All();

            HasMany(x => x.FichaTecnicaSequenciaOperacionals)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.MateriaisComposicaoCusto)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.MateriaisConsumo)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.MateriaisConsumoVariacao)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}