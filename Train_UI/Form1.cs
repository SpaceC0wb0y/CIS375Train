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
using System.Data;



namespace Train_UI
{
    
    public partial class Form1 : Form
    {
        int runtime = 0;
        string passcap = "";
        DBConnect trainconnect = new DBConnect();
        DBConnect trainconnect2 = new DBConnect();
        public Form1()
        {
            InitializeComponent();

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

        private void button2_Click(object sender, EventArgs e)
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
                        //int i = 0;
                        int seqnum;
                        int hubnum = 0;
                        int stationnum = 0;
                        int edgenum = 0;
                        int locomotivenum = 0;
                        string table = string.Empty;


                        int j = 0;
                        do
                        {

                            line = stringread.ReadLine();
                            if (line != null)
                            {

                                storeline = line.Split(new char[] { ' ', '"', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (j == 0)
                                {
                                    var telaBuilder = new StringBuilder(storeline[0]);
                                    telaBuilder[0] = '0';
                                    string hold = telaBuilder.ToString();
                                    seqnum = Convert.ToInt32(hold);

                                    for (int i = 1; i < storeline.Length; i++)
                                    {
                                        if (storeline[i] != "")
                                        {
                                            //label2.Text += " " + storeline[i];
                                        }
                                        else
                                        {

                                        }
                                    }


                                }

                                else if (storeline.Length == 0)
                                {
                                    // Do Nothing
                                }

                                else if (storeline[0] == "C")
                                {
                                    if (storeline[1] != "" && storeline[1] == "HUB")
                                    {
                                        hubnum = Convert.ToInt32(storeline[2]);
                                        table = "hub";
                                        //label3.Text = table;

                                    }

                                    else if (storeline[1] != "" && storeline[1] == "STATION")
                                    {
                                        stationnum = Convert.ToInt32(storeline[2]);
                                        table = "station";

                                    }
                                    else if (storeline[1] != "" && storeline[1] == "EDGE")
                                    {
                                        edgenum = Convert.ToInt32(storeline[2]);
                                        table = "tracks";

                                    }
                                    else if (storeline[1] != "" && storeline[1] == "LOCOMOTIVE")
                                    {
                                        locomotivenum = Convert.ToInt32(storeline[2]);
                                        table = "train";

                                    }
                                }



                                else if (storeline[0] == "T")
                                {
                                    //label4.Text += " " + storeline[1] + " " + storeline[2];

                                }
                                else
                                {


                                    if (table == "hub")
                                    {
                                        //trainconnect.Insert("")
                                        //trainconnect.Insert("INSERT into " + table + "(hub_id) VALUES(\"" + storeline[0] + "\")");
                                        trainconnect.Insert(table,"hub_id",storeline[0]);
                                    }
                                    else if (table == "station")
                                    {

                                        int rangeon = Convert.ToInt32(storeline[4]) - Convert.ToInt32(storeline[3]);
                                        int rangeoff = Convert.ToInt32(storeline[6]) - Convert.ToInt32(storeline[5]);
                                        string ron = Convert.ToString(rangeon);
                                        string roff = Convert.ToString(rangeoff);
                                        //trainconnect.Insert("INSERT into " + table + "(station_id,station_type,range_on,range_off,ticket_price) VALUES(\"" + storeline[0] + "\" , \"" + storeline[1] + "\" , \"" + ron + "\" , \"" + roff + "\" , \"" + storeline[7] + "\")");
                                        
                                        string send = storeline[0] + "," + storeline[1] + "," + ron + "," + roff + "," + storeline[7];
                                        trainconnect.Insert(table, "station_id,station_type,range_on,range_off,ticket_price", send);
                                    }

                                    else if (table == "tracks")
                                    {
                                        string send = storeline[0] + "," + storeline[1] + "," + storeline[2];
                                        trainconnect.Insert(table, "coming_from,going_to,weight", send);
                                    }
                                    else if (table == "train")
                                    {
                                        string send = storeline[0] + "," + storeline[1];
                                        trainconnect.Insert(table, "train_id,hub_id", send);
                                        if (storeline[2] == "P")
                                        {
                                            send = storeline[0];
                                            table = "passenger_train";
                                            trainconnect.Insert(table, "train_id", send);

                                        }
                                        else if (storeline[2] == "F")
                                        {
                                            send = storeline[0];
                                            table = "freight_train";
                                            trainconnect.Insert(table, "train_id", send);
                                        }
                                        table = "train";
                                        
                                       
                                        
                                    }
                                    else 
                                    {
                                        // DO NOTHING
                                    }

                                }
                            }




                            else
                            {

                            }

                            ++j;

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

                        int seqnum;
                        int daynum = 0;
                        int freightnum = 0;
                        int passengernum = 0;
                        int routenum = 0;
                        int route_id_iterator = 0; //THIS IS WHAT I ADDED
                        string table = string.Empty;


                        int j = 0;
                        do
                        {

                            line = stringread.ReadLine();
                            if (line != null)
                            {

                                storeline = line.Split(new char[] { ' ', '"', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (j == 0)
                                {
                                    var telaBuilder = new StringBuilder(storeline[0]);
                                    telaBuilder[0] = '0';
                                    string hold = telaBuilder.ToString();
                                    seqnum = Convert.ToInt32(hold);

                                    for (int i = 1; i < storeline.Length; i++)
                                    {
                                        if (storeline[i] != "")
                                        {
                                            //label2.Text += " " + storeline[i];
                                        }
                                        else
                                        {

                                        }
                                    }


                                }

                                else if (storeline.Length == 0)
                                {
                                    // Do Nothing
                                }

                                else if(storeline[0] == "RUN")
                                {
                                    runtime = Convert.ToInt32(storeline[1]);
                                }

                                else if (storeline[1] == "P")
                                {
                                    passcap = storeline[5];
                                }

                                else if (storeline[0] == "C")
                                {
                                    if (storeline[1] != "" && storeline[1] == "FREIGHT")
                                    {
                                        freightnum = Convert.ToInt32(storeline[2]);
                                        table = "daily_f_routes";


                                    }

                                    else if (storeline[1] != "" && storeline[1] == "PASSENGER")
                                    {
                                        passengernum = Convert.ToInt32(storeline[2]);
                                        table = "daily_p_routes";

                                    }
                                    else if (storeline[1] != "" && storeline[1] == "DAY")
                                    {
                                        daynum = Convert.ToInt32(storeline[2]);
                                        

                                    }
                                    else if (storeline[1] != "" && storeline[1] == "ROUTE")
                                    {

                                        routenum = Convert.ToInt32(storeline[2]);
                                        if (table == "daily_p_routes")
                                        {
                                            route_id_iterator += 1;
                                            table = "daily_p_routes";
                                        }
                                        else if (table == "daily_f_routes")
                                        {
                                            table = "daily_f_routes";
                                        }

                                    }
                                }
                                else if (storeline[0] == "T")
                                {
                                    //label4.Text += " " + storeline[1] + " " + storeline[2];

                                }
                                else
                                {


                                    if (table == "daily_f_routes")
                                    {
                                        string day = Convert.ToString(daynum);
                                        string send = day + "," + storeline[0] + "," + storeline[1] + "," + storeline[2] + "," + storeline[3];
                                        trainconnect.Insert(table, "day,station1,station2,start_time,cargo_capacity", send);
                                    }
                                    else if (table == "daily_p_routes")
                                    {
                                        string day = Convert.ToString(daynum);
                                        string send = day + "," + route_id_iterator.ToString() + "," + storeline[0] + "," + storeline[1];
                                        trainconnect.Insert(table, "day_num,route_id,station,start_time", send);
                                    }





                                }

                            }




                            else
                            {

                            }

                            ++j;

                        } while (line != null);
                    }
                }
            }
        }

        

        private void button5_Click(object sender, EventArgs e) // Repeatable routes
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
                        
                        int seqnum;
                        int freightnum = 0;
                        int passengernum = 0;
                        int routenum = 0;
                        int route_id_iterator = 0; //THIS IS WHAT I ADDED
                        string table = string.Empty;

                              
                        int j = 0;
                        do
                        {

                            line = stringread.ReadLine();
                            if (line != null)
                            {

                                storeline = line.Split(new char[] { ' ', '"', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (j == 0)
                                {
                                    var telaBuilder = new StringBuilder(storeline[0]);
                                    telaBuilder[0] = '0';
                                    string hold = telaBuilder.ToString();
                                    seqnum = Convert.ToInt32(hold);

                                    for (int i = 1; i < storeline.Length; i++)
                                    {
                                        if (storeline[i] != "")
                                        {
                                            //label2.Text += " " + storeline[i];
                                        }
                                        else
                                        {

                                        }
                                    }


                                }

                                else if (storeline.Length == 0)
                                {
                                    // Do Nothing
                                }

                                else if (storeline[0] == "C")
                                {
                                    if (storeline[1] != "" && storeline[1] == "FREIGHT")
                                    {
                                        freightnum = Convert.ToInt32(storeline[2]);
                                        table = "repeatable_f_routes";


                                    }

                                    else if (storeline[1] != "" && storeline[1] == "PASSENGER")
                                    {
                                        passengernum = Convert.ToInt32(storeline[2]);
                                        table = "repeatable_p_routes";

                                    }
                                    else if (storeline[1] != "" && storeline[1] == "ROUTE")
                                    {

                                        routenum = Convert.ToInt32(storeline[2]);
                                        if (table == "repeatable_p_routes")
                                        {
                                            route_id_iterator += 1;
                                            table = "repeatable_p_routes";
                                        }
                                        else if (table == "repeatable_f_routes")
                                        {
                                            table = "repeatable_f_routes";
                                        }

                                    }



                              
                                    

                                }
                                else if (storeline[0] == "T")
                                {
                                    //label4.Text += " " + storeline[1] + " " + storeline[2];

                                }
                                else
                                {


                                    if (table == "repeatable_f_routes")
                                    {
                                        string send = storeline[0] + "," + storeline[1] + "," + storeline[3] + "," + storeline[4];
                                        trainconnect.Insert(table, "station1,station2,start_time,cargo_capacity", send);
                                    }
                                    else if (table == "repeatable_p_routes")
                                    {

                                        string send = route_id_iterator.ToString() + "," + storeline[0] + "," + storeline[1];
                                        trainconnect.Insert(table, "route_id,station,start_time", send);
                                    }





                                }

                            }




                            else
                            {

                            }

                            ++j;

                        } while (line != null);
                    } 
                    }
            }
        }

        private void button6_Click(object sender, EventArgs e) // Daily Routes
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

                        int seqnum;
                        int daynum = 0;
                        int freightnum = 0;
                        int passengernum = 0;
                        int routenum = 0;
                        int route_id_iterator = 0; //THIS IS WHAT I ADDED
                        string table = string.Empty;


                        int j = 0;
                        do
                        {

                            line = stringread.ReadLine();
                            if (line != null)
                            {

                                storeline = line.Split(new char[] { ' ', '"', ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (j == 0)
                                {
                                    var telaBuilder = new StringBuilder(storeline[0]);
                                    telaBuilder[0] = '0';
                                    string hold = telaBuilder.ToString();
                                    seqnum = Convert.ToInt32(hold);

                                    for (int i = 1; i < storeline.Length; i++)
                                    {
                                        if (storeline[i] != "")
                                        {
                                            //label2.Text += " " + storeline[i];
                                        }
                                        else
                                        {

                                        }
                                    }


                                }

                                else if (storeline.Length == 0)
                                {
                                    // Do Nothing
                                }

                                else if (storeline[0] == "C")
                                {
                                    if (storeline[1] != "" && storeline[1] == "FREIGHT")
                                    {
                                        freightnum = Convert.ToInt32(storeline[2]);
                                        table = "daily_f_routes";


                                    }

                                    else if (storeline[1] != "" && storeline[1] == "PASSENGER")
                                    {
                                        passengernum = Convert.ToInt32(storeline[2]);
                                        table = "daily_p_routes";

                                    }
                                    else if (storeline[1] != "" && storeline[1] == "DAY")
                                    {
                                        daynum = Convert.ToInt32(storeline[2]);
                                        

                                    }
                                    else if (storeline[1] != "" && storeline[1] == "ROUTE")
                                    {

                                        routenum = Convert.ToInt32(storeline[2]);
                                        if (table == "daily_p_routes")
                                        {
                                            route_id_iterator += 1;
                                            table = "daily_p_routes";
                                        }
                                        else if (table == "daily_f_routes")
                                        {
                                            table = "daily_f_routes";
                                        }

                                    }
                                }
                                else if (storeline[0] == "T")
                                {
                                    //label4.Text += " " + storeline[1] + " " + storeline[2];

                                }
                                else
                                {


                                    if (table == "daily_f_routes")
                                    {
                                        string day = Convert.ToString(daynum);
                                        string send = day + "," + storeline[0] + "," + storeline[1] + "," + storeline[2] + "," + storeline[3];
                                        trainconnect.Insert(table, "day,station1,station2,start_time,cargo_capacity", send);
                                    }
                                    else if (table == "daily_p_routes")
                                    {
                                        string day = Convert.ToString(daynum);
                                        string send = day + "," + route_id_iterator.ToString() + "," + storeline[0] + "," + storeline[1];
                                        trainconnect.Insert(table, "day,route_id,station,start_time", send);
                                    }





                                }

                            }




                            else
                            {

                            }

                            ++j;

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

        private void button12_Click(object sender, EventArgs e) // RESET
        {
            trainconnect.Delete("DELETE from station");
            trainconnect.Delete("DELETE from passenger_train");
            trainconnect.Delete("DELETE from freight_train");
            trainconnect.Delete("DELETE from daily_f_routes");
            trainconnect.Delete("DELETE from daily_p_routes");
            trainconnect.Delete("DELETE from repeatable_f_routes");
            trainconnect.Delete("DELETE from repeatable_p_routes");
            trainconnect.Delete("DELETE from train");
            trainconnect.Delete("DELETE from hub");
            
            trainconnect.Delete("DELETE from tracks");
            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Form4 fourthForm = new Form4();
            fourthForm.Show();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            Form5 fiveForm = new Form5();
            fiveForm.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Form6 sixthForm = new Form6();
            sixthForm.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Form7 seventhForm = new Form7();
            seventhForm.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Form8 EightForm = new Form8();
            EightForm.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form9 NineForm = new Form9();
            NineForm.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Form11 ElevenForm = new Form11();
            ElevenForm.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Form12 twelveForm = new Form12();
            twelveForm.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Form13 thirteenForm = new Form13();
            thirteenForm.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Form14 fourteenForm = new Form14();
            fourteenForm.Show();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Form16 sixteenForm = new Form16();
            sixteenForm.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Form15 fifteenForm = new Form15();
            fifteenForm.Show();
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Graph railWay = new Graph();
            DataTable tracks = trainconnect2.SelectDataTable("select track_id,coming_from,going_to from track");
             foreach (DataRow dr in tracks.Rows)
            {
                // MyFunction(dr["Id"].ToString(), dr["Name"].ToString());
                string coming_from = dr["coming_from"].ToString();
                string going_to = dr["going_to"].ToString();
                string station_type = "";
                FreightStation A;
                PassengerStation B;
                Hub C;
                Hub D;

                if (coming_from.Contains("STATION"))
                {
                    string x = coming_from;
                    DataTable station = trainconnect2.SelectDataTable("select station_type from station where station_id = \"" + x + "\"");
                    foreach (DataRow dt in station.Rows)
                    {
                        if (dt["station_type"].ToString().Contains("F"))
                        {
                            A = new FreightStation(x);
                        }
                        else if (dt["station_type"].ToString().Contains("P"))
                        {
                            DataTable pStation = trainconnect2.SelectDataTable("select ticket_price,range_on,range_off from station where station_id = \"" + x + "\"");

                            foreach (DataRow du in pStation.Rows)
                            {
                                B = new PassengerStation(x,Convert.ToInt32(du["ticket_price"]),Convert.ToInt32(du["range_on"]), Convert.ToInt32(du["range_on"]), Convert.ToInt32(du["range_off"]),Convert.ToInt32(du["range_off"]));
                                
                            }
                           
                        }
                    }
                }

                if (going_to.Contains("STATION"))
                {
                    string x = going_to;
                    DataTable station = trainconnect2.SelectDataTable("select station_type from station where station_id = \"" + x + "\"");
                    foreach (DataRow dt in station.Rows)
                    {
                        if (dt["station_type"].ToString().Contains("F"))
                        {
                            A = new FreightStation(x);
                        }
                        else if (dt["station_type"].ToString().Contains("P"))
                        {
                            DataTable pStation = trainconnect2.SelectDataTable("select ticket_price,range_on,range_off from station where station_id = \"" + x + "\"");

                            foreach (DataRow du in pStation.Rows)
                            {
                                B = new PassengerStation(x, Convert.ToInt32(du["ticket_price"]), Convert.ToInt32(du["range_on"]), Convert.ToInt32(du["range_on"]), Convert.ToInt32(du["range_off"]), Convert.ToInt32(du["range_off"]));

                            }

                        }
                    }
                }

                if (coming_from.Contains("HUB"))
                {
                    string x = coming_from;
                    C = new Hub(x);

                }

                if (going_to.Contains("HUB"))
                {
                    string x = going_to;
                    D = new Hub(x);
                }


            }

        }
    }
}
