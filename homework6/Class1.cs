using System;
using System.IO;
namespace DoubleBinary
{
    class Program
    {
       public delegate double Func(double x); //создаем делегат такой же сигнатуры, как и функции

        public static double M(double x)
        {
            return x * x - 50 * x + 10;
        }

        public static double D(double x)
        {
            return x / x - 50 / x + 10;
        }

        public static double X(double x)
        {
            return x - x + 50 / x - 10;
        }

        public static void SaveFunc(string fileName, double a, double b, double h, Func F) //добавляем делегат, содержащий функцию
        {
            FileStream fs = new FileStream(fileName, FileMode.Create,
            FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(fs);
            double x = a;
            while (x <= b)
            {
                bw.Write(F(x)); //F меняется на выбранную нами функцию при вызове через меню, у всех функций присутствует параметр double x
                x += h;
            }
            bw.Close();
            fs.Close();
        }
        public static double Load(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader bw = new BinaryReader(fs);
            double min = double.MaxValue;
            double d;
            for (int i = 0; i < fs.Length / sizeof(double); i++)
            {
                // Считываем значение и переходим к следующему
                d = bw.ReadDouble();
                if (d < min) min = d;
            }
            bw.Close();
            fs.Close();
            return min;
        }

    
        static void Main(string[] args)
        {

            static void Task(int taskNum = 0) 
            {
                bool task = true;
                while (task) //бесконечный вызов меню 
                {
                    Console.WriteLine("Сделайте выбор: 1 - функция M, 2 - функция D, 3 - функция X");
                    taskNum = int.Parse(Console.ReadLine());

                    switch (taskNum) 
                    {
                        case 1:
                            {

                                SaveFunc("data.bin", -100, 100, 0.5, M);
                                Console.WriteLine(Load("data.bin"));
                                break;
                            }
                        case 2:
                            {

                                SaveFunc("data.bin", -100, 100, 0.5, D);
                                Console.WriteLine(Load("data.bin"));
                                break;

                            }

                        case 3:
                            {

                                SaveFunc("data.bin", -100, 100, 0.5, X);
                                Console.WriteLine(Load("data.bin"));
                                break;

                            }

                        default:
                            {

                                Console.WriteLine("Такой задачи нет. Введите корректный номер от 1 до 3");
                                break;
                            }
                            
                    }
                }
            }
            Task(); //вызов меню

            Console.ReadKey();

        }
    }
}
