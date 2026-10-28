using System;
using System.Reflection;
using IFix;

namespace SakuraPatch
{
    public class SakuraMenu
    {
        public static void OnUpdate()
        {
            // আমাদের ESP/Aimbot এর রিফ্লেকশন কোড এখানে বসবে
        }
    }
}

namespace COW.GamePlay
{
    public class Player
    {
        [Interpret]
        public void Update()
        {
            SakuraPatch.SakuraMenu.OnUpdate();
        }
    }
}
