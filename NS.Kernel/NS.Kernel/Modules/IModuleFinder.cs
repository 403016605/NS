using System;
using System.Collections.Generic;

namespace NS.Kernel.Modules
{
    public interface IModuleFinder
    {
        /// <summary>
        ///     Finds all modules.
        /// </summary>
        /// <returns>List of modules</returns>
        ICollection<Type> FindAll();
    }
}