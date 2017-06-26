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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Maths = System.Math;

namespace NFSScript.Math
{
    /// <summary>
    /// Representation of 2D vectors and points.
    /// </summary>
    public struct Vector2
    {
        /// <summary>
        /// Returns the x-coordinate of this <see cref="Vector2"/>.
        /// </summary>
        public float X { get; private set; }

        /// <summary>
        /// Returns the y-coordinate of this <see cref="Vector2"/>.
        /// </summary>
        public float Y { get; private set; }

        /// <summary>
        /// Constructs a new <see cref="Vector2"/> with given x, y components.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Returns the length of this <see cref="Vector2"/>.
        /// </summary>
        public float Length
        {
            get
            {
                return (float)Maths.Sqrt((X * X) + (Y * Y));
            }
        }

        /// <summary>
        /// Returns the length of this <see cref="Vector2"/> squared.
        /// </summary>
        public float LengthSquared
        {
            get
            {
                return (X * X) + (Y * Y);
            }
        }

        /// <summary>
        /// Makes this vector have a magnitude of 1.
        /// </summary>
        public void Normalize()
        {
            float length = Length;
            if (length == 0) return;

            float num = 1 / length;
            X *= num;
            Y *= num;
        }

        /// <summary>
        /// Returns the distance to a position.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public float DistanceTo(Vector2 position)
        {
            return (position - this).Length;
        }

        /// <summary>
        /// Returns the squared distance from <see cref="Vector2"/> and to the position defined.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public float DistanceToSquared(Vector2 position)
        {
            return DistanceSquared(position, this);
        }

        /// <summary>
        /// Returns the distance between a and b.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float Distance(Vector2 a, Vector2 b)
        {
            return (a - b).Length;
        }

        /// <summary>
        /// Returns the squared distance between a and b.
        /// </summary>
        /// <param name="posA"></param>
        /// <param name="posB"></param>
        /// <returns></returns>
        public static float DistanceSquared(Vector2 posA, Vector2 posB)
        {
            return (posA - posB).LengthSquared;
        }

        /// <summary>
        /// Returns the angle in degrees between from and to.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float Angle(Vector2 from, Vector2 to)
        {
            return Maths.Abs(SignedAngle(from, to));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static float SignedAngle(Vector2 from, Vector2 to)
        {
            return (float)((Maths.Atan2(to.Y, to.X) - Maths.Atan2(from.Y, from.X)) * (180.0 / Maths.PI));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public float ToHeading()
        {
            return (float)((Maths.Atan2(X, -Y) + Maths.PI) * (180.0 / Maths.PI));
        }

        /// <summary>
        /// Clamps a value between min and max
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public Vector2 Clamp(Vector2 value, Vector2 min, Vector2 max)
        {
            float x = value.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;

            float y = value.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;

            return new Vector2(x, y);
        }

        /// <summary>
        /// Linearly interpolates between vectors start and end by a factor.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public Vector2 Lerp(Vector2 start, Vector2 end, float factor)
        {
            Vector2 vector = new Vector2();

            vector.X = start.X + ((end.X - start.X) * factor);
            vector.Y = start.Y + ((end.Y - start.Y) * factor);

            return vector;
        }

        /// <summary>
        /// Dot Product of two vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float Dot(Vector2 left, Vector2 right)
        {
            return (left.X * right.X + left.Y * right.Y);
        }

        /// <summary>
        /// Reflects a vector off the vector defined by a normal.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        public static Vector2 Reflect(Vector2 vector, Vector2 normal)
        {
            Vector2 result = new Vector2();
            float dot = ((vector.X * normal.X) + (vector.Y * normal.Y));

            result.X = vector.X - ((2.0f * dot) * normal.X);
            result.Y = vector.Y - ((2.0f * dot) * normal.Y);

            return result;
        }

        /// <summary>
        /// Returns a vector that is made from the smallest components of two vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 Minimize(Vector2 left, Vector2 right)
        {
            Vector2 vector = new Vector2();
            vector.X = (left.X < right.X) ? left.X : right.X;
            vector.Y = (left.Y < right.Y) ? left.Y : right.Y;

            return vector;
        }

        /// <summary>
        /// Returns a vector that is made from the largest components of two vectors.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 Maximize(Vector2 left, Vector2 right)
        {
            Vector2 vector = new Vector2();
            vector.X = (left.X > right.X) ? left.X : right.X;
            vector.Y = (left.Y > right.Y) ? left.Y : right.Y;
            return vector;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Vector2 operator -(Vector2 value)
        {
            return new Vector2(-value.X, -value.Y);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector2 operator *(Vector2 value, float scale)
        {
            return new Vector2(value.X * scale, value.Y * scale);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static Vector2 operator /(Vector2 value, float scale)
        {
            return new Vector2(value.X / scale, value.Y / scale);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator != (Vector2 left, Vector2 right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Vector2"/>.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() << 2;
        }

        /// <summary>
        /// Returns a formmated <see cref="Vector2"/> string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("X = {0} Y = {1}", X, Y);
        }

        /// <summary>
        /// Returns a formmated <see cref="Vector2"/> string with a specified number format.
        /// </summary>
        /// <returns></returns>
        public string ToString(string numberFormat)
        {
            return string.Format("X = {0} Y = {1} Z = {2}", X.ToString(numberFormat), Y.ToString(numberFormat));
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector2"/> values are equal.
        /// </summary>
        /// <param name="position1"></param>
        /// <param name="position2"></param>
        /// <returns></returns>
        public static bool Equals(Vector2 position1, Vector2 position2)
        {
            return (position1.X == position2.X && position1.Y == position2.Y);
        }

        /// <summary>
        /// Determines whether the specified value is equal to the current <see cref="Vector2"/>.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Equals(float value)
        {
            return (X == value && Y == value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="Vector2"/> is equal to the current <see cref="Vector2"/>.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Vector2) || obj == null)
                return false;
            else return Equals(this, (Vector2)obj);
        }
    }
}