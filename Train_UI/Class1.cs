﻿using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Train_UI
{
     public class DBConnect
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
               if (OpenConnection())
               {
                    Console.WriteLine("Connection to DB successful...");
               }
               else
               {
                    Console.WriteLine("Something went wrong while connecting to DB..");
               }

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
          public bool OpenConnection()
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



                         case 1045:
                              MessageBox.Show("Invalid username/password, please try again");
                              break;
                    }
                    return false;
               }
          }

          //Close connection
          public bool CloseConnection()
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
          //Pre: Must pass comma seperated database variables, and also corresponding comma seperated databaseValues
          //Post: Inserts a new record into the database
          //Example of Usage: Insert("track", "track_id, coming_from, going_to", "EDGE1, STATION1, STATION2");
          public void Insert(string tableName, string DBvariables, string DBvalues)
          {
               DBvalues = "'" + DBvalues.Replace(",", "','") + "'";
               string query = "Insert INTO " + tableName + "(" + DBvariables + ")" + " VALUES(" + DBvalues + ");";
               Console.Write(query);

               //create command and assign the query and connection from the constructor
               MySqlCommand cmd = new MySqlCommand(query, connection);

               //Execute command
               cmd.ExecuteNonQuery();
          }

          //Pre: Must pass a delete query through this function with appropriate syntax based on mySQL
          //Post: deletes the record of a given condition specified in query
          public void Delete(string query)
          {
               MySqlCommand cmd = new MySqlCommand(query, connection);
               cmd.ExecuteNonQuery();
          }
          //Pre: Must pass an update query through this function with appropriate syntax based on mySQL
          //Post: Updates the record of a given condition specified in query
          public void Update(string query)
          {
               //create mysql command
               MySqlCommand cmd = new MySqlCommand();
               //Assign the query using CommandText
               cmd.CommandText = query;
               //Assign the connection using Connection
               cmd.Connection = connection;

               //Execute query
               cmd.ExecuteNonQuery();
          }
          //Pre: Must pass a select query through this function with appropriate syntax based on mySQL
          //Post: Prints the records fetched on console & stores data into list of lists
          public List<List<string>> Select(string query)
          {
               List<List<string>> queryStorage = new List<List<string>>();

               //Create Command
               MySqlCommand cmd = new MySqlCommand(query, connection);
               //Create a data reader and Execute the command
               MySqlDataReader dataReader = cmd.ExecuteReader();

               //Count number of fields in retreived from the table & PRINT them on console
               int fieldCount = dataReader.FieldCount;
               for (int i = 0; i < fieldCount; i++)
               {
                    Console.Write(dataReader.GetName(i).PadRight(15));
               }
               Console.WriteLine();

               //Read records fetched from database & store them in list of lists.
               while (dataReader.Read())
               {
                    List<string> temp = new List<string>();
                    for (int i = 0; i < fieldCount; i++)
                    {
                         Console.Write(dataReader[i].ToString().PadRight(15));
                         temp.Add(dataReader[i].ToString()); //Add all fields of query into inner list
                    }
                    queryStorage.Add(temp); //Store the record with fields(stored in temp) into outer list
                    Console.WriteLine(); //End line -> new record
               }
               dataReader.Close();
               return queryStorage; //Filled with query records
          }
          public MySqlDataReader SelectDataReader(string query)
          {
            bool isClosed = this.CloseConnection();
               if(isClosed == true)
            {
                this.OpenConnection();
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                return dataReader;
            }
            else
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                return dataReader;
            }
             
              /* MySqlCommand cmd = new MySqlCommand(query, connection);
               //Create a data reader and Execute the command
               MySqlDataReader dataReader = cmd.ExecuteReader();

               return dataReader;*/
          }
          public DataTable SelectDataTable(string query)
          {
               DataTable dtX = new DataTable();

               MySqlCommand cmd = new MySqlCommand(query, connection);
               //Create a data reader and Execute the command
               MySqlDataReader dataReader = cmd.ExecuteReader();

               dtX.Load(dataReader);

               return dtX;
          }
          //Count statement
          public int Count(string query)
          {
               int Count = -1;
               //Create Mysql Command
               MySqlCommand cmd = new MySqlCommand(query, connection);

               //ExecuteScalar will return one value
               Count = int.Parse(cmd.ExecuteScalar() + "");
               
               return Count;
          }

     }
}