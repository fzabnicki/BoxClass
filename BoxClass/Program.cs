using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxClassNamespace
{
    class Program
    {
        static void Main()
        {
            BoxClass a = new BoxClass(8, 9.123, 0.4);
            BoxClass b = new BoxClass(120, 540, 12, UnitOfMeasure.centimeter);
            BoxClass c = new BoxClass(2000, 1500, 9431, UnitOfMeasure.milimeter);
            BoxClass d = new BoxClass();
            BoxClass e = new BoxClass(7.817);
            List<BoxClass> array = new List<BoxClass> { a, b, c, d, e };
            Display(array);
            array.Sort(a);
            Console.WriteLine();
            Display(array);
            Console.ReadLine();

            void Display(List<BoxClass> boxarray)
            {
                foreach (var item in boxarray)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }
    }
}
