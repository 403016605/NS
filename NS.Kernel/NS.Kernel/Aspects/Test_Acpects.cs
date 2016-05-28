namespace NS.Kernel.Aspects
{
    internal class Test_Acpects
    {
        internal class MyAspectAttribute : AspectAttribute
        {
            public MyAspectAttribute()
                : base(typeof (MyInterceptor))
            {
            }

            public int TestValue { get; set; }
        }

        internal class MyInterceptor : NsInterceptorBase<MyAspectAttribute>
        {
            public override void BeforeExecution(INsBeforeExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }

            public override void AfterExecution(INsAfterExecutionInterceptionContext context)
            {
                Aspect.TestValue++;
            }
        }

        public class MyService
        {
            [MyAspect(TestValue = 41)] //Usage!
            public void DoIt()
            {
            }
        }

        public class MyClient
        {
            private readonly MyService _service;

            public MyClient(MyService service)
            {
                _service = service;
            }

            public void Test()
            {
                _service.DoIt();
            }
        }
    }
}