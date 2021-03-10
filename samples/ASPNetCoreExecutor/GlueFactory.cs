using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DotXxlJob.Core;

namespace ASPNetCoreExecutor
{
    public class GlueFactory
    {
        public static GlueFactory Instance = new GlueFactory();
        private ConcurrentDictionary<string, Type> cachedType = new ConcurrentDictionary<string, Type>();
        public List<string> AssemblyNames { get; } = new List<string>();
        private GlueFactory()
        {

        }

        public IJobHandler LoadNewInstance(string sourceCode)
        {
            if (string.IsNullOrEmpty(sourceCode) && sourceCode.Trim().Length > 0)
            {
                Type type = GetClassFromSource(sourceCode);
                var obj = Activator.CreateInstance(type);
                if (obj != null && obj is IJobHandler)
                {
                    return (IJobHandler)obj;
                }
            }
            throw new ArgumentException("无法加载glue对象");
        }

        private Type GetClassFromSource(string sourceCode)
        {
            var key = Encoding.ASCII.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(sourceCode)));
            Type type = null;
            if (!cachedType.TryGetValue(key, out type))
            {
                var loadAssemblies = new List<Assembly>();
                AssemblyNames.ForEach(name =>
                {
                    loadAssemblies.Add(Assembly.Load(new AssemblyName(name)));
                });
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System.Runtime")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System.Collections")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System.Net")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System.Net.Http")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System.Private.CoreLib")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System.Private.Uri")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("netstandard")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System.ComponentModel")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("System.Runtime.Extensions")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("Nacos.AspNetCore")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("Microsoft.Extensions.Http")));
                loadAssemblies.Add(Assembly.Load(new AssemblyName("DotXxlJob.Core")));

                var buildedAssembly = SourceComplier.Compile(sourceCode,
                    loadAssemblies.ToArray()
                    );
                type = buildedAssembly.GetTypes()[0];
                cachedType.TryAdd(key, type);
            }
            return type;
        }
    }
}
