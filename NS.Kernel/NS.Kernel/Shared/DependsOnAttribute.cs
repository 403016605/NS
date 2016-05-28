using System;

namespace NS.Kernel.Shared
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute
    {
        /// <summary>
        ///     Used to define dependencies of an ABP module to other modules.
        /// </summary>
        /// <param name="dependedModuleTypes">Types of depended modules</param>
        public DependsOnAttribute(params Type[] dependedModuleTypes)
        {
            DependedModuleTypes = dependedModuleTypes;
        }

        /// <summary>
        ///     Types of depended modules.
        /// </summary>
        public Type[] DependedModuleTypes { get; private set; }
    }
}