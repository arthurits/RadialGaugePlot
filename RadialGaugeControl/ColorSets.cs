﻿// All these colorset were sourced from ScotPlott.
// At the time the ScotPlott license file was accessed (2021-09-24) the original work was released under a MIT License.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plotting.Colorsets
{
    /// <summary>
    /// A color set is a collection of colors, like a color palette. Colors are stored as web-formatted colors (e.g., '#FFAA66') in a string array.
    /// System.Drawing.Color is intentionally avoided here to simplify porting to other rendering systems down the road.
    /// </summary>
    public interface IColorset
    {
        public string[] hexColors { get; }
    }

    public class Palette
    {
        // Matplotlib/D3/Vega/Tableau
        public static Palette Category10 => new Palette(new Colorsets.Category10());
        public static Palette Category20 => new Palette(new Colorsets.Category20());

        // Nord
        public static Palette Aurora => new Palette(new Colorsets.Aurora());
        public static Palette Frost => new Palette(new Colorsets.Frost());
        public static Palette Nord => new Palette(new Colorsets.Nord());
        public static Palette PolarNight => new Palette(new Colorsets.PolarNight());
        public static Palette SnowStorm => new Palette(new Colorsets.Snowstorm());

        // Misc
        public static Palette Microcharts => new Palette(new Colorsets.Microcharts());
        public static Palette OneHalfDark => new Palette(new Colorsets.OneHalfDark());
        public static Palette OneHalf => new Palette(new Colorsets.OneHalf());

        private readonly IColorset ColorSet;
        public readonly string Name;
        
        public Palette(IColorset colorset)
        {
            ColorSet = colorset ?? new Colorsets.Category10();
            Name = ColorSet.GetType().Name;
        }

        public Palette(string[] htmlColors, string name = "Custom")
        {
            ColorSet = new Colorsets.Custom(htmlColors);
            Name = name;
        }

        public override string ToString() => $"{Name} palette ({Count()} colors)";

        public int GetInt32(int index)
        {
            var (r, g, b) = GetRGB(index);
            return 255 << 24 | r << 16 | g << 8 | b;
        }

        public (byte r, byte g, byte b) GetRGB(int index)
        {
            index %= ColorSet.hexColors.Length;

            string hexColor = ColorSet.hexColors[index];
            if (!hexColor.StartsWith("#"))
                hexColor = "#" + hexColor;

            if (hexColor.Length != 7)
                throw new InvalidOperationException("Invalid hex color string");

            Color color = ColorTranslator.FromHtml(hexColor);

            return (color.R, color.G, color.B);
        }

        public Color GetColor(int index)
        {
            return Color.FromArgb(GetInt32(index));
        }

        public Color GetColor(int index, double alpha = 1)
        {
            return Color.FromArgb(alpha: (int)(alpha * 255), baseColor: GetColor(index));
        }

        /// <summary>
        /// Gets an array of Colors from the palette.
        /// </summary>
        /// <param name="count">Number of colors.</param>
        /// <param name="offset">Starting color index of the palette.</param>
        /// <param name="alpha">Value for the alpha channel of the returned colors.</param>
        /// <returns></returns>
        public Color[] GetColors(int count, int offset = 0, double alpha = 1)
        {
            Color[] colors = new Color[count];
            for (int i = offset; i < count + offset; i++)
            {
                colors[i] = GetColor(i, alpha);
            }

            //return Enumerable.Range(offset, count)
            //    .Select(x => GetColor(x, alpha))
            //    .ToArray();

            return colors;
        }

        public int Count()
        {
            return ColorSet.hexColors.Count();
        }
    }

    /// <summary>
    /// Sourced from Nord: https://github.com/arcticicestudio/nord https://www.nordtheme.com/docs/colors-and-palettes
    /// </summary>
    internal class Aurora : IColorset
    {
        public string[] hexColors => new string[]
        {
            "#BF616A", "#D08770", "#EBCB8B", "#A3BE8C", "#B48EAD",
        };
    }

    /// <summary>
    /// These colors were originally developed by Tableau: https://www.tableau.com/about/blog/2016/7/colors-upgrade-tableau-10-56782
    /// Vega obtained permission to release this color set under a BSD license: https://github.com/d3/d3-scale-chromatic/pull/16
    /// Vega placed these color values here under a BSD (3-clause) license: https://github.com/vega/vega/blob/af5cc1df42eb5aaf2f478d0bda69313643fe0532/docs/releases/v1.2.1/vega.js#L170-L205
    /// </summary>
    internal class Category10 : IColorset
    {
        public string[] hexColors => new string[]
        {
            "#1f77b4", "#ff7f0e", "#2ca02c", "#d62728", "#9467bd",
            "#8c564b", "#e377c2", "#7f7f7f", "#bcbd22", "#17becf",
        };
    }

    /// <summary>
    /// These colors were originally developed by Tableau: https://www.tableau.com/about/blog/2016/7/colors-upgrade-tableau-10-56782
    /// Vega obtained permission to release this color set under a BSD license: https://github.com/d3/d3-scale-chromatic/pull/16
    /// Vega placed these color values here under a BSD (3-clause) license: https://github.com/vega/vega/blob/af5cc1df42eb5aaf2f478d0bda69313643fe0532/docs/releases/v1.2.1/vega.js#L170-L205
    /// </summary>
    internal class Category20 : IColorset
    {
        public string[] hexColors => new string[]
        {
            "#1f77b4", "#aec7e8", "#ff7f0e", "#ffbb78", "#2ca02c",
            "#98df8a", "#d62728", "#ff9896", "#9467bd", "#c5b0d5",
            "#8c564b", "#c49c94", "#e377c2", "#f7b6d2", "#7f7f7f",
            "#c7c7c7", "#bcbd22", "#dbdb8d", "#17becf", "#9edae5",
        };
    }

    /// <summary>
    /// Used to define a custom colorset when creating a palette.
    /// </summary>
    internal class Custom : IColorset
    {
        public string[] hexColors { get; }

        public Custom(string[] htmlColors)
        {
            if (htmlColors is null)
                throw new ArgumentNullException("must provide at least one color");

            if (htmlColors.Length == 0)
                throw new ArgumentException("must provide at least one color");

            hexColors = htmlColors;
        }
    }

    /// <summary>
    /// Sourced from Nord: https://github.com/arcticicestudio/nord https://www.nordtheme.com/docs/colors-and-palettes
    /// </summary>
    internal class Frost : IColorset
    {
        public string[] hexColors => new string[]
        {
            "#8FBCBB", "#88C0D0", "#81A1C1", "#5E81AC",
        };
    }

    /// <summary>
    /// Sourced from the examples provided by Microcharts: https://github.com/microcharts-dotnet/Microcharts/blob/main/Sources/Microcharts.Samples/Data.cs.
    /// At the time the license file was accessed(2021-09-02) the original work was released under a MIT License, Copyright (c) 2017 Aloïs Deniel.
    /// </summary>
    internal class Microcharts : IColorset
    {
        public string[] hexColors => new string[]
        {
            "#266489", "#68B9C0", "#90D585", "#F3C151", "#F37F64",
            "#424856", "#8F97A4", "#DAC096", "#76846E", "#DABFAF",
            "#A65B69", "#97A69D",
        };
    }

    /// <summary>
    /// Sourced from NordConEmu: https://github.com/arcticicestudio/nord-conemu.
    /// Seems to be an extended version of Aurora.
    /// </summary>
    internal class Nord : IColorset
    {
        // suggested background: #2e3440
        public string[] hexColors => new string[]
        {
            "#bf616a", "#a3be8c", "#ebcb8b", "#81a1c1", "#b48ead", "#88c0d0", "#e5e9f0",
        };
    }

    /// <summary>
    /// Sourced from Son A. Pham's Sublime color scheme by the same name https://github.com/sonph/onehalf
    /// </summary>
    internal class OneHalf : IColorset
    {
        public string[] hexColors => new string[]
        {
            "#383a42", "#e4564a", "#50a14f", "#c18402", "#0084bc", "#a626a4", "#0897b3"
        };
    }

    /// <summary>
    /// Sourced from Son A. Pham's Sublime color scheme by the same name https://github.com/sonph/onehalf
    /// </summary>
    internal class OneHalfDark : IColorset
    {
        // suggested background: #2e3440
        public string[] hexColors => new string[]
        {
            "#e06c75", "#98c379", "#e5c07b", "#61aff0", "#c678dd", "#56b6c2", "#dcdfe4"
        };
    }

    /// <summary>
    /// Sourced from Nord: https://github.com/arcticicestudio/nord https://www.nordtheme.com/docs/colors-and-palettes
    /// </summary>
    internal class PolarNight : IColorset
    {
        public string[] hexColors => new string[]
        {
            "#2E3440", "#3B4252", "#434C5E", "#4C566A",
        };
    }

    /// <summary>
    /// Sourced from Nord: https://github.com/arcticicestudio/nord https://www.nordtheme.com/docs/colors-and-palettes
    /// </summary>
    internal class Snowstorm : IColorset
    {
        public string[] hexColors => new string[]
        {
            "#D8DEE9", "#E5E9F0", "#ECEFF4"
        };
    }
}
