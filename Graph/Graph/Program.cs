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
            Vertex V1 = new Vertex("Vertex 1");
            Vertex V2 = new Vertex("Vertex 2");
            Edge E1 = new Edge("Edge A", V1, V2, 10);
            Edge E2 = new Edge("Edge B", V1, V2, 15);
            Edge E3 = new Edge("Edge C", V1, V2, 20);

            Node Test = new Node(V2, E1);
            Test.AddIncidentEdge(E2);
            Test.AddIncidentEdge(E3);

            Console.WriteLine(V1);
            Console.WriteLine(V2);
            Console.WriteLine(E1);
            Console.WriteLine(E2);
            Console.WriteLine(E3);
            Console.WriteLine("Edges in Node:");
            Test.PrintContents();
        }
    }
}
