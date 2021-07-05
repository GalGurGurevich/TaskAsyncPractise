using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace just_practise
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Start WaitAll");
            var sw = Stopwatch.StartNew();
            try
            {
                var tasks = new[]
                {
                    WaitOneSecond(),
                    WaitTwoSeconds(),
                    WaitHalfASecond(),
                    WaitFailed(),
                    WaitCancelled()
                };
                Task.WaitAll(tasks);
            }
            catch { }
            sw.Stop();
            Console.WriteLine("End WaitAll {0}", sw.ElapsedMilliseconds);

            Console.WriteLine("Start WhenAll");
            sw = Stopwatch.StartNew();
            try
            {
                var results = await Task.WhenAll(
                    WaitOneSecond(),
                    WaitTwoSeconds(),
                    WaitHalfASecond(),
                    WaitFailed(),
                    WaitCancelled()
                );
            }
            catch { }
            sw.Stop();
            Console.WriteLine("End WhenAll {0}", sw.ElapsedMilliseconds);
            Console.ReadKey();
        }


        static async Task<string> WaitOneSecond()
        {
            Console.WriteLine("WaitOneSecond started");
            await Task.Delay(1000);
            Console.WriteLine("WaitOneSecond done");
            return "First";
        }

        static async Task<string> WaitTwoSeconds()
        {
            Console.WriteLine("WaitTwoSeconds started");
            await Task.Delay(2000);
            Console.WriteLine("WaitTwoSeconds done");
            return "Second";
        }

        static async Task<string> WaitHalfASecond()
        {
            Console.WriteLine("WaitHalfASecond started");
            await Task.Delay(500);
            Console.WriteLine("WaitHalfASecond done");
            return "Third";
        }

        static async Task<string> WaitFailed()
        {
            Console.WriteLine("WaitFailed started");
            await Task.Delay(750);
            Console.WriteLine("WaitFailed failed");
            throw new Exception("I Failed!!!");
        }

        static async Task<string> WaitCancelled()
        {
            Console.WriteLine("WaitCancelled started");
            var token = new CancellationTokenSource(2500);
            await Task.Delay(3000, token.Token);
            Console.WriteLine("WaitCancelled done");
            return "I will never return because I get cancelled";
        }
    }
}

