using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public interface IMatrix<T>
    {
        T this[int i, int j] { get; }
        int Width { get; }
        int Height { get; }
        event EventHandler<ElementChangedEventArgs> ElementChanged;
    }
}
