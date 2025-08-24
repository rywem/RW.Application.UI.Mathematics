
using RW.Library.Mathematics.Polynomials.Factorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using RW.Library.Mathematics.Polynomials.Factorization;
namespace RW.Application.UI.Mathematics.Prototyping
{
    public class FactorizationPrototyping
    { 
        public void Run()
        {

            // Polynomial: 2x^2 - 7x -4
            
            var poly = new Polynomial(
                new Term(-2, new Dictionary<string, int> { { "x", 2 } }),
                new Term(7, new Dictionary<string, int> { { "x", 1 } }),
                new Term(4, new Dictionary<string, int>())
            );

            var factors = PolynomialFactorizer.Factorize(poly, "x");

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

            var factors2 = PolynomialFactorizer.Factorize(poly2, "x");

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
