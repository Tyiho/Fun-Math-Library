using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Fun_Math_Library.Interfaces;

namespace Fun_Math_Library.Structs;

public readonly struct Matrix<T> : IMatrix<T> where T : INumber<T>
{
    private T[,] _entries { get; }
    public T[,] Entries => (T[,])_entries.Clone();
    public T this[int row, int column] => _entries[row,column];

    public int RowCount => _entries.GetLength(0);

    public int ColumnCount => _entries.GetLength(1);

    public IVector<T> GetColumn(int columnIndex)
    {
        if (columnIndex < 0 || columnIndex >= ColumnCount) throw new ArgumentOutOfRangeException(nameof(columnIndex));
        T[] column = new T[RowCount];
        for (int i = 0; i < RowCount; i++)
        {
            column[i] = _entries[i, columnIndex];
        }
        return new Vector<T>(column);
    }

    public IVector<T> GetRow(int rowIndex)
    {
        if (rowIndex < 0 || rowIndex >= RowCount) throw new ArgumentOutOfRangeException(nameof(rowIndex));
        T[] row = new T[ColumnCount];
        for (int j = 0; j < ColumnCount; j++)
        {
            row[j] = _entries[rowIndex, j];
        }
        return new Vector<T>(row);
    }

    public Matrix<T> GetTranspose()
    {
        T[,] transposedEntries = new T[ColumnCount, RowCount];
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                transposedEntries[j, i] = _entries[i, j];
            }
        }
        return new Matrix<T>(transposedEntries);
    }

    public Matrix<T> GetSubMatrix(int rowIndex, int columnIndex, int rowCount, int columnCount)
    {
        if (rowIndex < 0 || rowIndex >= RowCount) throw new ArgumentOutOfRangeException(nameof(rowIndex));
        if (columnIndex < 0 || columnIndex >= ColumnCount) throw new ArgumentOutOfRangeException(nameof(columnIndex));
        if (rowCount <= 0 || rowIndex + rowCount > RowCount) throw new ArgumentOutOfRangeException(nameof(rowCount));
        if (columnCount <= 0 || columnIndex + columnCount > ColumnCount) throw new ArgumentOutOfRangeException(nameof(columnCount));

        T[,] subMatrixEntries = new T[rowCount, columnCount];
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                subMatrixEntries[i, j] = _entries[rowIndex + i, columnIndex + j];
            }
        }
        return new Matrix<T>(subMatrixEntries);
    }

    public Matrix<T> GetSubMatrix(int rowIndex, int columnIndex, int rowCount, int columnCount, int rowToSkip, int columnToSkip)
    {
        if (rowIndex < 0 || rowIndex >= RowCount) throw new ArgumentOutOfRangeException(nameof(rowIndex));
        if (columnIndex < 0 || columnIndex >= ColumnCount) throw new ArgumentOutOfRangeException(nameof(columnIndex));
        if (rowCount <= 0 || rowIndex + rowCount > RowCount) throw new ArgumentOutOfRangeException(nameof(rowCount));
        if (columnCount <= 0 || columnIndex + columnCount > ColumnCount) throw new ArgumentOutOfRangeException(nameof(columnCount));
        if(rowToSkip < rowIndex || rowToSkip > rowIndex + rowCount) throw new ArgumentOutOfRangeException(nameof(rowToSkip));
        if(columnToSkip < columnIndex || columnToSkip > columnIndex + columnCount) throw new ArgumentOutOfRangeException(nameof(columnToSkip));


        T[,] subMatrixEntries = new T[rowCount - 1, columnCount - 1];
        for (int i = 0; i < rowCount; i++)
        {
            if(i + rowIndex == rowToSkip) continue; // Skip the specified row
            for (int j = 0; j < columnCount; j++)
            {
                if(j + columnIndex == columnToSkip) continue; // Skip the specified column

                //offset the row and column indices to account for the skipped row and column
                int row = i;
                int column = j;
                if (j + columnIndex > columnToSkip) column--;
                if (i + rowIndex > rowToSkip) row--;

                subMatrixEntries[row, column] = _entries[rowIndex + i, columnIndex + j];
            }
        }
        return new Matrix<T>(subMatrixEntries);
    }

    public T GetDeterminant()
    {
        if (!IsSquare()) throw new InvalidOperationException("Determinant is only defined for square matrices.");
        if (RowCount == 1) return _entries[0, 0];
        if (RowCount == 2) return _entries[0, 0] * _entries[1, 1] - _entries[0, 1] * _entries[1, 0];
        T determinant = T.Zero;
        for (int j = 0; j < ColumnCount; j++)
        {
            determinant += (j % 2 == 0 ? T.One : -T.One) * _entries[0, j] * GetSubMatrix(1, 0, RowCount - 1, ColumnCount - 1, 0, j).GetDeterminant();
        }
        return determinant;
    }

    public bool IsDiagonal()
    {
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                if (i != j && _entries[i, j] != T.Zero)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsIdentityMatrix()
    {
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColumnCount; j++)
            {
                if (i != j && _entries[i, j] != T.Zero)
                {
                    return false;
                }
                if (i == j && _entries[i, j] != T.One)
                {
                    return false;
                }
            }
        }
        return true; 
    }

    public bool IsSquare() => RowCount == ColumnCount;

    public bool IsLowerTriangular()
    {
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = i + 1; j < ColumnCount; j++)
            {
                if (_entries[i, j] != T.Zero)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsUpperTriangular()
    {
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < i; j++)
            {
                if (_entries[i, j] != T.Zero)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsTriangular() => IsLowerTriangular() || IsUpperTriangular();

    public bool IsZeroMatrix()
    {
        foreach (var entry in _entries)
        {
            if(entry != T.Zero)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsSymmetric()
    {
        if (!IsSquare()) return false;
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = i + 1; j < ColumnCount; j++)
            {
                if (_entries[i, j] != _entries[j, i])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public bool IsVector() => ColumnCount == 1;

    public IVector<T> ToVector()
    {
        if (!IsVector()) throw new InvalidOperationException("Matrix is not a vector.");
        return GetColumn(0);
    }

    public Matrix(T[,] entries)
    {
        _entries = entries;
    }
    
    public Matrix(IVector<T>[] columns)
    {
        int columnCount = columns.Length;
        int rowCount = columns[0].Dimension;
        _entries = new T[rowCount, columnCount];

        //for each column
        for (int j = 0; j < columnCount; j++)
        {
            //check if the corresponding vector is of correct size
            if (columns[j].Dimension != rowCount) throw new ArgumentException("All columns must have the same number of rows.");

            //then loop through rows and assign the values to the matrix
            for (int i = 0; i < rowCount; i++)
            {
                _entries[i, j] = columns[j].Components[i];
            }
        }
    }

    public Matrix(IVector<T> vector) : this([ vector ])
    {
        if (vector.Dimension == 0) throw new ArgumentException("Vector must have at least one component.");
    }

    public Matrix(IMatrix<T> matrix)
    {
        int rowCount = matrix.RowCount;
        int columnCount = matrix.ColumnCount;
        _entries = new T[rowCount, columnCount];

        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < columnCount; j++)
            {
                _entries[i, j] = matrix[i, j];
            }
        }
    }
}