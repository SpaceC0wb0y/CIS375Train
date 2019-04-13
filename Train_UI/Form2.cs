using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Train_UI
{
    public partial class Form2 : Form
    {
        DBConnect connection = new DBConnect();

        //MySqlConnection conn = DBUtils.GetDBConnection();
        //MySqlConnection conn8 = DBUtils.GetDBConnection();

        public Form2()
        {
            InitializeComponent();

            // MySqlConnection conn = DBUtils.GetDBConnection();
            // conn.Open();
            dataFetch();

           

        }

        public static double percentOnTime(ref List<List<string>> x, ref List<List<string>> y)
        {
            if (y.Count != 0)
                return ((x.Count / y.Count) * 100);
            else
                Console.WriteLine("Cannot compute percent on time trains, 0 routes were executed!");
            return 0.0;
        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        string queryString = "SELECT test.testcol FROM test WHERE test.idtest = 1";

        public void dataFetch()

        {
            label7.Text = "";
            List<List<string>> routesExecutedMETRIC = connection.Select("select route_id, train_id, computed_Route, route_type from RSdatabase.inprog_routes where time_completed != NULL; ");
            List<List<string>> onTimeTrainMETRIC = connection.Select("select distinct train_id from RSdatabase.inprog_routes where late = 0;");
            double perOnTimeTrain = percentOnTime(ref onTimeTrainMETRIC, ref routesExecutedMETRIC);
            Console.WriteLine(perOnTimeTrain);
            List<List<string>> lateTrainMETRIC = connection.Select("select distinct train_id from RSdatabase.inprog_routes where late = 1;");
            List<List<string>> mostUsedTrainMETRIC = connection.Select("select train_id, num_times_used from RSdatabase.train order by num_times_used desc; ");
            List<List<string>> mostUsedHubMETRIC = connection.Select("select hub_id, num_times_used from RSdatabase.hub order by num_times_used desc; ");
            List<List<string>> mostUsedStationMETRIC = connection.Select("select station_id, num_times_used from RSdatabase.station order by num_times_used desc; ");
            List<List<string>> mostUsedTrackMETRIC = connection.Select("select track_id, num_times_used from RSdatabase.tracks order by num_times_used desc; ");
            List<List<string>> mostCollisionProneTrack = connection.Select("select track_id, detected_coll from RSdatabase.tracks order by detected_coll desc; ");
            for (int i = 0; i < routesExecutedMETRIC.Count; i++)
            {
                for (int j = 0; j < routesExecutedMETRIC[i].Count; j++)
                {
                    label18.Text += " " + routesExecutedMETRIC[i][j];
                }
            }
            for (int i = 0; i < onTimeTrainMETRIC.Count; i++)
            {
                for (int j = 0; j < onTimeTrainMETRIC[i].Count; j++)
                {
                    label9.Text += " " + onTimeTrainMETRIC[i][j];
                }
            }
            for (int i = 0; i < lateTrainMETRIC.Count; i++)
            {
                for (int j = 0; j < lateTrainMETRIC[i].Count; j++)
                {
                    label10.Text += " " + lateTrainMETRIC[i][j];
                }
            }
            for (int i = 0; i < mostUsedTrainMETRIC.Count; i++)
            {
                for (int j = 0; j < mostUsedTrainMETRIC[i].Count; j++)
                {
                    label7.Text += " " + mostUsedTrainMETRIC[i][j];
                }
            }
            for (int i = 0; i < mostUsedHubMETRIC.Count; i++)
            {
                for (int j = 0; j < mostUsedHubMETRIC[i].Count; j++)
                {
                    label11.Text += " " + mostUsedHubMETRIC[i][j];
                }
            }
            for (int i = 0; i < mostUsedStationMETRIC.Count; i++)
            {
                for (int j = 0; j < mostUsedStationMETRIC[i].Count; j++)
                {
                    label20.Text += " " + mostUsedStationMETRIC[i][j];
                }
            }
            for (int i = 0; i < mostUsedTrackMETRIC.Count; i++)
            {
                for (int j = 0; j < mostUsedTrackMETRIC[i].Count; j++)
                {
                    label12.Text += " " + mostUsedTrackMETRIC[i][j];
                }
            }
            for (int i = 0; i < mostCollisionProneTrack.Count; i++)
            {
                for (int j = 0; j < mostCollisionProneTrack[i].Count; j++)
                {
                    label8.Text += " " + mostCollisionProneTrack[i][j];
                }
            }
            label16.Text = Convert.ToString(perOnTimeTrain);

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
