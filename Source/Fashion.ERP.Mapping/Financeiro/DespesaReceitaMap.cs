using System;
using Fashion.ERP.Domain.Financeiro;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Financeiro
{
    public class DespesaReceitaMap: FashionClassMap<DespesaReceita>
    {
        public DespesaReceitaMap() : base("despesaReceita", 0)
        {
            Map(x => x.Descricao).Not.Nullable().Length(60);
            Map(x => x.Ativo).Not.Nullable();
            Map(x => x.TipoDespesaReceita).Not.Nullable();
        }
    }
}