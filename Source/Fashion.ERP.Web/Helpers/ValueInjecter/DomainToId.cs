using Fashion.Framework.Domain;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public class DomainToId : FlatDomain<DomainObject, long>
    {
        protected override long SetValue(DomainObject sourcePropertyValue)
        {
            return sourcePropertyValue == null ? 0 : sourcePropertyValue.Id.GetValueOrDefault();
        }
    }

    public class DomainToNullabeId : FlatDomain<DomainObject, long?>
    {
        protected override long? SetValue(DomainObject sourcePropertyValue)
        {
            return sourcePropertyValue == null ? null : sourcePropertyValue.Id;
        }
    }
}