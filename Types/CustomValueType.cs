using System.Collections.Generic;

namespace NFSScript.Types
{
    /// <summary/>
    public class CustomValueType<TCustom, TValue>
    {
        /// <summary>
        /// A value
        /// </summary>
        protected readonly TValue value;

        /// <summary/>
        public CustomValueType(TValue value)
        {
            this.value = value;
        }

        /// <summary/>
        public static bool operator <(CustomValueType<TCustom, TValue> a, CustomValueType<TCustom, TValue> b)
        {
            return Comparer<TValue>.Default.Compare(a.value, b.value) < 0;
        }

        /// <summary/>
        public static bool operator >(CustomValueType<TCustom, TValue> a, CustomValueType<TCustom, TValue> b)
        {
            return !(a < b);
        }

        /// <summary/>
        public static bool operator <=(CustomValueType<TCustom, TValue> a, CustomValueType<TCustom, TValue> b)
        {
            return (a < b) || (a == b);
        }

        /// <summary/>
        public static bool operator >=(CustomValueType<TCustom, TValue> a, CustomValueType<TCustom, TValue> b)
        {
            return (a > b) || (a == b);
        }

        /// <summary/>
        public static bool operator ==(CustomValueType<TCustom, TValue> a, CustomValueType<TCustom, TValue> b)
        {
            return a.Equals((object)b);
        }

        /// <summary/>
        public static bool operator !=(CustomValueType<TCustom, TValue> a, CustomValueType<TCustom, TValue> b)
        {
            return !(a == b);
        }

        /// <summary/>
        public static TCustom operator +(CustomValueType<TCustom, TValue> a, CustomValueType<TCustom, TValue> b)
        {
            return (dynamic)a.value + b.value;
        }

        /// <summary/>
        public static TCustom operator -(CustomValueType<TCustom, TValue> a, CustomValueType<TCustom, TValue> b)
        {
            return ((dynamic)a.value - b.value);
        }

        /// <summary/>
        protected bool Equals(CustomValueType<TCustom, TValue> other)
        {
            return EqualityComparer<TValue>.Default.Equals(value, other.value);
        }

        /// <summary/>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CustomValueType<TCustom, TValue>)obj);
        }

        /// <summary/>
        public override int GetHashCode()
        {
            return EqualityComparer<TValue>.Default.GetHashCode(value);
        }

        /// <summary/>
        public override string ToString()
        {
            return value.ToString();
        }
    }
}
