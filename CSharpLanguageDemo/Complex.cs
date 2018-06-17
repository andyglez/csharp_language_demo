using System;

namespace CSharpLanguageDemo
{
    class Complex
    {
        public int Real { get; private set; }
        public int Imaginary { get; private set; }

        public static implicit operator Complex(double value) => new Complex() { Real = (int)value };
        public static explicit operator double(Complex x) => x.Real;
        public static Complex operator +(Complex x, Complex y) => new Complex() { Real = x.Real + y.Real, Imaginary = x.Imaginary + y.Imaginary };
        public static Complex operator -(Complex x, Complex y) => new Complex() { Real = x.Real - y.Real, Imaginary = x.Imaginary - y.Imaginary };
        public static Complex operator *(Complex x, Complex y) => new Complex() { Real = x.Real * y.Real, Imaginary = x.Imaginary * y.Imaginary };
        public static Complex operator /(Complex x, Complex y) => new Complex() { Real = x.Real / y.Real, Imaginary = x.Imaginary / y.Imaginary };
        public static bool operator ==(Complex x, Complex y) => x.Real == y.Real && x.Imaginary == y.Imaginary;
        public static bool operator !=(Complex x, Complex y) => !(x == y);
        public static bool operator <=(Complex x, Complex y) => x.Real < y.Real ? true : x.Real == y.Real && x.Imaginary <= y.Imaginary ? true : false;
        public static bool operator >=(Complex x, Complex y) => !(x < y);
        public static bool operator <(Complex x, Complex y) => x.Real < y.Real ? true : x.Real == y.Real && x.Imaginary < y.Imaginary ? true : false;
        public static bool operator >(Complex x, Complex y) => !(x <= y);
        public static Complex operator ^(Complex x, Complex y) => Math.Pow((double)x,(double)y);

        public override string ToString() => $"{Real} + {Imaginary}i";
        public override bool Equals(object obj) => obj is Complex ? this == ((Complex)obj) : false;
        public override int GetHashCode() => base.GetHashCode();
    }
}
