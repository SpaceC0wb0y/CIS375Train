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
    public partial class Form8 : Form
    {
        DBConnect trainconnect = new DBConnect();
        public Form8()
        {
            InitializeComponent();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stationID = textBox1.Text;
            string stationType = textBox2.Text;
            string rangeon = textBox3.Text;
            string rangeoff = textBox4.Text;
            string ticket_price = textBox5.Text;
            string send = stationID + "," + stationType + "," + rangeon + "," + rangeoff + "," + ticket_price;
            trainconnect.Insert("station", "station_id,station_type,range_on,range_off,ticket_price", send);
        }
    }
}
