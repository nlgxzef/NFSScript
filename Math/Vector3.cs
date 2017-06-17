/*
 * Copyright (C) 2007-2010 SlimDX Group
 *
 * Permission is hereby granted, free  of charge, to any person obtaining a copy of this software  and
 * associated  documentation  files (the  "Software"), to deal  in the Software  without  restriction,
 * including  without  limitation  the  rights  to use,  copy,  modify,  merge,  publish,  distribute,
 * sublicense, and/or sell  copies of the  Software,  and to permit  persons to whom  the Software  is
 * furnished to do so, subject to the following conditions:
 *
 * The  above  copyright  notice  and this  permission  notice shall  be included  in  all  copies  or
 * substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS",  WITHOUT WARRANTY OF  ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
 * NOT  LIMITED  TO  THE  WARRANTIES  OF  MERCHANTABILITY,  FITNESS  FOR  A   PARTICULAR  PURPOSE  AND
 * NONINFRINGEMENT.  IN  NO  EVENT SHALL THE  AUTHORS  OR COPYRIGHT HOLDERS  BE LIABLE FOR  ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,  OUT
 * OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using Maths = System.Math;

namespace NFSScript.Math
{
    /// <summary>
    /// Representation of 3D vectors and points.
    /// </summary>
    public struct Vector3
    {
        /// <summary>
        /// Returns the x-coordinate of this <see cref="Vector3"/>.
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Returns the y-coordinate of this <see cref="Vector3"/>.
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Returns the z-coordinate of this <see cref="Vector3"/>.
        /// </summary>
        public float Z { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 Back
        {
            get
            {
                return new Vector3(0.0f, 0.0f, -1f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 Down
        {
            get
            {
                return new Vector3(0.0f, -1f, 0.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 Forward
        {
            get
            {
                return new Vector3(0.0f, 0.0f, 1f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 Left
        {
            get
            {
                return new Vector3(-1f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 One
        {
            get
            {
                return new Vector3(1f, 1f, 1f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 Right
        {
            get
            {
                return new Vector3(1f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 Up
        {
            get
            {
                return new Vector3(0.0f, 1f, 0.0f);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static Vector3 Zero
        {
            get
            {
                return new Vector3(0.0f, 0.0f, 0.0f);
            }
        }

        /// <summary>
        /// Returns the length of this <see cref="Vector3"/>.
        /// </summary>
        public float Magnitude
        {
            get
            {
                return (float)Maths.Sqrt(X * X + Y * Y + Z * Z);
            }
        }

        /// <summary>
        /// Returns the squared length of this <see cref="Vector3"/> (Read only).
        /// </summary>
        public float MagnitudeSquared
        {
            get
            {
                return (X * X) + (Y * Y) + (Z * Z);
            }
        }

        /// <summary>
        /// Returns this <see cref="Vector3"/> with a magnitude of 1 (Read only).
        /// </summary>
        public Vector3 Normalized
        {
            get
            {
                Vector3 norm = new Vector3(X, Y, Z);
                norm.Set(norm.X / Magnitude, norm.Y / Magnitude, norm.Z / Magnitude);

                return norm;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> class with the specified coordinates.
        /// </summary>
        public Vector3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3"/> class using coordinates specified by an interger value.
        /// </summary>
        public Vector3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        /// <summary>
        /// Set the XYZ values of this <see cref="Vector3"/>.
        /// </summary>
        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        /// <summary>
        /// Returns the distance between two vectors.
        /// </summary>
        public static float Distance(Vector3 a, Vector3 b)
        {
            Vector3 diff = new Vector3(
                  a.X - b.X,
                  a.Y - b.Y,
                  a.Z - b.Z);
            return (float)Maths.Sqrt(Maths.Pow(diff.X, 2f) + Maths.Pow(diff.Y, 2f) + Maths.Pow(diff.Z, 2f));
        }

        /// <summary>
        /// Dot product between two vectors.
        /// </summary>
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z);
        }

        /// <summary>
        /// Returns a <see cref="Vector3"/> with the same direction as the specified <see cref="Vector3"/>, but with a length of one.
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        public static Vector3 Normalize(Vector3 vector)
        {
            float l = vector.Magnitude;
            return new Vector3(vector.X / l, vector.Y / l, vector.Z / l);
        }

        /// <summary>
        /// Cross product of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            float x = a.Y * b.Z - a.Z * b.Y;
            float y = a.Z * b.X - a.X * b.Z;
            float z = a.X * b.Y - a.Y * b.X;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Linearly interpolates between two vectors.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Vector3 Lerp(Vector3 start, Vector3 end, float factor)
        {
            float x = start.X + ((end.X - start.X) * factor);
            float y = start.Y + ((end.Y - start.Y) * factor);
            float z = start.Z + ((end.Z - start.Z) * factor);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Reflects a vector off the plane defined by a normal.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        public static Vector3 Reflect(Vector3 dir, Vector3 normal)
        {
            float dubdot = 2.0f * ((dir.X * normal.X) + (dir.Y * normal.Y) + (dir.Z * normal.Z));
            float x = dir.X - (dubdot * normal.X);
            float y = dir.Y - (dubdot * normal.Y);
            float z = dir.Z - (dubdot * normal.Z);

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Projects a vector onto another vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="onNormal"></param>
        /// <returns></returns>
        public static Vector3 Project(Vector3 vector, Vector3 onNormal)
        {
            return onNormal * Dot(vector, onNormal) / Dot(onNormal, onNormal);
        }

        /// <summary>
        /// Projects a vector onto a plane defined by a normal orthogonal to the plane.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="planeNormal"></param>
        /// <returns></returns>
        public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
        {
            return (vector - Project(vector, planeNormal));
        }

        /// <summary>
        /// Returns the angle in degrees between from and to.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float Angle(Vector3 from, Vector3 to)
        {
            float dot = Dot(from.Normalized, to.Normalized);

            return (float)Maths.Acos((dot) * (180.0 / Mathf.PI));
        }

        /// <summary>
        /// Returns a vector that is made from the smallest components of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            float x = (a.X < b.X) ? a.X : b.X;
            float y = (a.Y < b.Y) ? a.Y : b.Y;
            float z = (a.Z < b.Z) ? a.Z : b.Z;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Returns a vector that is made from the largest components of two vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            float x = (a.X > b.X) ? a.X : b.X;
            float y = (a.Y > b.Y) ? a.Y : b.Y;
            float z = (a.Z > b.Z) ? a.Z : b.Z;

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Moves a point current in a straight line towards a target point.
        /// </summary>
        /// <returns></returns>
        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float t)
        {
            Vector3 x = new Vector3(current.X, current.Y, current.Z);

            Vector3 direction = x - target;
            direction = direction.Normalized;

            return x -= direction * t;
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Vector3"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2 ^ Z.GetHashCode() >> 2;
        }

        /// <summary>
        /// Returns a formmated <see cref="Vector3"/> string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("X = {0} Y = {1} Z = {2}", X, Y, Z);
        }

        /// <summary>
        /// Returns a formmated <see cref="Vector3"/> string with a specified number format.
        /// </summary>
        /// <returns></returns>
        public string ToString(string numberFormat)
        {
            return string.Format("X = {0} Y = {1} Z = {2}", X.ToString(numberFormat), Y.ToString(numberFormat), Z.ToString(numberFormat));
        }

        /// <summary>
        /// Add a value to a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 l, Vector3 r)
        {
            return new Vector3(l.X + r.X, l.Y + r.Y, l.Z + r.Z);
        }

        /// <summary>
        /// Add a value to a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator +(Vector3 l, float r)
        {
            return new Vector3(l.X + r, l.Y + r, l.Z + r);
        }

        /// <summary>
        /// Add a value to a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator +(float l, Vector3 r)
        {
            return new Vector3(r.X + l, r.Y + l, r.Z + l);
        }

        /// <summary>
        /// Subtract a value from a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 l, Vector3 r)
        {
            return new Vector3(l.X - r.X, l.Y - r.Y, l.Z - r.Z);
        }

        /// <summary>
        /// Subtract a value from a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator -(Vector3 l, float r)
        {
            return new Vector3(l.X - r, l.Y - r, l.Z - r);
        }

        /// <summary>
        /// Subtract a value from a Vector3.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator -(float l, Vector3 r)
        {
            return new Vector3(r.X - l, r.Y - l, r.Z - l);
        }

        /// <summary>
        /// Multiply a Vector3 with a value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 l, float r)
        {
            return new Vector3(l.X * r, l.Y * r, l.Z * r);
        }

        /// <summary>
        /// Multiply a Vector3 with a value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator *(float l, Vector3 r)
        {
            return new Vector3(r.X * l, r.Y * l, r.Z * l);
        }

        /// <summary>
        /// Multiply two Vector3 values.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator *(Vector3 l, Vector3 r)
        {
            return new Vector3(r.X * l.X, r.Y * l.Y, r.Z * l.Z);
        }

        /// <summary>
        /// Divide a Vector3 value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 l, float r)
        {
            return new Vector3(l.X / r, l.Y / r, l.Z / r);
        }

        /// <summary>
        /// Divide a Vector3 value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator /(float l, Vector3 r)
        {
            return new Vector3(r.X / l, r.Y / l, r.Z / l);
        }

        /// <summary>
        /// Divide a Vector3 value.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 l, Vector3 r)
        {
            return new Vector3(r.X / l.X, r.Y / l.Y, r.Z / l.Z);
        }

        /// <summary>
        /// Returns a value determining whether the two Vector3 values are equal.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator ==(Vector3 l, Vector3 r)
        {
            return Equals(l, r);
        }

        /// <summary>
        /// Returns a value determining whether the two Vector3 values aren't equal.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator !=(Vector3 l, Vector3 r)
        {
            return !Equals(l, r);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector3"/> values are equal.
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public static bool Equals(Vector3 position1, Vector3 position2)
        {
            return (position1.X == position2.X && position1.Y == position2.Y && position1.Z == position2.Z);
        }

        /// <summary>
        /// Determines whether the specified value is equal to the current <see cref="Vector3"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Equals(float value)
        {
            return (X == value && Y == value && Z == value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector3"/> is equal to the current <see cref="Vector3"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vector3) || obj == null)
                return false;
            else return Equals(this, (Vector3)obj);
        }
    }
}
