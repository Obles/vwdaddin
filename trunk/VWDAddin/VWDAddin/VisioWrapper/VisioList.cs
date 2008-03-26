using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Visio;
using System.Diagnostics;

namespace VWDAddin.VisioWrapper
{
    public class VisioList<T> where T : VisioShape
    {
        public delegate bool IsListElementHandler(Shape Shape);

        private Shapes Shapes;
        private IsListElementHandler IsListElement;

        public VisioList(Shapes Shapes, IsListElementHandler IsListElement)
        {
            this.Shapes = Shapes;
            this.IsListElement = IsListElement;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Type[] types = new Type[1];
            types[0] = typeof(Shape);
            Object[] obj = new Object[1];
            Type ElemType = typeof(T);

            foreach (Shape shape in Shapes)
            {
                obj[0] = shape;
                yield return ElemType.GetConstructor(types).Invoke(obj) as T;
            }
        }
    }
}
