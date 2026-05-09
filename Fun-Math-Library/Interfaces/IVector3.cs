using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Fun_Math_Library.Interfaces;

public interface IVector3<T> : IVector<T> where T : INumber<T>
{
    T X { get; }
    T Y { get; }
    T Z { get; }
    T Dot(IVector3<T> other) => X * other.X + Y * other.Y + Z * other.Z;
    IVector3<T>Cross(IVector3<T> other);
}

