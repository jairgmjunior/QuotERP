using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class FichaTecnicaVariacaoMatrizMap : EmpresaClassMap<FichaTecnicaVariacaoMatriz>
    {
        public FichaTecnicaVariacaoMatrizMap()
            : base("fichatecnicavariacaomatriz", 0)
        {
            References(x => x.Variacao).Not.Nullable();

            HasManyToMany(x => x.Cores)
                .Table("fichatecnicavariacaomatrizcor")
                .ParentKeyColumn("fichatecnicavariacaomatriz_id")
                .ChildKeyColumn("cor_id");
        }
    }
}