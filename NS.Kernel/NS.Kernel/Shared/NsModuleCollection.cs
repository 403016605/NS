using System.Collections.Generic;
using System.Linq;
using NS.Kernel.Exceptions;
using NS.Kernel.Extensions;

namespace NS.Kernel.Shared
{
    public class NsModuleCollection : List<NsModuleInfo>
    {
        /// <summary>
        ///     Gets a reference to a module instance.
        /// </summary>
        /// <typeparam name="TModule">Module type</typeparam>
        /// <returns>Reference to the module instance</returns>
        public TModule GetModule<TModule>() where TModule : NsModule
        {
            var module = this.FirstOrDefault(m => m.Type == typeof (TModule));
            if (module == null)
            {
                throw new NsException("Can not find module for " + typeof (TModule).FullName);
            }

            return (TModule) module.Instance;
        }

        /// <summary>
        ///     Sorts modules according to dependencies.
        ///     If module A depends on module B, A comes after B in the returned List.
        /// </summary>
        /// <returns>Sorted list</returns>
        public List<NsModuleInfo> GetSortedModuleListByDependency()
        {
            var sortedModules = this.SortByDependencies(x => x.Dependencies);
            EnsureKernelModuleToBeFirst(sortedModules);
            return sortedModules;
        }

        private static void EnsureKernelModuleToBeFirst(List<NsModuleInfo> sortedModules)
        {
            var kernelModuleIndex = sortedModules.FindIndex(m => m.Type == typeof (NsKernelModule));
            if (kernelModuleIndex > 0)
            {
                var kernelModule = sortedModules[kernelModuleIndex];
                sortedModules.RemoveAt(kernelModuleIndex);
                sortedModules.Insert(0, kernelModule);
            }
        }
    }
}