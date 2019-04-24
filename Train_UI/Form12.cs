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
    public partial class Form12 : Form
    {
        DBConnect trainconnect = new DBConnect();
        public Form12()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string hub_id = textBox1.Text;
            trainconnect.Insert("hub", "hub_id", hub_id);
            MessageBox.Show("Success!");
        }
    }
}
