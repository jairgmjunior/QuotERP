using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProgramacaoProducaoMaterialMap : EmpresaClassMap<ProgramacaoProducaoMaterial>
    {
        public ProgramacaoProducaoMaterialMap()
            : base("programacaoproducaomaterial", 0)
        {
            Map(x => x.Quantidade);
            Map(x => x.Reservado);
            Map(x => x.Requisitado);

            References(x => x.ReservaMaterial).Cascade.All().Nullable();
            References(x => x.Material);
            References(x => x.Responsavel);
            References(x => x.DepartamentoProducao);
        }
    }
}