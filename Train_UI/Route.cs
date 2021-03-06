using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Train_UI

{

    public class FreightRoute 
    {
        public bool IsRepeatable { get; set; }
        public bool IsDaily {get; set; }
        public Vertex startStation { get; set; }
        public Vertex endStation { get; set; }
        public DateTime startTime { get; set; }
        public int startDay { get; set; }
        public int amountToDeliver { get; set; }

        public bool IsAssigned {get; set; }

        public FreightRoute(bool isRepeatable, bool isDaily, bool isAssigned , Vertex station1, Vertex station2, DateTime startTime, int amountToDeliver)
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

        public void SetStartStation(Vertex stat)
        {
            this.startStation = stat;
        }

        public void SetEndStation(Vertex stat)
        {
            this.endStation = stat;
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

        public DateTime GetArrivalTime()
        {
            return arrivalTime;
        }
 
        

    }
    

}






















