﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Route;
using Components;

/*
   Description: This Graph class found below will be used to generate the railway map that the program
   will run the railway simulation on. For this project, the map will be an undirected, 
   weighted graph that will utilize an adjacency list implementation, centered around 
   the edges involved. Temporary Station and Edge classes are used for testing purposes
   but final class will rely on seperate Station classes (Stations and Hubs) and a seperate 
   Edge class (Track). The terms edge and track are interchangeable in this program. This
   graph follows an adjacency list structure using a Dictionary object. The key value will be 
   the source Station and the value variable will be a linked list based on an internal Node 
   class which will hold the adjacency list for each Station (the vertices and edges that are 
   adjacent and incident respectively to the source Station (again, a station or hub). All methods
   defined in the algorithm will be implemented here as well as any auxiliary methods that need
   to be made that were not listed (i.e. getters, setters, etc.).
*/
namespace Graph
{
    //Description: Holds the graph implemented via an adjacency list.
    //each element in the adjacency list will represent a single Station
    //(a station or hub). Each Station will have its own linked list storing info
    //relating to all adjacent vertices and the edges that connect them.

    /***************GRAPH NOT FULLY TESTED*************/


    public class Graphy
    {
        //dictonary that stores each Station's adjacency list
        private Dictionary<Station, LinkedList<Node>> adjacencyList;
        //list of all vertices currently in graph
        private List<Station> vertices;
        //list of all edges currently in graph
        private List<Edge> edges;
        //list of all trains currently in graph
        private List<Train> trains;

        //Description: Constructor method for Graph class
        //Pre-Condition: None
        //Post-Condition: New, empty graph will be instantiated
        public Graphy()
        {
            //creates objects for all three fields
            adjacencyList = new Dictionary<Station, LinkedList<Node>>();
            vertices = new List<Station>();
            edges = new List<Edge>();
            trains = new List<Train>();
        }

        //////////////////////////////////////Justin's Algrithm In Tas's Code/////////////////////////////////////////////////////////////////
        //Descrtiption: Method that traverses the graph to find the shortest distance between two Stationes by traversing edges
        //Pre-Condition: Both Stationes must exist on the graph
        //Post-Condition: Returns the shortest distance between the two Stationes and the path they took

        public class ShortestPath
        {
            public Station Station;
            public int shortestDistance;
            public Station previousStation;

        }

        public bool AddTrain(Train T)
        {
            //checks if Station is already in list of trains (no duplicates allowed)
            if (trains.Contains(T))
            {
                return false;  //fail
            }
            //train is original, will add to list of trains
            else
            {
                trains.Add(T);
                return true;  //success
            }
        }

        //Description: Traverses the adjacency list produced in map generation to find the shortest path from two verticies
        //Pre-Condition: Both Verticies exist on the map, and there is at least one edge between them
        //Post-Condition: The a list of verticies that contains the shortest path as well as the distance of the path is returneds
        public List<Station> TrainRoute(Graphy graph, Station source, Station dest, Train tran, out int distance)
        {
            int infinity = 1000000;
            List<Station> unvisitedNodes = new List<Station>();
            unvisitedNodes = graph.GetVertices();
            List<Station> visitedNodes = new List<Station>();
            Station currentStation = source;
            LinkedList<Node> neighborUnvisitedNodes = new LinkedList<Node>();
            distance = 1000000;

            List<ShortestPath> shortestPaths = new List<ShortestPath>();

            foreach (var item in unvisitedNodes)
            {
                if (item == currentStation)
                {
                    shortestPaths.Add(new ShortestPath()
                    {
                        Station = currentStation,
                        shortestDistance = 0,
                        previousStation = null,
                    });
                }
                else
                {
                    shortestPaths.Add(new ShortestPath()
                    {
                        Station = item,
                        shortestDistance = infinity,
                        previousStation = null
                    });
                }
            }

            while (unvisitedNodes.Count > 0)
            {
                //visit the unvisited Station with the smallest known distance from the start Station
                currentStation = shortestPaths.Where(x => !visitedNodes.Contains(x.Station)).OrderBy(x => x.shortestDistance).First().Station;

                //for the current Station, examine its unvisited neighbours
                neighborUnvisitedNodes = graph.GetList(currentStation);

                //for the current Station, calculate the distance of each neighbour form the start Station
                foreach (Node items in neighborUnvisitedNodes.Where(x => !visitedNodes.Contains(x.GetStation())))
                {
                    int shortestEdge = 1000000;

                    // find the shortest edge between two verticies and use that to calculate shortest path with
                    for (int edgeNum = 0; edgeNum < items.GetIncidentEdges().Count(); edgeNum++)
                    {

                        if (items.GetIncidentEdges()[edgeNum].GetWeight() < shortestEdge)
                        {
                            //check if the edge is not occupied before assigning it to shortest edge
                            if (items.GetIncidentEdges()[edgeNum].isAvailable)
                            {
                                shortestEdge = items.GetIncidentEdges()[edgeNum].GetWeight();
                            }
                            else
                            {
                                Console.WriteLine("Collision Imminent! at edge " + items.GetIncidentEdges()[edgeNum].ToString());
                                items.GetIncidentEdges()[edgeNum].IncrementNumDetectedCollisions();
                                //call rerouting method in some way when using RunRoute
                            }

                        }
                        // if you want to see the ID of the shortest edge between the verticies
                        //Console.WriteLine("The Shortest Edge for " + currentStation.GetID() + " and " + items.GetStation().GetID() + " is " + items.GetIncidentEdges()[edgeNum].GetID());
                    }

                    // Tell the user if a route cannot be found between the two verticies i.e. shortestEdge does not change
                    if (shortestEdge >= 1000000)
                    {
                        Console.WriteLine("CRITICAL ERROR!: NO EDGES FOUND ROUTE IMPOSSIBLE");

                    }

                    distance = shortestPaths.Where(x => x.Station == currentStation).First().shortestDistance + shortestEdge;
                    //items.GetIncidentEdges()[0].GetWeight(); 
                    //if the calculated distance of a Station is less than the known distance, update the shortest distance
                    if (shortestPaths.Where(x => x.Station == items.GetStation()).First().shortestDistance > distance)
                    {
                        //update the previous Station for each of the updated distances
                        shortestPaths.Where(x => x.Station == items.GetStation()).First().shortestDistance = distance;
                        shortestPaths.Where(x => x.Station == items.GetStation()).First().previousStation = currentStation;
                    }
                }

                unvisitedNodes.Remove(currentStation);
                visitedNodes.Add(currentStation);
            }

            // put the visited nodes back into the graph
            graph.vertices = visitedNodes;

            //display the shortest path between two verticies
            Console.WriteLine("shortest distance from " + source.GetID() + " to " + dest.GetID() + " is: " + shortestPaths.Where(x => x.Station == dest).First().shortestDistance);
            Console.WriteLine("the shortest path from " + source.GetID() + " to " + dest.GetID() + " is: ");


            ShortestPath currentRow = shortestPaths.Where(x => x.Station == dest).First();
            List<Station> sp = new List<Station>();

            sp.Add(dest);

            while (currentRow.previousStation != source)
            {
                sp.Add(currentRow.previousStation);
                currentRow = shortestPaths.Where(x => x.Station == currentRow.previousStation).First();
            }

            sp.Add(source);

            sp.Reverse();


            return sp;

            // set the path of the train when first running the algorithm, assumed every subsequent call is for moving the train object
            // !!!!!!!!TEST!!!!!!!!!!!
            //if (tran.GetPath().Count == 0)
            //{
            //    tran.SetPath(sp);

            //    tran.SetCurrentStation(tran.GetPath().First());
            //    tran.SetCurrentStation(tran.GetPath().First());
            //   tran.SetNextStation(tran.GetPath().ElementAt(1));
            //    tran.SetHomeHub(tran.GetPath().First());
            //}

            //foreach (Station v in sp)
            //{

            //  Console.WriteLine(v.GetID());
            //}

            //reset info for next run
        }

        //Description: Method adds a Station to the adjacency list graph
        //Pre-Condition: Station object is already created beforehand
        //Post-Condition: New, empty adjacency list in the graph dictionary is added for the Station
        //and a boolean is returned to indicate success or fail
        public bool AddStation(Station V)
        {
            //checks if Station is already in graph (no duplicates allowed)
            if (adjacencyList.ContainsKey(V))
            {
                return false;  //fail
            }
            //Station is original, will add to graph
            else
            {
                adjacencyList.Add(V, new LinkedList<Node>());
                vertices.Add(V);  //adds Station to vertices collection
                return true;  //success
            }
        }


        //Description: Method returns number of vertices in the graph
        //Pre-Condition: None
        //Post-Condition: Count of vertices returned
        public int NumVertices()
        {
            return vertices.Count;
        }

        //Description: Method returns number of edges in the graph
        //Pre-Condition: None
        //Post-Condition: Count of edges returned
        public int NumEdges()
        {
            return edges.Count;
        }

        public List<Station> GetVertices()
        {
            return vertices;
        }

        public List<Edge> GetEdges()
        {
            return edges;
        }

        //Description: Method adds a new edge to the graph by adding it to the adjacency
        //lists of both vertices that it connects.
        //Pre-Condition: Edge object is made beforehand
        //Post-Condition: Adds edge E to graph and edge collection
        public bool AddEdge(Edge E)
        {
            //this variable checks if the source and destination vertices
            //were already marked as adjacent in the graph, meaning an edge between 
            //them was already added therefore each of their respective adjacency lists
            //have a node dedicated to each other
            bool adjacentNodeFound = false;

            //checks if source Station exists in graph
            if (!adjacencyList.ContainsKey(E.GetSource()))
            {
                return false; //it isn't
            }
            //checks if destination Station exists in graph
            if (!adjacencyList.ContainsKey(E.GetDest()))
            {
                return false;  //it isn't
            }

            //checks if specific edge already exists in graph
            if (edges.Contains(E))
            {
                return false;
            }

            //traverses the adjacency linked list for that particular Station
            LinkedList<Node> iterator = adjacencyList[E.GetSource()];

            foreach (var item in iterator)
            {
                //finds which node in the list has the adjacent Station
                if (E.GetDest() == item.GetStation())
                {
                    adjacentNodeFound = true;
                    item.AddIncidentEdge(E);  //adds the edge to the incident edge list for that particular node
                }
            }

            //this is the first edge connecting two vertices
            if (!adjacentNodeFound)
            {
                //creates a new element in source Station's adjacency list
                adjacencyList[E.GetSource()].AddLast(new Node(E.GetDest(), E));
                Console.WriteLine("A new node is supposed to be made");
            }

            //traverses the adjacency linked list for the other Station connected by the edge
            iterator = adjacencyList[E.GetDest()];
            adjacentNodeFound = false;   //resets boolean value

            foreach (var item in iterator)
            {
                if (E.GetSource() == item.GetStation())
                {
                    adjacentNodeFound = true;
                    item.AddIncidentEdge(E);  //adds the edge to the incident edge list for that particular node
                }
            }

            if (!adjacentNodeFound)
            {
                //creates a new element in destination Station's adjacency list
                adjacencyList[E.GetDest()].AddLast(new Node(E.GetSource(), E));
            }

            edges.Add(E);  //adds edge to edge list field
            return true;  //success
        }

        //Description: Method removes the Station from graph and from all adjacency lists that it appears in
        //Pre-Condition: None
        //Post-Condition: The adjacency list for V is removed and it's removed from all lists that it appears in
        public bool RemoveStation(Station V)
        {
            //checks if Station exists in graph
            if (!adjacencyList.ContainsKey(V))
            {
                return false; //it doesn't
            }

            //traverses the adjacency list for V
            LinkedList<Node> iterator = adjacencyList[V];
            //checks each node in the linked list of V to find all adjacent vertices
            foreach (var item in iterator)
            {
                Station temp = item.GetStation();  //checks which Station it is adjacent to
                LinkedList<Node> subIterator = adjacencyList[temp];  //goes to that adjacent Station's adjacency list
                //traverses the adjacent list to find which node holds V
                foreach (var node in subIterator)
                {
                    //checks if V is in the current node
                    if (node.GetStation() == V)
                    {
                        //gets all incident edges between V and the current Station
                        List<Edge> removal = node.GetIncidentEdges();
                        foreach (var tempEdge in removal)
                        {
                            edges.Remove(tempEdge);  //removes all of the incident edges from the edge list
                        }

                        subIterator.Remove(node);  //removes V from the other Station's adjacency list entirely
                        break;
                    }
                }
            }

            adjacencyList.Remove(V);  //removes V's adjacency list from dictionary
            vertices.Remove(V); //removes V from the Station list

            return true;  //success
        }

        //Description: Method removes and edge E wherever it is in the entire graph and all adjacency lists
        //Pre-Condition: None
        //Post-Condition: Removes edge from all places in the adjacency list where it appears
        public bool RemoveEdge(Edge E)
        {
            Station tempSource = E.GetSource();  //stores the source Station
            Station tempDest = E.GetDest();  //stores destination Station

            //checks if both vertices are in the graph
            if (!adjacencyList.ContainsKey(tempSource) || !adjacencyList.ContainsKey(tempDest))
            {
                return false; //one or both are missing
            }

            //iterates through source Station's adjacency list
            LinkedList<Node> iterator = adjacencyList[tempSource];
            foreach (var node in iterator)
            {
                //checks each node in linked list until it finds the one
                //with the destination Station
                if (node.GetStation() == tempDest)
                {
                    //List<Edge> checkSum = node.GetIncidentEdges();
                    //foreach(var item in checkSum)
                    //{
                    //    Console.WriteLine(item);
                    //}
                    //removes the edge from the local edge list of that node
                    node.RemoveIncidentEdge(E);

                    //checks if the incident edge collection is now empty
                    List<Edge> checkSum = node.GetIncidentEdges();
                    if (checkSum.Count == 0)
                    {
                        adjacencyList[tempSource].Remove(node);
                    }
                    //Console.WriteLine(checkSum.Count);
                    break;
                }
            }

            //iterates through the destination Station's adjacency list and does the same thing
            iterator = adjacencyList[tempDest];
            foreach (var node in iterator)
            {
                if (node.GetStation() == tempSource)
                {
                    node.RemoveIncidentEdge(E);
                    //checks if the incident edge collection is now empty

                    List<Edge> checkSum = node.GetIncidentEdges();
                    //foreach(var item in checkSum)
                    //{
                    //    Console.WriteLine(item);
                    //}
                    if (checkSum.Count == 0)
                    {
                        adjacencyList[tempDest].Remove(node);
                    }
                    //Console.WriteLine(checkSum.Count);
                    break;
                }
            }

            edges.Remove(E);  //removes from master edge list

            return true;
        }

        //Description: Returns the adjacency list for the specified Station if it exists in the graph
        //Pre-Condition: None
        //Post-Condition: Returns the adjacency list of ther Station or throws exception if Station is
        //not in the graph
        public LinkedList<Node> GetList(Station V)
        {
            if (!adjacencyList.ContainsKey(V))
            {
                throw new Exception("Station is not in graph");
            }
            else
            {
                return adjacencyList[V];
            }

        }

        //Description: Checks graph (Station collection of graph) for the Station based on Station ID
        //Pre-Condition: None
        //Post-Condition: Returns the Station if found, throws exception if not found
        public Station FindStation(string name)
        {
            foreach (Station item in vertices)
            {
                if (name == item.GetID())
                {
                    return item;
                }
            }

            throw new Exception("Station not found in graph");
        }

        //Description: Checks graph (edge collection of graph) for the edge based on edge ID
        //Pre-Condition: None
        //Post-Condition: Returns the edge if found, throws exception if not found
        public Edge FindEdge(Station startStation, Station endStation, string name)
        {
            foreach (var item in edges)
            {
                if (startStation == item.GetSource())
                {
                    if (endStation == item.GetDest())
                    {
                        if (name == item.GetID())
                        {
                            return item;
                        }
                    }
                }

                if (endStation == item.GetSource())
                {
                    if (startStation == item.GetDest())
                    {
                        if (name == item.GetID())
                        {
                            return item;
                        }
                    }
                }
            }

            throw new Exception("Edge not found in graph");
        }

        //Description: Given two vertices, the method checks if they are adjacent in the graph
        //Pre-Condition: None
        //Post-Condition: Returns true if they are adjacent, false if not
        public bool IsAdjacent(Station source, Station dest)
        {
            bool isAdjacentInSource = false;  //is adjacency proven in source Station's adjacency list?
            bool isAdjacentInDest = false;  //is adjacency proven in destination Station's adjacency list?

            if (!adjacencyList.ContainsKey(source))
            {
                return false;
            }
            if (!adjacencyList.ContainsKey(dest))
            {
                return false;
            }

            //an iterator to traverse the adjacency list for the source Station
            LinkedList<Node> iterator = adjacencyList[source];

            //traverses the linked list to check each node for the destination Station
            foreach (var item in iterator)
            {
                //checks if the destination Station is found in the current node 
                //of adjacency list
                if (dest == item.GetStation())
                {
                    isAdjacentInSource = true;  //success, vertices are adjacent in one list
                }
            }

            //an iterator to traverse the adjacency list for the destination Station
            iterator = adjacencyList[dest];

            //traverses the linked list to check each node for the source Station
            foreach (var item in iterator)
            {
                //checks if the source Station is found in the current node 
                //of adjacency list
                if (source == item.GetStation())
                {
                    isAdjacentInDest = true;  //success, vertices are adjacent in one list
                }
            }

            //was adjacency shown for both adjacency lists
            if (isAdjacentInSource && isAdjacentInDest)
            {
                return true;  //vertices are adjacent 
            }
            else
            {
                return false;  //vertices are not adjacent
            }
        }
    }



    //Description: This class represents a single node within a single linked within the adjacency list
    //the node stores the adjacent Station to the source Station and all edges that connects the two vertices
    //as well as accessors for them
    public class Node
    {
        private Station V;
        private List<Edge> incidentEdges;

        //Description: Constructor method for Node
        //Pre-Condition: Station must exist and be incident to source Station
        //Post-Condition: New Node in adjacency list for a Station is made
        public Node(Station V, Edge E)
        {
            this.V = V;
            incidentEdges = new List<Edge>();
            incidentEdges.Add(E);
        }

        //Description: Getter for the Station field
        //Pre-Condition: None
        //Post-Condition: Returns the adjacent Station
        public Station GetStation()
        {
            return V;
        }

        //Description: Creates a list of all incident edges between two vertices
        //Pre-Condition: None
        //Post-Condition: Returns a list of all adjacent edges
        public List<Edge> GetIncidentEdges()
        {
            var edgeList = new List<Edge>();

            foreach (var item in incidentEdges)
            {
                edgeList.Add(item);
            }

            return edgeList;
        }

        //Description: Adds a new incident edge to the edge list
        //Pre-Condition: None
        //Post-Condition: List edges has a new edge appended to it
        public void AddIncidentEdge(Edge E)
        {
            incidentEdges.Add(E);
        }

        //Description: The method removes the edge from the incident edge collection
        //Pre-Condition: The edge exists within the collection
        //Post-Condition: The edge E is removed from teh incident edge collection
        public void RemoveIncidentEdge(Edge E)
        {
            incidentEdges.Remove(E);
        }

        //Description: Prints out the Station and edge list for the current adjacent Station
        //Pre-Condition: None
        //Post-Condition: Prints out the Station info and all stored edge info line by line
        public void PrintContents()
        {
            Console.WriteLine("Node:");
            Console.WriteLine(V);  //prints Station info
            foreach (var item in incidentEdges)  //iterates through edge list
            {
                Console.WriteLine(item);  //prints info for each incident edge
            }
        }
    }

     

    //Description: Temporary edge class that theorietically represents a train track.
    public class Edge
    {
        int weight;  //weight of an track
        string edgeID;  //name/ID of track
        Station source;  //source Station that track spawns from (one end)
        Station dest;    ////destination Station that track goes to (one end)
        public bool isAvailable;
        private int numDetectedCollisions;
        private int numTimesUsed;
        private int cost;

        //Description: Edge constructor that intializes the fields of a track
        //Pre-Condition: Both source and destination Station must exist in graph
        //Post-Condition: New edge added to track
        public Edge(string name, Station source, Station dest, int weight)
        {
            edgeID = name;
            this.weight = weight;

            this.source = source;
            this.dest = dest;
            cost = 1000000;
            numDetectedCollisions = 0;
            numTimesUsed = 0;
            isAvailable = true;
        }

        //Description: Returns the edge ID
        //Pre-Condition: None
        //Post-Condition: Returns ID as a string
        public string GetID()
        {
            return edgeID;
        }

        //Description: Return the edge weight
        //Pre-Condition: None
        //Post-Condition: Return edge weight as an int
        public int GetWeight()
        {
            return weight;
        }

        //Description: Getter for source Station
        //Pre-Condition: None
        //Post-Condition: Returns the source Station object
        public Station GetSource()
        {
            return source;
        }

        //Description: Getter for destination Station
        //Pre-Condition: None
        //Post-Condition: Returns the destination Station object
        public Station GetDest()
        {
            return dest;
        }

        //Description: Gets the number of collisions detected on an edge
        //Pre-Condition: None
        //Post-Condition: Returns number of detected collisions field
        public int GetNumCollisions()
        {
            return numDetectedCollisions;
        }

        //Description: Gets number of times a track is used by a train
        //Pre-Condition: None
        //Post-Condition: Returns the number of times used
        public int GetNumTimesUsed()
        {
            return numTimesUsed;
        }

        //Description: Changes whether or not a track is available
        //Pre-Condition: None
        //Post-Condition: Availability status is set
        public void SetAvailability(bool availability)
        {
            isAvailable = availability;
        }

        //Description: Gets the availability status of an edge
        //Pre-Condition: None
        //Post-Condition: Returns true if edge is available, false if it isn't
        public bool GetAvailability()
        {
            return isAvailable;
        }

        //Description: Gets track cost
        //Pre-Condition: None
        //Post-Condition: Returns cost field of track object
        public int GetCost()
        {
            return cost;
        }

        //Description: Adds number of times an edge is used by 1
        //Pre-Condition: None
        //Post-Condition: Adds 1 to numTimesUsed field
        public void IncrementNumTimesUsed()
        {
            numTimesUsed++;
        }

        public void IncrementNumDetectedCollisions()
        {
            numDetectedCollisions++;
        }

        //Description: Method to replace object name when it is used as a string
        //Pre-Condition: None
        //Post-Condition: String to be used when object reference name is used in an output statement
        public override string ToString()
        {
            return "Edge: " + edgeID + ", " + source.GetID() + "-" + dest.GetID() + ", Edge Weight: " + weight;
        }



    }

}

