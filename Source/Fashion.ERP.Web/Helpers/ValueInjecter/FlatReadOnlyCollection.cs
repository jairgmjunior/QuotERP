using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Omu.ValueInjecter;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    /// <summary>
    /// Converte uma ReadOnlyCollection em um array.
    /// </summary>
    public class FlatReadOnlyCollection : LoopValueInjectionBase
    {
        protected override void Inject(object source, object target)
        {
            foreach (PropertyDescriptor t in target.GetProps())
            {
                if (!t.PropertyType.IsArray) continue; // !t.PropertyType.IsEnumerable() && 

                var values = UberFlatter.Flat(t.Name, source, type => type.Name == typeof(IReadOnlyCollection<>).Name).ToList();

                if (!values.Any() || values.First() == null) continue;

                var val = values.First().Property.GetValue(values.First().Component);

                if (AllowSetValue(val))
                    t.SetValue(target, SetValue(val));
            }
        }

        protected virtual object SetValue(object sourcePropertyValue)
        {
            var list = sourcePropertyValue as IList;

            if (list == null)
                return null;

            var targetArgumentType = sourcePropertyValue.GetType().GetGenericArguments()[0];
            var array = Array.CreateInstance(targetArgumentType, list.Count);
            list.CopyTo(array, 0);

            return array;
        }
    }
}