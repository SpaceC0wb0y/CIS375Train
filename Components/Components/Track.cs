using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Components
{
    class Track
    {
        private string trackID;
        private int cost;
        private int numTimesUsed;
        private IVertex source;
        private IVertex destination;
        private int numDetectedCollisions;
        private bool isInMaintenance;
        private bool isAvailable;
        private int distance;

        Track(string trackID, IVertex source, IVertex destination, int distance)
        {
            this.trackID = trackID;
            this.source = source;
            this.destination = destination;
            this.distance = distance;
            cost = 1000000;
            numDetectedCollisions = 0;
            numTimesUsed = 0;
            isInMaintenance = false;
            isAvailable = true;
        }

        public string GetID()
        {
            return trackID;
        }

        public IVertex GetSource()
        {
            return source;
        }

        public IVertex GetDest()
        {
            return destination;
        }

        public int GetDistance()
        {
            return distance;
        }

        public int GetCost()
        {
            return cost;
        }

        public int GetNumCollisions()
        {
            return numDetectedCollisions;
        }

        public int GetNumTimesUsed()
        {
            return numTimesUsed;
        }

        public void SetMaintainenceStatus(bool status)
        {
            isInMaintenance = status;
        }

        public bool GetMaintainenceStatus()
        {
            return isInMaintenance;
        }

        public void SetAvailability(bool availability)
        {
            isAvailable = availability;
        }

        public bool GetAvailability()
        {
            return isAvailable;
        }

        public void IncrementNumTimesUsed()
        {
            numTimesUsed++;
        }

        public void IncrementNumDetectedCollisions()
        {
            numDetectedCollisions++;
        }
    }
}
