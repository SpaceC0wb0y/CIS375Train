using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routing_and_Maintenence
{
    class Program
    {
        public class Train
        {
            bool onTrack;
            bool onStation;
            bool onHub;
            int currentStation;
            int currentHub;
            char currentTrack;
            char trainName;
            int startHub;
            string trainType;
            string routeToRun;
            string routeToRepeat;
            int distanceOnTrack;
            int totalDistanceTraveled;
            int totalTrains = 0; // total number of trains, asssigned by the number of train names in the UserInfo trainName list
            int objNumber = 0; // the object number, used to keep track of the amount of objects created 

            //float timeRunning
            //float totalTimeRunning

            void runRoute(string routeToRun, int startHub)
            {
                int[,] graph = new int[,] { }; // graph goes here (adjacency matrix)

                GFG a = new GFG(); 
                a.dijkstra(graph, 0); // takes in the graph

            }

            void runRepeatRoute(string routeToRepeat, int startHub)
            {

            }

            void returnToHome(int startHub)
            {

            }

   
            public Train(string RTRu, string RTRe, int TN, char SH) // constructor, takes in the hub name, aviliblity, and amount of trains in the hub and assigns them to the object
            {

                objNumber++;
            }

            void assignTrainAttributes()
            {
                    var TrainList = new List<Train>(); // list of railway objects
                    totalTrains = UserInfo.trainName.Count; // number of railways based on list of names

                    // for (int i = 0; i < totalRailways; i++ )
                    //  {
                    TrainList.Add(new Train("ABC", "BCA", 1, 'A'));

                    //}
            
            }

            //void stopForMaintence(){}
            //void returnToHubForMainence(int startHub) {}

        };

         // based on https://www.geeksforgeeks.org/csharp-program-for-dijkstras-shortest-path-algorithm-greedy-algo-7/
        class GFG // finds the vertex with the minimum distance value from the set of vertices
        {
            static int V = 9;
            // finds the minimum distance between two points
            int minDistance(int[] dist, bool[] sptSet)
            {
                int min = int.MaxValue, min_index = -1;

                for (int v = 0; v < V; v++)
                    if (sptSet[v] == false && dist[v] <= min)
                    {
                        min = dist[v];
                        min_index = v;
                    }
                return min_index;
            }
            //uses Dijkstra's aglorithm to find the sourtest path in a adjacency matric map.
            public void dijkstra(int[, ] graph, int src)
            {
                int[] dist = new int[V];

                bool[] sptSet = new bool[V];

                for (int i = 0; i < V; i++)
                {
                    dist[i] = int.MaxValue;
                    sptSet[i] = false;
                }

                dist[src] = 0;

                for (int count = 0; count < V-1; count++)
                {
                    int u = minDistance(dist, sptSet);

                    sptSet[u] = true;

                    for (int v = 0; v < V; v++)
                    {
                        if (!sptSet[v] && graph[u,v] != 0 && dist[u] != int.MaxValue && dist[u]
                            + graph[u,v] < dist[v])
                        {
                            dist[v] = dist[u] + graph[u, v];
                        }
                    }
                }
            }
        };


        public class UserInfo
        {
            static public List<string> routeToRun; // route that the train will run once
            static public List<string> routeToRepeat; // route that train will run for the entire day
            static public List<int> trainName; // name of the train
            static public List<char> startHub; // the location of the train at the beginning of the day
            static public List<int> railwayLength; // length of the railway in miles?
            static public List<int> railwayAvailibility; // dictates whither the railway is unavilible for the day, treat like a list of bools
            static public List<string> railwayName; // name of railway
            static public List<char> hubName; // name of hub
            static public List<int> hubAvailibility; // dictates whither the hub is unavilible for the day, treat like a list of bools
            static public List<int> stationName; // name of station
            static public List<int> stationAvailibility; // dictates whither the hub is unavilible for the day, treat like a list of bools
            static public List<int> trainsInHub; // what trains are currently in a hub
            static public List<int> trainsInStation; // what trains are currently in a station
            static public List<int> trainsOnRailway; // what trains are currently on the railway

            public UserInfo() // constructor, creates instances of all the lists
            {
               
            }
            
            public void AssignDummyData() // way to assign values to the lists without reading from a file
            {

                List<string> routeToRun = new List<string>();
                List<string> routeToRepeat = new List<string>();
                List<int> trainName = new List<int>();
                List<char> startHub = new List<char>();
                List<int> railwayLength = new List<int>();
                List<int> railwayAvailibility = new List<int>();
                List<string> railwayName = new List<string>();
                List<char> hubName = new List<char>();
                List<int> hubAvailibility = new List<int>();
                List<int> stationName = new List<int>();
                List<int> stationAvailibility = new List<int>();
                List<int> trainsInHub = new List<int>();
                List<int> trainsInStation = new List<int>();
                List<int> trainsOnRailway = new List<int>();

                routeToRun.Add("ABC");

                routeToRepeat.Add("BCA");

                trainName.Add(1);

                startHub.Add('A');

                railwayLength.Add(7);
                railwayLength.Add(8);
                railwayLength.Add(9);
                railwayLength.Add(10);

                railwayAvailibility.Add(0);
                railwayAvailibility.Add(0);
                railwayAvailibility.Add(0);
                railwayAvailibility.Add(0);

                railwayName.Add("7 Length");
                railwayName.Add("8 Length");
                railwayName.Add("9 Length");
                railwayName.Add("10 Length");

                hubName.Add('A');

                hubAvailibility.Add(0);

                trainsInHub.Add(1);

                stationName.Add(1);
                stationName.Add(2);
                stationName.Add(3);

                stationAvailibility.Add(0);
                stationAvailibility.Add(0);
                stationAvailibility.Add(0);

                trainsInStation.Add(0);
                trainsInStation.Add(0);
                trainsInStation.Add(0);

                trainsOnRailway.Add(0);
                trainsOnRailway.Add(0);
                trainsOnRailway.Add(0);


            }

            public void readStationFile() // read file containing Station info
            {

            }

            public void readRailwayFile() // read file contating Railway info
            {

            }

            public void readHubFile() // read file containing Hub info
            {
                //const string fileName = @"C:\Users\justi\source\repos\Routing and Maintenence\Routing and Maintenence\Hubs.txt";

                //List<string> lines = new List<string>();
                //hubName = new List<string>();
                //hubAvailibility = new List<string>();


                //using (StreamReader reader = new StreamReader(fileName))
                //{
                //    string line;
                //    while ((line = reader.ReadLine()) != null)
                //    {
                //        lines.Add(line);
                //    }
                //}

                //foreach (string repeatRoute in lines)
                //{
                //    routeToRepeat.Add(repeatRoute);
                //    Console.WriteLine(repeatRoute);
                //}

            }

            public void readTrainFile() // read file containing Train info
            {
                //const string fileName = @"C:\Users\justi\source\repos\Routing and Maintenence\Routing and Maintenence\Trains.txt";

                //List<string> lines = new List<string>();
                //trainName = new List<string>();
                //startHub = new List<string>();

                //using (StreamReader reader = new StreamReader(fileName))
                //{
                //    string line;
                //    while ((line = reader.ReadLine()) != null)
                //    {
                //        lines.Add(line);
                //    }
                //}

                //foreach (string Train in lines)
                //{
                //    trainName.Add(Train);
                //    Console.WriteLine(Train);
                //}

            }

            public void readDailyRouteFile() // read file containg Daily Route info
            {
                //const string fileName = @"C:\Users\justi\source\repos\Routing and Maintenence\Routing and Maintenence\DailyRoutes.txt";

                //List<string> lines = new List<string>();
                //routeToRun = new List<string>();

                //using (StreamReader reader = new StreamReader(fileName))
                //{
                //    string line;
                //    while ((line = reader.ReadLine()) != null)
                //    {
                //        lines.Add(line);
                //    }
                //}

                //foreach (string Route in lines)
                //{
                //    routeToRun.Add(Route);
                //    Console.WriteLine(Route);
                //}

            }

            public void readRepeatingRouteFile() // read file contatining Repeating Route info
            {
                //    const string fileName = @"C:\Users\justi\source\repos\Routing and Maintenence\Routing and Maintenence\RepeatingRoutes.txt";

                //    List<string> lines = new List<string>();
                //    routeToRepeat = new List<string>();

                //    using (StreamReader reader = new StreamReader(fileName))
                //    {
                //        string line;
                //        while ((line = reader.ReadLine()) != null)
                //        {
                //            lines.Add(line);
                //        }
                //    }

                //    foreach (string repeatRoute in lines)
                //    {
                //        routeToRepeat.Add(repeatRoute);
                //        Console.WriteLine(repeatRoute);
                //    }

            }
        };


        public class Train // class for the train
        {





          


        };
        public class Railway // class for the Railway
        {
            int _railwayLength; // length of the raliway
            int _isAvailible; // the aviliblity of the railway
            string _railwayName; // railway name
            int totalRailways = 0; // total number of railways, asssigned by the number of hub names in the UserInfo ralwayNames list
            int objNumber = 0; // the object number, used to keep track of the amount of objects created 
            //int trainLimitOnTrack

            public Railway(int rLength, int isavil, string RN) // constructor, takes in the hub name, aviliblity, and amount of trains in the hub and assigns them to the object
            {
                _railwayLength = rLength;
                _isAvailible = isavil;
                _railwayName = RN;
                objNumber++;
            }

            public Railway()
            {

            }

         

            public void assignRailwayValues() // assign the values from the UserInfo class
            {
                var RailwayList = new List<Railway>(); // list of railway objects
                //totalRailways = UserInfo.railwayName.Count; // number of railways based on list of names

                // for (int i = 0; i < totalRailways; i++ )
                //  {
                RailwayList.Add(new Railway(7, 0, "7 Length"));
                RailwayList.Add(new Railway(8, 0, "8 Length"));
                RailwayList.Add(new Railway(9, 0, "9 Length"));
                RailwayList.Add(new Railway(10, 0, "10 Length"));

                RailwayList.ForEach(Console.WriteLine);
                //}
            }

        };

        public class Hub // class for the hubs
        {
            char _hubName; // name of the hub
            int _isAvilable; // the avilibilty of the hub (0 for free, 1 for not avilible)
            int _trainsInHub; // trains currently in the hub
            int totalHubs = 0; // total number of hubs, asssigned by the number of hub names in the UserInfo hubname list
            int objNumber = 0; // the object number, used to keep track of the amount of objects created

            public Hub(char HN, int isavil, int tIH) // constructor, takes in the hub name, aviliblity, and amount of trains in the hub and assigns them to the object
            {
                _hubName = HN;
                _isAvilable = isavil;
                _trainsInHub = tIH;
                objNumber++;
            }


            public void assignHubValues() // assign the values from UserInfo into the class
            {
                var hubList = new List<Hub>(); // list of hub objects
               totalHubs = UserInfo.hubName.Count; // total hubs = number of hub names in the UserInfo class list

               // for (int i = 0; i < totalHubs; i++ )  // should loop though and add a hub object to the list for each hub name in the UserInfo class list
              //  {
                    hubList.Add(new Hub('A', 0, 1)); // create a hub (for now its manually put in)
                   

                //}
                
            }

        };

        public class Station // station class
        {
            int _stationName; // name of the station
            int _isAvilable; // station aviliblity 
            int _trainsInStation; // amount of trains in the station
            int totalStations = 0; // total number of stations
            int objNumber = 0; // object number of the station

            public Station(char SN, int isavil, int tIS) // constructor
            {
                _stationName = SN;
                _isAvilable = isavil;
                _trainsInStation = tIS;
                objNumber++;
            }

            public void assignStationValues() // assign values to the station class from the UserInfo class
            {
                var stationList = new List<Station>(); // list of station objects
                totalStations = UserInfo.stationName.Count;

                // for (int i = 0; i < totalstations; i++ )
                //  {
                stationList.Add(new Station('1', 0, 0));
                stationList.Add(new Station('2', 0, 0));
                stationList.Add(new Station('3', 0, 0));


                //}

            }

        };



        static void Main()
        {

            UserInfo A = new UserInfo(); // initialize
            Railway B = new Railway();

            A.AssignDummyData(); // call the class to create the dummy data

            B.assignRailwayValues();



            System.Console.ReadKey(); // stops the console from ending before user can see
        }
    }
}
