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
            // আপাতত টেস্ট করার জন্য আমরা গেমের Player.Update এ হুক দিচ্ছি
        }
    }
}

namespace COW.GamePlay
{
    public class Player
    {
        [Patch]
        public void Update()
        {
            SakuraPatch.SakuraMenu.OnUpdate();
        }
    }
}
