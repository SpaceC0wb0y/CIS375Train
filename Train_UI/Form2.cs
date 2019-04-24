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
        }
        private void Form2_Load(object sender, EventArgs e)
        {
               dataGridView1.DataSource = connection.SelectDataTable("select train_id, num_times_used from RSdatabase.train order by num_times_used desc;");
               dataGridView2.DataSource = connection.SelectDataTable("select station_id, num_times_used from RSdatabase.station order by num_times_used desc;");
               dataGridView3.DataSource = connection.SelectDataTable("select hub_id, num_times_used from RSdatabase.hub order by num_times_used desc;");
               dataGridView4.DataSource = connection.SelectDataTable("select track_id, num_times_used from RSdatabase.tracks order by num_times_used desc;");
               dataGridView5.DataSource = connection.SelectDataTable("select track_id, detected_coll from RSdatabase.tracks order by detected_coll desc;");
               dataGridView6.DataSource = connection.SelectDataTable("SELECT route_id, day, station1, station2, start_time from daily_f_routes where daily_f_routes.completedFlag = true;");
               dataGridView7.DataSource = connection.SelectDataTable("SELECT route_id, day, station, start_time from daily_p_routes where daily_p_routes.completedFlag = true;");
               dataGridView8.DataSource = connection.SelectDataTable("select day, route_id, station, start_time From RSdatabase.rp_late_train where rp_late_train.completedFlag = true;");
               dataGridView9.DataSource = connection.SelectDataTable("select day, station1, station2 from RSdatabase.rf_late_train where completedFlag = true;");
               dataGridView10.DataSource = connection.SelectDataTable("SELECT distinct X.train_assigned FROM(select distinct train_assigned from RSdatabase.daily_f_routes where start_time < train_start_time UNION select train_assigned from RSdatabase.rf_late_train, RSdatabase.repeatable_f_routes where rf_late_train.station1 = repeatable_f_routes.station1 and rf_late_train.station2 = repeatable_f_routes.station2 and rf_late_train.train_start_time > repeatable_f_routes.start_time) as X;");
               dataGridView11.DataSource = connection.SelectDataTable("SELECT distinct Y.train_assigned FROM ((select distinct train_assigned from RSdatabase.daily_p_routes where start_time < train_start_time) UNION (select train_assigned from RSdatabase.rp_late_train where train_start_time > start_time)) as Y;");
               dataGridView12.DataSource = connection.SelectDataTable("SELECT distinct Z.train_assigned FROM  (select distinct train_assigned from RSdatabase.daily_f_routes where start_time > train_start_time UNION select train_assigned from RSdatabase.rf_late_train, RSdatabase.repeatable_f_routes where rf_late_train.station1 = repeatable_f_routes.station1 and rf_late_train.station2 = repeatable_f_routes.station2 and rf_late_train.train_start_time < repeatable_f_routes.start_time) AS Z; ");
               dataGridView13.DataSource = connection.SelectDataTable("SELECT distinct W.train_assigned FROM (select distinct train_assigned from RSdatabase.daily_p_routes where start_time > train_start_time UNION select train_assigned from RSdatabase.rp_late_train where train_start_time < start_time) AS W;");
               dataGridView14.DataSource = connection.SelectDataTable("Select train_id, profit from freight_train;");
               dataGridView15.DataSource = connection.SelectDataTable("select station_id, cargoDelivered from station where station_type = 'F' order by cargoDelivered desc;");
               int ontimeroutes = connection.Count("SELECT count(*) from( select train_assigned from RSdatabase.daily_f_routes where start_time > train_start_time UNION select train_assigned from RSdatabase.rf_late_train, RSdatabase.repeatable_f_routes where rf_late_train.station1 = repeatable_f_routes.station1 and rf_late_train.station2 = repeatable_f_routes.station2 and rf_late_train.train_start_time < repeatable_f_routes.start_time UNION select train_assigned from RSdatabase.daily_p_routes where start_time > train_start_time UNION select train_assigned from RSdatabase.rp_late_train where train_start_time < start_time) as A;");
               int executedroutes = connection.Count("Select count(*) from (SELECT distinct(route_id) from daily_f_routes where daily_f_routes.completedFlag = true UNION SELECT distinct(route_id) from daily_f_routes where daily_f_routes.completedFlag = true UNION SELECT distinct(route_id) from daily_p_routes where daily_p_routes.completedFlag = true UNION select distinct(route_id) From RSdatabase.rp_late_train where rp_late_train.completedFlag = true UNION select distinct(route_id) from RSdatabase.rf_late_train where completedFlag = true) as A;");
               double onTimePerc = onTimePercent((dataGridView12.RowCount + dataGridView13.RowCount), executedroutes);
               label26.Text = onTimePerc.ToString("0.00") + " %";
          }
        public double onTimePercent(int ontimeroutes, int executedRoutes)
        {
               double percentage;
               if(executedRoutes == 0)
               {
                    return 0.0;
               }
               else
               {
                    percentage = (ontimeroutes / executedRoutes) * 100;
                    return percentage;
               }
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

          private void label3_Click(object sender, EventArgs e)
          {

          }

          private void label27_Click(object sender, EventArgs e)
          {

          }

          private void label28_Click(object sender, EventArgs e)
          {

          }

     }
}
