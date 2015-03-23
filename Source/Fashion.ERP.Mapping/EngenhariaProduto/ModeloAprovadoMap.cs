using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloAprovadoMap: FashionClassMap<ModeloAprovado>
    {
        public ModeloAprovadoMap()
            : base("modeloaprovado", 0)
        {
            Map(x => x.Tag).Not.Nullable();
            Map(x => x.Ano).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.Observacao).Length(250);
            Map(x => x.DataProgramacaoProducao).Not.Nullable();
            
            References(x => x.Colecao).Not.Nullable();
            References(x => x.ClassificacaoDificuldade).Not.Nullable();
            References(x => x.Funcionario).Not.Nullable();

            HasMany(x => x.FichaTecnicas)
                .KeyNullable()
                .Cascade.SaveUpdate();
        }
    }
}
