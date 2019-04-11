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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DBConnect trainconnect = new DBConnect();
            
            
            
            


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 secondForm = new Form2();
            secondForm.Show();

        }

        public int Countline(string x)
        {
            int count = 0;
            int len = x.Length;
            for (int i = 0; i != len; ++i)
                switch (x[i])
                {
                    case '\r':
                        ++count;
                        if (i + 1 != len && x[i + 1] == '\n')
                            ++i;
                        break;
                    case '\n':
                        // Uncomment below to include all other line break sequences
                        // case '\u000A':
                        // case '\v':
                        // case '\f':
                        // case '\u0085':
                        // case '\u2028':
                        // case '\u2029':
                        ++count;
                        break;
                }
            return count;
        }

        private void button2_Click(object sender, EventArgs e) // Station
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    string content = reader.ReadToEnd(); // Reads whole file puts into string
                    using (StringReader stringread = new StringReader(content)) // parses string line by line
                    {
                        int numLine = Countline(content);
                        string line = string.Empty;
                        string[] storeline;
                        int i = 0;
                        int seqnum; //Sequence number stored here
                        do
                        {
                            line = reader.ReadLine();
                            if (line != null)
                            {
                                // do something with the line
                                storeline = line.Split(null);
                                if (i == 0)
                                {
                                    string store2 = storeline[0];
                                    store2 = store2.Replace("(H)", "0");
                                    seqnum = Convert.ToInt32(store2);

                                }
                                else if (i == numLine){
                                    //Trailer Info
                                }
                                else
                                {
                                    //trainconnect.Insert();
                                }
                                ++i;
                            }
                            

                        } while (line != null);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) // Maintenance
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    string content = reader.ReadToEnd(); // Reads whole file puts into string
                    using (StringReader stringread = new StringReader(content)) // parses string line by line
                    {
                        string line = string.Empty;
                        string[] storeline;
                        int i = 0;
                        int seqnum; //Sequence number stored here
                        do
                        {
                            line = reader.ReadLine();
                            if (line != null)
                            {
                                // do something with the line
                                storeline = line.Split(null);
                                if (i == 0)
                                {
                                    string store2 = storeline[0];
                                    store2 = store2.Replace("(H)", "0");
                                    seqnum = Convert.ToInt32(store2);

                                }
                                else
                                {
                                    //trainconnect.Insert();
                                }
                                ++i;
                            }

                        } while (line != null);
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e) // Configuration
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    string content = reader.ReadToEnd(); // Reads whole file puts into string
                    using (StringReader stringread = new StringReader(content)) // parses string line by line
                    {
                        string line = string.Empty;
                        string[] storeline;
                        int i = 0;
                        int seqnum; //Sequence number stored here
                        do
                        {
                            line = reader.ReadLine();
                            if (line != null)
                            {
                                // do something with the line
                                storeline = line.Split(null);
                                if (i == 0)
                                {
                                    string store2 = storeline[0];
                                    store2 = store2.Replace("(H)", "0");
                                    seqnum = Convert.ToInt32(store2);

                                }
                                else
                                {
                                    //trainconnect.Insert();
                                }
                                ++i;
                            }

                        } while (line != null);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e) // Repeatable Routes
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    string content = reader.ReadToEnd(); // Reads whole file puts into string
                    using (StringReader stringread = new StringReader(content)) // parses string line by line
                    {
                        string line = string.Empty;
                        string[] storeline;
                        int i = 0;
                        int seqnum; //Sequence number stored here
                        do
                        {
                            line = reader.ReadLine();
                            if (line != null)
                            {
                                // do something with the line
                                storeline = line.Split(null);
                                if (i == 0)
                                {
                                    string store2 = storeline[0];
                                    store2 = store2.Replace("(H)", "0");
                                    seqnum = Convert.ToInt32(store2);

                                }
                                else
                                {
                                    //trainconnect.Insert();
                                }
                                ++i;
                            }

                        } while (line != null);
                    }
                }
            }
        }

        private void button6_Click(object sender, EventArgs e) //Daily Routes
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Text files | *.txt"; // file types, that will be allowed to upload
            dialog.Multiselect = false; // allow/deny user to upload more than one file at a time
            if (dialog.ShowDialog() == DialogResult.OK) // if user clicked OK
            {
                String path = dialog.FileName; // get name of file
                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding())) // do anything you want, e.g. read it
                {
                    string content = reader.ReadToEnd(); // Reads whole file puts into string
                    using (StringReader stringread = new StringReader(content)) // parses string line by line
                    {
                        string line = string.Empty;
                        //int numLines = content.text.Split('\n').Length;
                        string[] storeline;
                        int i = 0;
                        int seqnum; //Sequence number stored here
                        do
                        {
                            line = reader.ReadLine();
                            if (line != null)
                            {
                                // do something with the line
                                storeline = line.Split(null);
                                if (i == 0)
                                {
                                    string store2 = storeline[0];
                                    store2 = store2.Replace("(H)", "0");
                                    seqnum = Convert.ToInt32(store2);

                                }
                               // else if() { } 
                                else
                                {
                                    //trainconnect.Insert();
                                }
                                ++i;
                            }

                        } while (line != null);
                    }
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form3 thirdForm = new Form3();
            thirdForm.Show();
        }
    }
}
