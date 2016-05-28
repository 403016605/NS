using System;
using System.Linq;

namespace NS.Kernel.Modules
{
    internal interface IModuleManager
    {
        /// <summary>
        /// 初始化模块
        /// </summary>
        void InitializeModules();

        /// <summary>
        /// 关闭模块
        /// </summary>
        void ShutdownModules();
    }
}
