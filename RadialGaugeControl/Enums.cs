//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace RadialGaugePlot
{
    public enum RadialGaugeDirection
    {
        Clockwise,
        AntiClockwise
    }

    public enum RadialGaugeStart
    {
        InsideToOutside,
        OutsideToInside
    }

    public enum RadialGaugeMode
    {
        Stacked,
        Sequential,
        SingleGauge
    }

    public enum RadialGaugeLabelPos
    {
        Beginning = 0,
        Middle = 1,
        End = 2
    }

    /// <summary>
    /// Vertical (upper/middle/lower) and Horizontal (left/center/right) alignment
    /// </summary>
    public enum Alignment
    {
        UpperLeft,
        UpperRight,
        UpperCenter,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        LowerLeft,
        LowerRight,
        LowerCenter
    }
}
