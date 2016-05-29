using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NS.Kernel.Shared;
using System;
using System.Reflection;
using NS.Kernel.Modules;
using NS.Kernel.Modules.Impl;
using NS.Kernel.Reflection;
using NS.Kernel.Reflection.Impl;

namespace NS.Kernel.Dependency.Impl
{
    public class IocManager : IIocManager
    {
        public IocManager()
        {
            IocContainer = new WindsorContainer();
            IocContainer.Register(Component.For<IocManager, IIocManager>().UsingFactoryMethod(() => this));
            IocContainer.Register(
            Component.For<IAssemblyFinder>().ImplementedBy<FolderAssemblyFinder>().LifestyleSingleton(),
                Component.For<ITypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<IModuleFinder>().ImplementedBy<ModuleFinder>().LifestyleTransient(),
                Component.For<IModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton()
                );

        }

        private static readonly object SynObject = new object();

        private static IocManager _instance = null;
        public static IocManager Instance
        {
            get
            {
                // Double-Checked Locking
                if (null != _instance) return _instance;
                lock (SynObject)
                {
                    if (null == _instance)
                    {
                        _instance = new IocManager();
                    }
                }
                return _instance;
            }
        }

        public IWindsorContainer IocContainer { get; }

        public bool IsRegistered(Type type)
        {
            return IocContainer.Kernel.HasComponent(type);
        }

        public bool IsRegistered<TType>()
        {
            return IocContainer.Kernel.HasComponent(typeof(TType));
        }

        public void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type), lifeStyle));
        }

        public void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            IocContainer.Register(ApplyLifestyle(Component.For(type, impl).ImplementedBy(impl), lifeStyle));
        }

        public void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class
        {
            IocContainer.Register(ApplyLifestyle(Component.For<T>(), lifeStyle));
        }

        void IIocManager.Register<TType, TImpl>(DependencyLifeStyle lifeStyle)
        {
            IocContainer.Register(ApplyLifestyle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeStyle));
        }

        public object Resolve(Type type)
        {
            return IocContainer.Resolve(type);
        }

        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve(type, argumentsAsAnonymousType);
        }

        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            return IocContainer.Resolve<T>(argumentsAsAnonymousType);
        }

        public T Resolve<T>(Type type)
        {
            return (T)IocContainer.Resolve(type);
        }

        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }


        public void RegisterAssembly(Assembly assembly)
        {
            //Transient
            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ITransientDependency>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleTransient()
                );

            //Singleton
            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<ISingletonDependency>()
                    .WithService.Self()
                    .WithService.DefaultInterfaces()
                    .LifestyleSingleton()
                );

            //Windsor Interceptors
            IocContainer.Register(
                Classes.FromAssembly(assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<IInterceptor>()
                    .WithService.Self()
                    .LifestyleTransient()
                );
        }

        public void RegisterAssembly<TType>(Assembly assembly, DependencyLifeStyle dependencyLifeStyle) where TType : class
        {
            if (dependencyLifeStyle == DependencyLifeStyle.Transient)
            {
                IocContainer.Register(
                                Classes.FromAssembly(assembly)
                                    .IncludeNonPublicTypes()
                                    .BasedOn<TType>()
                                    .WithService.Self()
                                    .WithService.DefaultInterfaces()
                                    .LifestyleTransient()
                                );
            }
            else
            {
                IocContainer.Register(
                                Classes.FromAssembly(assembly)
                                    .IncludeNonPublicTypes()
                                    .BasedOn<TType>()
                                    .WithService.Self()
                                    .WithService.DefaultInterfaces()
                                    .LifestyleSingleton()
                                );
            }


        }


        public void Dispose()
        {
            IocContainer.Dispose();
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration,
            DependencyLifeStyle lifeStyle)
            where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeStyle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }
    }
}