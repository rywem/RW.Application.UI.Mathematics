using org.mariuszgromada.math.mxparser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Application.UI.Mathematics.GUI
{
    /// <summary>
    /// Interaction logic for EquationPlotter.xaml
    /// </summary>
    public partial class EquationPlotter : Window
    {
        public EquationPlotter()
        {
            InitializeComponent();

            // optional: a first render
            Plot.Plot.Axes.Title.Label.Text = "Equation Plot";
            Plot.Plot.Axes.Bottom.Label.Text = "x";
            Plot.Plot.Axes.Left.Label.Text = "f(x)";
            Plot.Refresh();
        }

        private void Plot_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string exprText = ExprBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(exprText))
                    throw new Exception("Please enter an equation (e.g., x^3 - 4*x + 1).");

                // parse bounds & resolution
                if (!double.TryParse(XMinBox.Text, out double xmin))
                    throw new Exception("x-min is not a valid number.");
                if (!double.TryParse(XMaxBox.Text, out double xmax))
                    throw new Exception("x-max is not a valid number.");
                if (!int.TryParse(PointsBox.Text, out int n) || n < 2)
                    throw new Exception("points must be an integer ≥ 2.");
                if (xmax <= xmin)
                    throw new Exception("x-max must be greater than x-min.");

                // build x array
                double dx = (xmax - xmin) / (n - 1);
                double[] xs = Enumerable.Range(0, n).Select(i => xmin + i * dx).ToArray();

                // build evaluator once and reuse
                // mXparser lets us define "x" as an Argument and evaluate the Expression quickly in a loop
                var xArg = new Argument("x = 0");
                var expression = new org.mariuszgromada.math.mxparser.Expression(exprText, xArg);

                // Optional: allow degree trig with: mXparser.setDegreesMode();
                // Default is radians (recommended for math plotting).

                // Evaluate y for each x (skip invalid values)
                double[] ys = new double[n];
                for (int i = 0; i < n; i++)
                {
                    xArg.setArgumentValue(xs[i]);
                    double y = expression.calculate();

                    // mXparser returns Double.NaN for invalid expressions or domain errors
                    // ScottPlot will draw breaks on NaNs, which is nice for vertical asymptotes, etc.
                    ys[i] = double.IsNaN(y) || double.IsInfinity(y) ? double.NaN : y;
                }

                // draw
                Plot.Plot.Clear();
                var plot = Plot.Plot.Add.Scatter(xs, ys );
                plot.LegendText = $"f(x) = {exprText}";
                Plot.Plot.Axes.AutoScale();
                Plot.Plot.ShowLegend(); // ScottPlot v5
                Plot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Plot Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
