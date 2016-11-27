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
        public AdditionalProperties(ObliqueClass outer)
        {
            valueIsOdd = IsOdd(outer);
            System.Threading.Interlocked.Increment(ref instance_count);
        }

        ~AdditionalProperties()
        {
            System.Threading.Interlocked.Decrement(ref instance_count);
        }

        public bool IsOdd(ObliqueClass o)
        {
            return (o.someValue % 2) == 1;
        }

        public bool valueIsOdd;
        public bool favoriteNumber;
        public override string ToString()
        {
            return (valueIsOdd ? " is odd" : " is even") + (favoriteNumber ? "  FAVORITE" : "  dull");
        }
    }

    public class AdditionalPropertiesFactory: PropertyInjector<ObliqueClass, AdditionalProperties>.IAdditiveFactory
    {
        public AdditionalProperties CreateInstance(ObliqueClass outer)
        {
            return new AdditionalProperties(outer);
        }
    }

    public static class AdditionalPropertiesInjector
    {
        public static PropertyInjector<ObliqueClass, AdditionalProperties> Injector = new PropertyInjector<ObliqueClass, AdditionalProperties>(new AdditionalPropertiesFactory());

        public static AdditionalProperties GetAdditionalProperties(this ObliqueClass o, bool createIfNotExists=true)
        {
            return Injector.GetFor(o, createIfNotExists);
        }


    }

}
