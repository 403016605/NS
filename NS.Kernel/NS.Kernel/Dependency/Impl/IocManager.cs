using Castle.MicroKernel.Registration;
using Castle.Windsor;
using NS.Kernel.Shared;
using System;

namespace NS.Kernel.Dependency.Impl
{
    public class IocManager : IIocManager
    {
        public static IocManager Instance { get; private set; }

        public IocManager()
        {
            this.IocContainer = new WindsorContainer();
            IocContainer.Register(Component.For<IocManager, IIocManager>().UsingFactoryMethod(() => this));
        }

        public IWindsorContainer IocContainer { get; private set; }

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
            return (T) IocContainer.Resolve(type);
        }



        public void Dispose()
        {
            IocContainer.Dispose();
        }

        public void Release(object obj)
        {
            IocContainer.Release(obj);
        }

        private static ComponentRegistration<T> ApplyLifestyle<T>(ComponentRegistration<T> registration, DependencyLifeStyle lifeStyle)
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
