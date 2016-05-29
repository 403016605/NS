using NS.Kernel;

namespace NS.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrap.Instance.Initialize();
            System.Console.ReadKey();
        }
    }
}
