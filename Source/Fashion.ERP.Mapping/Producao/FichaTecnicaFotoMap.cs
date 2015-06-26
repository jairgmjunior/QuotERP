using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaFotoMap : EmpresaClassMap<FichaTecnicaFoto>
    {
        public FichaTecnicaFotoMap()
            : base("fichatecnicafoto", 0)
        {
            Map(x => x.Descricao).Length(100);
            Map(x => x.Padrao);
            Map(x => x.Impressao);

            References(x => x.Arquivo).Cascade.All();
        }
    }
}