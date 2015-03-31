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
        [STAThread]
        static void Main(string[] args)
        {
            IGraph g;
            Parser parser = new Parser();
            parser.LoadFile();
            parser.loadGraph(out g);

            if (g == null)
            {
                Console.WriteLine("Wystąpił błąd podczas generowania grafu");
                return;
            }

            int minFlow = int.MaxValue;
            int source = 0;
            for (int i = 0; i < g.VerticesCount; i++)
            {
                if (i == source)
                    continue;
                IGraph g2 = new AdjacencyMatrixGraph(true, g.VerticesCount);
                int flow = g.FordFulkersonMaxFlow(source, i, out g2);
                if (flow <= minFlow)
                    minFlow = flow;
            }

            Console.WriteLine(minFlow);
            Console.ReadLine();
        }
    }
}
