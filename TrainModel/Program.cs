using System;
using System.Collections.Generic;

namespace TrainModel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Path to file: ");
            string path = Console.ReadLine();
            if(String.IsNullOrEmpty(path))
                path = "input.json";
            var result = FileParser.ParseJson(path);
            Graph graph = result.Item1;
            List<Train> trains = result.Item2;
            CollisionDetector collisionDetector = new CollisionDetector(graph, trains);
            Console.WriteLine("Will train in current configuration collide: " + (collisionDetector.DetectCollision() ? "Yes" : "No"));
        }
    }
}
