﻿using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Producao
{
    public class ProducaoMap : EmpresaClassMap<Domain.Producao.Producao>
    {
        public ProducaoMap() : base("producao", 0)
        {
            Map(x => x.Lote);
            Map(x => x.Ano);
            Map(x => x.Data);
            Map(x => x.DataAlteracao);
            Map(x => x.Descricao);
            Map(x => x.Observacao).Length(4000).Nullable();
            Map(x => x.SituacaoProducao).Not.Nullable();
            Map(x => x.TipoProducao).Not.Nullable();

            References(x => x.ProducaoProgramacao);
            References(x => x.RemessaProducao);
            
            HasMany(x => x.ProducaoItens)
                .Not.KeyNullable()
                .Cascade.AllDeleteOrphan();
        }
    }
}