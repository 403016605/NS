using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NS.Kernel.Reflection
{
    public interface IAssemblyFinder
    {
        /// <summary>
        /// This method should return all assemblies used by application.
        /// </summary>
        /// <returns>List of assemblies</returns>
        List<Assembly> GetAllAssemblies();
    }
}
