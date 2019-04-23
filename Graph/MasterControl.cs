using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components;
using Graph;
using Route;

namespace Master
{
    class Control
    {
        static void RunSimulation(Graph g, Clock MasterClock, int numDaysInBreakpoint)
        {
            List<FreightTrain> freightTrains = new List<FreightTrain>();
            List<FreightRoute> dummyRoutes = new List<FreightRoute>();
            for (int i = 0; i < numDaysInBreakpoint; i++)
            {
                //FT.Where(x => x.IsFreightTrain == true && x.IsAssigned == true)
                foreach (var item in g.GetTrains().Where(x => x.IsFreightTrain == true))
                {
                    freightTrains.Add(item);    
                }
                freightTrains[0].AssignRouteFT(g, freightTrains, dummyRoutes, MasterClock);

                while(MasterClock.GetTime().Hour < 23 && MasterClock.GetTime().Minute < 59)
                {
                    MasterClock.CustomTick(0, 1, 0);
                    foreach(var item in freightTrains)
                    {
                        item.RunRouteFT(g, item, MasterClock);
                    }
                }
                MasterClock.CustomTick(0, 1, 0);
            }
            // Read from the database and assign the database's list of attributes with the ones in the MasterControl program

            //TickInterval SimulationInterval = new TickInterval(0, 0, 15); 
            //DateTime EndofDay = new DateTime();
            //DateTime CurrentTime = new DateTime();
            //IList<FreightStation> ListOfFrightStations = new List<FreightStation>();
            //IList<Hub> ListOfHubs = new List<Hub>();
            //IList<Track> ListOfTracks = new List<Track>();
            //IList<FreightTrain> ListOfFrightTrains = new List<FreightTrain>();

            //EndofDay.AddHours(23).AddMinutes(59);

             /* Database Files fill attibutes with lists of Stations, Tracks, etc to be created */
            // put all of the frightstations in the graph
            //foreach(FreightStation FS in  ListOfFrightStations)
            //{
            //    FreightStation NewStation = FS; 
            //    NewStation = new FreightStation(FS.GetID(), FS.GetTicketPrice());
            //    g.AddStation(NewStation);
                
            //}
            // put all of the hubs in the graph
            //foreach(Hub H in  ListOfHubs)
            //{
             //   Hub NewHub = H; 
              //  NewHub = new Hub(H.GetID());
               // g.AddVertex(H);
                
            //}
            // put all of the Tracks in the graph
            //foreach(Track E in ListOfTracks)
            //{
            //    Track NewTrack = E;
             //   NewTrack = new Track(NewTrack.GetID() ,NewTrack.GetSource(), NewTrack.GetDest(), NewTrack.GetDistance());
             //   g.AddTrack(NewTrack);
            //}
            // put all of the frighttrains in the graph
            //foreach(FreightTrain FT in ListOfFrightTrains)
            //{
             //   FreightTrain NewFT = FT;
              //  NewFT = new FreightTrain(NewFT.GetID(), NewFT.GetSpeed());
               // g.AddTrain(NewFT);
            //}

            // need to put the assign route method somewhere, but does it go on the inside or outside? 

        // Run the Program until the end of the day (11:45 pm)

        //while (CurrentTime != EndofDay)
        {        
            // tick the clock every 15 minutes
         //   MasterClock.RunClock(SimulationInterval);

            //Method To Run Routes

            //Method To Update Info and pass it to the database (run for each train that exists)
            //{
                //example
                //foreach(FreightTrain FT in ListOfFrightTrains.Where(x => x.currentstatus != "Complete"))
                //{
                // FT.RunRouteFT(Graph, FT)
                //}
            //}

            // add 15 minutes to the current (Master) time
           // CurrentTime.AddMinutes(1);
        //}
            

        }
    }
    }
}