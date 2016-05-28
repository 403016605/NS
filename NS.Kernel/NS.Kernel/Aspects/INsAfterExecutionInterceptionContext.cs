using System;

namespace NS.Kernel.Aspects
{
    internal interface INsAfterExecutionInterceptionContext : INsInterceptionContext
    {
        Exception Exception { get; }
    }
}