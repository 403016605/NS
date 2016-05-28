using System.Reflection;

namespace NS.Kernel.Aspects
{
    internal interface INsInterceptionContext
    {
        object Target { get; }

        MethodInfo Method { get; }

        object[] Arguments { get; }

        object ReturnValue { get; }

        bool Handled { get; set; }
    }
}