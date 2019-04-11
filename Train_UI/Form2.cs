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
        MySqlConnection conn = DBUtils.GetDBConnection();
        MySqlConnection conn8 = DBUtils.GetDBConnection();

        public Form2()
        {
            InitializeComponent();
            
            MySqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();
            dataFetch();
            
            
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
            MySqlCommand myCommand = conn.CreateCommand();
            myCommand.CommandText = "SELECT test.testcol FROM test WHERE test.idtest = 1";
            MySqlCommand myCommand8 = conn8.CreateCommand();
            myCommand8.CommandText = "SELECT test.testcol FROM test WHERE test.idtest = 2";
            try
            {
                conn.Open();
                conn8.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            MySqlDataReader reader = myCommand.ExecuteReader();
            MySqlDataReader reader8 = myCommand8.ExecuteReader();
            reader8.Read();
            reader.Read();
            label7.Text = reader["testcol"].ToString();
            label8.Text = reader8["testcol"].ToString();
            /*while (reader.Read())
            {
                label7.Text = reader["testcol"].ToString();
                label8.Text = reader8["testcol"].ToString();

            }*/

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
