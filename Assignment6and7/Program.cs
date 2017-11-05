using System;
using System.Threading.Tasks;

namespace Asignment6and7
{
    public class Program
    {
        private static async Task LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }

        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }

        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }

        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
        }

        private static Task<int> FactorialDigitSum(int n)
        {
            Task<int> promise = Task.Run(() =>
            {
                int factorial = 1;
                for (int i = 1; i <= n; ++i)
                {
                    factorial = factorial * i;
                }
                int sum = 0;
                while (true)
                {
                    int digit = factorial % 10;
                    factorial = factorial / 10;
                    sum += digit;
                    if (factorial == 0) break;    
                }
                return sum;
            });
            return promise;
        }

        // Ignore this part .
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other . NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on
            // LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in the call hierarchy .
            var t = Task.Run(async () => await LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
    }
}