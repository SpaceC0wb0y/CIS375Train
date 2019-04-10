﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    //Description: In order to put station and hub objects in the same graph,
    //I decided to make a vertex interface and have the station and hub classes
    //implement it. It is empty because I am not sharing functionality between
    //stations and hubs
    public interface IVertex
    {
        
    }

    //Description: This is the base class for both passenger and freight stations
    //that holds common data and methods between the two
    public class Station : IVertex
    {
        protected string stationID;
        protected int numTrainsVisited; 
        protected int numTrainsParked;  //number of trains docked at a station
        protected int maxNumberOfTrains; //Station capacity
        protected int ticketPrice;  //fixed ticket price for a station
        protected int maxOn;  //maximum number of people getting on a train
        protected int minOn;  //minimum number of people getting on a train
        protected int minOff; //minimum number of people getting off a train
        protected int maxOff; //maximum number of people getting off a train

        //Description:
        //Pre-Condition:
        //Post-Condition:
        public string GetID()
        {
            return stationID;
        }

        //Description:
        //Pre-Condition:
        //Post-Condition:
        public bool Enter()
        {
            //there will be more code when adding in the max trains
            //additional feature here

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

        //Description:
        //Pre-Condition:
        //Post-Condition:
        public int GetNumTrainsVisited()
        {
            return numTrainsVisited;
        }

        //Description:
        //Pre-Condition:
        //Post-Condition:
        public int GetNumTrainsParked()
        {
            return numTrainsParked;
        }

        //Description:
        //Pre-Condition:
        //Post-Condition:
        public int GetTicketPrice()
        {
            return ticketPrice;
        }
        
    }

    public class FreightStation : Station
    {
        private int amountDelivered = 0;

        //Description:
        //Pre-Condition:
        //Post-Condition:
        public FreightStation(string stationID, int maxTrains, int ticketCost)
        {
            this.stationID = stationID;
            numTrainsVisited = 0;
            amountDelivered = 0;
            maxNumberOfTrains = maxTrains;
            ticketPrice = ticketCost;
        }

        //Description:
        //Pre-Condition:
        //Post-Condition:
        public int Resupply(int amount)
        {
            return amount;
        }

        //Description:
        //Pre-Condition:
        //Post-Condition:
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

    //This is a passenger station where people arrive and depart from and 
    //where passengert trains visit
    public class PassengerStation : Station
    {
        private int totalPeopleVisited; //total number of people getting on or off a train onto the station

        //Description: Constructor method
        //Pre-Condition:
        //Post-Condition: Creates a new object and initializes fields
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
    public class Hub : IVertex
    {
        private string hubID;  
        private int numTimesUsed;  

        //Description: Constructor method
        //Pre-Condition: None
        //Post-Condition: Creates new hub object
        public Hub(string hubID)
        {
            this.hubID = hubID;
            numTimesUsed = 0;
        }

        //Description: Gets the hub ID
        //Pre-Condition: None
        //Post-Condition: Returns hubID
        public string GetID()
        {
            return hubID;
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
