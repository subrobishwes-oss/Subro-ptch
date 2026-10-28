using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

class DnlibPatcher {
    static void Main() {
        try {
            Console.WriteLine("[*] Loading Assembly-CSharp.dll with dnlib...");
            var module = ModuleDefMD.Load("Assembly-CSharp.dll");
            
            var type = module.Find("AnimatorsRandomAnimController", false);
            if (type != null) {
                var method = type.FindMethod("Update");
                if (method != null) {
                    Console.WriteLine("[+] Found Update method! Patching with dnlib...");
                    method.Body.Instructions.Clear();
                    method.Body.Instructions.Add(OpCodes.Ret.ToInstruction());
                    Console.WriteLine("✅ Patched with dnlib!");
                }
            }
            module.Write("Assembly-CSharp_patched.dll");
            Console.WriteLine("✅ Saved Assembly-CSharp_patched.dll!");
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
