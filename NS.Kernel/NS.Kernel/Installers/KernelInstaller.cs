using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using NS.Kernel.Modules;
using NS.Kernel.Modules.Impl;
using NS.Kernel.Reflection;
using NS.Kernel.Reflection.Impl;

namespace NS.Kernel.Installers
{
    internal class KernelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //container.Register(
            //    Component.For<IAssemblyFinder>().ImplementedBy<FolderAssemblyFinder>().LifestyleSingleton(),
            //    Component.For<ITypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
            //    Component.For<IModuleFinder>().ImplementedBy<ModuleFinder>().LifestyleTransient(),
            //    Component.For<IModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton()
            //    );
        }
    }
}