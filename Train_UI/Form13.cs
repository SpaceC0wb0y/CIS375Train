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
                string send = train_id + "," + hub_id;
                trainconnect.Insert("train", "train_id,hub_id", send);
                MessageBox.Show("Success!");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
