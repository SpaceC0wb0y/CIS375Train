﻿using System;
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
    public partial class Form6 : Form
    {
        DBConnect trainconnect = new DBConnect();
        DBConnect trainconnect2 = new DBConnect();
        public Form6()
        {
            InitializeComponent();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            try
            {
                MySqlDataReader hubs = trainconnect.SelectDataReader("select train_id from train");

                //trainconnect.CloseConnection();
                while (hubs.Read())
                {
                    comboBox1.Items.Add(hubs.GetString("train_id"));
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

            MySqlDataReader stationinfo = trainconnect2.SelectDataReader("select hub_id,num_times_used,hours_travelled from train where train_id = \"" + stationid + "\"");

            while (stationinfo.Read())
            {
                label1.Text = stationinfo.GetString("hub_id");
                label2.Text = stationinfo.GetString("num_times_used");
                label3.Text = stationinfo.GetString("hours_travelled");
                //label4.Text = stationinfo.GetString("num_passenger");


            }

            trainconnect2.CloseConnection();
        }
    }
}
