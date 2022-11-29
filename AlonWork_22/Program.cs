using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlonWork_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите количество ячеек массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SumArray);
            Task<int> task2 = task1.ContinueWith<int>(func2);

            Func<Task<int[]>,int> func3 = new Func<Task<int[]>, int>(MaxArray);
            Task<int> task3 = task1.ContinueWith<int>(func3);


            task1.Start();
            Console.ReadKey();

        }
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
                Console.Write($"{ array[i],5}");
            }
            Console.WriteLine();
            Console.WriteLine();
            return array;
        }

        static int SumArray(Task<int[]> task)
        {
            int[] array = task.Result;
            //int sum = array.ToList().Sum();
            //return sum;
            int sum = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                sum += array[i];                
            }
            Console.Write("Сумма значений массива = ");
            Console.WriteLine(sum);
            Console.WriteLine();
            return sum;
        }

        static int MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = array[0];
            foreach (int a in array)
            {
                if (a > max)
                    max = a;
            }
            Console.Write("Наибольшее значение в массиве = ");
            Console.WriteLine(max);
            Console.WriteLine();
            return max;
        }
    }
}
