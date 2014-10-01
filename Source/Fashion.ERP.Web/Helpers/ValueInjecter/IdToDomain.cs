using System;
using Fashion.Framework.Domain;
using Fashion.Framework.UnitOfWork;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public class IdToDomain : UnflatDomain<long?, DomainObject>
    {
        protected override DomainObject SetValue(long? sourcePropertyValue, Type targetPropertyType)
        {
            // Verifica se o Id é válido
            if (!sourcePropertyValue.HasValue || sourcePropertyValue.Value <= 0)
                return null;

            var domain = Session.Current.Load(targetPropertyType, sourcePropertyValue) as DomainObject;// Creator.Create(targetPropertyType) as DomainObject;

            if (domain == null)
                return null;

            //domain.Id = sourcePropertyValue;
            return domain;
        }
    }
}