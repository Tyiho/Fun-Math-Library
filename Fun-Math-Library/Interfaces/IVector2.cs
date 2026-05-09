using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Fun_Math_Library.Interfaces;

public interface IVector2<T> : IVector<T> where T : INumber<T>
{
    T X { get; }
    T Y { get; }
    (T, T) ToCartesianCoordinates();
    (double, IAngle) ToRadialCoordinates();
    T Dot(IVector2<T> other) => X * other.X + Y * other.Y;
}