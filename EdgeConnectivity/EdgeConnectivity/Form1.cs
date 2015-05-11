using EdgeConnectivity.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EdgeConnectivity
{
    public partial class MainForm : Form
    {
        private Graph graph;
        private string fileName;
        private const string GRAPH_FILE_EXT = ".grph";
        private const string RESULT_FILE_EXT = ".edgc";

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.favicon;
        }

        private void loadGraph_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Plik z grafem (" + GRAPH_FILE_EXT + ")|*" + GRAPH_FILE_EXT;
            dialog.Title = "Podaj plik z grafem.";
            dialog.Multiselect = false;
            dialog.InitialDirectory = "~/";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
                graph = Graph.LoadGraph(fileName);
                if (graph == null)
                    MessageBox.Show("Błąd wczytywnia grafu z pliku.");
                int[,] m = (int[,])graph.matrix.Clone();
                m[0, 0] = 0;
            }
        }

        private void evaluate_button_Click(object sender, EventArgs e)
        {
            if(graph == null)
                MessageBox.Show("Wczytaj graf z pliku.");
            else
            {
                fileName = fileName.Replace(GRAPH_FILE_EXT, RESULT_FILE_EXT);
                int result = graph.EdgeConnectivity();
                if (Graph.WriteToFile(fileName, result))
                    MessageBox.Show("Zakończono obliczenia i zapisano wynik do pliku: " + fileName);
                else
                    MessageBox.Show("Błąd obliczenia lub zapisu do pliku.");
            }
        }
    }
}
