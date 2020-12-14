using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxClassNamespace
{
    public sealed class BoxClass : IEquatable<BoxClass>, IEnumerable<double>, IComparer<BoxClass>
    {
        public UnitOfMeasure Unit { get; }
        public double A { get; }
        public double B { get; }
        public double C { get; }
        public double this[int i] 
        {
            get 
            {
                switch (i)
                {
                    case 0:
                        return A;
                    case 1:
                        return B;
                    case 2:
                        return C;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
        public string Objetosc 
        { 
            get 
            {
                return $"{Math.Round(A * B * C, 9)} m3";
            } 
        }
        public string Pole 
        {
            get
            {
                return $"{Math.Round(((A * B) * 2) + ((B * C) * 2) + ((C * A) * 2), 6)} m2";
            }
        }
        public BoxClass(double? a = null, double? b = null, double? c = null, UnitOfMeasure unitOfMeasure = UnitOfMeasure.meter)
        {
            A = a.HasValue ? CheckWhatMeasure(a.Value, unitOfMeasure) : 0.1;
            B = b.HasValue ? CheckWhatMeasure(b.Value, unitOfMeasure) : 0.1;
            C = c.HasValue ? CheckWhatMeasure(c.Value, unitOfMeasure) : 0.1;
            CheckIfValuesAreCorrect();
            Unit = unitOfMeasure;
        }
        private double CheckWhatMeasure(double x,UnitOfMeasure unitOfMeasure)
        {
            switch (unitOfMeasure)
            {
                case UnitOfMeasure.milimeter:
                    return x / 1000;
                case UnitOfMeasure.centimeter:
                    return x / 100;
                default:
                    return x;
            }
        }
        private void CheckIfValuesAreCorrect()
        {
            if ((A < 0.001 || B < 0.001 || C < 0.001) || (A > 10 || B > 10 || C > 10)) 
                throw new ArgumentOutOfRangeException();
        }
        public override string ToString()
        {
            return $"{A:N3} m \u00D7 {B:N3} m \u00D7 {C:N3} m";
        }
        public string ToString(string format)
        {
            if (String.IsNullOrEmpty(format))
                return $"{A:N3} m \u00D7 {B:N3} m \u00D7 {C:N3} m";
            switch (format)
            {
                case "m":
                    return $"{A:N3} m \u00D7 {B:N3} m \u00D7 {C:N3} m";
                case "cm":
                    return $"{A * 100:N1} cm \u00D7 {B * 100:N1} cm \u00D7 {C * 100:N1} cm";
                case "mm":
                    return $"{A * 1000} mm \u00D7 {B * 1000} mm \u00D7 {C * 1000} mm";
                default:
                    throw new FormatException();

            }
        }
        public bool Equals(BoxClass other)
        {
            return (A == other.A || A == other.B || A == other.C &&
                    B == other.A || B == other.B || B == other.C &&
                    C == other.A || C == other.B || C == other.C);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + A.GetHashCode();
                hash = hash * 23 + B.GetHashCode();
                hash = hash * 23 + C.GetHashCode();
                return hash;
            }
        }
        IEnumerator<double> IEnumerable<double>.GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return A;
            yield return B;
            yield return C;
        }
        public static bool operator ==(BoxClass a, BoxClass b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(BoxClass a, BoxClass b)
        {
            return !a.Equals(b);
        }
        public static BoxClass operator +(BoxClass a, BoxClass b)
        {
            List<double> boxA = new List<double> { a.A, a.B, a.C };
            List<double> boxB = new List<double> { b.A, b.B, b.C };
            boxA.Sort();
            boxB.Sort();
            double second = boxA[1] > boxB[1] ? boxA[1] : boxB[1];
            double third = boxA[2] > boxB[2] ? boxA[2] : boxB[2];
            BoxClass c = new BoxClass(boxA[0] + boxB[0], second, third);
            return c;
        }
        public static explicit operator double[](BoxClass a)
        {
            double[] output = new double[3] { a.A, a.B, a.C };
            return output;
        } 
        public static implicit operator BoxClass(ValueTuple<int, int, int> values)
        {
            BoxClass output = new BoxClass(values.Item1, values.Item2, values.Item3, UnitOfMeasure.milimeter);
            return output;
        }
        public BoxClass Kompresuj()
        {
            string[] array = Objetosc.Split(' ');
            double input = Math.Pow(double.Parse(array[0]), 1D/3);
            BoxClass c = new BoxClass(input, input, input);
            return c;
        }
        public BoxClass Parse(string values)
        {
            string[] namesAndXes = values.Split(' ');
            UnitOfMeasure measure;
            if (namesAndXes[1] == "mm")
                measure = UnitOfMeasure.milimeter;
            else if (namesAndXes[1] == "cm")
                measure = UnitOfMeasure.centimeter;
            else
                measure = UnitOfMeasure.meter;
            BoxClass a = new BoxClass(double.Parse(namesAndXes[0]), double.Parse(namesAndXes[3]), double.Parse(namesAndXes[6]), measure);
            return a;
        }
        public int CompareTo(BoxClass other)
        {
            string[] baseBox = Objetosc.Split(' ');
            string[] otherBox = other.Objetosc.Split(' ');
            double a = double.Parse(baseBox[0]);
            double b = double.Parse(otherBox[0]);
            if (a>b)
                return 1;
            else if (b>a)
                return -1;
            string[] baseFieldBox = Pole.Split(' ');
            string[] otherFieldBox = other.Pole.Split(' ');
            double aField = double.Parse(baseFieldBox[0]);
            double bField = double.Parse(otherFieldBox[0]);
            if (aField > bField)
                return 1;
            else if (bField > aField)
                return -1;
            else if (A + B + C > other.A + other.B + other.C)
                return 1;
            else if (A + B + C < other.A + other.B + other.C)
                return -1;
            else
                return 0;
        }
        public int Compare(BoxClass x, BoxClass y)
        {
             return x.CompareTo(y);
        }
    }
}
