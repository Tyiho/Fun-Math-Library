using System;
using System.Collections.Generic;
using System.Text;

using Fun_Math_Library.Interfaces;

namespace Fun_Math_Library.Structs;

internal class Angle(double radians) : IAngle
{
    public double Radians { get; } = radians;

    public double ToDegrees()
    {
        return Radians * (180f / Math.PI);
    }

    public double GetAngleSquared()
    {
        return Radians * Radians;
    }

    public static IAngle FromDegrees(double degrees)
    {
        return new Angle(degrees / 180 * Math.PI);
    }

    public static implicit operator double(Angle angle) => angle.Radians;
}