using System;
using System.Collections.Generic;
using System.Reflection;

namespace NS.Kernel.Shared
{
    public class NsModuleInfo
    {
        /// <summary>
        ///     Creates a new AbpModuleInfo object.
        /// </summary>
        /// <param name="instance"></param>
        public NsModuleInfo(NsModule instance)
        {
            Dependencies = new List<NsModuleInfo>();
            Type = instance.GetType();
            Instance = instance;
            Assembly = Type.Assembly;
        }

        /// <summary>
        ///     The assembly which contains the module definition.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        ///     Type of the module.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        ///     Instance of the module.
        /// </summary>
        public NsModule Instance { get; private set; }

        /// <summary>
        ///     All dependent modules of this module.
        /// </summary>
        public List<NsModuleInfo> Dependencies { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}", Type.AssemblyQualifiedName);
        }
    }
}