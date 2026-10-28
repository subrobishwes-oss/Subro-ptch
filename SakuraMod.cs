using System;
using System.Reflection;
using IFix;

namespace SakuraPatch
{
    public class SakuraMenu
    {
        public static void OnUpdate()
        {
            // এখানে আমাদের ESP/Aimbot এর আসল কোড বসবে
        }
    }
}

public class AnimatorsRandomAnimController
{
    [Patch]
    public void Update()
    {
        SakuraPatch.SakuraMenu.OnUpdate();
    }
}
