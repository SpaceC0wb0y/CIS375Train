using Components;

namespace Graph
{
    //Description: This is a test program to test Graph.cs 
    class Program
    {
        static void Main(string[] args)
        {
            FreightStation A = new FreightStation("Vertex A", 25);
            FreightStation B = new FreightStation("Vertex B", 69);
            FreightStation C = new FreightStation("Vertex C", 420);
            FreightStation D = new FreightStation("Vertex D", 9001);
            FreightStation E = new FreightStation("Vertex E", 21);
            Vertex H1 = new Hub("Hub 1");
            Track E1 = new Track("Track A to B", A, B, 6);
            Track E2 = new Track("Track A to D", A, D, 1);
            Track E3 = new Track("Track B to D", B, D, 2);
            Track E4 = new Track("Track D to E", D, E, 1);
            Track E5 = new Track("Track B to E", B, E, 2);
            Track E6 = new Track("Track B to C", B, C, 5);
            Track E7 = new Track("Track E to C", E, C, 5);
            Track E8 = new Track("Shorter path from E to C", E, C, 4);
            Track E9 = new Track("Shorter path from B to C", B, C, 4);
            Track E10 = new Track("Shorter path from A to B", A, B, 5);

            ////////////////justin
          //  Train Trainboi = new Train("Trainboi", "Freight");
            //Train Trainboi2 = new Train("Trainboi2", "Freight");

            ///////////////justin

            //Node Test = new Node(V2, E1);
            //Test.AddIncidentTrack(E2);
            //Test.AddIncidentTrack(E3);

            //Console.WriteLine(V1);
            //Console.WriteLine(V2);
            //Console.WriteLine(E1);
            //Console.WriteLine(E2);
            //Console.WriteLine(E3);
            //Console.WriteLine("Tracks in Node:");
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

            //E10.isAvailible
            //List<Track> Tracks = test.GetTracks();

            //foreach (var item in Tracks)
            //{
            //    Console.WriteLine(item);
            //}

            //Console.WriteLine(test.NumTracks());

            //bool isTrack = test.AddTrack(E3);
            //if (isTrack)
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
            //    Track example2 = test.FindTrack(V1, V2, "Track 2");
            //    Console.WriteLine(example2);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}


            //Console.WriteLine(test.NumTracks());

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
            //tester.AddIncidentTrack(E3);

            //List<Track> testNode = tester.GetTracks();

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
            //Tracks = test.GetTracks();
            //foreach(var item in Tracks)
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

            //test.RemoveTrack(E2);
            //test.RemoveTrack(E1);
            //test.RemoveTrack(E3);
            //test.RemoveTrack(E4);
            //Tracks = test.GetTracks();

            //foreach (var item in Tracks)
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



           // test.TrainRoute(test, A, E, Trainboi);



           // Console.Write("");
            


         //   test.TrainRoute(test, C, E, Trainboi2);

          //  Console.ReadLine();

            //test.RemoveTrack(E4);
            //testList = test.GetList(V2);
            //foreach (var item in testList)
            //{
            //    item.PrintContents();
            //}

          
           

          

        

        }
    }
}