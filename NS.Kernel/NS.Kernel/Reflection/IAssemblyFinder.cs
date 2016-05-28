using System.Collections.Generic;
using System.Reflection;

namespace NS.Kernel.Reflection
{
    public interface IAssemblyFinder
    {
        /// <summary>
        ///     获取当前程序所使用的所有程序集.
        /// </summary>
        /// <returns>程序集集合</returns>
        List<Assembly> GetAllAssemblies();
    }
}