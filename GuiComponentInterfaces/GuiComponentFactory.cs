using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiComponentInterfaces
{
    class GuiComponentFactory
    {
        public IEnumerable<Type> getNumberComponents()
        {
            return new List<Type> { 
               typeof(PlaceholderTextGuiElementImpl) 
            };
        }

        public IEnumerable<Type> getTextComponents()
        {
            return new List<Type> { 
                // 
            };
        }
    }
}
