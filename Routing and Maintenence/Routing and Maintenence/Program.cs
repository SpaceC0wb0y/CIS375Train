using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This code is made by ya boi

namespace Routing_and_Maintenence
{
    class Program
    {
        public class Trains
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

            //float timeRunning
            //float totalTimeRunning

            void runRoute(string routeToRun, int startHub)
            {

            }

            void runRepeatRoute(string routeToRepeat, int startHub)
            {

            }

            void returnToHome(int startHub)
            {

            }

            void assignTrainAttributes()
            {

            }

            //void stopForMaintence(){}
            //void returnToHubForMainence(int startHub) {}

        };

        public class UserInfo
        {
           static public List<string> routeToRun;
            static public List<string> routeToRepeat;
            static public List<int> trainName;
            static public List<char> startHub; // should be a single char
            static public List<int> railwayLength; // should be an int
            static public List<int> railwayAvailibility; // should be an int
            static public List<string> railwayName;
            static public List<char> hubName; // should be an int
            static public List<int> hubAvailibility; // int
            static public List<int> stationName; // int
            static public List<int> stationAvailibility; // int

            public void AssignDummyData()
            {
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

                stationName.Add(1);
                stationName.Add(2);
                stationName.Add(3);

                stationAvailibility.Add(0);
                stationAvailibility.Add(0);
                stationAvailibility.Add(0);

            }

            public void readStationFile()
            {

            }

            public void readRailwayFile()
            {

            }

            public void readHubFile()
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

            public void readTrainFile()
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

            public void readDailyRouteFile()
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

            public void readRepeatingRouteFile()
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

        public class Railway
        {
            int railwayLength;
            bool isAvailible;
            char railwayName;

            //int trainLimitOnTrack

        };

        public class Hub
        {
            char hubName;
            bool isAvilable;
            char[] trainsInHub;
            int objNumber = 0;

            void assignHubDown()
            {
                foreach (char hubby in UserInfo.hubName)

                {
                    Hub a = new Hub() ;
                }
            }

        };

        public class Station
        {
            int stationName;
            bool isAvilable;
            char[] trainsInStation;
        };



        static void Main()
        {

            UserInfo A = new UserInfo();

            A.AssignDummyData();

            System.Console.ReadKey();
        }
    }
}
