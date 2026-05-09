using System;
using System.Collections.Generic;
using System.Text;

namespace Fun_Math_Library.Interfaces;

public interface IAngle
{
    double Radians { get; }
    double ToDegrees();
    double GetAngleSquared();
    static abstract IAngle FromDegrees(double degrees);
}