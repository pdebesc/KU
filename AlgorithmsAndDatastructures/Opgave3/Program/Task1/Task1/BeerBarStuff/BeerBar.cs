using System;
using System.Runtime.Remoting.Messaging;

namespace Task1.BeerBarStuff
{
  public class BeerBar
  {
    public BeerBar(int position, int beers, int demand)
    {
      Position = position;
      Beers = beers;
      Supply = beers - demand;
    }

    public int Position { get; }
    public int Beers { get; private set; }

    public int Supply { get; private set; }

    //public void SetSupply(int demand)
    //{
    //  Supply = Beers - demand;
    //}

    public bool CanSupply(BeerBar otherBar, bool includeCost)
    {
      var cost  = includeCost ? transportationCost(otherBar) : 0;        
      return Supply - cost > 0;
    }

    int transportationCost(BeerBar otherBar)
    {
      return 2 * Math.Abs(Position - otherBar.Position);
    }

    //private int canSend(BeerBar otherBar)
    //{
    //  var transportCost = transportationCost(otherBar);
    //  var canSupply = Supply - transportCost;

    //  var demand = Math.Abs(otherBar.Supply);
    //  return otherBar.Supply < 0 && demand < canSupply ? demand : canSupply;
    //}

    public void SendSupply(BeerBar otherBar, bool includeCost)
    {
      var sendLeft = otherBar.Position < Position;
      var transportCost = includeCost ? transportationCost(otherBar) : 0;
      var inDemand = otherBar.Supply < 0;
      var demand = inDemand ? -otherBar.Supply : 0;
      var canSupply = Supply - transportCost;

      var toSend = sendLeft ? Math.Min(demand, canSupply) : canSupply;

      var actualSupplyAndTransportCost = toSend + transportCost;

      otherBar.Supply += toSend;
      otherBar.Beers += toSend;
      Supply -= actualSupplyAndTransportCost;
      Beers -= actualSupplyAndTransportCost;

    }

    //public void SendSupply(BeerBar otherBar)
    //{
    //  var supply = canSend(otherBar);

    //  var transportCost = transportationCost(otherBar);

    //  var actualSupplyAndTransportCost = supply + transportCost;

    //  otherBar.Supply += supply;
    //  otherBar.Beers += supply;
    //  Supply -= actualSupplyAndTransportCost;
    //  Beers -= actualSupplyAndTransportCost;
    //}
  }
}