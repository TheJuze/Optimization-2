using System;
using System.Threading;

//Полином 17[П] + 5[Д]i+(1[А]+10[И]i)z+(3[В]+14[М]i)z^2+13[Л]z^3
namespace PAVLOV_DMITRY_206
{
    struct Complex//структура для работы с комплексными числами
    {
        public double X, Y;

        public Complex(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Complex operator -(Complex a)
        {
            a.X = -a.X;
            a.Y = -a.Y;
            return a;
        }

        public static Complex operator -(Complex a, Complex b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }

        public static Complex operator +(Complex a, Complex b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }

        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.X * b.X - a.Y * b.Y, a.X * b.Y + b.X * a.Y);
        }

        public static Complex operator *(double b, Complex a)
        {
            a.X *= b;
            a.Y *= b;
            return a;
        }

        public static Complex operator /(Complex a, Complex b)
        {
            a.X = (a.X * b.X - a.Y * b.Y) / (b.X * b.X + b.Y * b.Y);
            a.Y = (a.Y * b.X + a.X * b.Y) / (b.X * b.X + b.Y * b.Y);
            return a;
        }

        public static Complex operator /(double b, Complex a)
        {
            a.X /= b;
            a.Y /= b;
            return a;
        }

        public static Complex Power(Complex a, int power)
        {
            if (power == 0)
            {
                a.X = 1;
                a.Y = 0;
                return a;
            }
            Complex b = a;
            for (int i = 2; i <= power; i++)
            {
                b = b * a;
            }
            return b;
        }
        public static Complex Value(Vector X)
        {
            return new Complex(X.X, X.Y);
        }

    }

    struct Vector
    {
        public double X, Y;

        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator -(Vector a, Vector b)
        {
            a.X -= b.X;
            a.Y -= b.Y;
            return a;
        }

        public static Vector operator +(Vector a, Vector b)
        {
            a.X += b.X;
            a.Y += b.Y;
            return a;
        }

        public static Vector operator *(double b, Vector a)
        {
            a.X *= b;
            a.Y *= b;
            return a;
        }

        public static Vector toVector(Complex X)
        {
            return new Vector(X.X,X.Y);
        }

        public static double Norm(Vector a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y);
        }

    }

    static class Polynom
    {
        //Коээфициенты полинома 17[П] + 5[Д]i+(1[А]+10[И]i)z+(3[В]+14[М]i)z^2+13[Л]z^3
        static public Complex[] Coef = new Complex[4] { new Complex(17, 5), new Complex(1, 10), new Complex(3, 14), new Complex(13, 0) }; 

       /* static public Complex Poly(Complex[] coef, Complex X)//вычисление полинома
        {
            Complex z = coef[0] + coef[1] * X + coef[2] * Complex.Power(X, 2) + coef[3] * Complex.Power(X, 3);
            return z;
        }*/

        static public double getRe(Complex[] coef, Vector X)//вычисление действительной части полинома
        {
            return coef[0].X + coef[1].X * X.X - coef[1].Y * X.Y + coef[2].X * Math.Pow(X.X, 2) - 2 * coef[2].Y * X.X * X.Y -
                   coef[2].X * Math.Pow(X.Y, 2) + coef[3].X * Math.Pow(X.X, 3) - 3 * coef[3].Y * Math.Pow(X.X, 2) * X.Y -
                   3 * coef[3].X * X.X * Math.Pow(X.Y, 2) + coef[3].Y * Math.Pow(X.Y, 3);
        }
        static public double getRe_dx(Complex[] coef, Vector X)
        {
            return coef[1].X + 2 * coef[2].X * X.X - 2 * coef[2].Y * X.Y + 3 * coef[3].X * Math.Pow(X.X, 2) -
                   6 * coef[3].Y * X.X * X.Y - 3 * coef[3].X * Math.Pow(X.Y, 2);
        }
        static public double getRe_dy(Complex[] coef, Vector X)
        {
            return -coef[1].Y - 2 * coef[2].Y * X.X - 2 * coef[2].X * X.Y - 3 * coef[3].Y * Math.Pow(X.X, 2) -
                   6 * coef[3].X * X.X * X.Y + 3 * coef[3].Y * Math.Pow(X.Y, 2);
        }

        static public double getIm(Complex[] coef, Vector X)//вычисление мнимой части полинома
        {
            return coef[0].Y + X.X * coef[1].Y + coef[1].X * X.Y + coef[2].Y * Math.Pow(X.X, 2) + 2 * coef[2].X * X.X * X.Y -
                   coef[2].Y * Math.Pow(X.Y, 2) + coef[3].Y * Math.Pow(X.X, 3) + 3 * coef[3].X * Math.Pow(X.X, 2) * X.Y -
                   3 * coef[3].Y * X.X * Math.Pow(X.Y, 2) - coef[3].X * Math.Pow(X.Y, 3);
        }
        static public double getIm_dx(Complex[] coef, Vector X)//вычисление производной от мнимой части полинома по х
        {
            return coef[1].Y + 2 * coef[2].Y * X.X + 2 * coef[2].X * X.Y + 3 * coef[3].Y * Math.Pow(X.X, 2) +
                   6 * coef[3].X * X.X * X.Y - 3 * coef[3].Y * Math.Pow(X.Y, 2);
        }
        static public double getIm_dy(Complex[] coef, Vector X)//вычисление производной от мнимой части полинома по у
        {
            return coef[1].X + 2 * coef[2].X * X.X - 2 * coef[2].Y * X.Y + 3 * coef[3].X * Math.Pow(X.X, 2) -
                   6 * coef[3].Y * X.X * X.Y - 3 * coef[3].X * Math.Pow(X.Y, 2);
        }

        static public double Funct(Complex[] coef, Vector X)//вычисление функции |Polynom|^2
        {
            return Math.Pow(getRe(coef, X), 2) + Math.Pow(getIm(coef, X), 2);
        }

        static public Vector Grad(Complex[] coef, Vector X)//вычисление градиента от функции
        {
            return new Vector(2 * getRe(coef, X) * getRe_dx(coef, X) + 2 * getIm(coef, X) * getIm_dx(coef, X),
                2 * getRe(coef, X) * getRe_dy(coef, X) + 2 * getIm(coef, X) * getIm_dy(coef, X));
        }

        static public Complex[] Horner_sMethod(Complex[] oldCoef, int power, Complex root) //Метод Горнера
        {
            Complex[] newCoef = new Complex[4];
            if (power == 3)
            {
                newCoef[3] = new Complex(0, 0);
                newCoef[2] = oldCoef[3];
                newCoef[1] = oldCoef[2] + root * newCoef[2];
                newCoef[0] = oldCoef[1] + root * newCoef[1];
            }
            else if (power == 2)
            {
                newCoef[3] = new Complex(0, 0);
                newCoef[2] = new Complex(0, 0);
                newCoef[1] = oldCoef[2];
                newCoef[0] = oldCoef[1] + root * newCoef[1];
            }
            return newCoef;
        }
    }
    class Program
    {
        public const double Eps = 1e-6;
        static Complex[] coef = new Complex[4];
        static Complex[] roots = new Complex[3];
        static void CrushingGradient()//градиентный метод с дроблением шага
        {
            double a = 1, lambda = 0.65, delta = 0.5;
            coef = Polynom.Coef;
            Vector G= new Vector();
            Vector X_k = new Vector();
            int count = 0;
            for (int i = 0; i < 2; i++)
            {
                Vector X_i = new Vector(0, 0);//начальная точка
                Console.WriteLine("x={0}, y={1}, f={2}", X_i.X, X_i.Y, Polynom.Funct(coef, Vector.toVector(Complex.Value(X_i))));
                G = Polynom.Grad(coef, X_i);
                while (Vector.Norm(G) > Eps)
                {
                    X_k = X_i - a * G;
                   
                  //  count++;
                    if (Polynom.Funct(coef, X_k) - Polynom.Funct(coef, X_i) <= -a * delta * (Math.Pow(G.X, 2) + Math.Pow(G.Y, 2)))
                    {

                        X_i = X_k;
                        Console.WriteLine("x={0}, y={1}, f={2:E3}", X_i.X, X_i.Y, Polynom.Funct(coef, Vector.toVector(Complex.Value(X_i))));
                        G = Polynom.Grad(coef, X_k);
                    }
                    else a = a * lambda;
                }
                roots[i] = Complex.Value(X_k);
                Console.WriteLine("_______________________________________________");
                Console.WriteLine("{3} КОРЕНЬ: {0:00.00000000}+i*{1:00.00000000}={2:00.00000000}", roots[i].X, roots[i].Y, Polynom.Funct(coef, Vector.toVector(roots[i])),i+1);
                Console.WriteLine("_______________________________________________");
                coef = Polynom.Horner_sMethod(coef, 3 - i, roots[i]);
            }
            roots[2] = -coef[0] / coef[1];
            Console.WriteLine("_______________________________________________");
            Console.WriteLine("{3} КОРЕНЬ: {0:00.00000000}+i*{1:00.00000000}={2:00.00000000}", roots[2].X, roots[2].Y, Polynom.Funct(coef, Vector.toVector(roots[2])),3);
            Console.WriteLine("_______________________________________________");
        }
        static void Main()
        {
            CrushingGradient();
            Console.ReadKey();
        }
    }
}
