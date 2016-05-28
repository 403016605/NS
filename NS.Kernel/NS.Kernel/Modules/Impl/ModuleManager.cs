using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Logging;
using NS.Kernel.Dependency;
using NS.Kernel.Exceptions;
using System.Reflection;
using NS.Kernel.Shared;

namespace NS.Kernel.Modules.Impl
{
    class ModuleManager : IModuleManager
    {
        public ILogger Logger { get; set; }

        private readonly NsModuleCollection _modules;

        private readonly IIocManager _iocManager;
        private readonly IModuleFinder _moduleFinder;

        public ModuleManager(IIocManager iocManager, IModuleFinder moduleFinder)
        {
            this._modules = new NsModuleCollection();
            this._iocManager = iocManager;
            this._moduleFinder = moduleFinder;
            this.Logger = NullLogger.Instance;
        }

        public virtual void InitializeModules()
        {
            LoadAll();

            List<NsModuleInfo> sortedModules = _modules.GetSortedModuleListByDependency();

            sortedModules.ForEach(module => module.Instance.PreInitialize());
            sortedModules.ForEach(module => module.Instance.Initialize());
            sortedModules.ForEach(module => module.Instance.PostInitialize());
        }

        public virtual void ShutdownModules()
        {
            List<NsModuleInfo> sortedModules = _modules.GetSortedModuleListByDependency();
            sortedModules.Reverse();
            sortedModules.ForEach(sm => sm.Instance.Shutdown());
        }

        private void LoadAll()
        {
            Logger.Debug("Loading Ns modules...");

            ICollection<Type> moduleTypes = AddMissingDependedModules(_moduleFinder.FindAll());
            Logger.Debug("Found " + moduleTypes.Count + " NS modules in total.");

            //Register to IOC container.
            foreach (var moduleType in moduleTypes)
            {
                if (!NsModule.IsNsModule(moduleType))
                {
                    throw new NsInitializationException("This type is not an NS module: " + moduleType.AssemblyQualifiedName);
                }

                if (!_iocManager.IsRegistered(moduleType))
                {
                    _iocManager.Register(moduleType);
                }
            }

            //Add to module collection
            foreach (var moduleType in moduleTypes)
            {
                var moduleObject = (NsModule) _iocManager.Resolve(moduleType);

                moduleObject.IocManager = this._iocManager;
                //moduleObject.Configuration = _iocManager.Resolve<IAbpStartupConfiguration>();

                _modules.Add(new NsModuleInfo(moduleObject));

                Logger.DebugFormat("Loaded module: " + moduleType.AssemblyQualifiedName);
            }

            EnsureKernelModuleToBeFirst();

            SetDependencies();

            Logger.DebugFormat("{0} modules loaded.", _modules.Count);
        }

        private void EnsureKernelModuleToBeFirst()
        {
            int kernelModuleIndex = _modules.FindIndex(m => m.Type == typeof(NsKernelModule));
            if (kernelModuleIndex > 0)
            {
                NsModuleInfo kernelModule = this._modules[kernelModuleIndex];
                _modules.RemoveAt(kernelModuleIndex);
                _modules.Insert(0, kernelModule);
            }
        }

        private void SetDependencies()
        {
            foreach (var moduleInfo in _modules)
            {
                //Set dependencies according to assembly dependency
                foreach (var referencedAssemblyName in moduleInfo.Assembly.GetReferencedAssemblies())
                {
                    Assembly referencedAssembly = Assembly.Load(referencedAssemblyName);
                    List<NsModuleInfo> dependedModuleList = _modules.Where(m => m.Assembly == referencedAssembly).ToList();
                    if (dependedModuleList.Count > 0)
                    {
                        moduleInfo.Dependencies.AddRange(dependedModuleList);
                    }
                }

                //Set dependencies for defined DependsOnAttribute attribute(s).
                foreach (var dependedModuleType in NsModule.FindDependedModuleTypes(moduleInfo.Type))
                {
                    NsModuleInfo dependedModuleInfo = _modules.FirstOrDefault(m => m.Type == dependedModuleType);
                    if (dependedModuleInfo == null)
                    {
                        throw new NsInitializationException("Could not find a depended module " + dependedModuleType.AssemblyQualifiedName + " for " + moduleInfo.Type.AssemblyQualifiedName);
                    }

                    if ((moduleInfo.Dependencies.FirstOrDefault(dm => dm.Type == dependedModuleType) == null))
                    {
                        moduleInfo.Dependencies.Add(dependedModuleInfo);
                    }
                }
            }
        }

        private static ICollection<Type> AddMissingDependedModules(ICollection<Type> allModules)
        {
            List<Type> initialModules = allModules.ToList();
            foreach (var module in initialModules)
            {
                FillDependedModules(module, allModules);
            }

            return allModules;
        }

        private static void FillDependedModules(Type module, ICollection<Type> allModules)
        {
            foreach (var dependedModule in NsModule.FindDependedModuleTypes(module))
            {
                if (!allModules.Contains(dependedModule))
                {
                    allModules.Add(dependedModule);
                    FillDependedModules(dependedModule, allModules);
                }
            }
        }
    }
}
