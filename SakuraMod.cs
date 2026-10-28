using System;
using IFix;
using UnityEngine;

namespace SakuraPatch
{
    public class SakuraMenu
    {
        // এখানে আমরা আমাদের ESP এর আসল কোড বসাব
        public static void OnUpdate()
        {
            // ইলাস্ট্রেশনের জন্য (পরে আমরা আসল Vector3 লজিক বসাব)
            int test = 1;
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
