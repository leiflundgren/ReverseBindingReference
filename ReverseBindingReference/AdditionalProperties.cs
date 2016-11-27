using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseBindingReference
{
    /// <summary>
    /// Here is the extra info we would like to have
    /// </summary>
    public class AdditionalProperties
    {
        public static int instance_count = 0;
        public AdditionalProperties(ForeignClass outer)
        {
            valueIsOdd = IsOdd(outer);
            System.Threading.Interlocked.Increment(ref instance_count);
        }

        ~AdditionalProperties()
        {
            System.Threading.Interlocked.Decrement(ref instance_count);
        }

        public bool IsOdd(ForeignClass o)
        {
            return (o.someValue % 2) == 1;
        }
        public Object MyExtraProperty { get; set; }
        public bool valueIsOdd;
        public bool favoriteNumber;
        public override string ToString()
        {
            return (valueIsOdd ? " is odd" : " is even") + (favoriteNumber ? "  FAVORITE" : "  dull");
        }
    }

    public class AdditionalPropertiesFactory: PropertyInjector<ForeignClass, AdditionalProperties>.IAdditiveFactory
    {
        public AdditionalProperties CreateInstance(ForeignClass outer)
        {
            return new AdditionalProperties(outer);
        }
    }

    public static class AdditionalPropertiesInjector
    {
        public static PropertyInjector<ForeignClass, AdditionalProperties> Injector = new PropertyInjector<ForeignClass, AdditionalProperties>(new AdditionalPropertiesFactory());

        public static AdditionalProperties GetAdditionalProperties(this ForeignClass o, bool createIfNotExists=true)
        {
            return Injector.GetFor(o, createIfNotExists);
        }


    }

}
