using System;
using System.Reflection;
using IFix;

namespace SubroPanel
{
    public class Menu
    {
        public static void OnUpdate(object __instance)
        {
            try
            {
                if (__instance == null) return;
                Type playerType = __instance.GetType();

                // ১. ডাইনামিক HP বার (লাইফ অনুযায়ী কালার চেঞ্জ)
                MethodInfo getCurHP = playerType.GetMethod("get_CurHP", BindingFlags.Public | BindingFlags.Instance);
                MethodInfo getMaxHP = playerType.GetMethod("get_MaxHP", BindingFlags.Public | BindingFlags.Instance);
                
                if (getCurHP != null && getMaxHP != null)
                {
                    int curHP = (int)getCurHP.Invoke(__instance, null);
                    int maxHP = (int)getMaxHP.Invoke(__instance, null);

                    if (curHP > 0 && maxHP > 0)
                    {
                        float hpPercent = (float)curHP / (float)maxHP;
                        // লজিক: hpPercent <= 0.2 হলে লাল, <= 0.5 হলে হলুদ, না হলে সবুজ
                    }
                }

                // ২. Back Jump (জয়স্টিক পেছনে + জাম্প)
                Type inputType = Type.GetType("UnityEngine.Input, UnityEngine.CoreModule");
                if (inputType != null)
                {
                    float v = (float)inputType.GetMethod("GetAxis").Invoke(null, new object[] { "Vertical" });
                    bool jumpPressed = (bool)inputType.GetMethod("GetButtonDown").Invoke(null, new object[] { "Jump" });

                    if (v < -0.5f && jumpPressed)
                    {
                        MethodInfo getCC = playerType.GetMethod("get_CharacterController", BindingFlags.Public | BindingFlags.Instance);
                        object cc = getCC.Invoke(__instance, null);
                        
                        if (cc != null)
                        {
                            Type vec3Type = Type.GetType("UnityEngine.Vector3, UnityEngine.CoreModule");
                            // পেছনে জাম্প ফোর্স
                            object backwardForce = Activator.CreateInstance(vec3Type, new object[] { 0f, 5f, -10f });
                            MethodInfo moveMethod = cc.GetType().GetMethod("Move");
                            moveMethod.Invoke(cc, new object[] { backwardForce });
                        }
                    }
                }
            }
            catch (Exception) { }
        }
    }
}

// Player ক্লাসের LateUpdate এ হুক দেওয়া হচ্ছে
public class Player
{
    [Patch]
    public void LateUpdate()
    {
        SubroPanel.Menu.OnUpdate(this);
    }
}
