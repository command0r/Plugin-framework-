using Prise.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading.Tasks;

namespace Prise
{
    public abstract class DisposableAssemblyUnLoader : IDisposable
    {
        protected ConcurrentDictionary<string, IAssemblyLoadContext> loadContexts;
        protected ConcurrentDictionary<string, WeakReference> loadContextReferences;
        protected bool disposed = false;

        protected DisposableAssemblyUnLoader()
        {
            this.loadContexts = new ConcurrentDictionary<string, IAssemblyLoadContext>();
            this.loadContextReferences = new ConcurrentDictionary<string, WeakReference>();
        }

        public virtual void UnloadAll()
        {
            DisposeAndUnloadContexts();
        }

        public async virtual Task UnloadAllAsync()
        {
            DisposeAndUnloadContexts();
        }

        public virtual void Unload(string pluginAssemblyName)
        {
            UnloadContext(pluginAssemblyName);
        }

        public async virtual Task UnloadAsync(string pluginAssemblyName)
        {
            UnloadContext(pluginAssemblyName);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                DisposeAndUnloadContexts();
            }
            this.disposed = true;
        }

        protected virtual void UnloadContext(string pluginAssemblyName)
        {
            var pluginName = Path.GetFileNameWithoutExtension(pluginAssemblyName);
            var loadContext = this.loadContexts[pluginName];
            loadContext.Unload();
            loadContext.Dispose();

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        protected virtual void DisposeAndUnloadContexts()
        {
            if (loadContexts != null)
                foreach (var loadContext in loadContexts.Values)
                {
                    loadContext.Unload();
                    loadContext.Dispose();
                }

            this.loadContexts.Clear();
            this.loadContexts = null;

            if (loadContextReferences != null)
                foreach (var refrence in loadContextReferences.Values)
                {
                    // https://docs.microsoft.com/en-us/dotnet/standard/assembly/unloadability#use-collectible-assemblyloadcontext
                    for (int i = 0; refrence.IsAlive && (i < 10); i++)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }

            this.loadContextReferences.Clear();
            this.loadContextReferences = null;

            GC.Collect(); // collects all unused memory
            GC.WaitForPendingFinalizers(); // wait until GC has finished its work
            GC.Collect();
        }
    }
}
