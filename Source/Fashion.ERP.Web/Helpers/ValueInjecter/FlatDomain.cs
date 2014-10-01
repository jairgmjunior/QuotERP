using System.ComponentModel;
using System.Linq;
using Omu.ValueInjecter;

namespace Fashion.ERP.Web.Helpers.ValueInjecter
{
    public abstract class FlatDomain<TSourceProperty, TTargetProperty> : LoopValueInjectionBase
    {
        protected override void Inject(object source, object target)
        {
            foreach (PropertyDescriptor t in target.GetProps())
            {
                if (!t.PropertyType.IsAssignableFrom(typeof(TTargetProperty))) continue;

                var values = UberFlatter.Flat(t.Name, source, type => typeof(TSourceProperty).IsAssignableFrom(type)).ToList();

                if (!values.Any() || values.First() == null) continue;

                var val = values.First().Property.GetValue(values.First().Component);

                if (AllowSetValue(val))
                    t.SetValue(target, SetValue((TSourceProperty)val));
            }
        }

        protected abstract TTargetProperty SetValue(TSourceProperty sourceValues);
    }
}