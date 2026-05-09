using Fun_Math_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;

namespace Fun_Math_Library.Structs;
public class Vector<T>(T[] components) : IVector<T> where T : INumber<T>
{
    public ImmutableArray<T> Components { get; } = [.. components];
    public int Dimension => Components.Length;

    public double Magnitude => Math.Sqrt(Components.Select(c => Convert.ToDouble(c * c)).Sum());

    public bool IsOrthogonalTo(Vector<T> other) => Dot(other) == T.Zero;
    public bool IsOrthonormalTo(Vector<T> other) => Dot(other) == T.Zero && IsNormal && other.IsNormal;
    public bool IsNormal => Math.Abs(Magnitude - 1) < .00000001; // Allow for some floating point imprecision

    public T Dot(Vector<T> other)
    {
        if (other.Dimension != this.Dimension) throw new ArgumentException("Vectors must have the same dimension for dot product.");
        return Components.Zip(other.Components, (a, b) => a * b).Aggregate(T.Zero, (sum, product) => sum + product);
    }

    public Vector(IVector<T> vector) : this(vector.Components.ToArray()) { }

    public static bool operator ==(Vector<T> left, Vector<T> right) => left.Components.SequenceEqual(right.Components);
    public static bool operator !=(Vector<T> left, Vector<T> right) => !left.Components.SequenceEqual(right.Components);

    public static Vector<T> operator +(Vector<T> left, Vector<T> right)
    {
        if(left.Dimension != right.Dimension) throw new ArgumentException("Vectors must have the same dimension for addition.");
        return new Vector<T>(left.Components.Zip(right.Components, (a, b) => a + b).ToArray());
    }

    public static Vector<T> operator -(Vector<T> left, Vector<T> right)
    {
        if (left.Dimension != right.Dimension) throw new ArgumentException("Vectors must have the same dimension for subtraction.");
        return new Vector<T>(left.Components.Zip(right.Components, (a, b) => a - b).ToArray());
    }

    public static Vector<T> operator *(Vector<T> vector, T scalar) => new Vector<T>(vector.Components.Select(c => c * scalar).ToArray());
    public static Vector<T> operator *(T scalar, Vector<T> vector) => new Vector<T>(vector.Components.Select(c => c * scalar).ToArray());
    public static Vector<T> operator /(Vector<T> vector, T scalar) => new Vector<T>(vector.Components.Select(c => c / scalar).ToArray());

    public override string ToString()
    {
        return $"({string.Join(", ", Components)})";
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector<T> vector && vector == this && obj.GetHashCode() == this.GetHashCode();
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Components);
    }
}

