namespace CleanCodePerformanceComparison;
using System;

public class Circle : ShapeBase
{
    public double Radius { get; set; }

    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
}