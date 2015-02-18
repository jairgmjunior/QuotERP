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
            Map(x => x.TempoMaximoProducao).Nullable();
            Map(x => x.QuantidadeProducaoAprovada).Nullable();
            Map(x => x.DataCadastro);
            Map(x => x.DataAlteracao);
            
            References(x => x.Artigo);
            References(x => x.Colecao);
            References(x => x.Marca);
            References(x => x.Segmento);
            References(x => x.Natureza);
            References(x => x.Classificacao);
            References(x => x.ClassificacaoDificuldade);
            References(x => x.FichaTecnicaMatriz).Cascade.All();
        }
    }
}