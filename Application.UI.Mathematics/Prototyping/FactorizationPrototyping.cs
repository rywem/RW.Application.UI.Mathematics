

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
            try
            {
                // 1) Classic reducible
                var poly1 = Polynomial.Parse("2x^2 - 7x - 4");
                var factorTree1 = PolynomialFactorization.Factorize(poly1);
                // factorTree1.ToString() => "(2x+1)(x-4)"
                var expanded1 = PolynomialExpansion.Expand(factorTree1); // back to 2x^2 - 7x - 4

                // 2) Irreducible over Q (discriminant = 17)
                var poly2 = Polynomial.Parse("-2x^2 - 7x - 4");
                var factorTree2 = PolynomialFactorization.Factorize(poly2);
                // => "(-1)(2x^2+7x+4)"  (product with integer-only leaves)
                var expanded2 = PolynomialExpansion.Expand(factorTree2);

                // 3) Multivariate expansion
                var factorTree3 = Factor.Product(
                    Factor.Parse("(a+h)"),
                    Factor.Parse("(a+h)"),
                    Factor.Parse("(a+h)")
                );
                var expanded3 = PolynomialExpansion.Expand(factorTree3);
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
