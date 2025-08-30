using RW.Library.Mathematics.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RW.Application.UI.Mathematics.Prototyping
{
    public class PolynomalParserPrototyping
    {
        public void Run()
        {
            var px2 = PolynomialParser.Parse("2x^3*y^2-y+2x+4");
            // x^2 - 3x + 4
            var p1 = PolynomialParser.Parse("x^2-3x+4");
            // Terms (after combine): 1x^2, -3x, 4

            // 2xy^2 - y + 2x + 4
            var p2 = PolynomialParser.Parse("2xy^2-y+2x+4");
            // Terms: 2x y^2, -1y, 2x, 4

            // 3*x*y^2 - 5
            var p3 = PolynomialParser.Parse("3*x*y^2 - 5");

            // Implicit signs and coefficients
            var p4 = PolynomialParser.Parse("-x + y - 2"); // -1x, +1y, -2
        }
    }
}
