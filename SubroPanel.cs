using System;
using System.Reflection;
using IFix;

namespace SubroPanel
{
    public class Menu
    {
        public static void OnUpdate()
        {
            // এখানে আমরা আমাদের ESP/Aimbot লজিক বসাব (পরে)
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
            SubroPanel.Menu.OnUpdate();
        }
    }
}
