using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using NS.Kernel.Reflection;
using NS.Kernel.Modules;
using NS.Kernel.Reflection.Impl;
using NS.Kernel.Modules.Impl;

namespace NS.Kernel.Installers
{
    internal class KernelInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<ITypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<IModuleFinder>().ImplementedBy<ModuleFinder>().LifestyleTransient(),
                Component.For<IModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton()
                );
        }
    }
}