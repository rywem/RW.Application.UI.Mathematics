using org.mariuszgromada.math.mxparser;
using ScottPlot;
using ScottPlot.Palettes;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace RW.Application.UI.Mathematics.GUI
{
    /// <summary>
    /// Interaction logic for EquationPlotter.xaml
    /// </summary>
    public partial class EquationPlotter : Window
    {
        public List<string> Equations { get; } = new();

        // Bind this to the ListBox so users can see/remove equations
        public ObservableCollection<string> EquationList { get; } = new();

        // Keep references so we can remove the correct plotted series later
        private readonly List<(string Expr, Scatter Series)> _seriesByExpr = new();

        // Palette & counter to rotate line colors
        private readonly IPalette _palette = new Category10();
        private int _seriesCount = 0;

        public EquationPlotter(List<string> equations)
        {
            InitializeComponent();
            DataContext = this;
            ConfigureAxes();

            Equations = equations ?? new List<string>();
            foreach (var eq in Equations)
                TryEvaluateAndAddSeries(eq);

            Plot.Plot.Axes.AutoScale();
            Plot.Plot.ShowLegend();
            Plot.Refresh();
        }

        public EquationPlotter(string equation)
        {
            InitializeComponent();
            DataContext = this;
            ConfigureAxes();

            ExprBox.Text = equation;
            Plot.Refresh();
        }

        public EquationPlotter()
        {
            InitializeComponent();
            DataContext = this;
            ConfigureAxes();
            Plot.Refresh();
        }

        /// <summary>
        /// Set up plot titles and axis labels.
        /// </summary>
        private void ConfigureAxes()
        {
            Plot.Plot.Axes.Title.Label.Text = "Equation Plot";
            Plot.Plot.Axes.Bottom.Label.Text = "x";
            Plot.Plot.Axes.Left.Label.Text = "f(x)";
        }
        /// <summary>
        /// Plot button: replaces existing lines with the single expression from the textbox.
        /// </summary>
        private void Plot_Click(object sender, RoutedEventArgs e)
        {
            var exprText = ExprBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(exprText))
            {
                MessageBox.Show("Please enter an equation (e.g., x^3 - 4*x + 1).",
                    "Plot Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ClearAllInternal();
            TryEvaluateAndAddSeries(exprText);

            Plot.Plot.Axes.AutoScale();
            Plot.Plot.ShowLegend();
            Plot.Refresh();
        }
     


       
        /// <summary>
        /// Add button: appends the current expression as a new colored line.
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var exprText = ExprBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(exprText))
            {
                MessageBox.Show("Please enter an equation (e.g., x^3 - 4*x + 1).",
                    "Add Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TryEvaluateAndAddSeries(exprText);
            Plot.Plot.Axes.AutoScale();
            Plot.Plot.ShowLegend();
            Plot.Refresh();
        }
        /// <summary>
        /// Clear button: removes all equations and lines.
        /// </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearAllInternal();
            Plot.Plot.Axes.AutoScale();
            Plot.Refresh();
        }

        /// <summary>
        /// Remove button inside the list: removes the selected equation and its line.
        /// </summary>
        private void RemoveEquation_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not System.Windows.Controls.Button btn) return;
            if (btn.Tag is not string exprText) return;

            // Remove the first matching entry (mirrors ListBox order)
            int idx = EquationList.IndexOf(exprText);
            if (idx < 0 || idx >= _seriesByExpr.Count)
                return;

            var (_, series) = _seriesByExpr[idx];

            // Remove from plot and internal collections
            Plot.Plot.Remove(series);
            _seriesByExpr.RemoveAt(idx);
            EquationList.RemoveAt(idx);

            Plot.Plot.Axes.AutoScale();
            Plot.Refresh();
        }

        /// <summary>
        /// Programmatically add an equation (same as clicking Add).
        /// </summary>
        public void AddEquationPlot(string equation) => TryEvaluateAndAddSeries(equation);

        /// <summary>
        /// Parse UI bounds and resolution. Throws if invalid.
        /// </summary>
        private (double xmin, double xmax, int n) ParseBounds()
        {
            if (!double.TryParse(XMinBox.Text, out double xmin))
                throw new Exception("x-min is not a valid number.");
            if (!double.TryParse(XMaxBox.Text, out double xmax))
                throw new Exception("x-max is not a valid number.");
            if (!int.TryParse(PointsBox.Text, out int n) || n < 2)
                throw new Exception("points must be an integer ≥ 2.");
            if (xmax <= xmin)
                throw new Exception("x-max must be greater than x-min.");
            return (xmin, xmax, n);
        }

        /// <summary>
        /// Evaluate f(x) defined by <paramref name="exprText"/> on the current domain
        /// and add it as a new colored series. Shows a MessageBox on error.
        /// </summary>
        private void TryEvaluateAndAddSeries(string exprText)
        {
            try
            {
                var (xmin, xmax, n) = ParseBounds();

                double dx = (xmax - xmin) / (n - 1);
                double[] xs = Enumerable.Range(0, n).Select(i => xmin + i * dx).ToArray();

                var xArg = new Argument("x = 0");
                var expression = new org.mariuszgromada.math.mxparser.Expression(exprText, xArg);

                double[] ys = new double[n];
                for (int i = 0; i < n; i++)
                {
                    xArg.setArgumentValue(xs[i]);
                    double y = expression.calculate();
                    ys[i] = double.IsNaN(y) || double.IsInfinity(y) ? double.NaN : y;
                }

                var color = _palette.GetColor(_seriesCount++);

                var series = Plot.Plot.Add.Scatter(xs, ys);
                series.LegendText = $"f(x) = {exprText}";
                series.LineWidth = 2;
                series.Color = color;

                EquationList.Add(exprText);
                _seriesByExpr.Add((exprText, series));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Plot Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Remove all plotted series and reset internal state.
        /// </summary>
        private void ClearAllInternal()
        {
            Plot.Plot.Clear();
            _seriesByExpr.Clear();
            EquationList.Clear();
            _seriesCount = 0;
            ConfigureAxes();
        }

    }
}
