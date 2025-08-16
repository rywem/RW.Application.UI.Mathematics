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
using WpfMath.Parsers;
using WpfMath.Rendering;
using XamlMath;
using XamlMath.Colors;

namespace RW.Application.UI.Mathematics.GUI
{
    /// <summary>
    /// Interaction logic for LaTeXExample.xaml
    /// </summary>
    public partial class LaTeXExample : Window
    {
        // Reuse these across renders
        private const double FontSizePx = 28.0;
        private const string FontFamilyName = "Segoe UI";

        public LaTeXExample()
        {
            InitializeComponent();
            // Initial render to the right-hand image, and re-render on typing
            RenderToImage(InputBox.Text);
            InputBox.TextChanged += (_, __) => RenderToImage(InputBox.Text);
        }
        private void RenderButton_Click(object sender, RoutedEventArgs e)
            => RenderToImage(InputBox.Text);

        private void RenderToImage(string latex)
        {
            try
            {
                // 1) Parse LaTeX via the singleton parser
                var parser = WpfTeXFormulaParser.Instance;
                var formula = parser.Parse(latex);

                // 2) Build environment (style, size, font)
                var env = WpfTeXEnvironment.Create(TexStyle.Display, FontSizePx, FontFamilyName);

                // 3) Render to a BitmapSource
                BitmapSource bmp = formula.RenderToBitmap(env);

                RenderedImage.Source = bmp;
                ToolTipService.SetToolTip(RenderedImage, null);
            }
            catch (Exception ex)
            {
                RenderedImage.Source = null;
                ToolTipService.SetToolTip(RenderedImage, ex.Message);
            }
        }
    }
}
