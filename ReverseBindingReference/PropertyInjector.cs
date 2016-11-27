using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ReverseBindingReference
{
    public class PropertyInjector<Base, Additive> 
        where Base : class  
        where Additive : class
    {
        public interface IAdditiveFactory
        {
            Additive CreateInstance(Base b);
        }

        public PropertyInjector(IAdditiveFactory factory = null) { this.Factory = factory; }

        private static readonly ConditionalWeakTable<Base, Additive> referencesTable = new ConditionalWeakTable<Base, Additive>();
        private static readonly object pad = new object();

        public IAdditiveFactory Factory { get; set; }

        public Additive GetFor(Base b, bool createIfNotExists = true)
        {
            lock ( pad )
            {
                return referencesTable.GetValue(b, ( b2 ) => {
                    if ( !createIfNotExists )
                        return null;
                    else if ( Factory != null )
                        return Factory.CreateInstance(b2);
                    else
                        return Activator.CreateInstance<Additive>();
                });                
            }
        }
    }
}
