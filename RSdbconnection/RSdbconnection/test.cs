using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSdbconnection;
using MySql.Data.MySqlClient;

namespace ConnectMySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Getting Connection ...");
            DBConnect connection = new DBConnect();
            List<List<string>> routesExecutedMETRIC = connection.Select("select route_id, train_id, computed_Route, route_type from RSdatabase.inprog_routes where time_completed != NULL; ");
            List<List<string>> onTimeTrainMETRIC = connection.Select("select distinct train_id from RSdatabase.inprog_routes where late = 0;");
            List<List<string>> lateTrainMETRIC = connection.Select("select distinct train_id from RSdatabase.inprog_routes where late = 1;");
            List<List<string>> mostUsedTrainMETRIC = connection.Select("select train_id, num_times_used from RSdatabase.train order by num_times_used desc; ");
            List<List<string>> mostUsedHubMETRIC = connection.Select("select hub_id, num_times_used from RSdatabase.hub order by num_times_used desc; ");
            List<List<string>> mostUsedStationMETRIC = connection.Select("select station_id, num_times_used from RSdatabase.station order by num_times_used desc; ");
            List<List<string>> mostUsedTrackMETRIC = connection.Select("select track_id, num_times_used from RSdatabase.tracks order by num_times_used desc; ");
            List<List<string>> mostCollisionProneTrack = connection.Select("select track_id, detected_coll from RSdatabase.tracks order by detected_coll desc; ");

               //If you want to know how to iterate through List of Lists, check Handy Resources in DesignDocs of Omar&Maitra
        }
    }

}