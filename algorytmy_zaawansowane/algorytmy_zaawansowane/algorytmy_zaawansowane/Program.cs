using ASD.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace algorytmy_zaawansowane
{
    class Program
    {
        static void Main(string[] args)
        {
            IGraph g = new AdjacencyMatrixGraph(true, 30);
            for (int i = 0; i < g.VerticesCount; i++)
            {
                for (int j = 0; j < g.VerticesCount; j++)
                {
                    if (i != j)
                        g.AddEdge(i, j);
                }
            }


            int minFlow = int.MaxValue;
            int source = 0;
            for (int i = 0; i < g.VerticesCount; i++)
            {
                if (i == source)
                    continue;
                IGraph g2 = new AdjacencyMatrixGraph(true, 6);
                int flow = g.FordFulkersonMaxFlow(source, i, out g2);
                if (flow <= minFlow)
                    minFlow = flow;
                Console.WriteLine(i);
            }

            Console.WriteLine(minFlow);
            Console.ReadLine();
        }
    }
}
