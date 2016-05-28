using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NS.Kernel.Shared
{
    public class NsModuleInfo
    {

        /// <summary>
        /// The assembly which contains the module definition.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Type of the module.
        /// </summary>
        public Type Type { get; private set; }

        /// <summary>
        /// Instance of the module.
        /// </summary>
        public NsModule Instance { get; private set; }

        /// <summary>
        /// All dependent modules of this module.
        /// </summary>
        public List<NsModuleInfo> Dependencies { get; private set; }

        /// <summary>
        /// Creates a new AbpModuleInfo object.
        /// </summary>
        /// <param name="instance"></param>
        public NsModuleInfo(NsModule instance)
        {
            this.Dependencies = new List<NsModuleInfo>();
            this.Type = instance.GetType();
            this.Instance = instance;
            this.Assembly = Type.Assembly;
        }

        public override string ToString()
        {
            return string.Format(format: "{0}", arg0: Type.AssemblyQualifiedName);
        }
    }
}