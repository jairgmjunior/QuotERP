using System;
using System.ComponentModel;
using System.Linq;
using Omu.ValueInjecter;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public abstract class UnflatDomain<TSourceProperty, TTargetProperty> : LoopValueInjectionBase
    {
        protected override void Inject(object source, object target)
        {
            foreach (PropertyDescriptor sourceProp in source.GetProps())
            {
                // Verifica se a propriedade é do tipo TSourceProperty
                var underlyingType = Nullable.GetUnderlyingType(typeof (TSourceProperty)); // Se é um tipo nullable
                if (!sourceProp.PropertyType.IsAssignableFrom(typeof(TSourceProperty)) &&
                    !sourceProp.PropertyType.IsAssignableFrom(underlyingType))
                    continue;

                var endpoints = UberFlatter.Unflat(sourceProp.Name, target, t => typeof(TTargetProperty).IsAssignableFrom(t)).ToList();
                if (!endpoints.Any()) continue;
                var value = sourceProp.GetValue(source);

                if (AllowSetValue(value))
                    foreach (var endpoint in endpoints)
                        endpoint.Property.SetValue(endpoint.Component, SetValue((TSourceProperty)value, endpoint.Property.PropertyType));
            }
        }

        protected abstract TTargetProperty SetValue(TSourceProperty sourcePropertyValue, Type targetPropertyType);
    }
}