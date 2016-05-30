using System;
using System.Reflection;

namespace NS.Calc
{
    public class CalcModule : NS.Kernel.Shared.NsModule
    {
        public override void PreInitialize()
        {
            Console.WriteLine("初始化【之前】...");
        }

        public override void Initialize()
        {
            Console.WriteLine("初始化...");

            this.IocManager.RegisterAssembly(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            Console.WriteLine($"{this.IocManager.Resolve<ICalc>().Add(1, 1)}");

            Console.WriteLine("初始化【之前】...");
        }

        public override void Shutdown()
        {
            Console.WriteLine("关闭...");
        }
    }
}
