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
    public interface IVertex
    {
        
    }

    //Description: This is the base class
    public class Station : IVertex
    {
        protected string stationID;
        protected int numTrainsVisited;
        protected int numTrainsParked;
        protected int maxNumberOfTrains;
        protected int ticketPrice;
        protected int maxOn;
        protected int minOn;
        protected int minOff;
        protected int maxOff;

        public string GetID()
        {
            return stationID;
        }
        
        public bool Enter()
        {
            //there will be more code when adding in the max trains
            //additional feature here

            numTrainsVisited++;
            numTrainsParked++;

            return true;
        }

        public bool Leave(Track nextTrack)
        {
            if (nextTrack.GetAvailability())
            {
                numTrainsParked--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetNumTrainsVisited()
        {
            return numTrainsVisited;
        }

        public int GetNumTrainsParked()
        {
            return numTrainsParked;
        }

        public int GetTicketPrice()
        {
            return ticketPrice;
        }
        
    }

    public class FreightStation : Station
    {
        private int amountDelivered = 0;

        public FreightStation(string stationID, int maxTrains, int ticketCost)
        {
            this.stationID = stationID;
            numTrainsVisited = 0;
            amountDelivered = 0;
            maxNumberOfTrains = maxTrains;
            ticketPrice = ticketCost;
        }

        //This relies on train feature
        public int Resupply(int amount)
        {
            return amount;
        }

        public void UnloadCargo(int amount)
        {
            amountDelivered += amount;
        }

        //This is to test polymorphic references, not an actual piece of functionality
        public void Print()
        {
            ;
        }
    }

    public class PassengerStation : Station
    {
        private int totalPeopleVisited; //total number of people getting on or off a train onto the station
        public PassengerStation(string stationID, int maxTrains, int ticketCost, int minOn, int maxOn, int minOff, int maxOff)
        {
            this.stationID = stationID;
            totalPeopleVisited = 0;
            numTrainsVisited = 0;
            maxNumberOfTrains = maxTrains;
            ticketPrice = ticketCost;
            this.minOn = minOn;
            this.maxOn = maxOn;
            this.minOff = minOff;
            this.maxOff = maxOff;
        }

        public int GetTotalPeopleVisited()
        {
            return totalPeopleVisited;
        }


        public int AddPeopleOnTrain()
        {
            Random randomNum = new Random();
            int amountOn = 0;
            amountOn = randomNum.Next(minOn, maxOn + 1);
            totalPeopleVisited += amountOn;
            return amountOn;
        }

        public int SubPeopleOffTrain(PassengerTrain train)
        {
            Random randomNum = new Random();
            int amountOff = 0;
            amountOff = randomNum.Next(minOff, maxOff + 1);

            if (amountOff > train.GetNumPassengers())
            {
                amountOff = train.GetNumPassengers();
                totalPeopleVisited += amountOff;
                return amountOff;
            }
            else
            {
                totalPeopleVisited += amountOff;
                return amountOff;
            }
        }
    }

    public class Hub : IVertex
    {
        private string hubID;
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

        public Crew GetNewCrew()
        {
            numTimesUsed++;
            Crew newCrew = new Crew(this);
            return newCrew;
        }

    }
}
