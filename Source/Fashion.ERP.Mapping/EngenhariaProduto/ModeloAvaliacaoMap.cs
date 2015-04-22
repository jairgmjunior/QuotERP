using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloAvaliacaoMap: EmpresaClassMap<ModeloAvaliacao>
    {
        public ModeloAvaliacaoMap()
            : base("modeloavaliacao", 0)
        {
            Map(x => x.QuantidadeTotaAprovacao).Not.Nullable();
            Map(x => x.Observacao).Length(250);

            Map(x => x.Tag).Not.Nullable();
            Map(x => x.Ano).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Complemento);
            Map(x => x.Aprovado).Not.Nullable();
            Map(x => x.Catalogo).Not.Nullable();
           
            References(x => x.Colecao).Not.Nullable();
            References(x => x.ModeloReprovacao).Cascade.All();
            References(x => x.ClassificacaoDificuldade).Not.Nullable();
            
            HasMany(x => x.ModelosAprovados)
                .KeyNullable()
                .Cascade.SaveUpdate();
        }
    }
}
