using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ModeloAprovacaoMap: EmpresaClassMap<ModeloAprovacao>
    {
        public ModeloAprovacaoMap()
            : base("modeloaprovacao", 0)
        {
            Map(x => x.Quantidade).Not.Nullable();
            Map(x => x.Observacao).Length(250);
            Map(x => x.Referencia).Not.Nullable();
            Map(x => x.Descricao).Not.Nullable();
            Map(x => x.MedidaBarra);
            Map(x => x.MedidaComprimento);

            References(x => x.Comprimento);
            References(x => x.Barra);
            References(x => x.ProdutoBase);
            References(x => x.Grade);
            References(x => x.FichaTecnica).Cascade.SaveUpdate();
        }
    }
}
