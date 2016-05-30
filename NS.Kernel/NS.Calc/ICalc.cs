using NS.Kernel.Dependency;

namespace NS.Calc
{
    public interface ICalc : ITransientDependency
    {
        int Add(int left, int right);

    }
}