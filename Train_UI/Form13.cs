using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Train_UI
{
    public partial class Form13 : Form
    {
        DBConnect trainconnect = new DBConnect();

        public Form13()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string train_id = textBox1.Text;
                string hub_id = textBox2.Text;
                string train_type = listBox1.Text;
                string send = train_id + "," + hub_id;
                if (train_type == "P")
                {
                    trainconnect.Insert("train", "train_id,hub_id", send);
                    trainconnect.Insert("passenger_train", "train_id", train_id);
                    MessageBox.Show("Success!");
                }
                else if ( train_type == "F")
                {
                    trainconnect.Insert("train", "train_id,hub_id", send);
                    trainconnect.Insert("freight_train", "train_id", train_id);
                    MessageBox.Show("Success!");
                }
          
                
                
                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form13_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
