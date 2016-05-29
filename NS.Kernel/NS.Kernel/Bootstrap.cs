using NS.Kernel.Dependency;
using NS.Kernel.Installers;
using NS.Kernel.Modules;
using System;

namespace NS.Kernel
{
    /// <summary>
    ///     框架初始化
    /// </summary>
    public class Bootstrap : IDisposable
    {
        /// <summary>
        ///     Is this object disposed before?
        /// </summary>
        protected bool isDisposed;

        private IModuleManager moduleManager;

        /// <summary>
        ///     Creates a new <see cref="Bootstrap" /> instance.
        /// </summary>
        public Bootstrap()
            : this(Dependency.Impl.IocManager.Instance)
        {
        }

        /// <summary>
        ///     Creates a new <see cref="Bootstrap" /> instance.
        /// </summary>
        /// <param name="iocManager">IIocManager that is used to bootstrap the ABP system</param>
        public Bootstrap(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        // Creates an syn object.
        private static readonly object SynObject = new object();

        private static Bootstrap _instance = null;
        public static Bootstrap Instance
        {
            get
            {
                // Double-Checked Locking
                if (null != _instance) return _instance;
                lock (SynObject)
                {
                    if (null == _instance)
                    {
                        _instance = new Bootstrap();
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        ///     Gets IIocManager object used by this class.
        /// </summary>
        public IIocManager IocManager { get; }

        /// <summary>
        ///     Disposes the ABP system.
        /// </summary>
        public virtual void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            isDisposed = true;

            if (moduleManager != null)
            {
                moduleManager.ShutdownModules();
            }
        }

        /// <summary>
        ///     Initializes the ABP system.
        /// </summary>
        public virtual void Initialize()
        {
            IocManager.IocContainer.Install(new KernelInstaller());


            //IocManager.Resolve<AbpStartupConfiguration>().Initialize();

            moduleManager = IocManager.Resolve<IModuleManager>();
            moduleManager.InitializeModules();
        }
    }
}