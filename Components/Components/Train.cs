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

    public class PassengerTrain : Train
    {
        private PassengerStation currentDockedStation;
        int numPassengers;
        int totalPassengers;

        public PassengerTrain(PassengerStation station)
        {
            numPassengers = 0;
            totalPassengers = 0;
            currentDockedStation = station;
        }

        public void AddPassengers()
        {
            int numPeople = currentDockedStation.AddPeopleOnTrain();
            numPassengers += numPeople;
            totalPassengers += numPeople;
        }

        public void SubtractPassengers()
        {
            int numPeople = currentDockedStation.SubPeopleOffTrain(this);
            numPassengers -= numPeople;
        }

        public int GetNumPassengers()
        {
            return numPassengers;
        }

        public int GetTotalPassengers()
        {
            return totalPassengers;
        }
    }

}
