# Radial gauge plot
A customizable radial gauge control for plotting simple data. Built using C# (WinForms) in .NET 5.

The goal is two-fold (work in progress):
* Integrate this plot into [ScottPlot](https://github.com/ScottPlot/ScottPlot), already present in version 4.1.18 onwards.
* Create a standalone control (probably as a nuget packet) that's easy to mantain and update.

Copyright © 2021 by Arthurits Ltd. No commercial nor profit use allowed. This software is provided only for personal and not-for-profit use.

Sponsor this project!

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/paypalme/ArthuritsLtd)

Download latest release: [![GitHub release (latest by date)](https://img.shields.io/github/v/release/arthurits/RadialGaugePlot?include_prereleases)](https://github.com/arthurits/RadialGaugePlot/releases)

## Developing status
Currently fine tuning some internal routines.
The `TestControl` project is functional and shows the functionally that is supported by the plot.

## Usage
Drag the control from the toolbox onto the form and cumtomize the plot as shown in the following code. The control does not refresh automatically. Instead, `Render()` should be invoked everytime it must be graphically updated.

```csharp
public FrmTestControl()
{
    InitializeComponent();
    
    plot1.Palette = Plotting.Colorsets.Palette.Microcharts;
    plot1.PlotTitle = "Example title";
    plot1.Update(new double[] { 100, 80, 65, 45, -20 },
                 new string[] { "alpha", "beta", "gamma", "delta", "epsilon" });
    plot1.Legend.IsVisible = true;
    plot1.Legend.Location = RadialGaugePlot.Alignment.UpperRight;
    plot1.Render();
}
```

## Screenshots
![Screenshot](https://github.com/arthurits/RadialGaugePlot/blob/master/TestControl/images/Screenshot01.png?raw=true "Radial gauge example 1")
![Screenshot](https://github.com/arthurits/RadialGaugePlot/blob/master/TestControl/images/Screenshot02.png?raw=true "Radial gauge example 2")
![Screenshot](https://github.com/arthurits/RadialGaugePlot/blob/master/TestControl/images/Screenshot03.png?raw=true "Radial gauge example 3")

## External dependencies
This project uses controls and routines from the following Gits:
* [ScottPlot](https://github.com/ScottPlot/ScottPlot)
* [Microcharts](https://github.com/dotnet-ad/Microcharts)
