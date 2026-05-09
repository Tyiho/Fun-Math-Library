using System.Collections.Immutable;
using Fun_Math_Library.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Fun_Math_Library.Structs;

public readonly struct Vector2<T>(T x, T y) : IVector<T>, IVector2<T> where T : INumber<T>
{
    public ImmutableArray<T> Components { get; } = [x, y];
    public T X  => Components[0];
    public T Y => Components[1]; 

    public (T, T) ToTuple()
    {
        return (X, Y);
    }

    public (T, T) ToCartesianCoordinates()
    {
        return (X, Y);
    }

    public (double, IAngle) ToRadialCoordinates()
    {
        double x = Convert.ToDouble(X);
        double y = Convert.ToDouble(Y);
        double radius = Math.Sqrt(x * x + y * y);
        Angle angle = new Angle( Math.Atan2(y, x));
        return (radius, angle);
    }

    public static Vector2<T> FromCartesianCoordinates(T x, T y)
    {
        return new Vector2<T>(x, y);
    }
    public static Vector2<T> FromCartesianCoordinates((T,T) coordinates)
    {
        return new Vector2<T>(coordinates.Item1,coordinates.Item2);
    }
    public static Vector2<double> FromRadialCoordinates(double radius, IAngle angle)
    {
        var x = radius * Math.Cos(angle.Radians);
        var y = radius * Math.Sin(angle.Radians);
        return new Vector2<double>(x, y);
    }
    public static Vector2<double> FromRadialCoordinates((double, IAngle) coordinates)
    {
        var x = coordinates.Item1 * Math.Cos(coordinates.Item2.Radians);
        var y = coordinates.Item1 * Math.Sin(coordinates.Item2.Radians);
        return new Vector2<double>(x, y);
    }

    public Vector2(IVector2<T> vector2) : this(vector2.X, vector2.Y) { }

    public Vector2(IVector<T> vector) : this(vector.Components[0], vector.Components[1])
    {
        if (vector.Dimension != 2) throw new ArgumentException("Vector must have exactly 2 components.");
    }

    public static bool operator ==(Vector2<T> left, Vector2<T> right) => left.X == right.X && left.Y == right.Y;
    public static bool operator !=(Vector2<T> left, Vector2<T> right) => left.X != right.X || left.Y != right.Y;
    public static Vector2<T> operator +(Vector2<T> left, Vector2<T> right) => new Vector2<T>(left.X + right.X, left.Y + right.Y);
    public static Vector2<T> operator +(Vector2<T> left, IVector2<T> right) => new Vector2<T>(left.X + right.X, left.Y + right.Y);
    public static Vector2<T> operator +(IVector2<T> left, Vector2<T> right) => new Vector2<T>(left.X + right.X, left.Y + right.Y);
    public static Vector2<T> operator -(Vector2<T> left, Vector2<T> right) => new Vector2<T>(left.X - right.X, left.Y - right.Y);
    public static Vector2<T> operator *(Vector2<T> vector, T scalar) => new Vector2<T>(vector.X * scalar, vector.Y * scalar);
    public static Vector2<T> operator *(T scalar, Vector2<T> vector) => new Vector2<T>(vector.X * scalar, vector.Y * scalar);
    public static Vector2<T> operator /(Vector2<T> vector, T scalar) => new Vector2<T>(vector.X / scalar, vector.Y / scalar);

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        return obj is Vector2<T> vector && vector == this && obj.GetHashCode() == this.GetHashCode();
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static implicit operator (T, T)(Vector2<T> vector) => (vector.X, vector.Y);
    public static implicit operator Vector2<T>((T, T) tuple) => new Vector2<T>(tuple.Item1, tuple.Item2);
}