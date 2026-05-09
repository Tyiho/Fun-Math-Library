# Fun-Math-Library

## What is this?
A c# library I built to practice linear algebra concepts. It was made for the fun of it, and does not include a test project like my normal repositories. This means that I cannot garuntee everything works correctly, but in all likelihood it should.

## What does it include?
Vectors, Angles and Matrices. The basic objects needed for linear algebra practice. 

### Vectors
There are several vector based structs and interfaces.
1) **IVector<T>** This is the interface that serves as the base for all of the vectors. Includes some built in methods like Magnitude and Dot.
2) **IVector2<T>**, **IVector3<T>** Specialized interfaces for 2 and 3 dimensional vectors respectively. These interfaces specify extra parameters and methods exclusively for 2 and 3 dimension vectors. Also defines use of x,y, and z for the components in these dimensions.
3) **Vector<T>** general vector struct for vectors of any dimension.
4) **Vector2<T>** vector struct for 2 dimensional vector. Includes conversion to tuple of size 2, and use of x and y for reading components.
5) **Vector3<T>** vector struct for 3 dimensional vector. Includes conversion to tuple of size 3, and use of x, y, and z for reading components.
6) **RadialVector2** 2 dimensional struct for representing a vector with radial coordinates. Does not have generic type, instead this vector is represented with doubles in conversions and methods.

### Angle
Created specifically for RadialVector2, there is an interface and struct to represent an angle.
1) **IAngle** The interface that serves as the base for Angle. Specifies methods such as ToDegrees and GetAngleSquared.
2) **Angle** Angle struct to represent angles. Defaults to radians, but defines a method to get *(double)* Degrees.

### Matrices
Matrices are pretty important to linear algebra (I wonder why?). Anyways, similar to Angle, there is an interface and struct to represent a Matrix.
1) **IMatrix** The interface that serves as the base for Matrix. Specifies methods that Matrix struct defines such as IsTriangular, and IsIdentity.
2) **Matrix** The struct for a matrix. Includes a multidimensional array to store values. Methods such as ToVector, GetSubMatrix, and GetDeterminant.
