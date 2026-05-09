using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Numerics;
using System.Text;

namespace Fun_Math_Library.Interfaces;

public interface IMatrix<out T> where T : INumber<T>
{
    T GetDeterminant();
    bool IsSquare();
    bool IsLowerTriangular();
    bool IsUpperTriangular();
    bool IsTriangular();
    bool IsDiagonal();
    bool IsZeroMatrix();
    bool IsIdentityMatrix();
    int RowCount { get; }
    int ColumnCount { get; }
    T this[int row, int column] { get; }

}