using System;
using System.Threading.Tasks;

namespace ParallelBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            // Два параллельных таска (плюс основной):
            var t1 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine($"First: {i + 1}");
                    Task.Delay(300).Wait();
                }
            });

            var t2 = Task.Run(() =>
            {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine($"Second: {i + 1}");
                    Task.Delay(500).Wait();
                }
            });

            t1.ContinueWith(t => Console.WriteLine("First task finished"));
            Task.WaitAll(new Task[] { t1, t2 });
            Console.WriteLine("Finished");

            int n = 15;
            var factorial = Task.Run(() => Factorial(n));
            factorial.ContinueWith(t => Console.WriteLine($"Factorial: {t.Result}"));

            while (!factorial.IsCompleted)
            {
                Console.WriteLine("Че-то делается");
                Task.Delay(300).Wait();
            }

            Console.ReadKey();
        }

        #region Factorial
        private static int Factorial(int n)
        {
            Task.Delay(100).Wait();
            if (n <= 1)
                return 1;
            else
                return n * Factorial(n - 1);
        }
        #endregion
    }
}
