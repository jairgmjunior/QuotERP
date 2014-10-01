﻿using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class MeioPagamentoMap : FashionClassMap<MeioPagamento>
    {
        public MeioPagamentoMap()
            : base("meiopagamento", 0)
        {
            Map(x => x.Descricao).Length(100).Not.Nullable();
        }
    }
}