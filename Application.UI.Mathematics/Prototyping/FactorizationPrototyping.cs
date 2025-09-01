using RW.Library.Mathematics.Polynomials;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
namespace RW.Application.UI.Mathematics.Prototyping
{
    public class FactorizationPrototyping
    { 
        public void Run()
        {
            try
            {                
                var polynomial = Polynomial.Parse("4x^2-2x^3-2");
                var factors = PolynomialFactorization.Factorize(polynomial);       
                var expandedFactors = PolynomialExpansion.Expand(factors);
                /* var p2 = Polynomial.Parse("3 + 4x^2 - 2x^3");
                 var f2 = PolynomialFactorization.Factorize(p2);       // (2x+1)(x-4)
                 var e2 = PolynomialExpansion.Expand(f2);               // 2x^2 - 7x - 4*/
                // A) Parse a powered factor and expand
                //var powered2 = Factor.Product(Factor.ParseMany("(a+h)^3");
                //var expanded2 = PolynomialExpansion.Expand(powered2); // a^3 + 3a^2h + 3ah^2 + h^3
                //var factorized = PolynomialFactorization.Factorize(expanded2);
                //// B) Convenience: build a product directly from one string
                //var powered3 = Factor.ProductFromString("(a+h)^3");
                //var expanded2 = PolynomialExpansion.Expand(powered3);

                //// C) Factorization still exact
                //var p1 = Polynomial.Parse("3 + 4x^2 - 2x^3");
                //var f1 = PolynomialFactorization.Factorize(p1);       // (2x+1)(x-4)
                //var e1 = PolynomialExpansion.Expand(f1);               // 2x^2 - 7x - 4

                // D) Reject fractional powers
                try
                {
                    var bad = Factor.ParseMany("(a+h)").ToArray();
                }
                catch (NotSupportedException ex)
                {
                    Console.WriteLine(ex.Message); // "Non-integer exponent '1/3' is not supported. Use an integer >= 0."
                }
                // => a^3 + 3a^2h + 3ah^2 + h^3 (ordering per ToString())

                // returns [(-1), (2x+1), (x-4)] as Factor objects (each wrapping a Polynomial)
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }


    }
}
