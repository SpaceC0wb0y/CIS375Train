using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Routing_and_Maintenence
{
    //class Program
    //{
    //    public class Train
    //    {
    //        bool onTrack;
    //        bool onStation;
    //        bool onHub;
    //        int currentStation;
    //        int currentHub;
    //        char currentTrack;
    //        char trainName;
    //        int startHub;
    //        string trainType;
    //        string routeToRun;
    //        string routeToRepeat;
    //        int distanceOnTrack;
    //      public static int totalDistance = 0; // total distance the train travels
    //        int totalTrains = 0; // total number of trains, asssigned by the number of train names in the UserInfo trainName list
    //        int objNumber = 0; // the object number, used to keep track of the amount of objects created 
    //    }

        public class DijkstrasAlgorithm
        {

            private static readonly int NO_PARENT = -1;

            // Function that implements Dijkstra's  
            // single source shortest path  
            // algorithm for a graph represented  
            // using adjacency matrix  
            // representation  
            private static void dijkstra(int[,] adjacencyMatrix,
                                                int startVertex, int endVertex)
            {
                int nVertices = adjacencyMatrix.GetLength(0);

                // shortestDistances[i] will hold the  
                // shortest distance from src to i  
                int[] shortestDistances = new int[nVertices];

                // added[i] will true if vertex i is  
                // included / in shortest path tree  
                // or shortest distance from src to  
                // i is finalized  
                bool[] added = new bool[nVertices];

                // Initialize all distances as  
                // INFINITE and added[] as false  
                for (int vertexIndex = 0; vertexIndex < nVertices;
                                                    vertexIndex++)
                {
                    shortestDistances[vertexIndex] = int.MaxValue;
                    added[vertexIndex] = false;
                }

                // Distance of source vertex from  
                // itself is always 0  
                shortestDistances[startVertex] = 0;

                // Parent array to store shortest  
                // path tree  
                int[] parents = new int[nVertices];

                // The starting vertex does not  
                // have a parent  
                parents[startVertex] = NO_PARENT;

                // Find shortest path for all  
                // vertices  
                for (int i = 1; i < nVertices; i++)
                {

                    // Pick the minimum distance vertex  
                    // from the set of vertices not yet  
                    // processed. nearestVertex is  
                    // always equal to startNode in  
                    // first iteration.  
                    int nearestVertex = -1;
                    int shortestDistance = int.MaxValue;
                    for (int vertexIndex = 0;
                            vertexIndex < nVertices;
                            vertexIndex++)
                    {
                        if (!added[vertexIndex] &&
                            shortestDistances[vertexIndex] <
                            shortestDistance)
                        {
                            nearestVertex = vertexIndex;
                            shortestDistance = shortestDistances[vertexIndex];
                        }
                    }

                    // Mark the picked vertex as  
                    // processed  
                    added[nearestVertex] = true;

                    // Update dist value of the  
                    // adjacent vertices of the  
                    // picked vertex.  
                    for (int vertexIndex = 0;
                            vertexIndex < nVertices;
                            vertexIndex++)
                    {
                        int edgeDistance = adjacencyMatrix[nearestVertex, vertexIndex];

                        if (edgeDistance > 0
                            && ((shortestDistance + edgeDistance) <
                                shortestDistances[vertexIndex]))
                        {
                            parents[vertexIndex] = nearestVertex;
                            shortestDistances[vertexIndex] = shortestDistance +
                                                            edgeDistance;
                        }
                    }
                }

                printSolution(startVertex, endVertex, shortestDistances, parents);
            }

            // A utility function to print  
            // the constructed distances  
            // array and shortest paths  
            private static void printSolution(int startVertex, int endVertex,
                                            int[] distances,
                                            int[] parents)
            {
                int nVertices = distances.Length;
                int distanceTraveled = 0;
                Console.Write("Vertex\t Distance\tTotalDistance\tPath");

                for (int vertexIndex = 0;
                        vertexIndex < nVertices;
                        vertexIndex++)
                {
                    if (vertexIndex != startVertex)
                    {
                        if (vertexIndex == endVertex)
                        {
                            Console.Write("\n" + startVertex + " -> ");
                            Console.Write(vertexIndex + " \t\t ");
                            Console.Write(distances[vertexIndex] + "\t\t");
                            distanceTraveled += distances[vertexIndex];
                            Console.Write(distanceTraveled + "\t\t\t\t");
                            Train.totalDistance += distanceTraveled;
                        }

                        if (vertexIndex == endVertex)
                            printPath(vertexIndex, parents);


                    }
                }
                Console.Write("\n");
            }

            // Function to print shortest path  
            // from source to currentVertex  
            // using parents array  
            private static void printPath(int currentVertex,
                                        int[] parents)
            {

                // Base case : Source node has  
                // been processed  
                if (currentVertex == NO_PARENT)
                {
                    return;
                }
                printPath(parents[currentVertex], parents);
                Console.Write(currentVertex + " ");
            }





            public static void Main(String[] args)
            {

                // say you need to get from 0 to 3

                int[,] adjacencyMatrix = { { 0, 4, 0, 0, 0, 0, 0, 8, 0 },
                                    { 4, 0, 8, 0, 0, 0, 0, 11, 0 },
                                    { 0, 8, 0, 7, 0, 4, 0, 0, 2 },
                                    { 0, 0, 7, 0, 9, 14, 0, 0, 0 },
                                    { 0, 0, 0, 9, 0, 10, 0, 0, 0 },
                                    { 0, 0, 4, 0, 10, 0, 2, 0, 0 },
                                    { 0, 0, 0, 14, 0, 2, 0, 1, 6 },
                                    { 8, 11, 0, 0, 0, 0, 1, 0, 7 },
                                    { 0, 0, 2, 0, 0, 0, 6, 7, 0 } };
                dijkstra(adjacencyMatrix, 0, 6);
                dijkstra(adjacencyMatrix, 6, 3);

                Console.Write("Total Distance tracveled by Train Boi: " + Train.totalDistance);

                Console.ReadKey();

            }
        }

    }
//}
