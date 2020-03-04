using Prise.Infrastructure;
using System;
using System.Runtime.InteropServices;

namespace Prise
{
    public class DefaultNativeAssemblyUnloader : INativeAssemblyUnloader
    {
        public void UnloadNativeAssembly(string fullPathToLoadedNativeAssembly, IntPtr pointerToAssembly)
        {
            NativeLibrary.Free(pointerToAssembly);
        }
    }
}
