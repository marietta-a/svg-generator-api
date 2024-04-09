namespace svg_generator_api
{
    public class SVGFigureDimensions
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public static string GetFilePath => $"{Directory.GetCurrentDirectory()}\\svg.json";
    }
}
