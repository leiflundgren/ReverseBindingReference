using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseBindingReference
{
    class Program
    {
        static void Main( string[] args )
        {
            Demo_Principle();
            Demo_GC_Working();
        }


        private static void Demo_Principle()
        {
            ForeignClass three = new ForeignClass(3);
            ForeignClass forteen = new ForeignClass(14);

            {
                forteen.GetAdditionalProperties().favoriteNumber = true;
            }

            {

                foreach ( ForeignClass o in new ForeignClass[] { three, forteen } )
                {
                    Print(o, o.GetAdditionalProperties());
                }
            }
        }

        private static void Demo_GC_Working()
        {
            var savedInstances = new HashSet<ForeignClass>();

            for ( int i=0; i<1000000; ++i )
            {
                var o = new ForeignClass(i);
                var a = o.GetAdditionalProperties();
                a.favoriteNumber = (i % 7) == 0;

                if ( i%5 == 0)
                {
                    savedInstances.Add(o);
                }
                // Removes half of added objects again
                if (  i > 1000 && i%10==0 )
                {
                    savedInstances.Remove(new ForeignClass(i)); // remove using other instance, requires objects Equals is nice
                }
                if ( i % 1000 == 0 )
                    Console.WriteLine("When at " + i + " currently " + AdditionalProperties.instance_count + " additional objects");
            }


            Console.WriteLine("Before GC 1 currently " + AdditionalProperties.instance_count + " additional objects");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("After GC 1currently " + AdditionalProperties.instance_count + " additional objects");

            savedInstances = null;

            Console.WriteLine("Before GC 2 currently " + AdditionalProperties.instance_count + " additional objects");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            Console.WriteLine("After GC 2 currently " + AdditionalProperties.instance_count + " additional objects");

        }


        private static void Print( ForeignClass o, AdditionalProperties a )
        {
            Console.WriteLine(o.someValue.ToString() + (a.valueIsOdd ? " is odd" : " is even") + (a.favoriteNumber ? "  FAVORITE" : "  dull"));
        }
    }
}
