using System;
using System.Collections.Generic;
using System.Linq;

namespace NS.Kernel.Modules
{
    public interface IModuleFinder
    {
        /// <summary>
        /// Finds all modules.
        /// </summary>
        /// <returns>List of modules</returns>
        ICollection<Type> FindAll();
    }
}
