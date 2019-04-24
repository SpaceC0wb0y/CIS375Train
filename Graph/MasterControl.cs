using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components;
using Graph;
using Route;
using Database;

namespace Master
{
    class Control
    {
        static void RunSimulation(Graph g, Clock MasterClock, int numDaysInBreakpoint)
        {
            Graph graphy = new Graph();
            DBConnect database = new DBConnect();
            List<List<string>> dbTrainFetch = database.Select("SELECT train.train_id, train.speed from train JOIN freight_train ON train.train_id = freight_train.train_id;");
            List<List<string>> dbDroutesFetch = database.Select("SELECT day, station1, station2, start_time, cargo_capacity from daily_f_routes;");
            List<FreightRoute> fdr = new List<FreightRoute>(); 
            List<FreightTrain> ft = new List<FreightTrain>();
               FreightTrain f = new FreightTrain();
               FreightRoute dr = new FreightRoute();
               if (dbTrainFetch != null && dbTrainFetch.Count > 0)
               {
                foreach (var t1 in dbTrainFetch)
                { 
                    f.trainID = t1[0];
                    f.speed = t1[1];
                }
                    ft.Add(f);
                    f = new FreightTrain();

               }
            if (dbDroutesFetch != null && dbDroutesFetch.Count > 0)
            {
                foreach (var t1 in dbDroutesFetch)
                {
                    dr.startDay = (int)t1[0];
                    dr.setStartStation(graphy.FindStation(t1[1]));
                    dr.setEndStaiton(graphy.FindStation(t1[2]));
                    dr.startTime = Convert.ToDateTime(t1[3]);
                    dr.amountToDeliver = t1[4];
                }
                fdr.Add(f);
                dr = new FreightRoute();

            }




            for (int i = 0; i < numDaysInBreakpoint; i++)
            {
                //FT.Where(x => x.IsFreightTrain == true && x.IsAssigned == true)
                foreach (var item in g.GetTrains().Where(x => x.IsFreightTrain == true))
                {
                    freightTrains.Add(item);
                }
                freightTrains[0].AssignRouteFT(g, freightTrains, dummyRoutes, MasterClock);

                while (MasterClock.GetTime().Hour < 23 && MasterClock.GetTime().Minute < 59)
                {
                    MasterClock.CustomTick(0, 1, 0);
                    foreach (var item in freightTrains)
                    {
                        item.RunRouteFT(g, item, MasterClock);
                    }
                }
                MasterClock.CustomTick(0, 1, 0);
            }


            foreach(Vertex v in graphy.vertices)
            {
                string station_id = v.GetID().To_String();
                string num_times_visited = v.GetNumTrainsVisited().To_String();
                string amount_delivered = v.GetAmountDelivered().To_String();
                string query = "UPDATE station SET num_times_used = " + num_times_visited + " where station_id = " + "'" + station_id + "'";
                database.Update(query);
                query = "UPDATE station SET cargoDelivered = " + amount_delivered + " where station_id = " + "'" + station_id + "'";
                database.Update(query);



            }

            foreach(Edge e in graphy.edges)
            {
                string track_id = e.GetID().To_String();
                string num_times_used = e.GetNumTimesUsed().To_String();
                string num_collisions = e.GetNumCollisions().To_String();
                string query = "UPDATE track SET num_times_used = " + num_times_used + " where track_id = " + "'" + track_id + "'";
                database.Update(query);
                query = "UPDATE station SET detected_coll = " + num_collisions + " where track_id = " + "'" + track_id + "'";
                database.Update(query);

            }

            foreach(FrightTrain ft in graphy.trains)
            {
                string train_id = ft.trainID.To_String();
                string distance_travelled = ft.distanceTravelled.To_String();
                string num_times_used = ft.getNumberOfTimesUsed().To_String() ;
                string query = "UPDATE train SET miles_travelled = " + distance_travelled + " where train_id = " + "'" + train_id + "'";
                database.Update(query);
                query = "UPDATE train SET num_times_used = " + num_times_used + " where train_id = " + "'" + train_id + "'";
                database.Update(query);
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
      //  {        
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
