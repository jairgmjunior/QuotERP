using System;
using Fashion.Framework.Domain;

namespace Fashion.ERP.Domain
{
    public class DomainBase<T> : DomainObject, IEquatable<T> where T : DomainObject
    {
        #region Equals
        public virtual bool Equals(T other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(other.Id, Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;

            var domain = obj as T;
            return domain != null && Equals(domain);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        #endregion

        #region Operators

        public static bool operator ==(DomainBase<T> x, DomainBase<T> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(DomainBase<T> x, DomainBase<T> y)
        {
            return !(x == y);
        }

        #endregion
    }
}