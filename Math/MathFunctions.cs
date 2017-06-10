using System;

namespace NFSScript.Math
{
    /// <summary>
    /// Math functions!
    /// </summary>
    public static class Mathf
    {
        /// <summary>
        /// Returns 3.14159265358979, you know... PI.
        /// </summary>
        public const float PI = 3.14159265358979323846f;

        /// <summary>
        /// Degrees-to-radians conversion constant (Read only).
        /// </summary>
        public static float Deg2Rad = (PI * 2) / 360;

        /// <summary>
        /// Radians-to-degrees conversion constant (Read only).
        /// </summary>
        public static float Rad2Deg = 360 / (PI * 2);

        /// <summary>
        /// A small floating point value, a VERY small floating point value.
        /// </summary>
        public const float Epsilon = 1.192093E-07F;

        /// <summary>
        /// 
        /// </summary>
        public const float SingleMileToKM = 1.609344f;

        /// <summary>
        /// Converts the given <paramref name="speed"/> using the given <paramref name="speedMeasurementConversionType"/>
        /// </summary>
        /// <param name="speed">The speed.</param>
        /// <param name="speedMeasurementConversionType">The conversion type to use.</param>
        /// <returns>The converted speed if succeeded, else 0.</returns>
        public static float ConvertSpeed(float speed, SpeedMeasurementConversionTypes speedMeasurementConversionType)
        {
            switch (speedMeasurementConversionType)
            {
                case SpeedMeasurementConversionTypes.MPSToKPH:
                    return speed * SpeedMeasurementConversions.SingleMPSToKPH;
                case SpeedMeasurementConversionTypes.MPSToMPH:
                    return speed * SpeedMeasurementConversions.SingleMPSToMPH;
                case SpeedMeasurementConversionTypes.KPHToMPS:
                    return speed * SpeedMeasurementConversions.SingleKPHToMPS;
                case SpeedMeasurementConversionTypes.KPHToMPH:
                    return speed * SpeedMeasurementConversions.SingleKPHToMPH;
                case SpeedMeasurementConversionTypes.MPHToMPS:
                    return speed * SpeedMeasurementConversions.SingleMPHToMPS;
                case SpeedMeasurementConversionTypes.MPHToKMH:
                    return speed * SpeedMeasurementConversions.SingleMPHToKPH;
            }
            return 0f;
        }

        /// <summary>
        /// Converts degree to radian.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double DegreeToRadian(double angle)
        {
            return PI * angle / 180.0;
        }

        /// <summary>
        /// Converts radian to degrees.
        /// </summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static double RadianToDegree(double angle)
        {
            return angle * (180.0 / PI);
        }

        /// <summary>
        /// 
        /// </summary>
        public static float ByteToFloat(byte b)
        {
            return b / byte.MaxValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public static byte FloatToByte(float f)
        {
            float b = f * byte.MaxValue;
            if (b > byte.MaxValue)
                b = byte.MaxValue;
            else if (b < byte.MinValue)
                b = byte.MinValue;
            return (byte)b;
        }

        /// <summary>
        /// Returns the absolute of value of a float.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static float Abs(float number)
        {
            if (number < 0)
                number = -number;
            return number;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SpeedMeasurements
    {
        /// <summary>
        /// m/s
        /// </summary>
        MetresPerSecond,

        /// <summary>
        /// km/h
        /// </summary>
        KilometresPerHour,

        /// <summary>
        /// mp/h
        /// </summary>
        MilesPerHour
    }

    /// <summary>
    /// 
    /// </summary>
    public enum SpeedMeasurementConversionTypes
    {
        /// <summary>
        /// m/s to km/h
        /// </summary>
        MPSToKPH,
        /// <summary>
        /// m/s to mp/h
        /// </summary>
        MPSToMPH,

        /// <summary>
        /// km/h to m/s
        /// </summary>
        KPHToMPS,
        /// <summary>
        /// km/h to mp/h
        /// </summary>
        KPHToMPH,

        /// <summary>
        /// mp/h to m/s
        /// </summary>
        MPHToMPS,
        /// <summary>
        /// mp/h to km/h
        /// </summary>
        MPHToKMH
    }

    /// <summary>
    /// 
    /// </summary>
    public static class SpeedMeasurementConversions
    {
        /// <summary>
        /// 1 m/s for km/h
        /// </summary>
        public static readonly float SingleMPSToKPH = 3.6f;

        /// <summary>
        /// 1 m/s for mp/h
        /// </summary>
        public static readonly float SingleMPSToMPH = 2.236936f;

        /// <summary>
        /// 1 km/h for m/s
        /// </summary>
        public static readonly float SingleKPHToMPS = 0.277778f;

        /// <summary>
        /// 1 km/h for mp/h
        /// </summary>
        public static readonly float SingleKPHToMPH = 0.621371f;

        /// <summary>
        /// 1 mp/h for m/s
        /// </summary>
        public static readonly float SingleMPHToMPS = 0.44704f;

        /// <summary>
        /// 1 mp/h for km/h
        /// </summary>
        public static readonly float SingleMPHToKPH = 1.609344f;
    }
}
