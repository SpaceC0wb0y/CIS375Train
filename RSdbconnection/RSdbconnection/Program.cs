using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace RSdbconnection
{
     class DBConnect
     {
          private MySqlConnection connection;
          private string server;
          private string database;
          private string uid;
          private string password;
          private string port;

          //Constructor
          public DBConnect()
          {
               Initialize();
          }

          //Initialize values
          private void Initialize()
          {
               server = "rsinstance.cfwuqmgvld6f.us-east-1.rds.amazonaws.com";
               port = "3306";
               database = "RSdatabase";
               uid = "cis375";
               password = "railwaysystem";
               string connectionString;
               connectionString = "SERVER=" + server + ";" + "PORT=" + port + ";" + "DATABASE=" +
                 database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

               connection = new MySqlConnection(connectionString);
          }

          //open connection to database
          private bool OpenConnection()
          {
               try
               {
                    connection.Open();
                    return true;
               }
               catch (MySqlException ex)
               {
                    //When handling errors, you can your application's response based 
                    //on the error number.
                    //The two most common error numbers when connecting are as follows:
                    //0: Cannot connect to server.
                    //1045: Invalid user name and/or password.
                    switch (ex.Number)
                    {
                         case 0:
                              MessageBox.Show("Cannot connect to server.  Contact administrator");
                              break;

                              //John is GOAT

                         case 1045:
                              MessageBox.Show("Invalid username/password, please try again");
                              break;
                    }
                    return false;
               }
          }

          //Close connection
          private bool CloseConnection()
          {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
          }

          //Insert statement
          // specify attributes in this format "(attribute1, attribute2, ..)"
          // specify values in this format "('value1', 'value2'..)
          public void Insert(string query)
          {
               //open connection
               if (this.OpenConnection() == true)
               {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    this.CloseConnection();
               }
          }


           public void Delete(string query)
           {
               if (this.OpenConnection() == true)
               {
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    this.CloseConnection();
               }
           }

          public void Update(string query)
          { 

               //Open connection
               if (this.OpenConnection() == true)
               {
                    //create mysql command
                    MySqlCommand cmd = new MySqlCommand();
                    //Assign the query using CommandText
                    cmd.CommandText = query;
                    //Assign the connection using Connection
                    cmd.Connection = connection;

                    //Execute query
                    cmd.ExecuteNonQuery();
           
                    //close connection
                    this.CloseConnection();
               }
          }
          //Specify Attributes again, comma seperated
          public List<string>[] Select(string query, string specifyAttr)
          {
               //Create a list to store the result
               string[] splitAttr= specifyAttr.Split(',');

               List<string>[] list = new List<string>[splitAttr.Length];

               for(int i = 0; i < list.Length; i++)
               {
                    list[i] = new List<string>();
               }


               //Open connection
               if (this.OpenConnection() == true)
               {
                    //Create Command
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    //Create a data reader and Execute the command
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    //Read the data and store them in the list
                    while (dataReader.Read())
                    {
                         for (int i = 0; i < list.Length; i++)
                         {
                              string x = splitAttr[i];
                              list[i].Add(dataReader[x]);
                         }
                    }

                    //close Data Reader
                    dataReader.Close();

                    //close Connection
                    this.CloseConnection();

                    //return list to be displayed
                    return list;
               }
               else
               {
                    return list;
               }
          }

     }

}
