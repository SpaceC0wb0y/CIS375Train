using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_UI
                   
{
    //Description: In order to put station and hub objects in the same graph,
    //I decided to make a vertex interface and have the station and hub classes
    //implement it. It is empty because I am not sharing functionality between
    //stations and hubs
    public abstract class Vertex
    {
        public bool IsFreightStation { get; set; }
        public bool IsPassengerStation { get; set; }
        public bool IsHub { get; set; }

        //CHECK THIS
        protected string ID;

        //Description: Gets the ID for a station
        //Pre-Condition: None
        //Post-Condition: Returns station ID field
        public string GetID()
        {
            return ID;
        }
    }

    //Description: This is the base class for both passenger and freight stations
    //that holds common data and methods between the two
    public class Station : Vertex
    {
        protected int numTrainsVisited; 
        protected int numTrainsParked;  //number of trains docked at a station
        protected int ticketPrice;  //fixed ticket price for a station
        protected int maxOn;  //maximum number of people getting on a train
        protected int minOn;  //minimum number of people getting on a train
        protected int minOff; //minimum number of people getting off a train
        protected int maxOff; //maximum number of people getting off a train


        //Description: This allows a train to signal that it is entering a station
        //Pre-Condition: None
        //Post-Condition: 
        public bool Enter()
        {


            numTrainsVisited++;
            numTrainsParked++;

            return true;
        }

        //Description:
        //Pre-Condition:
        //Post-Condition:
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

        //Description: Gets the number of trains that visit the station in a given time period
        //Pre-Condition: None
        //Post-Condition: Return nuber of trains visited 
        public int GetNumTrainsVisited()
        {
            return numTrainsVisited;
        }

        //Description: Gets the number of trains docked at a station
        //Pre-Condition: None
        //Post-Condition: Returns number of trains docked at a station
        public int GetNumTrainsParked()
        {
            return numTrainsParked;
        }

        //Description: Gets the fixed ticket price at a station
        //Pre-Condition: None
        //Post-Condition: Returns ticket price
        public int GetTicketPrice()
        {
            return ticketPrice;
        }
        
    }

    //This class represents freight stations
    public class FreightStation : Station
    {
        private int amountDelivered = 0;  //amount of cargo delivered 

        //Description: Constructor method
        //Pre-Condition: None
        //Post-Condition: Creates and intializes a new object
        public FreightStation(string stationID)
        {
            this.ID = stationID;
            numTrainsVisited = 0;
            amountDelivered = 0;
            IsFreightStation = true;
            IsPassengerStation = false;
            IsHub = false;
        }


        //Description: Train calls this to unload cargo
        //Pre-Condition: None
        //Post-Condition: Amount delivered field will be updated
        public void UnloadCargo(int amount)
        {
            amountDelivered += amount;
        }

        //Description: Gets the amount of cargo delivered to a station
        //Pre-Condition: None
        //Post-Condition: Returns amount delivered field
        public int GetAmountDelivered()
        {
            return amountDelivered;
        }
    }

    //This is a passenger station where people arrive and depart from and 
    //where passengert trains visit
    public class PassengerStation : Station
    {
        private int totalPeopleVisited; //total number of people getting on or off a train onto the station

        //Description: Constructor method
        //Pre-Condition:
        //Post-Condition: Creates a new object and initializes fields
        public PassengerStation(string stationID, int ticketCost, int minOn, int maxOn, int minOff, int maxOff)
        {
            this.ID = stationID;
            totalPeopleVisited = 0;
            numTrainsVisited = 0;
            ticketPrice = ticketCost;
            this.minOn = minOn;
            this.maxOn = maxOn;
            this.minOff = minOff;
            this.maxOff = maxOff;
            IsFreightStation = false;
            IsPassengerStation = true;
            IsHub = false;
        }

        //Description: Gets the number of people that arrived/departed a station in givent time period
        //Pre-Condition: None
        //Post-Condition: Returns total people visited
        public int GetTotalPeopleVisited()
        {
            return totalPeopleVisited;
        }

        //Description: Adds people from station to visiting train
        //Pre-Condition:
        //Post-Condition:  Updates number of people on train, and number of people
        //that visited a station
        public int AddPeopleOnTrain()
        {
            Random randomNum = new Random();  //used for random umber generation
            int amountOn = 0;
            amountOn = randomNum.Next(minOn, maxOn + 1);  //random number of passengers enter train within set range
            totalPeopleVisited += amountOn;
            return amountOn;  //sends number back to calling train object
        }

        //Description:  Departs people from train to station
        //Pre-Condition: None
        //Post-Condition:  Updates number of people on train, and number of people
        //that visited a station. 
        public int SubPeopleOffTrain(PassengerTrain train)
        {
            Random randomNum = new Random();
            int amountOff = 0;
            amountOff = randomNum.Next(minOff, maxOff + 1);  //subtracts people from train based on a random
                                                             //number between the specified range

            //more people are being subtracted than the train even has
            if (amountOff > train.GetNumPassengers())
            {
                //Simply subtracts remaining passengers from train
                amountOff = train.GetNumPassengers();
                totalPeopleVisited += amountOff;
                return amountOff;  //return number to calling train
            }
            else
            {
                totalPeopleVisited += amountOff;
                return amountOff;  //returns number to calling train
            }
        }
    }

    //This class represents hubs where trains and crews depart from at the beginning of the 
    //day/shift respectively and return to at the end of the day/shift
    public class Hub : Vertex
    {
        private int numTimesUsed;  

        //Description: Constructor method
        //Pre-Condition: None
        //Post-Condition: Creates new hub object
        public Hub(string hubID)
        {
            this.ID = hubID;
            numTimesUsed = 0;
            IsFreightStation = false;
            IsPassengerStation = false;
            IsHub = true;
        }

        //Description: Gets the number of times hub is used
        //Pre-Condition: None
        //Post-Condition: Returns number of times used field
        public int GetNumTimesUsed()
        {
            return numTimesUsed;
        }

        //Description: Assigns a new crew to a train 
        //Pre-Condition: None
        //Post-Condition: New crew object made and assigned to train
        public Crew GetNewCrew()
        {
            numTimesUsed++;
            Crew newCrew = new Crew(this);  //creates a new crew object
            return newCrew; //returns the reference to it
        }

        //Description: Train must signify when it is departing from a hub
        //Pre-Condition: None
        //Post-Condition: Number of times used is updated
        public void DepartHub()
        {
            numTimesUsed++;
        }

        //Description: Train must signify when it is entering at a hub
        //Pre-Condition: None
        //Post-Condition: Number of times used is updated
        public void ArriveAtHub()
        {
            numTimesUsed++;
        }
    }
}
