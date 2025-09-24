using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using Mathos.Parser;

class ChartBuilder
{
    private string fun;
    private MathParser parser;

    public ChartBuilder(string fun)
    {
        this.fun = fun;
        parser = new MathParser ();
    }

    public void Draw(Chart chart, double rangeA, double rangeB, double step = 0.1)
    {
        chart.Series.Clear();

        var series = new Series
        {
            BorderWidth = 2,
            ChartType = SeriesChartType.Line,
            Name = "Функция"
        };

        for (double x = rangeA; x <= rangeB; x += step)
        {
            try
            {
                parser.LocalVariables["x"] = x;
                double y = parser.Parse(fun);
                series.Points.AddXY(x, y);
            }
            catch
            {
                continue;
            }
        }

        chart.Series.Add(series);
    }

    public void DrawRootPoint(Chart chart, double xRoot, string seriesName = "Корень")
    {
        var pointSeries = new Series
        {
            ChartType = SeriesChartType.Point,
            MarkerStyle = MarkerStyle.Circle,
            MarkerSize = 10,
            Color = Color.Red,
            Name = seriesName
        };

        parser.LocalVariables["x"] = xRoot;
        double yRoot = parser.Parse(fun);

        pointSeries.Points.AddXY(xRoot, yRoot);
        chart.Series.Add(pointSeries);
    }
}