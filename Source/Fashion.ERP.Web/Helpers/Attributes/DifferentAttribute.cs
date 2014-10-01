using System;
using System.ComponentModel.DataAnnotations;

namespace Fashion.ERP.Web.Helpers.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DifferentAttribute : CompareAttribute
    {
        public DifferentAttribute(string otherProperty)
            : base(otherProperty)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(OtherProperty);
            if (property != null)
            {
                object objB = property.GetValue(validationContext.ObjectInstance, null);
                if (Equals(value, objB) == false)
                    return null;

                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return null;
        }
    }
}