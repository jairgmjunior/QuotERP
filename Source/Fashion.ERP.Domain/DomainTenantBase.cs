using Fashion.Framework.Common.Base;
using Fashion.Framework.Domain;

namespace Fashion.ERP.Domain
{
    public abstract class DomainTenantBase<T> : DomainBase<T>, ITenantObject where T : DomainObject
    {
        /// <summary>
        /// Identificador do tenant
        /// </summary>
        public virtual long? IdTenant { get; set; }
    }
}