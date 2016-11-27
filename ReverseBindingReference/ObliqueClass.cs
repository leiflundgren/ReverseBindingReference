using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseBindingReference
{
    /// <summary>
    /// This class is the example we use to represent a class we have, 
    /// that we can not modify and add a property to.
    /// (We would want to have a property holding our AdditionalProperties.)
    /// </summary>
    public class ObliqueClass
    {
        public int someValue;

        public ObliqueClass(int v)
        {
            someValue = v;
        }
        public ObliqueClass()
        {
            someValue = new Random().Next();
        }
    }
}
