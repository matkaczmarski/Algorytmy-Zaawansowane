using ASD.Graph;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace algorytmy_zaawansowane
{
    class Parser
    {
        private string filePath = "";

        public bool LoadFile()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "Text files (*.txt)|*.txt";
            fd.Multiselect = false;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                filePath = fd.FileName;
                return true;
            }
            return false;
        }

        public void loadGraph(out IGraph graph)
        {
            try
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);
                int size = Int32.Parse(lines[0].Trim());
                graph = new AdjacencyMatrixGraph(true, size);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(':');
                    int v1 = Int32.Parse(parts[0].Trim());
                    string[] rest = parts[1].Trim().Split(',');

                    foreach (string v2_string in rest)
                    {
                        int v2 = Int32.Parse(v2_string.Trim());
                        if (graph.GetEdgeWeight(v1, v2) == null)
                            graph.AddEdge(v1, v2);
                        if (graph.GetEdgeWeight(v2, v1) == null)
                            graph.AddEdge(v2, v1);
                    }
                }
            }
            catch (Exception ex)
            {
                graph = null;
                return;
            }
        }
    }
}
