# Radial gauge plot
A customizable radial gauge control for plotting simple data. Built using C# (WinForms) in .NET 5.

The goal is two-fold (work in progress):
* Integrate this plot into [ScottPlot](https://github.com/ScottPlot/ScottPlot), already present in version 4.1.18 onwards.
* Create a standalone control (probably as a nuget packet) that's easy to mantain and update.

Copyright © 2021-2023 by Arthurits Ltd. No commercial nor profit use allowed. This software is provided only for personal and not-for-profit use.

Sponsor this project!

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/paypalme/ArthuritsLtd)

Download latest release: [![GitHub release (latest by date)](https://img.shields.io/github/v/release/arthurits/RadialGaugePlot?include_prereleases)](https://github.com/arthurits/RadialGaugePlot/releases)

## Developing status
Currently fine tuning some internal routines.
The `TestControl` project is functional and shows the functionally that is supported by the plot.

## Usage
Drag and drop the control from the toolbox onto the form and then add the following start-up sequence.
Please note that the control does not refresh automatically. Instead, `Render()` should be invoked everytime it must be graphically updated.

```csharp
public FrmTestControl()
{
    InitializeComponent();
    
    // Optional customization
    plot1.Palette = Plotting.Colorsets.Palette.Microcharts;
    plot1.PlotTitle = "RadialGauge plot";
    plot1.Legend.IsVisible = true;
    plot1.Legend.Location = RadialGaugePlot.Alignment.UpperRight;

    // Required
    plot1.Update(new double[] { 100, 80, 65, 45, -20 },
                 new string[] { "alpha", "beta", "gamma", "delta", "epsilon" });
    plot1.Render();
}
```

## Plot API
These are the properties and methods for this control
### Base plot
### RadialGauge plot
- AngleRange: The maximum angular interval that the gauges will consist of. It takes values in the range [0-360], default value is 360. Outside this range, unexpected side-effects might happen.
- Data: Data to be plotted. It's copied from of the data passed to either the constructor or the `Update(double[], bool)` method.
- DataAngular (protected): Angular data (rows: gauges; first column: initial angle; second column: swept angle) computed from `Data`.
- BackTransparency:  Dimmed percentage used to draw the gauges' background. Values in the range [0-1], default value is 0.9 (90 %). Outside this range, unexpected side-effects might happen.
- Color: Array of colors for the gauges. These colors are dimmed according to `BackTransparency` to draw the gauges' background. Length must be equal to the length of data passed to either the constructor or the `Update(double[], bool)` method.
- GaugeDirection: value from `RadialGaugeDirection` enum that determines whether the gauges are drawn clockwise (default value) or anti-clockwise (counter clockwise). The setter calls the `ComputeAngularData` function.
- GaugeLabelsPosition: Determines the gauge label position as a percentage of the gauge length, 0 being the beginning and 1 (default value) the ending of the gauge.
- GaugeLabels: array of string for the gauges. Length must be equal to the length of data passed to either the constructor or the `Update(double[], bool)` method.
- GaugeLabelsColor: Color of the value labels drawn inside the gauges. Default value is white.
- GaugeLabelsFontFraction: Size of the gague label text as a percentage of the gauge width. Values in the range [0-1], default value is 0.75 (75%). Other values might produce unexpected side-effects.
- GaugeMode: value from `RadialGaugeMode` enum that determines whether the gauges are drawn stacked (dafault value), sequentially, or as a single gauge (ressembling a pie plot). The setter calls the `ComputeMaxMin()` and `ComputeAngularData()` functions.
- GaugeSpaceFraction: the empty space between gauges as a percentage of the gauge width. Values in the range [0-1], default value is 0.5 (50%). Other values might produce unexpected side-effects.
- GaugeStart: value from `RadialGaugeStart` enum that determines whether the gauges are drawn starting from the inside (default value) or from the outside. Default value set to `RadialGaugeStart.InsideToOutside`.
- LineWidth: size (in pixels) of each gauge. If <0, then it will be calculated from the available space. Default value set to -1 (automatic sizing).
- MaxScale (protected): the maximum value for scaling the gauges. This value is associated to `StartingAngleGauges` and to `AngleRange` properties. The setter calls `ComputeAngularData()`.
- MinScale (protected): the minimum value for scaling the gauges (default value 0). This value is associated to `StartingAngleGauges` and to `AngleRange` properties. The setter calls `ComputeAngularData()`.
- CircularBackground: `false` if the gauges' background is adjusted to `AngleRange`. Default value is set to `true` (full-circle background gauges).
- StartingAngleBackGauges: the initial angle (in degrees) where the background gauges begin. Default value is 270° the same as `StartingAngleGauges`.
- StartingAngleGauges: angle (in degrees) at which the gauges start: 270° for North (default value), 0° for East, 90° for South, 180° for West, and so on. Expected values in the range [0°-360°], otherwise unexpected side-effects might happen.
- ShowGaugeValues: `true` (default value) if value labels are shown inside the gauges. Size of the text is set by `GaugeLabelsFontFraction` and color by `GaugeLabelsColor`.
- EndCap: end cap line style. Value from `System.Drawing.Drawing2D.LineCap` enum. Default value set to `Triangle`.
- StartCap: start cap line style. Value from `System.Drawing.Drawing2D.LineCap` enum. Default value set to `Round`.
- PlotTitle: title string which is rendered at the top of the plot.

## Screenshots
![Screenshot](https://github.com/arthurits/RadialGaugePlot/blob/master/TestControl/images/Screenshot01.png?raw=true "Radial gauge example 1")
![Screenshot](https://github.com/arthurits/RadialGaugePlot/blob/master/TestControl/images/Screenshot02.png?raw=true "Radial gauge example 2")
![Screenshot](https://github.com/arthurits/RadialGaugePlot/blob/master/TestControl/images/Screenshot03.png?raw=true "Radial gauge example 3")

## External dependencies
This project uses controls and routines from the following Gits:
* [ScottPlot](https://github.com/ScottPlot/ScottPlot)
* [Microcharts](https://github.com/dotnet-ad/Microcharts)
