using System;

namespace Lab8
{
    public struct Fraction
    {
        private long numerator;
        private long denominator;

        // TODO: Implement properties and constructor

        public long Numerator
        {
            get => numerator;
            set
            {
                if (denominator == 0)
                  numerator = value;
                else
                {
                    long gcd = GCD(value, denominator);

                    numerator = value / Math.Abs(gcd);
                    denominator /= Math.Abs(gcd);
                }
  
            }
        }

        public long Denominator
        {
            get => denominator;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    long gcd = GCD(value, numerator);
                    denominator = value/Math.Abs(gcd);
                    numerator /= gcd;

                }
            }
        }

        public Fraction(long n) : this()
        {
            Numerator = n;
            Denominator = 1;
            Simplify();
        }
        public Fraction(long n, long d) : this(n)
        {
            if (d == 0)
                throw new ArgumentException();
            Denominator = d;
            Simplify();
        }

        public override string ToString()
        {
            var whole = numerator / denominator;
            var num = numerator - whole * denominator;
            var sign = numerator > 0;

            var str = string.Empty;
            if (!sign)
            {
                str += "-";
                num = -num;
                whole = -whole;
            }
            if (num == 0)
                str += $"[{whole}]";
            else if (whole != 0)
                str += $"[{whole} {num}/{denominator}]";
            else
                str += $"[{num}/{denominator}]";

            return str;
        }

        // TODO: Implement all others methods, operators, etc.
        static long GCD(long x, long y)
        {
            while (y != 0)
            {
                long temp = y;
                y = x % y;
                x = temp;
            }
            return x;
        }

        public void Simplify()
        {
            long gcd = GCD(numerator, denominator);
            Numerator = Numerator / Math.Abs(gcd);
            Denominator = Denominator / Math.Abs(gcd);
        }

        public Fraction Reciprocal()
        {
            if (numerator < 0)
            {
                return new Fraction(-denominator, -numerator);
            }
           
            return new Fraction(denominator, numerator);
        }

        //- implicit from long type to Fraction
        public static implicit operator Fraction(long x) => new Fraction(x);
        //- explicit from Fraction to long
        public static explicit operator long(Fraction x)
        {
            return x.Numerator / x.Denominator;
        }
        //- explicit from Fraction to double
        public static explicit operator double(Fraction x)
        {
            return (double)x.Numerator / (double)x.Denominator;
        }

        //implement arithmetic operators + - * / and unary -
        public static Fraction operator +(Fraction x, Fraction y)
        {
            if (x.denominator != y.denominator)
            {
                return new Fraction(x.numerator * y.denominator + y.numerator * x.denominator, x.denominator * y.denominator);
            }
            return new Fraction(x.numerator + y.numerator, x.denominator);
        }

        public static Fraction operator -(Fraction x, Fraction y)
        {
            if (x.denominator != y.denominator)
            {
                if (x.denominator % y.denominator == 0)
                {
                    return new Fraction(x.numerator - y.numerator * (x.denominator / y.denominator), x.denominator);
                }
                if (y.denominator % x.denominator == 0)
                {
                    return new Fraction(x.numerator * (y.denominator / x.denominator) - y.numerator, y.denominator);
                }
                return new Fraction(x.numerator * y.denominator - y.numerator * x.denominator, x.denominator * y.denominator);
            }
            return new Fraction(x.numerator - y.numerator, x.denominator);
        }

        public static Fraction operator *(Fraction x, Fraction y)
        {
            long gcd1 = GCD(x.numerator, y.denominator);
            long gcd2 = GCD(x.denominator, y.numerator);

            return new Fraction((x.numerator / gcd1) * (y.numerator / gcd2), (x.denominator / gcd2) * (y.denominator / gcd1));
        }

        public static Fraction operator /(Fraction x, Fraction y)
        {
            if (y.denominator == x.denominator)
            {
                return new Fraction(x.numerator, y.numerator);
            }

            long gcd1 = GCD(x.numerator, y.denominator);
            long gcd2 = GCD(x.denominator, y.numerator);

            return new Fraction((x.numerator / gcd1) * (y.denominator / gcd1), (x.denominator / gcd2) * (y.numerator / gcd2));
        }

        public static Fraction operator -(Fraction x) => new Fraction((-1)*x.numerator, x.denominator);

    //implement comparison operators == != < > <= >=
        public static bool operator ==(Fraction f1, Fraction f2) => (f1.numerator * f2.denominator == f2.numerator * f1.denominator);
        public static bool operator !=(Fraction f1, Fraction f2) => (f1.numerator * f2.denominator != f2.numerator * f1.denominator);
        public static bool operator >(Fraction f1, Fraction f2) => (f1.numerator * f2.denominator > f2.numerator * f1.denominator);
        public static bool operator <(Fraction f1, Fraction f2) => (f1.numerator * f2.denominator < f2.numerator * f1.denominator);
        public static bool operator >=(Fraction f1, Fraction f2) => (f1.numerator * f2.denominator >= f2.numerator * f1.denominator);
        public static bool operator <=(Fraction f1, Fraction f2) => (f1.numerator * f2.denominator <= f2.numerator * f1.denominator);

        //override methods related to these operators
        public override bool Equals(object obj)
        {
            if(obj.GetType() == typeof(Fraction))
            {
                return (numerator * ((Fraction)obj).denominator == ((Fraction)obj).numerator * denominator);
            }

            return (numerator == (new Fraction((long)obj)).numerator * denominator);
        }

        public override int GetHashCode()
        {
            return Numerator.GetHashCode() ^ Denominator.GetHashCode();
        }
    }

}