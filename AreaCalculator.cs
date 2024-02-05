using BenchmarkDotNet.Attributes;
using System;

namespace CleanCodePerformanceComparison;

public class AreaCalculator
{
    [Params(1, 1000, 5000)]
    public int numberOfRepetitions { get; set; }

    [Benchmark(Baseline = true)]
    public double CalculateAreaUsingPolymorphism()
    {
        var totalAreaAccumulator = 0.0;

        for (int i = 0; i < numberOfRepetitions; i++)
        {
            totalAreaAccumulator += PolymorphismCalculation(new Square { Side = 5 });
            totalAreaAccumulator += PolymorphismCalculation(new Rectangle { Width = 6, Height = 4 });
            totalAreaAccumulator += PolymorphismCalculation(new Triangle { Base = 3, Height = 8 });
            totalAreaAccumulator += PolymorphismCalculation(new Circle { Radius = 2.5 });
        }

        return totalAreaAccumulator;
    }

    private double PolymorphismCalculation(ShapeBase shape)
    {   
        return shape.Area();
    }

    [Benchmark]
    public double CalculateAreaUsingSwitch()
    {
        var totalAreaAccumulator = 0.0;
        for (int i = 0; i < numberOfRepetitions; i++)
        {
            totalAreaAccumulator += SwitchCalculation(new ShapeUnion(ShapeType.Square, 5, 0));
            totalAreaAccumulator += SwitchCalculation(new ShapeUnion(ShapeType.Rectangle, 6, 4));
            totalAreaAccumulator += SwitchCalculation(new ShapeUnion(ShapeType.Triangle, 3, 8));
            totalAreaAccumulator += SwitchCalculation(new ShapeUnion(ShapeType.Circle, 2.5, 0));
        }

        return totalAreaAccumulator;
    }

    private double SwitchCalculation(ShapeUnion shape)
    {
        switch (shape.type)
        {
            case ShapeType.Square: return shape.Width * shape.Width;
            case ShapeType.Rectangle: return shape.Width * shape.Height;
            case ShapeType.Triangle: return 0.5 * shape.Width * shape.Height;
            case ShapeType.Circle: return Math.PI * shape.Width * shape.Width;
            default: return 0;
        }
    }
}