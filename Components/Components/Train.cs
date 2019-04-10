using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    public class Train
    {
    }


    //This class represents a passenger train that transport people to passenger stations
    public class PassengerTrain : Train
    {
        private PassengerStation currentDockedStation; //station that train is currently at
        int numPassengers;  //passenegrs currently on train
        int totalPassengers;  //total passengers that entered train in a given time frame

        //Description: Constructor method
        //Pre-Condition: None
        //Post-Condition: Generates passenger train object 
        public PassengerTrain(PassengerStation station)
        {
            numPassengers = 0;
            totalPassengers = 0;
            currentDockedStation = station;
        }

        //Description: Adds people from station to train
        //Pre-Condition: None
        //Post-Condition: Train gets more people, fields updated
        public void AddPassengers()
        {
            int numPeople = currentDockedStation.AddPeopleOnTrain();  //station has to add people on train
            numPassengers += numPeople;  
            totalPassengers += numPeople;
        }

        //Description: Remvoes people from train onto station
        //Pre-Condition: None
        //Post-Condition: Passenger field is decreased
        public void SubtractPassengers()
        {
            int numPeople = currentDockedStation.SubPeopleOffTrain(this);  //station has to remove people from train
            numPassengers -= numPeople;
        }

        //Description: Get number of passengers on a train
        //Pre-Condition: None
        //Post-Condition: Returns number of passengers
        public int GetNumPassengers()
        {
            return numPassengers;
        }

        //Description: Get total passengers that have traveled on a train in a given time frame
        //Pre-Condition: None
        //Post-Condition: Returns total passenger number
        public int GetTotalPassengers()
        {
            return totalPassengers;
        }

        //Description: Resets the trains object's fields
        //Pre-Condition: None
        //Post-Condition: All fields set to 0 (or its type equivalent)
        public void ClearFields()
        {
            currentDockedStation = null;
            numPassengers = 0;
            totalPassengers = 0;
        }
    }

}
