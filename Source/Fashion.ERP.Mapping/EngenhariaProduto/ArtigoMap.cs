﻿using Fashion.ERP.Domain.EngenhariaProduto;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.EngenhariaProduto
{
    public class ArtigoMap : FashionClassMap<Artigo>
    {
        public ArtigoMap()
            : base("artigo", 0)
        {
            Map(x => x.Descricao).Length(60).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();
        }
    }
}