using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Components;

namespace Components
{
    //Description: This program just serves as a test driver for the components
    //I am bulding. Test cases I make will be placed here.
    class Component_Test
    {
        static void Main(string[] args)
        {
            //Station station = new Station("Station 1");
            //Hub hub = new Hub("Hub 1");

            //List<IVertex> list = new List<IVertex>
            //{
            //    station,
            //    hub
            //};

            //list.ForEach(Console.WriteLine);

            FreightStation train = new FreightStation("train1", 3, 10);
            //Console.WriteLine(train.GetID());


            Dictionary<IVertex, Node> test = new Dictionary<IVertex, Node>();


            test.Add(train, new Node(train));

            //Console.WriteLine(test[train].example.GetID());

            //IVertex vertex = new FreightStation("stationA");

            //Ivertex ex = graph.Find("station");

            //if (vertex.GetType() == typeof(FreightStation))
            //{
            //    var vertex2 = (FreightStation)vertex;
            //    vertex2.Print();
            //}


        }

        
    }

    class Node
    {
        public IVertex example;

        public Node(IVertex example)
        {
            this.example = example;
        }
    }
}
