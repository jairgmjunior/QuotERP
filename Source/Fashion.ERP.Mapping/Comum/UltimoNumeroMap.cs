using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class UltimoNumeroMap: EmpresaClassMap<UltimoNumero>
    {
        public UltimoNumeroMap()
            : base("ultimonumero", 0)
        {
            Map(x => x.Numero).Not.Nullable();
            Map(x => x.NomeTabela).Not.Nullable();
        }
    }
}