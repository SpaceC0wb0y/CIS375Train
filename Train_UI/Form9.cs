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
    public partial class Form9 : Form
    {
        DBConnect trainconnect = new DBConnect();
        DBConnect trainconnect2 = new DBConnect();
        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlDataReader hubs = trainconnect.SelectDataReader("select station_id from station");

                
                while (hubs.Read())
                {
                    comboBox1.Items.Add(hubs.GetString("station_id"));
                }
                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stationid = comboBox1.GetItemText(comboBox1.SelectedItem);

            MySqlDataReader stationinfo = trainconnect2.SelectDataReader("DELETE from station where station_id = \"" + stationid + "\"");

            this.Controls.Clear();
            this.InitializeComponent();
            trainconnect.CloseConnection();
            reload();

        }

        public void reload()
        {
            MySqlDataReader hubs = trainconnect.SelectDataReader("select station_id from station");


            while (hubs.Read())
            {
                comboBox1.Items.Add(hubs.GetString("station_id"));
            }
        }
    }
}
