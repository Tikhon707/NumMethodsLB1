using System;
using System.Globalization;


namespace ConsoleApp1
{
    internal class Methods
    {
        static double Divide(Func<double, double> f, double a, double b, double precision = 1e-10, int i = 0)
        {
            while (true)
            {
                double x0 = (a + b) / 2;

                if (f(x0) == 0.0 || Math.Abs(b - a) < precision)
                {
                    return x0;
                }

                if (f(a) * f(x0) > 0)
                {
                    a = x0;
                }
                else
                {
                    b = x0;
                }
                Console.WriteLine("{0}ое(е) приближение - a = {1:F4}\t b = {2:F4}", ++i, a, b);
                
            }
        }

        public static double Newton(Func<double, double> f, double a, double b, double precision = 1e-10)
        {
            double fDir(double x) => 3 * Math.Pow(x, 2) + 0.5;
            double f2ndDir(double x) => 6 * x;

            int i = 0;
            double x0, x1;

            if (f(a) * f2ndDir(a) > 0)
            {
                Console.WriteLine("Начальное приближение корня {0}", a);
                x0 = a;
            }
            else
            {
                Console.WriteLine("Начальное приближение корня {0}", b);
                x0 = b;
            }

            do
            {
                x1 = x0;
                x0 = x1 - f(x1) / fDir(x1);
                Console.WriteLine("{0}ое(e) приближение - {1}", ++i, x0);

            } while (Math.Abs(f(x0)) >= precision && i < 100);
            return x0;
        }

        static void Main()
        {
            //локальная функция
            double f(double x) => Math.Pow(x, 3) + x/2 - 1;
            

            Console.Write("Введите начало отрезка: ");
            var a = double.Parse(Console.ReadLine());
            Console.Write("Введите конец отрезка: ");
            var b = double.Parse(Console.ReadLine());
            Console.Write("Введите точность вычислений: ");
            var eps = double.Parse(Console.ReadLine().Replace('.', ','));
            
            Console.Write("Укажите код метода и нажмите ВВОД:\n1 - Деление пополам\n2 - Метод Ньютона\n");
            double DoOperation(int op)
            {
                switch (op)
                {
                    case 1: return Divide(f, a, b, eps);
                    case 2: return Newton(f, a, b, eps);
                    default: 
                        throw new ArgumentException("Метод с таким кодом не существует, пожалуйста, вводите корректный код.");
                }
            }
            double result = DoOperation(int.Parse(Console.ReadLine()));
            
            Console.WriteLine("Ответ: x = {0}\r\nЗначение функции: f(x) = {1}", result, f(result));
            Console.ReadKey();
        }

    }
}
