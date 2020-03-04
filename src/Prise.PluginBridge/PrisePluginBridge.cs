using Prise.Proxy;
using System.Reflection;

namespace Prise.PluginBridge
{
    public static class PrisePluginBridge
    {
        public static object Invoke(object remoteObject, MethodInfo targetMethod, params object[] args)
            => PriseProxy.Invoke(remoteObject, targetMethod, args, new JsonSerializerParameterConverter(), new JsonSerializerResultConverter());
    }
}
