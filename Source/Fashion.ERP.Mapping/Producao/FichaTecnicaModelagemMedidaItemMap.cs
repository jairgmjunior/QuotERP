using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaModelagemMedidaItemMap : EmpresaClassMap<FichaTecnicaModelagemMedidaItem>
    {
        public FichaTecnicaModelagemMedidaItemMap()
            : base("fichatecnicamodelagemmedidaitem", 0)
        {
            Map(x => x.Medida);

            References(x => x.Tamanho);
        }
    }
}