using System;

namespace NS.Kernel.Aspects
{
    internal abstract class AspectAttribute : Attribute
    {
        protected AspectAttribute(Type interceptorType)
        {
            InterceptorType = interceptorType;
        }

        public Type InterceptorType { get; set; }
    }
}