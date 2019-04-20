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

        public bool IsAssigned {get; set; }

        public FreightRoute(bool isRepeatable, bool isDaily, bool isAssigned ,Vertex station1, Vertex station2, DateTime startTime, int amountToDeliver)
        {
            IsRepeatable = isRepeatable;
            IsDaily = isDaily;
            IsAssigned = isAssigned;
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

        public bool IsAssigned {get; set; }

        private Vertex destinationStation;

        public DateTime arrivalTime;

        public PassengerRoute(bool isRepeatable, bool isDaily, bool isAssigned, Vertex destinationStation, DateTime arrivalTime)
        {
            IsDaily = isDaily;
            IsRepeatable = isRepeatable;
            IsAssigned = isAssigned;

            this.destinationStation = destinationStation;
            this.arrivalTime = arrivalTime;
        }


        public Vertex GetDestinationStation()
        {
            return destinationStation;
        }
 
        }
    

}






















