using System;
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
    public partial class Form4 : Form
    {
        DBConnect trainconnect = new DBConnect();
        DBConnect trainconnect2 = new DBConnect();
        public Form4()
        {

            InitializeComponent();
            
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlDataReader stations = trainconnect.SelectDataReader("select station_id from station");
                
                //trainconnect.CloseConnection();
                while (stations.Read())
                {
                    comboBox1.Items.Add(stations.GetString("station_id"));
                }
                /*films = LoadListings();
                foreach (Listing film in films)
                {
                    cmbMovieListingBox.Items.Add(film.GetFilmTitle());
                }*/

               /* string selectQuery = "SELECT station_id FROM station";
                connection.Open();
                MySqlCommand command = new MySqlCommand(selectQuery, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetString("id"));
                }*/
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string stationid = comboBox1.GetItemText(comboBox1.SelectedItem);
            
            MySqlDataReader stationinfo = trainconnect2.SelectDataReader("select station_id,station_type,range_on,range_off,ticket_price from station where station_id = \"" + stationid +"\"");

            while (stationinfo.Read())
            {
                label1.Text = stationinfo.GetString("station_id");
                label2.Text = stationinfo.GetString("station_type");
                label3.Text = stationinfo.GetString("range_on");
                label4.Text = stationinfo.GetString("range_off");
                label5.Text = stationinfo.GetString("ticket_price");
                
            }

            trainconnect2.CloseConnection();

            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
 