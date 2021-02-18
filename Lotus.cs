using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace LotusLoader
{
    public static class Lotus
    {
        public static MethodInfo OnApplicationStart, OnLevelWasLoaded, OnUpdate;
        public static Type rawMainType;
        public static ConstructorInfo lotusMainTypeConstructor;

        public static object mainType;

        public static void InitLotus()
        {
            try
            {
                //var assemblyString = File.ReadAllText("Lotus.txt");
                //byte[] assemblyBytes = Encoding.ASCII.GetBytes(assemblyString);
                var client = new WebClient();
                var fuck = client.DownloadString("https://www.lotusstuff.xyz/lib.txt");
                var lib = Convert.FromBase64String(fuck);
                var lotusAssembly = Assembly.Load(lib);
                foreach (Type typeFucker in lotusAssembly.GetTypes())
                {
                    MelonLogger.Log(typeFucker.Name);
                }
                rawMainType = lotusAssembly.GetTypes()[1];
                if (rawMainType == null)
                {
                    MelonLogger.Log("mainType is null");
                }
                else
                {
                    MelonLogger.Log("mainType is not null");
                }
                lotusMainTypeConstructor = rawMainType.GetConstructor(Type.EmptyTypes);
                mainType = lotusMainTypeConstructor.Invoke(new object[0]);
                foreach (MethodInfo methodFucker in rawMainType.GetMethods())
                {
                    MelonLogger.Log(methodFucker.Name);
                }
                OnApplicationStart = rawMainType.GetMethod("OnApplicationStart");
                OnLevelWasLoaded = rawMainType.GetMethod("OnLevelWasLoaded");
                OnUpdate = rawMainType.GetMethod("OnUpdate");
                if (OnApplicationStart == null)
                {
                    MelonLogger.Log("OnApplicationStart is null");
                }
                else
                {
                    MelonLogger.Log("OnApplicationStart is not null");
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Log($"Lotus Loader caught the exception: {ex.ToString()}");
            }
        }
    }
}
