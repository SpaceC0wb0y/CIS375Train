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

            //FreightStation train = new FreightStation("train1", 3, 10);
            //Console.WriteLine(train.GetID());


            //Dictionary<Vertex, Node> test = new Dictionary<Vertex, Node>();


            //test.Add(train, new Node(train));

            //Console.WriteLine(test[train].example.GetID());

            //Vertex vertex = new FreightStation("stationA");

            //Vertex ex = graph.Find("station");

            //if (vertex.GetType() == typeof(FreightStation))
            //{
            //    var vertex2 = (FreightStation)vertex;
            //    vertex2.Print();
            //}

            //Dictionary<Vertex, Node> test = new Dictionary<Vertex, Node>();


            //PassengerStation station1 = new PassengerStation("station1", 4, 5, 5, 10, 5, 10);

            //test.Add(train, new Node(train));

            //Console.WriteLine(test[train].example.GetID();


            /************CRITICAL PAY ATTENTION**************************/
            /*The following code shows that if you add a station or hub to the graph, it
             will IMPLICITLY CONVERT it from its orginal type (passenger station or freight station or hub)
             into a Vertex object. This is just the way C# works. Once that happends, you cant access the unique methods
             of any of those classes. The only solution is to do what the if statements show. You gotta check the IsPassengerStation property
             or whichever property you need for the type you want. Then if its true, you can explicitly cast back to the original object type
             and do what you have to do*/
            Dictionary<Vertex, Node> graph = new Dictionary<Vertex, Node>();
            PassengerStation station1 = new PassengerStation("station1", 4, 5, 5, 10, 5, 10);

            graph.Add(station1, new Node(station1));
            Track track1 = new Track();

            if (graph[station1].example.IsPassengerStation)
            {
                PassengerStation passengerStation = (PassengerStation)graph[station1].example;
                passengerStation.Leave(track1);
            }

            if (graph[station1].example.IsFreightStation)
            {
                FreightStation freightStation = (FreightStation)graph[station1].example;
                freightStation.Print();
            }

            /*****************END OF MAJOR SECTION******************************/
            
        }
    }

    class Node
    {
        public Vertex example;

        public Node(Vertex example)
        {
            this.example = example;
        }
    }
}
