using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    //Description: In order to put station and hub objects in the same graph,
    //I decided to make a vertex interface and have the station and hub classes
    //implement it. It is empty because I am not sharing functionaality between
    //stations and hubs
    interface IVertex
    {
        
    }

    //Description: This is the base class
    public class Station : IVertex
    {
        protected string stationID;
        protected int trainsVisited;

        public string GetID()
        {
            return stationID;
        }
        
    }

    public class FreightStation : Station
    {
        private int amountDelivered = 0;


        public FreightStation(string stationID)
        {
            this.stationID = stationID;
            trainsVisited = 0;
            amountDelivered = 0;
        }

        //This relies on train feature
        public int Resupply(int amount)
        {
            trainsVisited++;
            return amount;
        }

        public void UnloadCargo(int amount)
        {
            amountDelivered += amount;
            trainsVisited++;
        }

        //This is to test polymorphic references, not an actual piece of functionality
        public void Print()
        {
            ;
        }
    }

    public class PassengerStation : Station
    {
        private int numPeople;  //number of people entering a train from a station, randomely generated
        private int totalPeople; //total number of people getting on or off a train onto the station
        PassengerStation(string stationID)
        {
            this.stationID = stationID;
            numPeople = 0;
            totalPeople = 0;
        }

        public int GetTotalPeople()
        {
            return totalPeople;
        }
    }

    public class Hub : IVertex
    {
        private string hubID;
        private const int resupplyTime = 1; //in hours
        private int numTimesUsed;  //number of times a train enters the hub

        public Hub(string hubID)
        {
            this.hubID = hubID;
            numTimesUsed = 0;
        }

        public string GetID()
        {
            return hubID;
        }

        public int GetNumTimesUsed()
        {
            return numTimesUsed;
        }

        public Crew ChangeCrew()
        {
            numTimesUsed++;
            Crew newCrew = new Crew(this);
            return newCrew;
        }

    }
}
