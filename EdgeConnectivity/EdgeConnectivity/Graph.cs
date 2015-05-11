using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EdgeConnectivity
{
    /// <summary>
    /// Klasa definiująca graf zapisany przy pomocy macierzy incydencji.
    /// </summary>
    public class Graph
    {
        public const int UNWEIGHTED_EDGE = 1;
        public const int NO_EDGE = 0;

        /// <summary>
        /// Macierz incydencji opisywanego grafu.
        /// </summary>
        public int[,] matrix;

        /// <summary>
        /// Liczba wierzchołków grafu.
        /// </summary>
        public int VerticiesCount { get { return matrix.GetLength(0); } }

        /// <summary>
        /// Tworzy graf o danej liczbie wierzchołków.
        /// </summary>
        /// <param name="verticesCount">Liczba wierzchołków w tworzonym grafie.</param>
        public Graph(int verticesCount)
        {
            matrix = new int[verticesCount, verticesCount];
        }

        /// <summary>
        /// Dodaje krawędź do grafu.
        /// </summary>
        /// <param name="v1">Pierwszy wierzchołek krawędzi.</param>
        /// <param name="v2">Drugi wierzchołek krawędzi.</param>
        /// <returns>Informację, czy dodawanie krawędzi się powiodło (true) czy też nie (false).</returns>
        public bool AddEdge(int v1, int v2)
        {
            if (!CheckForValidIndex(v1) || !CheckForValidIndex(v2))
                return false;
            matrix[v1, v2] = UNWEIGHTED_EDGE;
            matrix[v2, v1] = UNWEIGHTED_EDGE;
            return true;
        }

        /// <summary>
        /// Usuwa krawędź z grafu.
        /// </summary>
        /// <param name="v1">Pierwszy wierzchołek krawędzi.</param>
        /// <param name="v2">Drugi wierzchołek krawędzi.</param>
        /// <returns>Informację, czy usuwanie krawędzi się powiodło (true) czy też nie (false).</returns>
        public bool RemoveEdge(int v1, int v2)
        {
            if (!HasEdge(v1, v2))
                return false;
            matrix[v1, v2] = NO_EDGE;
            matrix[v2, v1] = NO_EDGE;
            return true;
        }

        /// <summary>
        /// Zwraca informację, czy graf posiada daną krawędź.
        /// </summary>
        /// <param name="v1">Pierwszy wierzchołek krawędzi.</param>
        /// <param name="v2">Drugi wierzchołek krawędzi.</param>
        /// <returns>Informację, czy graf posiada daną krawędź (true) czy też nie (false).</returns>
        public bool HasEdge(int v1, int v2)
        {
            if (!CheckForValidIndex(v1) || !CheckForValidIndex(v2)) 
                return false;
            return matrix[v1, v2] != NO_EDGE;
        }

        /// <summary>
        /// Sprawdza, czy podany index wierzchołka jest poprawny.
        /// </summary>
        /// <param name="v">Index wierzchołka.</param>
        /// <returns>Informację, czy podany index wierzchołka jest poprawny (true) czy też nie (false).</returns>
        private bool CheckForValidIndex(int v)
        {
            return (v >= 0 && v < VerticiesCount);
        }

        /// <summary>
        /// Sprawdza, czy istnieje ścieżka od źródła do ujścia w sieci rezydualnej.
        /// </summary>
        /// <param name="rGraph">Sieć rezydualna.</param>
        /// <param name="s">Źródło.</param>
        /// <param name="t">Ujście.</param>
        /// <param name="parent">Znaleziona ścieżka.</param>
        /// <returns>Informacja, czy znaleziono ścieżkę (true) czy też nie (false).</returns>
        bool bfs(int[,] rGraph, int s, int t, int[] parent)
        {
            bool[] visited = new bool[VerticiesCount];

            Queue<int> q = new Queue<int>();
            q.Enqueue(s);
            visited[s] = true;
            parent[s] = -1;

            while (q.Count > 0)
            {
                int u = q.Dequeue();

                for (int v = 0; v < VerticiesCount; v++)
                {
                    if (visited[v] == false && rGraph[u, v] > 0)
                    {
                        q.Enqueue(v);
                        parent[v] = u;
                        visited[v] = true;
                    }
                }
            }

            return (visited[t] == true);
        }

        /// <summary>
        /// Znajduje wartość maksymalnego przepływu w grafie.
        /// </summary>
        /// <param name="s">Źródło.</param>
        /// <param name="t">Ujście.</param>
        /// <returns>Wartość maksymalnego przepływu.</returns>
        public int MaximumFlow(int s, int t)
        {
            if (!CheckForValidIndex(s) || !CheckForValidIndex(t))
                return -1;

            int u, v;
            int[,] rGraph = new int[VerticiesCount, VerticiesCount];
            for (u = 0; u < VerticiesCount; u++)
                for (v = 0; v < VerticiesCount; v++)
                    rGraph[u, v] = matrix[u, v];

            int[] parent = new int[VerticiesCount];

            int max_flow = 0;

            while (bfs(rGraph, s, t, parent))
            {
                int path_flow = Int32.MaxValue;
                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    path_flow = Math.Min(path_flow, rGraph[u, v]);
                }

                for (v = t; v != s; v = parent[v])
                {
                    u = parent[v];
                    rGraph[u, v] -= path_flow;
                    rGraph[v, u] += path_flow;
                }

                max_flow += path_flow;
            }

            return max_flow;
        }

        /// <summary>
        /// Tworzy kopię grafu.
        /// </summary>
        /// <returns>Kopia grafu.</returns>
        public Graph Clone()
        {
            Graph clone = new Graph(VerticiesCount);
            clone.matrix = (int[,])this.matrix.Clone();
            return clone;
        }

        /// <summary>
        /// Znajduje wartość spójności krawędziowej grafu.
        /// </summary>
        /// <returns>Spójność krawędziowa.</returns>
        public int EdgeConnectivity()
        {
            Random rand = new Random();
            int s = rand.Next(VerticiesCount);
            int minResult = int.MaxValue;
            for (int t = 0; t < VerticiesCount; t++)
            {
                if (t == s)
                    continue;

                //tworzenie grafu, może coś pozmieniać?
                Graph tmpGraph = Clone();

                int maxFlow = tmpGraph.MaximumFlow(s, t);
                minResult = maxFlow < minResult ? maxFlow : minResult;
            }
            return minResult;
        }


        /// <summary>
        /// Wczytuje graf z pliku.
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku.</param>
        /// <returns>Graf utworzony na podstawie pliku.</returns>
        public static Graph LoadGraph(string filePath)
        {
            Graph graph;
            try
            {
                string[] lines = System.IO.File.ReadAllLines(filePath);
                int size = Int32.Parse(lines[0].Trim());
                graph = new Graph(size);
                for (int i = 1; i < lines.Length; i++)
                {
                    if (lines[i].Count() > 0 && lines[i][0] == '#')
                        continue;
                    string[] parts = lines[i].Split(':');
                    int v1 = Int32.Parse(parts[0].Trim());
                    string[] rest = parts[1].Trim().Split(',');

                    foreach (string v2_string in rest)
                    {
                        int v2 = Int32.Parse(v2_string.Trim());
                        graph.AddEdge(v1, v2);
                        graph.AddEdge(v2, v1);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }     
            return graph;
        }

        /// <summary>
        /// Zapisuje wynik do pliku.
        /// </summary>
        /// <param name="fileName">Ścieżka do pliku.</param>
        /// <param name="result">Wynik.</param>
        /// <returns>Informację, czy zapis do pliku powiódł się (true) czy też nie (false).</returns>
        public static bool WriteToFile(string fileName, int result)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, false))
                {
                    writer.WriteLine("Uzyskana przez algorytm wartość spójności krawędziowej grafu wynosi: " + result);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
