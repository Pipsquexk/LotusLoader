using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MelonLoader;

namespace LotusLoader
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            base.OnApplicationStart();
            Lotus.InitLotus();
            Lotus.OnApplicationStart.Invoke(Lotus.mainType, null);
        }

        public override void OnLevelWasLoaded(int level)
        {
            base.OnLevelWasLoaded(level);
            Lotus.OnLevelWasLoaded.Invoke(Lotus.mainType, new object[1] { level });
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            Lotus.OnUpdate.Invoke(Lotus.mainType, null);
        }
    }
}
