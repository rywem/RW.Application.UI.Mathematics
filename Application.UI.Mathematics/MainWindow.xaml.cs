using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RW.Application.UI.Mathematics
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PlotSineDemo();
        }

        /// <summary>
        /// Plots y = sin(x) using ScottPlot 5.x API on the WPF control named 'Plot'.
        /// </summary>
        private void PlotSineDemo()
        {
            double[] xs = GenerateRange(0, 2 * Math.PI, 0.01);
            double[] ys = xs.Select(Math.Sin).ToArray();

            // clear any previous plottables (ScottPlot 5.x)
            Plot.Plot.Clear();

            var scatter = Plot.Plot.Add.Scatter(xs, ys);
            scatter.LegendText = "y = sin(x)";

            // optional cosmetics (ScottPlot 5.x)
            Plot.Plot.Title("Sine Wave");
            Plot.Plot.Axes.Bottom.Label.Text = "x (radians)";
            Plot.Plot.Axes.Left.Label.Text = "y";
            Plot.Plot.ShowLegend();

            Plot.Plot.Axes.AutoScale();
            Plot.Refresh();
        }
        private void OpenEquationPlotter_Click(object sender, RoutedEventArgs e)
        {
            // create and show the EquationPlotter window
            var plotter = new RW.Application.UI.Mathematics.GUI.EquationPlotter();
            plotter.Show();
        }
        //OpenLaTeXExample_Click
        private void OpenLaTeXExample_Click(object sender, RoutedEventArgs e)
        {
            // create and show the EquationPlotter window
            var latexExample = new RW.Application.UI.Mathematics.GUI.LaTeXExample();
            latexExample.Show();
        }
        /// <summary>
        /// Returns a numeric range starting at 'start', stepping by 'step', up to (and possibly excluding) 'end'.
        /// </summary>
        public static double[] GenerateRange(double start, double end, double step)
        {
            if (step <= 0) throw new ArgumentOutOfRangeException(nameof(step), "Step must be positive.");
            int count = (int)Math.Floor((end - start) / step) + 1;
            if (count < 1) return Array.Empty<double>();

            double[] data = new double[count];
            for (int i = 0; i < count; i++)
                data[i] = start + i * step;

            return data;
        }
    }
}