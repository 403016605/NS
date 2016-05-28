using NS.Kernel.Dependency;
using NS.Kernel.Modules;
using System;

namespace NS.Kernel
{
    /// <summary>
    /// 框架初始化
    /// </summary>
    public class Bootstrap : IDisposable
    {
        /// <summary>
        /// Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// Is this object disposed before?
        /// </summary>
        protected bool isDisposed;

        private IModuleManager moduleManager;

        /// <summary>
        /// Creates a new <see cref="Bootstrap"/> instance.
        /// </summary>
        public Bootstrap()
            : this(Dependency.Impl.IocManager.Instance)
        {

        }

        /// <summary>
        /// Creates a new <see cref="Bootstrap"/> instance.
        /// </summary>
        /// <param name="iocManager">IIocManager that is used to bootstrap the ABP system</param>
        public Bootstrap(IIocManager iocManager)
        {
            this.IocManager = iocManager;
        }

        /// <summary>
        /// Initializes the ABP system.
        /// </summary>
        public virtual void Initialize()
        {
            this.IocManager.IocContainer.Install(new Installers.KernelInstaller());



            //IocManager.Resolve<AbpStartupConfiguration>().Initialize();

            this.moduleManager = IocManager.Resolve<IModuleManager>();
            moduleManager.InitializeModules();
        }

        /// <summary>
        /// Disposes the ABP system.
        /// </summary>
        public virtual void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            this.isDisposed = true;

            if (this.moduleManager != null)
            {
                moduleManager.ShutdownModules();
            }
        }
    }
}
