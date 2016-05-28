using System;

namespace NS.Kernel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IIocManager
    {
        #region 注册

        /// <summary>
        /// 注册一个类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="lifeStyle">当前类型的生命周期</param>
        void Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where T : class;

        /// <summary>
        /// 注册一个类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="lifeStyle">类型的生命周期</param>
        void Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        /// <summary>
        /// 注册类型与它的实现.
        /// </summary>
        /// <typeparam name="TType">类型(接口或者抽象类)</typeparam>
        /// <typeparam name="TImpl">类型(接口或者抽象类)<paramref name="type"/>的实现</typeparam>
        /// <param name="lifeStyle">类型的生命周期</param>
        void Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// 注册类型与它的实现.
        /// </summary>
        /// <param name="type">类型(接口或者抽象类)</param>
        /// <param name="impl">类型(接口或者抽象类)<paramref name="type"/>的实现</param>
        /// <param name="lifeStyle">类型的生命周期</param>
        void Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        #endregion

        #region 解析

        /// <summary>
        /// 从IOC容器中获取对象.
        /// 返回的对象在用完后必须释放 (see <see cref="Release"/>).
        /// </summary> 
        /// <typeparam name="T">要获取实例的类型</typeparam>
        /// <returns>对象的实例</returns>
        T Resolve<T>();

        /// <summary>
        /// 从IOC容器中获取对象.
        /// 返回的对象在用完后必须释放 (see <see cref="Release"/>) 
        /// </summary> 
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="type">解析的对象类型</param>
        /// <returns>对象的实例</returns>
        T Resolve<T>(Type type);

        /// <summary>
        /// 从IOC容器中获取对象.
        /// 返回的对象在用完后必须释放 (see <see cref="Release"/>).
        /// </summary> 
        /// <typeparam name="T">获取的对象类型</typeparam>
        /// <param name="argumentsAsAnonymousType">构造函数的参数</param>
        /// <returns>对象实例</returns>
        T Resolve<T>(object argumentsAsAnonymousType);

        /// <summary>
        /// 从IOC容器中获取对象.
        /// 返回的对象在用完后必须释放 (see <see cref="Release"/>).
        /// </summary> 
        /// <param name="type">获取的对象类型</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type);

        /// <summary>
        /// 从IOC容器中获取对象.
        /// 返回的对象在用完后必须释放 (see <see cref="Release"/>).
        /// </summary> 
        /// <param name="type">获取的对象类型</param>
        /// <param name="argumentsAsAnonymousType">构造函数的参数</param>
        /// <returns>对象实例</returns>
        object Resolve(Type type, object argumentsAsAnonymousType);

        /// <summary>
        /// 释放已经加载的对象.
        /// </summary>
        /// <param name="obj">要释放的对象实例</param>
        void Release(object obj);

        #endregion

        #region 检查

        /// <summary>
        /// 检查类型是否已经注册.
        /// </summary>
        /// <param name="type">待检查的类型</param>
        bool IsRegistered(Type type);

        /// <summary>
        /// 检查类型是否已经注册.
        /// </summary>
        /// <typeparam name="TType">待检查的类型</typeparam>
        bool IsRegistered<TType>();

        #endregion
    }

    public enum DependencyLifeStyle
    {
        Singleton, Transient
    }
}
