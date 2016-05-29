using System;
using System.Reflection;

namespace NS.Calc
{
    public class CalcModule : NS.Kernel.Shared.NsModule
    {
        public override void PreInitialize()
        {
            Console.WriteLine("A PreInitialize...");
        }

        public override void Initialize()
        {
            Console.WriteLine("A 初始化开始...");

            this.IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());

            Console.WriteLine("A 初始化结束...");
        }

        public override void PostInitialize()
        {
            Console.WriteLine("A PostInitialize...");
        }

        public override void Shutdown()
        {
            Console.WriteLine("A Shutdown...");
        }
    }
}
