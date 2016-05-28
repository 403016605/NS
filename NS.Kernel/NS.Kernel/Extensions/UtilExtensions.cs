using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NS.Kernel.Extensions
{
    public static class UtilExtensions
    {
        public static List<Assembly> ListAssemblies(this DirectoryInfo dirInfo)
        {
            var assemblies = new List<Assembly>();

            foreach (var fileInfo in dirInfo.GetFiles("*.dll"))
            {
                assemblies.Add(Assembly.LoadFile(fileInfo.FullName));
            }

            return assemblies;
        }
    }
}