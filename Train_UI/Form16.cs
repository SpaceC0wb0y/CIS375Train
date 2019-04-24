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
    public partial class Form16 : Form
    {
        DBConnect trainconnect = new DBConnect();
        DBConnect trainconnect2 = new DBConnect();
        DBConnect trainconnect3 = new DBConnect();
        public Form16()
        {
            InitializeComponent();
        }

        private void Form16_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlDataReader hubs = trainconnect.SelectDataReader("select track_id from tracks");


                while (hubs.Read())
                {
                    comboBox1.Items.Add(hubs.GetString("track_id"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stationid = comboBox1.GetItemText(comboBox1.SelectedItem);

            try
            {
                MySqlDataReader stationinfo = trainconnect2.SelectDataReader("DELETE from tracks where track_id = \"" + stationid + "\"");
                MessageBox.Show("Success!");
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            this.Controls.Clear();
            this.InitializeComponent();
            trainconnect.CloseConnection();
            reload();
        }

        public void reload()
        {
            MySqlDataReader hubs = trainconnect.SelectDataReader("select track_id from tracks");


            while (hubs.Read())
            {
                comboBox1.Items.Add(hubs.GetString("track_id"));
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                string stationid = comboBox1.GetItemText(comboBox1.SelectedItem);

                MySqlDataReader stationinfo = trainconnect3.SelectDataReader("select coming_from,going_to,weight,num_times_used from tracks where track_id = \"" + stationid + "\"");

                while (stationinfo.Read())
                {
                    label1.Text = stationinfo.GetString("coming_from");
                    label2.Text = stationinfo.GetString("going_to");
                    label3.Text = stationinfo.GetString("weight");
                    label4.Text = stationinfo.GetString("num_times_used");


                }

                trainconnect2.CloseConnection();
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
