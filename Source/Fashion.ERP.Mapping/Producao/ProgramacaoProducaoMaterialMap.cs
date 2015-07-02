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

            References(x => x.ReservaMaterial).Cascade.All();
            References(x => x.Material);
            References(x => x.DepartamentoProducao);
        }
    }
}