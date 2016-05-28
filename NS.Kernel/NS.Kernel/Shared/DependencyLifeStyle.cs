namespace NS.Kernel.Shared
{
    /// <summary>
    ///     依赖的生命周期
    /// </summary>
    public enum DependencyLifeStyle
    {
        /// <summary>
        ///     单例模式
        /// </summary>
        Singleton,

        /// <summary>
        ///     每次都创建新的实例
        /// </summary>
        Transient
    }
}