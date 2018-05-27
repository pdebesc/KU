using System;
using System.Collections.Generic;
using Task1.BeerBarStuff;

namespace Task1
{
  class Program
  {
    static void Main(string[] args)
    {

      const int b = 22;

      var positions = new[] { 0, 5, 13, 33, 36 };
      var beers = new[] { 20, 40, 80, 10, 20 };
      
      var beerBars = new Queue<BeerBar>();

      for (var i = 0; i < positions.Length; i++)
      {
        beerBars.Enqueue(new BeerBar(positions[i], beers[i], b));
      }

      var canSupplyB = CanSupplyB(b, beerBars);

      Console.WriteLine("Can supply " + b + " :" + canSupplyB);
      Console.ReadLine();
    }

    private static bool CanSupplyB(int b, Queue<BeerBar> beerBars)
    {
      var demands = new Queue<BeerBar>();
      while (beerBars.Count > 0)
      {
        var beerBar = beerBars.Dequeue();

        if (beerBar.Supply < 0)
        {
          demands.Enqueue(beerBar);
          continue;
        }

        var includeCost = true;
        while (demands.Count != 0 && beerBar.CanSupply(demands.Peek(), includeCost))
        {
          var previousBar = demands.Peek();
          beerBar.SendSupply(previousBar, includeCost);
          if (previousBar.Supply == 0)
          {
            demands.Dequeue();
          }
          includeCost = false;
        }


        if (beerBars.Count != 0 && beerBar.CanSupply(beerBars.Peek(), true))
        {
          beerBar.SendSupply(beerBars.Peek(), true);
        }
      }

      return demands.Count == 0;
    }
  }
}
