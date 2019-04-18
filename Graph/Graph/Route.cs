using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace Route
{

    public class FreightRoute 
    {
        public bool IsRepeatable { get; set; }
        public bool IsDaily {get; set; }
        private Vertex startStation;
        private Vertex endStation;
        private DateTime startTime;
        private int amountToDeliver;


        public FreightRoute(bool isRepeatable, bool isDaily, Vertex station1, Vertex station2, DateTime startTime, int amountToDeliver)
        {
            IsRepeatable = isRepeatable;
            IsDaily = isDaily;
            this.startStation = station1;
            this.endStation = station2;
            this.startTime = startTime;
            this.amountToDeliver = amountToDeliver;
        }
        
        public Vertex GetStartStation()
        {
            return startStation;
        }

        public Vertex GetEndStation()
        {
            return endStation;
        }

        public DateTime GetStartTime()
        {
            return startTime;
        }

        public int GetAmountToDeliver()
        {
            return amountToDeliver;
        }
    }

    public class PassengerRoute
    {
        public bool IsRepeatable { get; set; }
        public bool IsDaily {get; set; }

        private Vertex destinationStation;

        private DateTime arrivalTime;

        public PassengerRoute(bool isRepeatable, bool isDaily, Vertex destinationStation, DateTime arrivalTime)
        {
            IsDaily = isDaily;
            IsRepeatable = isRepeatable;
            this.destinationStation = destinationStation;
            this.arrivalTime = arrivalTime;
        }

        public Vertex GetDestinationStation()
        {
            return destinationStation;
        }
 
    }

}






















