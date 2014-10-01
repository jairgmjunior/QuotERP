﻿using Fashion.ERP.Domain.Comum;
using Fashion.Framework.Mapping;

namespace Fashion.ERP.Mapping.Comum
{
    public class UnidadeMap : EmpresaClassMap<Unidade>
    {
        public UnidadeMap()
            : base("unidade", 0)
        {
            Map(x => x.Codigo).Not.Nullable();
            Map(x => x.DataAbertura).Not.Nullable();
            Map(x => x.DataCadastro).Not.Nullable();
            Map(x => x.Ativo).Not.Nullable();            
        }
    }
}