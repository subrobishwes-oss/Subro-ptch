using System;
using IFix;

namespace SubroPanel
{
    public class Menu
    {
        public static void OnUpdate()
        {
            // এখানে আমাদের ESP/Aimbot লজিক বসবে
        }
    }
}

// সঠিক ক্লাস: AnimatorsRandomAnimController
public class AnimatorsRandomAnimController
{
    [Patch]
    public void Update()
    {
        SubroPanel.Menu.OnUpdate();
    }
}
