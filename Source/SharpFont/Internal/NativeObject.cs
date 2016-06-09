using System;

namespace SharpFont
{
    public abstract class NativeObject
    {
        private IntPtr reference;

        protected NativeObject(IntPtr reference)
        {
            this.reference = reference;
        }

        internal virtual IntPtr Reference
        {
            get
            {
                return reference;
            }
            set
            {
                reference = value;
            }
        }
    }
}