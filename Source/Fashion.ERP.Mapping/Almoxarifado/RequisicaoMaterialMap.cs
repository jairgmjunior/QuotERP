using Fashion.ERP.Domain.Almoxarifado;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Almoxarifado
{
    public class RequisicaoMaterialMap : EmpresaClassMap<RequisicaoMaterial>
    {
        public RequisicaoMaterialMap()
            : base("requisicaomaterial", 0)
        {
            Map(x => x.Numero).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.Observacao).Nullable();
            Map(x => x.Origem).Nullable();
            Map(x => x.SituacaoRequisicaoMaterial).Not.Nullable();

            References(x => x.TipoItem).Not.Nullable();
            References(x => x.Requerente).Not.Nullable();
            References(x => x.UnidadeRequerente).Not.Nullable();
            References(x => x.ReservaMaterial).Nullable().Cascade.All();
            References(x => x.UnidadeRequisitada).Not.Nullable();
            References(x => x.CentroCusto).Not.Nullable();

            HasMany(x => x.SaidaMaterials)
                .KeyNullable()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.RequisicaoMaterialItems)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}