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

            Map(x => x.Tag).Nullable();
            Map(x => x.Ano).Nullable();
            Map(x => x.SequenciaTag);
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Complemento);
            Map(x => x.Aprovado);
            Map(x => x.Catalogo);
           
            References(x => x.Colecao);
            References(x => x.ClassificacaoDificuldade);

            References(x => x.ModeloReprovacao).Cascade.All();
            
            HasMany(x => x.ModelosAprovados)
                .KeyNullable()
                .Cascade.SaveUpdate();
        }
    }
}
