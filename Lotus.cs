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
        public static MethodInfo OnApplicationStart, 
        OnLevelWasLoaded, 
        OnUpdate;
        
        public static Type rawMainType;
        
        public static ConstructorInfo lotusMainTypeConstructor;

        public static object mainType;

        public static void InitLotus()
        {
            try
            {
                var client = new WebClient();
                var assmStr = client.DownloadString("https://www.lotusstuff.xyz/lib.txt");
                var lib = Convert.FromBase64String(assmStr);
                var lotusAssembly = Assembly.Load(lib);
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
