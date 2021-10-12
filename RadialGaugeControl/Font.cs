using System;
using System.Drawing;

namespace Drawing
{
    public class Font
    {
        public float Size { get; set; } = 12;
        public Color Color { get; set; } = Color.Black;
        //public Alignment Alignment = Alignment.UpperLeft;
        public bool Bold { get; set; } = false;
        public float Rotation { get; set; } = 0;
        
        [System.ComponentModel.Browsable(false)]
        public string DefaultFont => InstalledFont.Sans();

        private string _Name;
        public string Name
        {
            get => _Name;
            set => _Name = InstalledFont.ValidFontName(value); // ensure only valid font names can be assigned
        }

        public Font() => Name = DefaultFont;
    }

    public static class InstalledFont
    {
        public static string Default() => Sans();

        public static string Serif() =>
            ValidFontName(new string[] { "Times New Roman", "DejaVu Serif", "Times" });

        public static string Sans() =>
            ValidFontName(new string[] { "Segoe UI", "DejaVu Sans", "Helvetica" });

        public static string Monospace() =>
            ValidFontName(new string[] { "Consolas", "DejaVu Sans Mono", "Courier" });

        /// <summary>
        /// Returns a font name guaranteed to be installed on the system
        /// </summary>
        public static string ValidFontName(string fontName)
        {
            foreach (FontFamily installedFont in FontFamily.Families)
                if (string.Equals(installedFont.Name, fontName, System.StringComparison.OrdinalIgnoreCase))
                    return installedFont.Name;
            
            return SystemFonts.DefaultFont.Name;
        }

        /// <summary>
        /// Returns a font name guaranteed to be installed on the system
        /// </summary>
        public static string ValidFontName(string[] fontNames)
        {
            string name;
            foreach (string preferredFont in fontNames)
            {
                name = ValidFontName(preferredFont);
                if (string.Equals(name, preferredFont, System.StringComparison.OrdinalIgnoreCase))
                    return name;
            }

            return SystemFonts.DefaultFont.Name;
        }
    }

}
