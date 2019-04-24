﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route;
using Graph;

namespace Components
{
    //parent classes representing trains running in the system
    public class Train
    {
        protected int profitGenerated;  //amount of profit generated by a train (in dollars)
        public string trainID { get; set; }

        public int index {get; set; }
        public int indexForRouteList { get; set; }
        public int speed { get; set; }
        public int distanceTravelled { get; set; }
        protected int numberOfTimesUsed;
        //protected Crew currentCrew;
        public Vertex originHub {get; set; }




        protected Track currentTrack;  //when its on a track, this field is updated
        public Vertex currentLocation { get; set; }  //where in the graph is the train
        public Vertex nextLocation { get; set; }
        public DateTime ExpectedArrivalTime { get; set; }
        public DateTime StopTime { get; set; }


        public string currentStatus { get; set; }
        public bool IsPassengerTrain { get; set; }
        public bool IsFreightTrain { get; set; }
        
        public bool IsAssigned {get; set; }

        public void incrementNumberOfTimesUsed()
        {
            numberOfTimesUsed++;
        }

        public int getNumberOfTimesUsed()
        {
            return numberOfTimesUsed;
        }

        //public int GetSpeed()
        //{
        //    return speed;
        //}

        //public string GetID()
        //{
        //    return trainID;
        //}

        //INCOMPLETE
        //public Hub ChangeOriginHub(Hub newHub)
        //{
        //    originHub = newHub;
        //    return originHub;
        //}

        //THIS WILL BE COMPLETED LATER
        //public Crew ChangeCrew()
        //{

        //}
        

    }



    //This class represents a passenger train that transport people to passenger stations
    public class PassengerTrain : Train
    {
        private IList<Vertex> RoutePT = new List<Vertex>();
        private IList<PassengerRoute> RoutesOfAPT = new List<PassengerRoute>();
        private PassengerStation currentDockedStation; //station that train is currently at
        private int numPassengers;  //passengers currently on train
        private int totalPassengers;  //total passengers that entered train in a given time frame
        private int totalDistanceFromClosestHUBToRouteSourceStation;
        private IList<Vertex> ClosestPathToTheHUB = new List<Vertex>();
        private List<Vertex> TheRoute = new List<Vertex>();
        private List<Vertex> TheRouteBackHome = new List<Vertex>();


        //Description: Constructor method
        //Pre-Condition: None
        //Post-Condition: Generates passenger train object 
        public PassengerTrain(string trainID, int speed)
        {
            this.trainID = trainID;
            numPassengers = 0;
            totalPassengers = 0;
            currentDockedStation = null;
            profitGenerated = 0;
            IsPassengerTrain = true;
            IsFreightTrain = false;
            this.speed = speed;
            distanceTravelled = 0;
            IsAssigned = false;
            StopTime = new DateTime();
            StopTime = StopTime.AddHours(23).AddMinutes(59);
        }

        //Description: Adds profit based on number people that just boarded train from a station
        //Pre-Condition: None
        //Post-Condition: Profit field is updated
        public PassengerStation GetCurrentDockedStation()
        {
            return currentDockedStation;
        }

        //Description: Adds profit based on number people that just boarded train from a station
        //Pre-Condition: None
        //Post-Condition: Profit field is updated
        private void GenerateProfit(int numPeople, int ticketPrice)
        {
            profitGenerated += numPeople * ticketPrice;  //number of people boarding train times the ticketPrice for that train
        }

        //Description: Gets the profit generated (in dollars) by a train during a given period of time
        //Pre-Condition: None
        //Post-Condition: Profit field is returned
        public int GetProfitGenerated()
        {
            return profitGenerated;
        }

        //Description: Adds people from station to train
        //Pre-Condition: None
        //Post-Condition: Train gets more people, fields updated
        public void AddPassengers()
        {
            int numPeople = currentDockedStation.AddPeopleOnTrain();  //station has to add people on train
            numPassengers += numPeople;   //updating fields
            totalPassengers += numPeople;

            GenerateProfit(numPeople, currentDockedStation.GetTicketPrice());  //more people coming on means more profit
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
            profitGenerated = 0;
        }

           // Method that should assign routes to passenger trains by cycling through each train until all routes are assigned
        //MEANT FOR USE IN THE MASTER CONTROL 
        //Pre-Condition: Passenger train has to exist on the map, controller must produce a premade list of passenger trains and routes
        //Post-Condition: passenger trains get assigned a single station based on the arrival time, moving in round robin format until each
        //every route has been assigned, then find the shortest path of each of those routes to find the true route to assign to the trains
        public void AssignRoutePT(Graph g, IList<PassengerTrain> PTList, IList<PassengerRoute> PTRoute)
        {
            //IDictionary<Node, Linklist> graph;
            IList<PassengerTrain> PT;                      // PT = Passenger Trains
            IList<PassengerRoute> PR;              // PR = Passenger Route

            PT = PTList;
            PR = PTRoute;


            if (PT.Count > 0 && g != null && PR.Count > 0)
            {
                PR = PR.OrderBy(x => x.arrivalTime).ToList();

                foreach (PassengerRoute R in PR)
                {
                    if (PT.Count > 0)
                    {
                        //int totalDistance = 1000000;

                        if (PR.Where(x => x.IsAssigned == false).Count() > 0) // LINQ
                        {

                            foreach (PassengerTrain pt in PT.Where(x => x.IsPassengerTrain == true))
                            {

                                if (pt.RoutePT.Count() == 0)
                                {
                                  //  pt.originHub = pt.currentLocation;
                                    pt.RoutePT.Add(pt.originHub);
                                }
                                else
                                {
                                    pt.RoutePT.Add(R.GetDestinationStation());
                                }

                            }
                        }
                        R.IsAssigned = true;
                    }

                }

                foreach (PassengerTrain pt in PT.Where(x => x.IsPassengerTrain == true))
                {
                    int temp = 1000000;
                    int homeTemp = 1000000;

                    pt.RoutePT.Reverse();

                    IList<Vertex> closestPathToHub = g.TrainRoute(g, pt.currentLocation, pt.originHub, pt, out temp);
                    if (distanceTravelled > temp)
                    {
                        //Issue: how do we make sure we hit the vertices between the first and last verticies in the new route?
                        pt.totalDistanceFromClosestHUBToRouteSourceStation = temp;
                        pt.ClosestPathToTheHUB = closestPathToHub;
                        pt.TheRouteBackHome = g.TrainRoute(g, pt.RoutePT.Last(), pt.RoutePT.First(), pt, out homeTemp);

                    }
                }
            }
            else
            {
                Console.WriteLine("ERROR, NO TRAINS");
            }

        }
    }
        
    

    public class FreightTrain : Train
    {
        private FreightStation currentlyDockedStation;  //which freight station its currently visiting
        private int totalDistanceFromClosestHUBToRouteSourceStation;

        //I made it List from IList
        private List<Vertex> ClosestPathToTheHUB = new List<Vertex>();
        private List<Vertex> TheRoute = new List<Vertex>();
        private List<Vertex> TheIntermediateRoute = new List<Vertex>();
        private List<Vertex> TheRouteBackHome = new List<Vertex>();
        private List<Vertex> TheNextRoute = new List<Vertex>();

        DateTime ArrivalTimeNextStation;

        //I made it List from IList
        private List<FreightRoute> ListOfFrieghtRoutes = new List<FreightRoute>();

        private int amountOfCargoDelivered;  //for a given time period
        private const int PROFIT_PER_TON = 10000;  //$10,000 arbitrary unit per ton of cargo 

        //Description: Constructor method 
        //Pre-Condition: None
        //Post-Condition: New object made with fields initialized
        public FreightTrain(string trainID, int speed)
        {
            currentlyDockedStation = null;
            amountOfCargoDelivered = 0;
            profitGenerated = 0;
            IsPassengerTrain = false;
            IsFreightTrain = true;
            this.speed = speed;
            this.trainID = trainID;
            StopTime = new DateTime();
            StopTime = StopTime.AddHours(23).AddMinutes(59);
            ArrivalTimeNextStation = new DateTime();
        }

        //Description: Train unloads cargo at current freight station on its route
        //Pre-Condition: None
        //Post-Condition: profit generated field and amount delivered fields are updated
        public void UnloadCargo(int amount)
        {
            currentlyDockedStation.UnloadCargo(amount);
            amountOfCargoDelivered += amount;
            GenerateProfit(amount);
        }

        //Description: Train generates profit based of fixed rate per ton of cargo delivered 
        //Pre-Condition: None
        //Post-Condition: Profit generated field is updated
        private void GenerateProfit(int amount)
        {
            profitGenerated += PROFIT_PER_TON * amount;
        }

        //Description: Gets the total profit generated by the freight train
        //Pre-Condition: None
        //Post-Condition: Returns profit generated field
        public int GetProfitGenerated()
        {
            return profitGenerated;
        }

        //Description: Gets the amount of cargo, in tons, delviered by a freight train
        //Pre-Condition: None
        //Post-Condition: Returns amount of cargo delivered field
        public int GetAmountOfCargoDelivered()
        {
            return amountOfCargoDelivered;
        }


        //Description: Method will run though one iteration of a train's route, when the route is completed
        // the freight train will return to its home hub, collision and rerouting to be included
        //Pre-Condition: Freight Train has to exist on the graph, and must be assigned a route beforehand
        //Post-Condition: Train will be set to one of 5 status, when the train status is 'finished' the route stops operating
        public void RunRouteFT(Graph graph, FreightTrain tran, Clock Master)
        {
            int distance = 1000000;
            TheIntermediateRoute = graph.TrainRoute(graph, tran.currentLocation, tran.nextLocation, tran, out distance, out currentTrack);

            //float hoursToGetThere = (float)temp / speed;
            //t.ExpectedArrivalTime = Master.GetTime();
            //t.ExpectedArrivalTime.AddHours(hoursToGetThere);
            float hoursToGetThere = (float)distance / speed;
            tran.ArrivalTimeNextStation = Master.GetTime();
            tran.ArrivalTimeNextStation = tran.ArrivalTimeNextStation.AddHours(hoursToGetThere);

            if (tran.currentStatus == "initial")
                tran.currentStatus = "running";

            if (tran.currentStatus != "finished" || tran.currentStatus != "down" || tran.currentStatus != "completed")
            {
                if (Master.GetTime() >= tran.ArrivalTimeNextStation)
                {
                    if (tran.nextLocation == tran.TheRoute.Last())
                    {

                        tran.currentStatus = "completed";

                        if (tran.currentLocation.IsFreightStation == true || tran.currentLocation.IsPassengerStation == true)
                        {
                            Station tempStation = (Station)tran.currentLocation;
                            tempStation.Enter();
                        }

                        tran.currentLocation = tran.TheRoute[index + 1];
                        if (tran.currentLocation.IsFreightStation == true && tran.currentLocation == ListOfFrieghtRoutes[indexForRouteList].GetEndStation())
                        {
                            FreightStation tempStation = (FreightStation)tran.currentLocation;
                            tempStation.UnloadCargo(ListOfFrieghtRoutes[indexForRouteList].GetAmountToDeliver());
                            tran.GenerateProfit(ListOfFrieghtRoutes[indexForRouteList].GetAmountToDeliver());
                        }
                        tran.nextLocation = tran.TheRouteBackHome[0];

                    }
                    else
                    {
                        //if (tran.ExpectedArrivalTime == currentTime)
                        // {
                        tran.currentLocation = tran.TheRoute[index + 1];
                        //tran.currentTrack = null;
                        ////}
                        //if (tran.currentTrack != null)
                        //{
                        //    //train is on a track
                        //    tran.currentLocation = null;
                        //}

                        if (tran.currentLocation.IsFreightStation == true || tran.currentLocation.IsPassengerStation == true)
                        {
                            Station tempStation = (Station)tran.currentLocation;
                            tempStation.Enter();
                        }

                        // warp train to the station 
                        // set the current vertex to the next vertex over for the next run
                        tran.currentLocation = tran.TheRoute[index + 1];

                        if (tran.currentLocation.IsFreightStation == true && tran.currentLocation == ListOfFrieghtRoutes[indexForRouteList].GetEndStation())
                        {
                            FreightStation tempStation = (FreightStation)tran.currentLocation;
                            tempStation.UnloadCargo(ListOfFrieghtRoutes[indexForRouteList].GetAmountToDeliver());
                            tran.GenerateProfit(ListOfFrieghtRoutes[indexForRouteList].GetAmountToDeliver());
                        }

                        // set the next vertex to two over from the list for the next run  
                        tran.nextLocation = tran.TheRoute[index + 2];

                        Console.WriteLine("Train Arriving at station " + tran.currentLocation);

                    }
                    tran.distanceTravelled += distance;

                    index += 1;
                    indexForRouteList += 1;
                }
               

            }
            else if (tran.currentStatus == "completed")
            {
                // have return something to let the master know that the train has finished running for the day
                Console.WriteLine("TRAIN HAS FINSIHED ROUTE");

                TheIntermediateRoute = graph.TrainRoute(graph, tran.currentLocation, tran.nextLocation, tran, out distance);

                //float hoursToGetThere = (float)temp / speed;
                //t.ExpectedArrivalTime = Master.GetTime();
                //t.ExpectedArrivalTime.AddHours(hoursToGetThere);
                hoursToGetThere = (float)distance / speed;
                tran.ArrivalTimeNextStation = Master.GetTime();
                tran.ArrivalTimeNextStation = tran.ArrivalTimeNextStation.AddHours(hoursToGetThere);


                if (Master.GetTime() >= tran.ArrivalTimeNextStation)
                {
                    tran.currentLocation = tran.TheRouteBackHome[index + 1];
                    tran.currentLocation = tran.TheRoute[index + 1];
                    tran.nextLocation = tran.TheRouteBackHome[index + 2];
                }

            }

        }
    


          // Method that should assign routes to freight trains by cycling through each train until all routes are assigned
        //MEANT FOR USE IN THE MASTER CONTROL 
        //Pre-Condition: Freight train has to exist on the map, controller must produce a premade list of freight trains and routes
        //Post-Condition: each train gets assigned a route until all routes are assigned
        public void AssignRouteFT(Graph g, List<FreightTrain> FTList, List<FreightRoute> FTRoute, Clock Master)
        {
            //IDictionary<Node, Linklist> graph;
            List<FreightTrain> FT;                      // FT = Freight Trains
            List<FreightRoute> FR;              // FR = Freight Route
            List<FreightTrain> AssignedFreightTrain = new List<FreightTrain>();

            FT = FTList;
            FR = FTRoute;

            if (FT.Count > 0 && g != null && FR.Count > 0)
            {
                // foreach route that exists in the list of freight routes
                foreach (FreightRoute R in FR.OrderBy(x => x.GetStartTime()))
                {

                    FreightTrain t = new FreightTrain("Test", 60);
                    if (FT.Where(x => x.IsFreightTrain == true && x.IsAssigned == true).Count() > 0)
                    {
                        int totalDistance = 1000000;
                        
                        // foreach train in list of freight trains
                        foreach (FreightTrain ft in FT.Where(x => x.IsFreightTrain == true && x.IsAssigned == true))
                        {
                            int temp = 1000000;
                            int routeTemp = 100000;
                            int homeTemp = 100000;
                            // find the closet path to the hub from where the current station is to where its home hub is
                            List<Vertex> closestPathToHub = g.TrainRoute(g, ft.currentLocation, R.GetStartStation(), ft, out temp);
                            if (totalDistance > temp)      
                            {
                                t = ft;
                                t.originHub = t.currentLocation;
                                t.totalDistanceFromClosestHUBToRouteSourceStation = temp;
                                // assign the closet path to the hub to the train
                                t.ClosestPathToTheHUB = closestPathToHub;
                                // assign the full shortest path route to the train
                                t.TheRoute = g.TrainRoute(g, R.GetStartStation(), R.GetEndStation(), t, out routeTemp);
                                // assign the shortest path from the end of the full route to the train's home hub to the train 
                                t.TheRouteBackHome = g.TrainRoute(g, R.GetEndStation(), t.originHub, t, out homeTemp);


                                float hoursToGetThere = (float)temp / speed;
                                t.ExpectedArrivalTime = Master.GetTime();
                                t.ExpectedArrivalTime = t.ExpectedArrivalTime.AddHours(hoursToGetThere);

                                //float hoursToGetThere = (float)temp / speed;
                                ////ft.ExpectedArrivalTime = Clock.GetTime;
                                //ft.ExpectedArrivalTime.AddHours(hoursToGetThere);
                                //if (ft.ExpectedArrivalTime <= Route.ArrivalTime)
                                //{
                                //    //train will get there on time
                                //    //Code here

                                //}
                                //else{
                                //    //train will be late
                                //    //probably will not assign
                                //}

                            }

                        }

                        FT.Where(x => x.IsFreightTrain == true && t.GetID() == x.GetID()).FirstOrDefault().IsAssigned = true;
                        R.IsAssigned = true;
                    }
                    else
                    {
                        R.IsAssigned = false;
                    }
                }

                // FT[0].gets


                // Once a fT has 2 phases and hase some time remaining to comeplete another ride.
                // Check
                // if there exists unassigned routes in the freight route list

                bool areThereAnyUnAssignedRoutes = FR.Where(x => x.IsAssigned == false).Count() > 0;
                int unassignedRoutesCount = FR.Where(x => x.IsAssigned == false).Count(); // 5

                // NOT ASSIGNING WE are doing feasibility check 
                while (areThereAnyUnAssignedRoutes) // LINQ
                {
                    foreach (FreightTrain ft in FT.Where(x => x.IsFreightTrain == true))
                    {
                        // there is time to do another ride
                        if (ft.ExpectedArrivalTime.Hour < StopTime.Hour && ft.ExpectedArrivalTime.Minute < StopTime.Minute)
                        {
                            // check if there are any sutable routes
                            IOrderedEnumerable<FreightRoute> suitableFR = FR.Where(x => x.IsAssigned == false && x.GetStartTime() >= ft.ExpectedArrivalTime).OrderBy(x => x.GetStartTime());
                            suitableFR.ToList();


                            // there are routes that come after the arrival time
                            if (suitableFR != null && suitableFR.Count() > 0)
                            {
                                int totalDistance = 1000000;
                                int temp1 = 1000000;
                                int temp2 = 1000000;
                                int distanceToHome = 1000000;


                                foreach (FreightRoute R in suitableFR)
                                {
                                    IList<Vertex> verticesOldDestToNewSource = g.TrainRoute(g, ft.currentLocation, R.GetStartStation(), ft, out temp1);
                                    IList<Vertex> verticesNewSourceToNewDest = g.TrainRoute(g, R.GetStartStation(), R.GetEndStation(), ft, out temp2);
                                    if (totalDistance > (temp1 + temp2))
                                    {
                                        List<Vertex> verticesNewDestToHub = g.TrainRoute(g, R.GetEndStation(), ft.originHub, ft, out distanceToHome);
                                        // calculate expected arrival time
                                        float hoursToGetThere = (float)temp1 + temp2 + distanceToHome / speed;
                                        DateTime newExpectedArrivalTime = ft.ExpectedArrivalTime;
                                        newExpectedArrivalTime = newExpectedArrivalTime.AddHours(hoursToGetThere);

                                        //check if it reaches hub in time for end
                                        if (newExpectedArrivalTime.Hour < ft.StopTime.Hour && newExpectedArrivalTime.Minute < ft.StopTime.Minute)
                                        {
                                            //add to the list
                                            ft.TheRoute.AddRange(verticesOldDestToNewSource);
                                            ft.TheRoute.AddRange(verticesNewSourceToNewDest);
                                            ft.TheRouteBackHome = verticesNewDestToHub;
                                            R.IsAssigned = true;
                                            areThereAnyUnAssignedRoutes = FR.Where(x => x.IsAssigned == false).Count() > 0;
                                        }
                                    }
                                }
                            }
                        }

                    }


                    // infinite loop breaker
                    if (unassignedRoutesCount != FR.Where(x => x.IsAssigned == false).Count())
                    {
                        //5
                        unassignedRoutesCount = FR.Where(x => x.IsAssigned == false).Count();
                    }
                    else
                    {
                        break;
                    }

                }
            
            }
            else
            {
                Console.WriteLine("Error, No Trains");
            }
        
        }
    }
}
    

