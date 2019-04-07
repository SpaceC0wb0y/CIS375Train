using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
   Description: This Graph class found below will be used to generate the railway map that the program
   will run the railway simulation on. For this project, the map will be an undirected, 
   weighted graph that will utilize an adjacency list implementation, centered around 
   the edges involved. Temporary Vertex and Edge classes are used for testing purposes
   but final class will rely on seperate Vertex classes (Stations and Hubs) and a seperate 
   Edge class (Track). The terms edge and track are interchangeable in this program. This
   graph follows an adjacency list structure using a Dictionary object. The key value will be 
   the source vertex and the value variable will be a linked list based on an internal Node 
   class which will hold the adjacency list for each vertex (the vertices and edges that are 
   adjacent and incident respectively to the source vertex (again, a station or hub). All methods
   defined in the algorithm will be implemented here as well as any auxiliary methods that need
   to be made that were not listed (i.e. getters, setters, etc.).
*/
namespace Graph
{
    //Description: Holds the graph implemented via an adjacency list.
    //each element in the adjacency list will represent a single vertex
    //(a station or hub). Each vertex will have its own linked list storing info
    //relating to all adjacent vertices and the edges that connect them.

    /***************GRAPH NOT FULLY TESTED*************/
    public class Graph
    {
        //dictonary that stores each vertex's adjacency list
        private Dictionary<Vertex, LinkedList<Node>> adjacencyList;  
        //list of all vertices currently in graph
        private List<Vertex> vertices;
        //list of all edges currently in graph
        private List<Edge> edges;

        //Description: Constructor method for Graph class
        //Pre-Condition: None
        //Post-Condition: New, empty graph will be instantiated
        public Graph()
        {
            //creates objects for all three fields
            adjacencyList = new Dictionary<Vertex, LinkedList<Node>>();
            vertices = new List<Vertex>();
            edges = new List<Edge>();
        }

        //Description: Method adds a vertex to the adjacency list graph
        //Pre-Condition: Vertex object is already created beforehand
        //Post-Condition: New, empty adjacency list in the graph dictionary is added for the vertex
        //and a boolean is returned to indicate success or fail
        public bool AddVertex(Vertex V)
        {
            //checks if vertex is already in graph (no duplicates allowed)
            if (adjacencyList.ContainsKey(V)) 
            {
                return false;  //fail
            }
            //vertex is original, will add to graph
            else
            {
                adjacencyList.Add(V, new LinkedList<Node>());
                vertices.Add(V);  //adds vertex to vertices collection
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

        public List<Vertex> GetVertices()
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

            //checks if source vertex exists in graph
            if (!adjacencyList.ContainsKey(E.GetSource()))
            {
                return false; //it isn't
            }
            //checks if destination vertex exists in graph
            if (!adjacencyList.ContainsKey(E.GetDest()))
            {
                return false;  //it isn't
            }

            //checks if specific edge already exists in graph
            if (edges.Contains(E))
            {
                return false;
            }

            //traverses the adjacency linked list for that particular vertex
            LinkedList<Node> iterator = adjacencyList[E.GetSource()];

            foreach(var item in iterator)
            {
                //finds which node in the list has the adjacent vertex
                if (E.GetDest() == item.GetVertex())
                {
                    adjacentNodeFound = true;
                    item.AddIncidentEdge(E);  //adds the edge to the incident edge list for that particular node
                }
            }

            //this is the first edge connecting two vertices
            if (!adjacentNodeFound)
            {
                //creates a new element in source vertex's adjacency list
                adjacencyList[E.GetSource()].AddLast(new Node(E.GetDest(), E));
                Console.WriteLine("A new node is supposed to be made");
            }

            //traverses the adjacency linked list for the other vertex connected by the edge
            iterator = adjacencyList[E.GetDest()];
            adjacentNodeFound = false;   //resets boolean value

            foreach (var item in iterator)
            {
                if (E.GetSource() == item.GetVertex())
                {
                    adjacentNodeFound = true;
                    item.AddIncidentEdge(E);  //adds the edge to the incident edge list for that particular node
                }
            }

            if (!adjacentNodeFound)
            {
                //creates a new element in destination vertex's adjacency list
                adjacencyList[E.GetDest()].AddLast(new Node(E.GetSource(), E));
            }

            edges.Add(E);  //adds edge to edge list field
            return true;  //success
        } 

        //Description: Method removes the vertex from graph and from all adjacency lists that it appears in
        //Pre-Condition: None
        //Post-Condition: The adjacency list for V is removed and it's removed from all lists that it appears in
        public bool RemoveVertex(Vertex V)
        {
            //checks if vertex exists in graph
            if (!adjacencyList.ContainsKey(V))
            {
                return false; //it doesn't
            }

            //traverses the adjacency list for V
            LinkedList<Node> iterator = adjacencyList[V];
            //checks each node in the linked list of V to find all adjacent vertices
            foreach(var item in iterator)
            {
                Vertex temp = item.GetVertex();  //checks which vertex it is adjacent to
                LinkedList<Node> subIterator = adjacencyList[temp];  //goes to that adjacent vertex's adjacency list
                //traverses the adjacent list to find which node holds V
                foreach(var node in subIterator)
                {
                    //checks if V is in the current node
                    if (node.GetVertex() == V)
                    {
                        //gets all incident edges between V and the current vertex
                        List<Edge> removal = node.GetIncidentEdges();
                        foreach(var tempEdge in removal)
                        {
                            edges.Remove(tempEdge);  //removes all of the incident edges from the edge list
                        }
                    
                        subIterator.Remove(node);  //removes V from the other vertex's adjacency list entirely
                        break;
                    }
                }
            }

            adjacencyList.Remove(V);  //removes V's adjacency list from dictionary
            vertices.Remove(V); //removes V from the vertex list

            return true;  //success
        }

        //Description: Method removes and edge E wherever it is in the entire graph and all adjacency lists
        //Pre-Condition: None
        //Post-Condition: Removes edge from all places in the adjacency list where it appears
        public bool RemoveEdge(Edge E)
        {
            Vertex tempSource = E.GetSource();  //stores the source vertex
            Vertex tempDest = E.GetDest();  //stores destination vertex

            //checks if both vertices are in the graph
            if (!adjacencyList.ContainsKey(tempSource) || !adjacencyList.ContainsKey(tempDest))
            {
                return false; //one or both are missing
            }

            //iterates through source vertex's adjacency list
            LinkedList<Node> iterator = adjacencyList[tempSource];
            foreach(var node in iterator)
            {
                //checks each node in linked list until it finds the one
                //with the destination vertex
                if (node.GetVertex() == tempDest)
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

            //iterates through the destination vertex's adjacency list and does the same thing
            iterator = adjacencyList[tempDest];
            foreach(var node in iterator)
            {
                if (node.GetVertex() == tempSource)
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

        //Description: Returns the adjacency list for the specified vertex if it exists in the graph
        //Pre-Condition: None
        //Post-Condition: Returns the adjacency list of ther vertex or throws exception if vertex is
        //not in the graph
        public LinkedList<Node> GetList(Vertex V)
        {
            if (!adjacencyList.ContainsKey(V))
            {
                throw new Exception("Vertex is not in graph");
            }
            else
            {
                return adjacencyList[V];
            }
          
        }

        //Description: Checks graph (vertex collection of graph) for the vertex based on vertex ID
        //Pre-Condition: None
        //Post-Condition: Returns the vertex if found, throws exception if not found
        public Vertex FindVertex(string name)
        {
            foreach(var item in vertices)
            {
                if (name == item.GetID())
                {
                    return item;
                }
            }

            throw new Exception("Vertex not found in graph");
        }

        //Description: Checks graph (edge collection of graph) for the edge based on edge ID
        //Pre-Condition: None
        //Post-Condition: Returns the edge if found, throws exception if not found
        public Edge FindEdge(Vertex startVertex, Vertex endVertex, string name)
        {
            foreach(var item in edges)
            {
                if (startVertex == item.GetSource())
                {
                    if (endVertex == item.GetDest())
                    {
                        if (name == item.GetID())
                        {
                            return item;
                        }
                    }
                }

                if (endVertex == item.GetSource())
                {
                    if (startVertex == item.GetDest())
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
        public bool IsAdjacent(Vertex source, Vertex dest)
        {
            bool isAdjacentInSource = false;  //is adjacency proven in source vertex's adjacency list?
            bool isAdjacentInDest = false;  //is adjacency proven in destination vertex's adjacency list?

            if(!adjacencyList.ContainsKey(source))
            {
                return false;
            }
            if (!adjacencyList.ContainsKey(dest))
            {
                return false;
            }

            //an iterator to traverse the adjacency list for the source vertex
            LinkedList<Node> iterator = adjacencyList[source];

            //traverses the linked list to check each node for the destination vertex
            foreach(var item in iterator)
            {
                //checks if the destination vertex is found in the current node 
                //of adjacency list
                if (dest == item.GetVertex())
                {
                    isAdjacentInSource = true;  //success, vertices are adjacent in one list
                }
            }

            //an iterator to traverse the adjacency list for the destination vertex
            iterator = adjacencyList[dest];

            //traverses the linked list to check each node for the source vertex
            foreach (var item in iterator)
            {
                //checks if the source vertex is found in the current node 
                //of adjacency list
                if (source == item.GetVertex())
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
    //the node stores the adjacent vertex to the source vertex and all edges that connects the two vertices
    //as well as accessors for them
    public class Node
    {
        private Vertex V;  //vertex at the other end of the edge
        private List<Edge> incidentEdges;   //list of all edges between the source and destination vertex

        //Description: Constructor method for Node
        //Pre-Condition: Vertex must exist and be incident to source vertex
        //Post-Condition: New Node in adjacency list for a vertex is made
        public Node(Vertex V, Edge E)
        {
            this.V = V;
            incidentEdges = new List<Edge>();
            incidentEdges.Add(E);
        }

        //Description: Getter for the vertex field
        //Pre-Condition: None
        //Post-Condition: Returns the adjacent vertex
        public Vertex GetVertex()
        {
            return V;
        }

        //Description: Creates a list of all incident edges between two vertices
        //Pre-Condition: None
        //Post-Condition: Returns a list of all adjacent edges
        public List<Edge> GetIncidentEdges()
        {
            var edgeList = new List<Edge>();

            foreach(var item in incidentEdges)
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

        //Description: Prints out the vertex and edge list for the current adjacent vertex
        //Pre-Condition: None
        //Post-Condition: Prints out the Vertex info and all stored edge info line by line
        public void PrintContents()
        {
            Console.WriteLine("Node:");
            Console.WriteLine(V);  //prints vertex info
            foreach (var item in incidentEdges)  //iterates through edge list
            {
                Console.WriteLine(item);  //prints info for each incident edge
            }
        }
    }

    //Description: Temporary Vertex class that will represent the stations
    //and hubs.
    public class Vertex
    {
        private string ID;  //name/ID of vertex

        //Description: Constructor method
        //Pre-Condition: None
        //Post-Condition: New vertex object made and name field initialized
        public Vertex(string name)
        {
            ID = name;
        }

        //Description: Getter method of ID field
        //Pre-Condition: None
        //Post-Condition: Returns the value of ID
        public string GetID()
        {
            return ID;
        }

        //Description: Method to replace object name when it is used as a string
        //Pre-Condition: None
        //Post-Condition: String to be used when object reference name is used in an output statement
        public override string ToString()
        {
            return "Vertex Name: " + ID;
        }
    }

    //Description: Temporary edge class that theorietically represents a train track.
    public class Edge
    {
        int weight;  //weight of an track
        string edgeID;  //name/ID of track
        Vertex source;  //source vertex that track spawns from (one end)
        Vertex dest;    ////destination vertex that track goes to (one end)

        //Description: Edge constructor that intializes the fields of a track
        //Pre-Condition: Both source and destination vertex must exist in graph
        //Post-Condition: New edge added to track
        public Edge(string name, Vertex source, Vertex dest, int weight)
        {
            edgeID = name;
            this.weight = weight;

            this.source = source;
            this.dest = dest;
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

        //Description: Getter for source vertex
        //Pre-Condition: None
        //Post-Condition: Returns the source vertex object
        public Vertex GetSource()
        {
            return source;
        }

        //Description: Getter for destination vertex
        //Pre-Condition: None
        //Post-Condition: Returns the destination vertex object
        public Vertex GetDest()
        {
            return dest;
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
