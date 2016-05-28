using System;
using System.Collections.Generic;
using System.Linq;
using NS.Kernel.Reflection;
using NS.Kernel.Shared;

namespace NS.Kernel.Modules.Impl
{
    internal class ModuleFinder : IModuleFinder
    {
        private readonly ITypeFinder typeFinder;

        public ModuleFinder(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }

        public ICollection<Type> FindAll()
        {
            return typeFinder.Find(NsModule.IsNsModule).ToList();
        }
    }
}