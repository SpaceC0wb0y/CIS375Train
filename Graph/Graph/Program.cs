using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    //Description: This is a test program to test Graph.cs 
    class Program
    {
        static void Main(string[] args)
        {
            Vertex A = new Vertex("Vertex A");
            Vertex B = new Vertex("Vertex B");
            Vertex C = new Vertex("Vertex C");
            Vertex D = new Vertex("Vertex D");
            Vertex E = new Vertex("Vertex E");
            Edge E1 = new Edge("Edge A to B", A, B, 6);
            Edge E2 = new Edge("Edge A to D", A, D, 1);
            Edge E3 = new Edge("Edge B to D", B, D, 2);
            Edge E4 = new Edge("Edge D to E", D, E, 1);
            Edge E5 = new Edge("Edge B to E", B, E, 2);
            Edge E6 = new Edge("Edge B to C", B, C, 5);
            Edge E7 = new Edge("Edge E to C", E, C, 5);
            Edge E8 = new Edge("Shorter path from E to C", E, C, 4);
            Edge E9 = new Edge("Shorter path from B to C", B, C, 4);
            Edge E10 = new Edge("Shorter path from A to B", A, B, 5);

            ////////////////justin
            Train Trainboi = new Train("Trainboi", "Freight");
            //Train Trainboi2 = new Train("Trainboi2", "Freight");

            ///////////////justin

            //Node Test = new Node(V2, E1);
            //Test.AddIncidentEdge(E2);
            //Test.AddIncidentEdge(E3);

            //Console.WriteLine(V1);
            //Console.WriteLine(V2);
            //Console.WriteLine(E1);
            //Console.WriteLine(E2);
            //Console.WriteLine(E3);
            //Console.WriteLine("Edges in Node:");
            //Test.PrintContents();

            Graph test = new Graph();
            //test.AddVertex(V1);
            //bool isThere = test.AddVertex(V1);
            //test.AddVertex(V2);
            //Console.WriteLine(isThere);

            //int count = test.NumVertices();
            //Console.WriteLine("Num: " + count);

            test.AddVertex(A);
            test.AddVertex(B);
            test.AddVertex(C);
            test.AddVertex(D);
            test.AddVertex(E);

            
            //List<Vertex> vertices = test.GetVertices();

            //foreach (var item in vertices)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine(test.NumVertices());

            test.AddEdge(E1);
            test.AddEdge(E2);
            test.AddEdge(E3);
            test.AddEdge(E4);
            test.AddEdge(E5);
            test.AddEdge(E6);
            test.AddEdge(E7);
            test.AddEdge(E8);
            test.AddEdge(E9);
            test.AddEdge(E10);


            //List<Edge> edges = test.GetEdges();

            //foreach (var item in edges)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine(test.NumEdges());

            //bool isEdge = test.AddEdge(E3);
            //if (isEdge)
            //{
            //    Console.WriteLine("unique");
            //}
            //else
            //{
            //    Console.WriteLine("not unique");
            //}

            //try
            //{
            //    test.FindVertex("isisj");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //try
            //{
            //    Vertex example = test.FindVertex("Vertex 1");
            //    Console.WriteLine(example);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //try
            //{
            //    Edge example2 = test.FindEdge(V1, V2, "Edge 2");
            //    Console.WriteLine(example2);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}


            //Console.WriteLine(test.NumEdges());

            //bool isAdjacent = test.IsAdjacent(V2, V4);
            //if (isAdjacent)
            //{
            //    Console.WriteLine("They are adjacent");
            //}
            //else
            //{
            //    Console.WriteLine("They are not adjacent");
            //}

            //Node tester = new Node(V1, E1);
            //tester.AddIncidentEdge(E3);

            //List<Edge> testNode = tester.GetEdges();

            //foreach(var item in testNode)
            //{
            //    Console.WriteLine(item);
            //}

            //tester.PrintContents();

            //bool isRemoved = test.RemoveVertex(V1);
            //if (isRemoved)
            //{
            //    Console.WriteLine("Removal of V1 returned true");
            //}

            //vertices = test.GetVertices();
            //foreach(var item in vertices)
            //{
            //    Console.WriteLine(item);
            //}
            //edges = test.GetEdges();
            //foreach(var item in edges)
            //{
            //    Console.WriteLine(item);
            //}

            //isAdjacent = test.IsAdjacent(V1, V2);
            //if (isAdjacent)
            //{
            //    Console.WriteLine("\nThey are adjacent");
            //}
            //else
            //{
            //    Console.WriteLine("\nThey are not adjacent");
            //}

            //test.RemoveEdge(E2);
            //test.RemoveEdge(E1);
            //test.RemoveEdge(E3);
            //test.RemoveEdge(E4);
            //edges = test.GetEdges();

            //foreach (var item in edges)
            //{
            //    Console.WriteLine(item);
            //}

            //isAdjacent = test.IsAdjacent(V1, V2);
            //if (isAdjacent)
            //{
            //    Console.WriteLine("They are adjacent");
            //}
            //else
            //{
            //    Console.WriteLine("They are not adjacent");
            //}

            //LinkedList<Node> testList = test.GetList(V1);
            //Console.WriteLine(testList.Count);
            //foreach (var item in testList)
            //{
            //    item.PrintContents();
            //}
            //Console.WriteLine();

            //testList = test.GetList(V2);
            //Console.WriteLine(testList.Count);
            //foreach (var item in testList)
            //{
            //    item.PrintContents();
            //}

            //Console.WriteLine("\n\n\n");



            test.TrainRoute(test, A, E, Trainboi);



            Console.Write("");
            


         //   test.TrainRoute(test, C, E, Trainboi2);

            Console.ReadLine();

            //test.RemoveEdge(E4);
            //testList = test.GetList(V2);
            //foreach (var item in testList)
            //{
            //    item.PrintContents();
            //}

          
           

          

        

        }
    }
}