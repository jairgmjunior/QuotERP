using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class OrdemProducaoFluxoBasicoMap : FashionClassMap<OrdemProducaoFluxoBasico>
    {
        public OrdemProducaoFluxoBasicoMap()
            : base("ordemproducaofluxobasico", 0)
        {
            HasManyToMany(x => x.DepartamentoProducoes)
                .Table("ordemproducaofluxobasicodepartamentoproducao")
                .ParentKeyColumn("ordemproducaofluxobasico_id")
                .ChildKeyColumn("departamentoproducao_id")
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}