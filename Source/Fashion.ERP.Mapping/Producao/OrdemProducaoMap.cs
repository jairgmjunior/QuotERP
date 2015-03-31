﻿using Fashion.ERP.Domain.Producao;
using Fashion.Framework.Mapping;
using FluentNHibernate.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class OrdemProducaoMap : EmpresaClassMap<OrdemProducao>
    {
        public OrdemProducaoMap()
            : base("ordemproducao", 0)
        {
            Map(x => x.Numero).Not.Nullable();
            Map(x => x.Data).Not.Nullable();
            Map(x => x.DataProgramacao).Not.Nullable();
            Map(x => x.DataPrevisao).Not.Nullable();
            Map(x => x.Observacao).Length(4000);
            Map(x => x.TipoOrdemProducao);
            Map(x => x.SituacaoOrdemProducao);

            References(x => x.FichaTecnica).Not.Nullable();
            References(x => x.OrdemProducaoFluxoBasico).Cascade.Delete();

            HasMany(x => x.OrdemProducaoItens)
                .Not.LazyLoad()
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);

            HasMany(x => x.OrdemProducaoMateriais)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Access.CamelCaseField(Prefix.Underscore);
        }
    }
}