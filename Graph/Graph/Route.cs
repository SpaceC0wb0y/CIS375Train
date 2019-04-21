using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;
using Components;

namespace Route
{
    public class FreightRoute 
    {
        public bool IsRepeatable { get; set; }
        public bool IsDaily {get; set; }
        private Station startStation;
        private Station endStation;
        private DateTime startTime;
        private int amountToDeliver;

        public bool IsAssigned {get; set; }

        public FreightRoute(bool isRepeatable, bool isDaily, bool isAssigned , Station station1, Station station2, DateTime startTime, int amountToDeliver)
        {
            IsRepeatable = isRepeatable;
            IsDaily = isDaily;
            IsAssigned = isAssigned;
            this.startStation = station1;
            this.endStation = station2;
            this.startTime = startTime;
            this.amountToDeliver = amountToDeliver;
        }
        
        public Station GetStartStation()
        {
            return startStation;
        }

        public void SetStartStation(Station stat)
        {
            this.startStation = stat;
        }

        public Station GetEndStation()
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

        private Station destinationStation;

        public DateTime arrivalTime;

        public PassengerRoute(bool isRepeatable, bool isDaily, bool isAssigned, Station destinationStation, DateTime arrivalTime)
        {
            IsDaily = isDaily;
            IsRepeatable = isRepeatable;
            IsAssigned = isAssigned;

            this.destinationStation = destinationStation;
            this.arrivalTime = arrivalTime;
        }


        public Station GetDestinationStation()
        {
            return destinationStation;
        }

        public DateTime GetArrivalTime()
        {
            return arrivalTime;
        }
 
        

    }
    

}






















