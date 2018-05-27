using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numberOfPrices = 500;
            var prices = new int[numberOfPrices];
            var random = new Random();
            for (var i = 0; i < numberOfPrices; i++)
            {
                prices[i] = random.Next(1, 6);
            }

            const int C = 5;

            //var numberOfWaysToSpendCSlow = NumberOfWaysToSpendCSlow(C, prices);

            var stopwatch = Stopwatch.StartNew();
            //Console.WriteLine("Slow number of ways = " + numberOfWaysToSpendCSlow + " Took " + stopwatch.Elapsed.Milliseconds + " ms.");
            stopwatch.Restart();
            var numberOfWaysToSpendCFast = NumberOfWaysToSpendCFast(C, prices, new int?[C + 1, prices.Length + 1]);
            Console.WriteLine("Fast number of ways = " + numberOfWaysToSpendCFast + " Took " + stopwatch.Elapsed.Milliseconds + " ms.");
            Console.ReadLine();
        }


        static int NumberOfWaysToSpendCFast(int C, IEnumerable<int> prices, int?[,] calculated)
        {

            int pricesLength = prices.Count();
            if (C < 0)
            {
                return 0;
            }

            if (calculated[C, pricesLength] != null)
            {
                return (int)calculated[C, pricesLength];
            }

            if (C == 0)
            {
                calculated[C, pricesLength] = 1;
                return 1;
            }

            if (pricesLength == 0)
            {
                calculated[C, pricesLength] = 0;
                return 0;
            }

            calculated[C, pricesLength] = NumberOfWaysToSpendCFast(C, prices.Take(pricesLength - 1), calculated) +
                                          NumberOfWaysToSpendCFast(C - prices.Last(), prices.Take(pricesLength - 1), calculated);

            return (int)calculated[C, pricesLength];
        }
        
        static int NumberOfWaysToSpendCSlow(int C, int[] prices)
        {
            var count = 0;
            var fastPowerSet = PowerSet(prices);
            foreach (var row in fastPowerSet)
            {
                if (row.Sum() == C)
                {
                    count++;
                }
            }

            return count;
        }

        static public IList<IList<T>> PowerSet<T>(IList<T> list)
        {
            if (list.Count == 0)
                return new List<IList<T>>(new[] { new List<T>() });
            else
            {
                IList<IList<T>> ps = PowerSet(list.Skip(1).ToArray());
                foreach (List<T> ss in ps.ToArray())
                    ps.Add(new List<T>(ss) { list.First() });
                return ps;
            }
        }
    }
}
