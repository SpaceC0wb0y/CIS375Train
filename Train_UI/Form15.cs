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
    public partial class Form15 : Form
    {
        DBConnect trainconnect = new DBConnect();
        public Form15()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string coming_from = textBox1.Text;
            string going_to = textBox2.Text;
            string weight = textBox3.Text;
            string send = coming_from + "," + going_to + "," + weight;
            try
            {
                trainconnect.Insert("tracks","coming_from,going_to,weight",send);
                MessageBox.Show("Success");
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
