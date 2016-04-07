using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class ElementChangedEventArgs : EventArgs
    {
        public int I { get; private set; }
        public int J { get; private set; }
        public ElementChangedEventArgs(int i, int j)
        {
            I = i;
            J = j;
        }
    }
}
