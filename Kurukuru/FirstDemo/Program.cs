using System;
using System.Threading;
using System.Threading.Tasks;
using Kurukuru;

namespace FirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo1();
            AsyncDemo().GetAwaiter().GetResult();
        }

        static void Demo1()
        {
            Spinner.Start("Processing...", () =>
            {
                Thread.Sleep(1000 * 3);

                // MEMO: If you want to show as failed, throw a exception here.
                // throw new Exception("Something went wrong!");
            });

            Spinner.Start("Stage 1...", spinner =>
            {
                Thread.Sleep(1000 * 3);
                spinner.Text = "Stage 2...";
                Thread.Sleep(1000 * 3);
                spinner.Fail("Something went wrong!");
            });
        }

        static async Task AsyncDemo()
        {
            await Spinner.StartAsync("Processing...", async () =>
            {
                await Task.Delay(1000 * 3);

                // MEMO: If you want to show as failed, throw a exception here.
                // throw new Exception("Something went wrong!");
            });

            await Spinner.StartAsync("Stage 1...", async spinner =>
            {
                await Task.Delay(1000 * 3);
                spinner.Text = "Stage 2...";
                await Task.Delay(1000 * 3);
                spinner.Fail("Something went wrong!");
            });
        }
    }
}
