using static NFSScript.World.EASharpBindings;

namespace NFSScript.World
{
    /// <summary>
    /// Render class.
    /// </summary>
    public static class Render
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public static void TriggerEffect(EffectType type)
        {
            CallBinding(_EASharpBinding_422, (int)type);
        }
    }
}
