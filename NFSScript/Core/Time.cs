using System;
using NFSScript.Core;

namespace NFSScript
{
    /// <summary>
    /// 
    /// </summary>
    public static class Time
    {
        /// <summary>
        /// Returns NFSScript's run time in milliseconds.
        /// </summary>
        public static float Runtime
        {
            get
            {
                return (float)(DateTime.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime()).TotalMilliseconds;
            }
        }

        /// <summary>
        /// Returns the game's run time in milliseconds.
        /// </summary>
        public static float GameRuntime
        {
            get
            {
                return (float)(DateTime.UtcNow - GameMemory.memory.GetMainProcess().StartTime.ToUniversalTime()).TotalMilliseconds;
            }
        }

        /// <summary>
        /// Returns a float that moves back and foruth.
        /// </summary>
        /// <param name="speed">How fast the ping pong occurs</param>
        /// <param name="t">Factor</param>
        /// <returns></returns>
        public static float PingPong(float speed, float t)
        {            
            return (float)(System.Math.Sin(t * speed) + 1) / 2.0f;
        }
    }
}
