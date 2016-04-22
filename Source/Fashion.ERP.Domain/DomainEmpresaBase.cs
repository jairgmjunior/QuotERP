using System.Collections;
using Fashion.Framework.Common.Base;
using Fashion.Framework.Domain;

namespace Fashion.ERP.Domain
{
    
    public abstract class DomainEmpresaBase<T> : DomainTenantBase<T>, IEmpresaObject where T : DomainObject
    {
        /// <summary>
        /// Identificador da empresa
        /// </summary>
        public virtual long? IdEmpresa { get; set; }
    }
}