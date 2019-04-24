using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train_UI
{
    //This class represents an edge for the graph, connecting stations and hubs to each other
    public class Track
    {
        private string trackID;  
        private int cost;  //cost to install it
        private int numTimesUsed;  //Number of times a train went on it
        private Vertex source; //source station/hub
        private Vertex destination; //station/hub opposite to source
        private int numDetectedCollisions; 
        private bool isInMaintenance;
        private bool isAvailable;
        private int distance;  //weight of edge

        public Track()
        {
            ;
        }

        //Description: Cosntructor method
        //Pre-Condition: None
        //Post-Condition: New Track object with fields initialized
        public Track(string trackID, Vertex source, Vertex destination, int distance)
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

        //Description: Get the track ID
        //Pre-Condition: None
        //Post-Condition:  Returns Track id
        public string GetID()
        {
            return trackID;
        }

        //Description: Returns the source station/hub (one end of an edge)
        //Pre-Condition: None
        //Post-Condition: Returns source vertex
        public Vertex GetSource()
        {
            return source;
        }

        //Description: Returns the destination vertex, station/hub (other end of an edge)
        //Pre-Condition: None
        //Post-Condition: Returns destination vertex
        public Vertex GetDest()
        {
            return destination;
        }

        //Description: Gets the edge weight
        //Pre-Condition: None
        //Post-Condition: Returnes edge weight (in miles)
        public int GetDistance()
        {
            return distance;
        }

        //Description: Gets track cost
        //Pre-Condition: None
        //Post-Condition: Returns cost field of track object
        public int GetCost()
        {
            return cost;
        }

        //Description: Gets the number of collisions detected on an edge
        //Pre-Condition: None
        //Post-Condition: Returns number of detected collisions field
        public int GetNumCollisions()
        {
            return numDetectedCollisions;
        }

        //Description: Gets number of times a track is used by a train
        //Pre-Condition: None
        //Post-Condition: Returns the number of times used
        public int GetNumTimesUsed()
        {
            return numTimesUsed;
        }

        //Description: Changes whether or not a track is in maintenance
        //Pre-Condition: None
        //Post-Condition: Maintenance status is set
        public void SetMaintainenceStatus(bool status)
        {
            isInMaintenance = status;
        }

        //Description: Gets the maintenance status of an edge
        //Pre-Condition: None
        //Post-Condition: Returns true if edge is in maintenance, false if it isn't
        public bool GetMaintainenceStatus()
        {
            return isInMaintenance;
        }

        //Description: Changes whether or not a track is available
        //Pre-Condition: None
        //Post-Condition: Availability status is set
        public void SetAvailability(bool availability)
        {
            isAvailable = availability;
        }

        //Description: Gets the availability status of an edge
        //Pre-Condition: None
        //Post-Condition: Returns true if edge is available, false if it isn't
        public bool GetAvailability()
        {
            return isAvailable;
        }

        //Description: Adds number of times an edge is used by 1
        //Pre-Condition: None
        //Post-Condition: Adds 1 to numTimesUsed field
        public void IncrementNumTimesUsed()
        {
            numTimesUsed++;
        }

        //Description: Adds number of times a collision is detected by 1
        //Pre-Condition: None
        //Post-Condition: Adds 1 to number of detected collisions field
        public void IncrementNumDetectedCollisions()
        {
            numDetectedCollisions++;
        }
    }
}
