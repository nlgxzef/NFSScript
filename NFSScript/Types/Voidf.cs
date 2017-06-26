using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFSScript.Types;

namespace NFSScript
{
    /// <summary>
    /// Void.
    /// </summary>
    public class Voidf : CustomValueType<Voidf, Voidf>
    {
        private Voidf(Voidf value) : base(value)
        { }
    }
}
