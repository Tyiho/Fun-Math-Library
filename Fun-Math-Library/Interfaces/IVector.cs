using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;
using System.Text;
using Fun_Math_Library.Structs;

namespace Fun_Math_Library.Interfaces;

public interface IVector<T> where T : INumber<T>
{
    ImmutableArray<T> Components { get; }
    double Magnitude => Math.Sqrt(Components.Select(c => Convert.ToDouble(c * c)).Sum());
    int Dimension => Components.Length;

    bool IsOrthogonalTo(IVector<T> other) => Dot(other) == T.Zero;
    bool IsOrthonormalTo(IVector<T> other) => Dot(other) == T.Zero && IsNormal && other.IsNormal;
    bool IsZero => Components.All(c => c == T.Zero);
    bool IsNormal => Math.Abs(Magnitude - 1) < .00000001; // Allow for some floating point imprecision

    T Dot(IVector<T> other)
    {
        if (Dimension != other.Dimension) throw new ArgumentException("Vectors must have the same dimension for dot product.");
        return Components.Zip(other.Components, (a, b) => a * b).Aggregate(T.Zero, (sum, product) => sum + product);
    }
    T[] ToArray() => Components.ToArray();
    IEnumerable<T> AsEnumerable() => Components.AsEnumerable();
    IMatrix<T> ToColumnMatrix() => new Matrix<T>(this);
}