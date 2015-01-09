using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class RequisicaoMaterialItemMap : EmpresaClassMap<RequisicaoMaterialItem>
    {
        public RequisicaoMaterialItemMap()
            : base("requisicaomaterialitem", 0)
        {
            Map(x => x.QuantidadeAtendida).Not.Nullable();
            Map(x => x.QuantidadeSolicitada).Not.Nullable();
            Map(x => x.SituacaoRequisicaoMaterial).Not.Nullable();

            References(x => x.Material).Not.Nullable();
            References(x => x.RequisicaoMaterialItemCancelado).Cascade.All();
        }
    }

}