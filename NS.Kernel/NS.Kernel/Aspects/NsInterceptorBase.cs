namespace NS.Kernel.Aspects
{
    internal abstract class NsInterceptorBase<TAspect> : INsInterceptor<TAspect>
    {
        public TAspect Aspect { get; set; }

        public virtual void BeforeExecution(INsBeforeExecutionInterceptionContext context)
        {
        }

        public virtual void AfterExecution(INsAfterExecutionInterceptionContext context)
        {
        }
    }
}