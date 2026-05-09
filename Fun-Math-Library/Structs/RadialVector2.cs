using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;
using System.Text;
using Fun_Math_Library.Interfaces;

namespace Fun_Math_Library.Structs;

public readonly struct RadialVector2(double radius, IAngle angle) : IVector<double>, IVector2<double>
{
    public ImmutableArray<double> Components { get; } = [radius * Math.Cos(angle.Radians), radius * Math.Sin(angle.Radians)];
    public double X => Components[0];
    public double Y => Components[1];
    public double Radius { get; } = radius;
    public IAngle Angle { get; } = angle;

    public (double, double) ToCartesianCoordinates()
    {
        return (X, Y);
    }

    public (double, IAngle) ToRadialCoordinates()
    {
        return (Radius, Angle);
    }

    public (double, IAngle) ToTuple()
    {
        return (Radius, Angle);
    }

    public static RadialVector2 FromCartesianCoordinates(double x, double y)
    {
        return new RadialVector2(Math.Sqrt(x * x + y * y), new Angle(Math.Atan2(y, x)));
    }
    public static RadialVector2 FromCartesianCoordinates((double, double) coordinates)
    {
        var x = coordinates.Item1;
        var y = coordinates.Item2;
        return new RadialVector2(Math.Sqrt(x * x + y * y), new Angle(Math.Atan2(y, x)));
    }
    public static RadialVector2 FromRadialCoordinates(double radius, IAngle angle)
    {
        return new RadialVector2(radius, angle);
    }
    public static RadialVector2 FromRadialCoordinates((double, IAngle) coordinates)
    {
        return new RadialVector2(coordinates.Item1, coordinates.Item2);
    }


    public RadialVector2(IVector2<double> vector2) : this(Math.Sqrt(vector2.X * vector2.X + vector2.Y * vector2.Y),new Angle(Math.Atan2(vector2.Y,vector2.X))) { }
    public RadialVector2(IVector<double> vector) : this(Math.Sqrt(vector.Components[0] * vector.Components[0] + vector.Components[1] * vector.Components[1]), new Angle(Math.Atan2(vector.Components[1], vector.Components[0]))) 
    {
        if (vector.Dimension != 2) throw new ArgumentException("Vector must have exactly 2 components.");
    }

    public static implicit operator (double, IAngle)(RadialVector2 vector) => (vector.Radius, vector.Angle);
    public static implicit operator RadialVector2((double, IAngle) tuple) => new RadialVector2(tuple.Item1, tuple.Item2);

}