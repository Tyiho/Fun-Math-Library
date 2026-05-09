using Fun_Math_Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Text;

namespace Fun_Math_Library.Structs;
public readonly struct Vector3<T>(T x, T y, T z) : IVector<T>, IVector3<T> where T : INumber<T>
{
    public ImmutableArray<T> Components { get; } = [x, y, z];

    public T X => Components[0];
    public T Y => Components[1];
    public T Z => Components[2];

    public IVector3<T> Cross(IVector3<T> other)
    {
        return new Vector3<T>(
            Y * other.Z - Z * other.Y,
            Z * other.X - X * other.Z,
            X * other.Y - Y * other.X
        );
    }

    public Vector3<T> Cross(Vector3<T> other)
    {
        return new Vector3<T>(
            Y * other.Z - Z * other.Y,
            Z * other.X - X * other.Z,
            X * other.Y - Y * other.X
        );
    }

    public (T, T,T) ToTuple()
    {
        return (X, Y, Z);
    }

    public static bool operator ==(Vector3<T> left, Vector3<T> right) => left.X == right.X && left.Y == right.Y && left.Z == right.Z;
    public static bool operator !=(Vector3<T> left, Vector3<T> right) => left.X != right.X || left.Y != right.Y || left.Z != right.Z;
    public static Vector3<T> operator +(Vector3<T> left, Vector3<T> right) => new Vector3<T>(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    public static Vector3<T> operator +(Vector3<T> left, IVector3<T> right) => new Vector3<T>(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
    public static Vector3<T> operator +(IVector2<T> left, Vector3<T> right) => new Vector3<T>(left.X + right.X, left.Y + right.Y, right.Z);
    public static Vector3<T> operator -(Vector2<T> left, Vector3<T> right) => new Vector3<T>(left.X - right.X, left.Y - right.Y, -right.Z);
    public static Vector3<T> operator *(Vector3<T> vector, T scalar) => new Vector3<T>(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
    public static Vector3<T> operator *(T scalar, Vector3<T> vector) => new Vector3<T>(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
    public static Vector3<T> operator /(Vector3<T> vector, T scalar) => new Vector3<T>(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);

    public override string ToString()
    {
        return $"({X}, {Y}, {Z})";
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector3<T> vector && vector == this && obj.GetHashCode() == this.GetHashCode();
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public Vector3(IVector3<T> vector3) : this(vector3.X, vector3.Y,vector3.Z) { }

    public Vector3(IVector<T> vector) : this(vector.Components[0], vector.Components[1], vector.Components[2])
    {
        if (vector.Dimension != 3) throw new ArgumentException("Vector must have exactly 3 components.");
    }


    public static implicit operator (T, T, T)(Vector3<T> vector) => (vector.X, vector.Y, vector.Z);
    public static implicit operator Vector3<T>((T, T, T) tuple) => new Vector3<T>(tuple.Item1, tuple.Item2, tuple.Item3);
    public static implicit operator ImmutableArray<T>(Vector3<T> vector) => [vector.X, vector.Y, vector.Z];
}

