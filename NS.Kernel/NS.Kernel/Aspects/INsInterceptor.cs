namespace NS.Kernel.Aspects
{
    internal interface INsInterceptor<TAspect>
    {
        TAspect Aspect { get; set; }

        void BeforeExecution(INsBeforeExecutionInterceptionContext context);

        void AfterExecution(INsAfterExecutionInterceptionContext context);
    }
}