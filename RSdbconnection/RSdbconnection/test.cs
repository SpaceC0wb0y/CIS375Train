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
               connection.Select("select route_id, train_id, computed_Route, route_type from RSdatabase.inprog_routes where time_completed != NULL; ");
               connection.Select("select distinct train_id from RSdatabase.inprog_routes where late = 0;");
               connection.Select("select distinct train_id from RSdatabase.inprog_routes where late = 1;");
               connection.Select("select train_id, num_times_used from RSdatabase.train order by num_times_used desc; ");
               connection.Select("select hub_id, num_times_used from RSdatabase.hub order by num_times_used desc; ");
               connection.Select("select station_id, num_times_used from RSdatabase.station order by num_times_used desc; ");
               connection.Select("select track_id, num_times_used from RSdatabase.tracks order by num_times_used desc; ");
               connection.Select("select track_id, detected_coll from RSdatabase.tracks order by detected_coll desc; ");



          }
    }

}