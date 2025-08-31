

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RW.Library.Mathematics.Polynomials;
namespace RW.Application.UI.Mathematics.Prototyping
{
    public class FactorizationPrototyping
    { 
        public void Run()
        {
            var factors = new List<Factor>
            {
                new Factor("(a+h)"),
                new Factor("(a+h)"),
                new Factor("(a+h)"),

            };
            /*var factors = new List<Factor>
            {
            new Factor("(2x+1)"),
            new Factor("(x-4)")
            };*/
            var expanded = PolynomialExpansion.ExpandFactors(factors);
            Debug.WriteLine(expanded);
            // Polynomial: 2x^2 - 7x -4

            var poly4 = new Polynomial(
                new Term(-1, new Dictionary<string, int> { { "x", 3 } }),
                new Term(1, new Dictionary<string, int> { { "x", 1 } }) //,
                //new Term(5, new Dictionary<string, int> { { "x", 1 } }),
                //new Term(-3, new Dictionary<string, int>())
            );
             factors = PolynomialFactorization.Factorize(poly4, "x");
            // Polynomial: 2x^2 - 7x -4

            var poly = new Polynomial(
                new Term(1, new Dictionary<string, int> { { "x", 3 } }),
                //new Term(5, new Dictionary<string, int> { { "x", 1 } }),
                new Term(-125, new Dictionary<string, int>())
            );

            factors = PolynomialFactorization.Factorize(poly, "x");

            Debug.WriteLine("Factors:");
            foreach (var f in factors)
            {
                Debug.WriteLine(f);
            }
            // Polynomial: x^2 - 5x + 6
            // results in (x - 2)(x - 3)
            var poly2 = new Polynomial(
                new Term(1, new Dictionary<string, int> { { "x", 2 } }),
                new Term(-5, new Dictionary<string, int> { { "x", 1 } }),
                new Term(6, new Dictionary<string, int>())
            );

            Debug.WriteLine($"Polynomial: {poly2}");

            var factors2 = PolynomialFactorization.Factorize(poly2, "x");

            Debug.WriteLine("Factors:");
            foreach (var f in factors2)
            {
                Debug.WriteLine(f);
            }
            /* // Example: 2x^2y + -5xy + 2y
             var poly = new Polynomial(
                 new Term(2, new Dictionary<string, int> { { "x", 2 }, { "y", 1 } }),
                 new Term(-5, new Dictionary<string, int> { { "x", 1 }, { "y", 1 } }),
                 new Term(2, new Dictionary<string, int> { { "y", 1 } })
             );

             Debug.WriteLine($"Polynomial: {poly}");

             // Factor over x
             var xFactors = PolynomialFactorizer.Factorize(poly, "x");
             Debug.WriteLine("Factors in x:");
             foreach (var f in xFactors) Debug.WriteLine(f);

             // Factor over y (if possible)
             var yFactors = PolynomialFactorizer.Factorize(poly, "y");
             Debug.WriteLine("Factors in y:");
             foreach (var f in yFactors) 
                 Debug.WriteLine(f);*/
        }
        
    
    }
}
