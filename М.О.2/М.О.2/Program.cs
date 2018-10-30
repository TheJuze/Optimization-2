using System;
using static System.Math;
//многомерная минимизация
//методы нахождения минимума функции
//Павлов Дмитрий 206 группа
// A0 = Д + П*i = 5 + 17i 
// А1 = И + А*i = 10 + i 
// А2 = М + В*i = 14 + 3i 
// А3 = Л*i = 13i 
// f(x, y) = |А0+A1*z+A2*z^2+A3*z^3|^2 
// функция имеет три локальных минимума
//в x=-0.969085 y=0.245384
//x=0.435595 y=-0.923594
//x=0.436993 y=1.03343
namespace М.О._2
{
    class Program
    {
        const double Eps = 1e-6;
        const double Left = 0;
        const double Right = 1.5;
        
        const double X1= 0,X2=0;//то4ка, с которой начинается минимизация
        static double Funct(double x,double y)//возвращает значение функции в точке
        {
            return Pow(-39*x*x*y+14*x*x-6*x*y+10*x+13*y*y*y-14*y*y-y+5,2)+Pow(13*x*x*x+3*x*x-39*x*y*y+28*x*y+x-3*y*y-10*y+17,2);
        }
        static double Derivative_x(double x,double y)//первая производная по х
        {
        return 2*(-39*x*x*y+14*x*x-6*x*y+10*x+13*y*y*y-14*y*y-y+5)*(-78*x*y+28*x-6*y+10)+2*(13*x*x*x+3*x*x-39*x*y*y+28*x*y+x-3*y*y-10*y+17)*(26*x*x+6*x-39*y*y+28*y+1);
        }
        static double Derivative_y(double x, double y)//первая производная по у
        {
            return 2 * (-39 * x * x * y + 14 * x * x - 6 * x * y + 10 * x + 13 * y * y * y - 14 * y * y - y + 5) * (-39 * x * x - 6 * x + 39 * y * y - 28 * y - 1) + 2 * (13 * x * x * x + 3 * x * x - 39 * x * y * y + 28 * x * y + x - 3 * y * y - 10 * y + 17) * (-78 * x * y + 28 * x - 6 * y - 10);
        }
        static double F(double x, double y, double o)//слегка видоизмененная функция для МНГС // о - альфа
        {
            double q1 = Derivative_x(x, y);
            double q2 = Derivative_y(x, y);
            return Pow(-39 *(x-o*q1) * (x - o * q1) * (y - o * q2) + 14 * (x - o * q1) * (x - o * q1) - 6 * (x - o * q1) * (y - o * q2) + 10 * (x - o * q1) + 13 * (y - o * q2) * (y - o * q2) * (y - o * q2) - 14 * (y - o * q2) * (y - o * q2) - (y - o * q2) + 5, 2) + Pow(13 * (x - o * q1) * (x - o * q1) * (x - o * q1) + 3 * (x - o * q1) * (x - o * q1) - 39 * (x - o * q1) * (y - o * q2) * (y - o * q2) + 28 * (x - o * q1) * (y - o * q2) + (x - o * q1) - 3 * (y - o * q2) * (y - o * q2) - 10 * (y-o*q2) + 17, 2);
        }
        static double GR(double x,double y)//золотое сечение для МНГС
        {
            double a = Left;
            double b = Right;
            double s5 = Sqrt(5);
            double c = (3 - s5) / 2 * (b - a) + a;
            double d = (s5 - 1) / 2 * (b - a) + a;
                while ((b - a) / 2 > Eps)
                {
                    if (F(x,y,c)<=F(x,y,d))
                    {
                        b = d;
                        d = c;
                        c = (3 - s5) / 2 * (b - a) + a;
                    }
                    else
                    {
                        a = c;
                        c = d;
                        d = (s5 - 1) / 2 * (b - a) + a;
                    }
                }
            return (a + b) / 2;
        }
        static double GoldenRatio(int p, double k)//золотое сечение p=0 - x фиксирован, p=1 - y фиксирован, k - фиксированная переменная
        {
            double a = Left;
            double b = Right;
            double s5 = Sqrt(5);
            double c = (3 - s5) / 2 * (b - a) + a;
            double d = (s5 - 1) / 2 * (b - a) + a;
            if (p==1)
            {
                while ((b - a) / 2 > Eps)
                {
                    if (Funct(c,k) <= Funct(d,k))
                    {
                        b = d;
                        d = c;
                        c = (3 - s5) / 2 * (b - a) + a;
                    }
                    else
                    {
                        a = c;
                        c = d;
                        d = (s5 - 1) / 2 * (b - a) + a;
                    }
                }
            }
            else
            {
                while ((b - a) / 2 > Eps)
                {
                    if (Funct(k,c) <= Funct(k,d))
                    {
                        b = d;
                        d = c;
                        c = (3 - s5) / 2 * (b - a) + a;
                    }
                    else
                    {
                        a = c;
                        c = d;
                        d = (s5 - 1) / 2 * (b - a) + a;
                    }
                }
            }
            return (a + b) / 2;
        }

        static void Main()
        {
            Console.WriteLine("Функция |А0+A1*z+A2*z^2+A3*z^3|^2");
            Console.WriteLine("A0 = Д + П*i = 5 + 17i");
            Console.WriteLine("А1 = И + А*i = 10 + i");
            Console.WriteLine("А2 = М + В*i = 14 + 3i");
            Console.WriteLine("А3 = Л*i = 13i");
            Console.WriteLine("Нажмите цифру для одномерной минимизации:");
            Console.WriteLine("1.Метод покоординатного спуска");
            Console.WriteLine("2.Метод градиентного спуска с дроблением шага");
            Console.WriteLine("3.Метод градиентного спуска с постоянным шагом");
            Console.WriteLine("4.Метод наискорейшего градиентного спуска");
            Console.WriteLine("5.Метод градиентного спуска с заранее заданным шагом");
            int key = Convert.ToInt16(Console.ReadLine());
            switch (key)
            {
                case 1:
                    CoordinateDescent();
                    break;
                case 2:
                    CrushingGradient();
                    break;
                case 3:
                    ConstantGradient();
                    break;
                case 4:
                    FastestGradient();
                    break;
                case 5:
                    DivergentSeries();
                    break;
            }
        }
        
        static void CoordinateDescent()//метод покоординатного спуска
        {
            Console.WriteLine("метод покоординатного спуска");
            double a = X1, b = X2;
            double previous = Funct(a, b);
            Console.WriteLine("x={0},y={1},F(x,y)={2}", a, b, previous);
            a = GoldenRatio(1, b);
            b = GoldenRatio(0, a);
            double current = Funct(a, b);
            while(Abs(current-previous)>Eps)
            {
                previous = current;
                Console.WriteLine("x={0},y={1},F(x,y)={2}", a, b, previous);
                a = GoldenRatio(1, b);
                b = GoldenRatio(0, a);
                current = Funct(a, b);
            }
            Console.WriteLine("Минимум достигается в точке:");
            Console.WriteLine("x={0},y={1},F(x,y)={2}", a, b, current);
            Console.ReadKey();
        }

        static void CrushingGradient()//метод градиентного спуска с дроблением шага
        {
            Console.WriteLine("метод градиентного спуска с дроблением шага");
            double x = X1;
            double y = X2;
            double a=1;
            double delta = 0.95;
            Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x,y));
            //градиент (q1,q2) в 1 точке
            double q1 = Derivative_x(x, y);
            double q2 = Derivative_y(x, y);
            //
            double norm = Sqrt(q1 * q1 + q2 * q2);
            while (norm >= Eps)
            {
                while (Funct(x-a*q1,y-a*q2)-Funct(x,y)>-a*Eps* (q1 * q1 + q2 * q2))
                {
                    a = a * delta;
                }
                x = x - a * q1;
                y = y - a * q2;
                Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
                q1 = Derivative_x(x, y);
                q2 = Derivative_y(x, y);
                norm = Sqrt(q1 * q1 + q2 * q2);
            }
            Console.WriteLine("Минимум достигается в точке:");
            Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
            Console.ReadKey();
        }
        
        static void ConstantGradient()//метод градиентного спуска с постоянным шагом
        {
            Console.WriteLine("метод градиентного спуска с постоянным шагом");
            double a = 0.0001;//shag
            double x = X1;
            double y = X2;
            Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
            //градиент (q1,q2)
            double q1 = Derivative_x(x, y);
            double q2 = Derivative_y(x, y);
            //
            double norm = Sqrt(q1 * q1 + q2 * q2);
            while (norm>=Eps)
            {
                x = x - a * q1;
                y = y - a * q2;
                Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
                q1 = Derivative_x(x, y);
                q2 = Derivative_y(x, y);
                norm = Sqrt(q1 * q1 + q2 * q2);
            }

            Console.WriteLine("Минимум достигается в точке:");
            Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
            Console.ReadKey();
        }

        static void DivergentSeries()//метод градиентного спуска с заранее заданным шагом
        {
            Console.WriteLine("метод градиентного спуска с заранее заданным шагом");
            double x = X1;
            double y = X2;
            Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
            double k = 1000;
            //градиент (q1,q2)
            double q1 = Derivative_x(x, y);
            double q2 = Derivative_y(x, y);
            //
            double norm = Sqrt(q1 * q1 + q2 * q2);
            while (norm >= Eps)
            {
                k++;
                x = x - q1/k;
                y = y - q2/k;
                Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
                q1 = Derivative_x(x, y);
                q2 = Derivative_y(x, y);
                norm = Sqrt(q1 * q1 + q2 * q2);
            }
            Console.WriteLine("Минимум достигается в точке:");
            Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
            Console.ReadKey();
        }

        static void FastestGradient()//метод наискорейшего градиентного спуска
        {
            Console.WriteLine("метод наискорейшего градиентного спуска");
            double x = X1;
            double y = X2;
            double a;
            Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
            //градиент (q1,q2)
            double q1 = Derivative_x(x, y);
            double q2 = Derivative_y(x, y);
            //
            double norm = Sqrt(q1 * q1 + q2 * q2);
            while (norm >= Eps)
            {
                a=GR(x, y);
                x = x - a*q1;
                y = y - a*q2;
                Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
                q1 = Derivative_x(x, y);
                q2 = Derivative_y(x, y);
                norm = Sqrt(q1 * q1 + q2 * q2);
            }
            Console.WriteLine("Минимум достигается в точке:");
            Console.WriteLine("x={0},y={1},F(x,y)={2}", x, y, Funct(x, y));
            Console.ReadKey();
        }
    }
}