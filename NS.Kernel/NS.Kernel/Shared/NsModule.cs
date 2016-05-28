using System;
using System.Collections.Generic;
using System.Linq;
using NS.Kernel.Dependency;
using NS.Kernel.Exceptions;

namespace NS.Kernel.Shared
{
    public abstract class NsModule
    {
        /// <summary>
        ///     Gets a reference to the IOC manager.
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        ///     This is the first event called on application startup.
        ///     Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        public virtual void PreInitialize()
        {
        }

        /// <summary>
        ///     This method is used to register dependencies for this module.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        ///     This method is called lastly on application startup.
        /// </summary>
        public virtual void PostInitialize()
        {
        }

        /// <summary>
        ///     This method is called when the application is being shutdown.
        /// </summary>
        public virtual void Shutdown()
        {
        }

        /// <summary>
        ///     Checks if given type is an Abp module class.
        /// </summary>
        /// <param name="type">Type to check</param>
        public static bool IsNsModule(Type type)
        {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof (NsModule).IsAssignableFrom(type);
        }

        /// <summary>
        ///     Finds depended modules of a module.
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IsNsModule(moduleType))
            {
                throw new NsInitializationException("This type is not an NS module: " + moduleType.AssemblyQualifiedName);
            }

            var list = new List<Type>();

            if (moduleType.IsDefined(typeof (DependsOnAttribute), true))
            {
                var dependsOnAttributes =
                    moduleType.GetCustomAttributes(typeof (DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }

            return list;
        }
    }
}