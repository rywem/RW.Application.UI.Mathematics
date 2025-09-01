using org.mariuszgromada.math.mxparser;
using RW.Library.Mathematics.Polynomials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        }
        private void ParseButton_Click(object sender, RoutedEventArgs e) => Run();

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Run();
        }

        private void Run()
        {
            StatusBlock.Text = "";
            FactorsList.ItemsSource = null;
            ExpandedText.Text = "";

            var expr = InputBox.Text?.Trim();
            if (string.IsNullOrWhiteSpace(expr))
            {
                StatusBlock.Text = "Enter a polynomial expression.";
                return;
            }

            try
            {
                // Parse
                var poly = Polynomial.Parse(expr);

                // Factorize (univariate only per your implementation)
                var factorTree = PolynomialFactorization.Factorize(poly);

                // Expand back
                var expanded = PolynomialExpansion.Expand(factorTree);

                // Show flattened factors for readability
                var flattened = FlattenProductFactors(factorTree)
                    .Select(PrettyPrintPolynomial)
                    .ToList();

                // If nothing flattened (shouldn't happen), at least show the leaf
                if (flattened.Count == 0)
                    flattened.Add(PrettyPrintPolynomial(expanded));

                FactorsList.ItemsSource = flattened;
                ExpandedText.Text = PrettyPrintPolynomial(expanded);
            }
            catch (Exception ex)
            {
                StatusBlock.Text = ex.Message;
            }
        }

        /// <summary>
        /// Flattens a Factor tree into multiplicative leaves.
        /// Each returned item is a Polynomial (integer coefficients per your guarantees).
        /// </summary>
        private static IEnumerable<Polynomial> FlattenProductFactors(Factor f)
        {
            var list = new List<Polynomial>();
            Collect(f, list);
            // Combine any adjacent pure-constant leaves into a single constant (optional)
            var (constVal, others) = SplitConstants(list);
            if (constVal.HasValue)
                yield return new Polynomial(new Term(constVal.Value));

            foreach (var p in others)
                yield return p;

            static void Collect(Factor n, List<Polynomial> acc)
            {
                if (n.IsLeaf && n.Polynomial != null)
                {
                    acc.Add(n.Polynomial);
                    return;
                }
                foreach (var c in n.Children)
                    Collect(c, acc);
            }

            static (int? constant, List<Polynomial> others) SplitConstants(List<Polynomial> polys)
            {
                int running = 1;
                bool hasConst = false;
                var nonConst = new List<Polynomial>();

                foreach (var p in polys)
                {
                    // detect pure constant polynomial
                    if (p.Terms.All(t => t.Variables.Count == 0))
                    {
                        var sum = p.Terms.Sum(t => t.Coefficient);
                        running = checked(running * sum);
                        hasConst = true;
                    }
                    else
                    {
                        nonConst.Add(p);
                    }
                }
                return (hasConst ? running : (int?)null, nonConst);
            }
        }

        /// <summary>
        /// Uses Polynomial.ToString but inserts spaces around +/− for nicer reading.
        /// </summary>
        private static string PrettyPrintPolynomial(Polynomial p)
        {
            var raw = p.ToString();
            // insert spaces around plus/minus that are not at start
            var pretty = raw.Replace("+", " + ").Replace("-", " - ");
            // collapse any double spaces that may appear
            while (pretty.Contains("  "))
                pretty = pretty.Replace("  ", " ");
            return pretty.Trim();
        }
    }

}
