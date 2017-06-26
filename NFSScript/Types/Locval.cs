namespace NFSScript
{
    /// <summary/>
    public struct Locval
    {
        /// <summary/>
        public int value { get; private set; }

        /// <summary/>
        public Locval(int value)
        {
            this.value = value;
        }

        /// <summary/>
        public static implicit operator int(Locval instance)
        {
            return instance.value;
        }

        /// <summary/>
        public static implicit operator Locval(int value)
        {
            return new Locval(value);
        }
    }
}
