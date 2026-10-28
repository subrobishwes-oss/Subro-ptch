using System;
using IFix;

namespace SakuraPatch
{
    public class SakuraMenu
    {
        public static void OnUpdate()
        {
            int test = 1 + 2; // IFix-কে বোঝানোর জন্য লজিক
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
