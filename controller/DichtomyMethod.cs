using Mathos.Parser;
using System;

class DichtomyMethod
{
    private readonly MathParser parser;
    private string fun;
    private double rangeA;
    private double rangeB;
    private double accuracy;
    private double funA;
    private double funB;
    private double middleSegment;

    public DichtomyMethod(string fun, double rangeA, double rangeB, double accuracy)
    {
        parser = new MathParser();
        this.fun = fun;
        this.rangeA = rangeA;
        this.rangeB = rangeB;
        this.accuracy = accuracy;
        middleSegment = 0;
        funA = 0;
        funB = 0;
    }

    public bool CheckFunction()
    {
        try
        {
            parser.LocalVariables["x"] = 0;
            parser.Parse(fun);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool CheckInterval()
    {
        parser.LocalVariables["x"] = rangeA;
        funA = parser.Parse(fun);

        parser.LocalVariables["x"] = rangeB;
        funB = parser.Parse(fun);

        if (Math.Abs(funA) < accuracy || Math.Abs(funB) < accuracy) return true;

        return funA * funB < 0;
    }

    public double MiddleSegment()
    {
        return middleSegment = (rangeA + rangeB) / 2;
    }

    public double Solve()
    {
        if (!CheckFunction()) return 0;

        parser.LocalVariables["x"] = rangeA;
        funA = parser.Parse(fun);
        if (Math.Abs(funA) < accuracy) return rangeA;

        parser.LocalVariables["x"] = rangeB;
        funB = parser.Parse(fun);
        if (Math.Abs(funB) < accuracy) return rangeB;

        if (!CheckInterval()) return 0;

        int iteration = 0;

        while ((rangeB - rangeA) / 2 > accuracy && iteration < 1000)
        {
            middleSegment = MiddleSegment();

            parser.LocalVariables["x"] = middleSegment;
            double funC = parser.Parse(fun);

            if (Math.Abs(funC) < accuracy)
            {
                return middleSegment;
            }

            if (funA * funC < 0)
            {
                rangeB = middleSegment;
                funB = funC;
            }
            else
            {
                rangeA = middleSegment;
                funA = funC;
            }

            iteration++;
        }

        return middleSegment;
    }
}