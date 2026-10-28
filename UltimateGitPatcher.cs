using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System.Linq;

class UltimateGitPatcher {
    static void Main() {
        try {
            Console.WriteLine("[*] Loading Assembly-CSharp.dll...");
            var module = ModuleDefMD.Load("Assembly-CSharp.dll");
            
            // ১. __External ভাঙা টাইপগুলো মুছে ফেলা (যাতে IFix ক্র্যাশ না করে)
            var typesToRemove = module.Types.Where(t => t.Name.String.StartsWith("__External")).ToList();
            foreach(var t in typesToRemove) {
                module.Types.Remove(t);
            }
            Console.WriteLine($"[+] Removed {typesToRemove.Count} broken __External types.");

            // ২. আমাদের টার্গেট ক্লাস খুঁজে বের করা
            var targetType = module.Find("AnimatorsRandomAnimController", false);
            if (targetType != null) {
                var targetMethod = targetType.FindMethod("Update");
                if (targetMethod != null) {
                    // ৩. মেথডের ভেতরে RET বসানো
                    targetMethod.Body.Instructions.Clear();
                    targetMethod.Body.Instructions.Add(OpCodes.Ret.ToInstruction());
                    
                    // ৪. IFix-এর [Patch] অ্যাট্রিবিউট বসানো (দরকার নেই, শুধু IFixConfig দিয়েই হবে)
                    Console.WriteLine("[+] Patched AnimatorsRandomAnimController.Update() with RET.");
                }
            }
            
            module.Write("Assembly-CSharp_Stripped.dll");
            Console.WriteLine("[+] Saved as Assembly-CSharp_Stripped.dll");
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
