using System;
using dnlib.DotNet;
using dnlib.DotNet.Emit;

class DnlibPatcher {
    static void Main() {
        try {
            Console.WriteLine("[*] Loading Assembly-CSharp.dll with dnlib...");
            var module = ModuleDefMD.Load("Assembly-CSharp.dll");
            
            // IFix.Core থেকে IFixAttribute টাইপটা লোড করা
            var iFixModule = ModuleDefMD.Load("IFix.Core.dll");
            var iFixAttrType = iFixModule.Find("IFix.IFixAttribute", false);
            if (iFixAttrType == null) {
                Console.WriteLine("❌ IFix.IFixAttribute not found in IFix.Core.dll!");
                return;
            }
            var importedAttrType = module.Import(iFixAttrType);
            var ctor = importedAttrType.FindMethod(".ctor");

            var type = module.Find("AnimatorsRandomAnimController", false);
            if (type != null) {
                var method = type.FindMethod("Update");
                if (method != null) {
                    Console.WriteLine("[+] Found Update method! Patching with dnlib...");
                    
                    // ১. [IFix.IFixAttribute] ট্যাগ বসানো
                    var customAttr = new CustomAttribute(ctor);
                    method.CustomAttributes.Add(customAttr);
                    
                    // ২. কোড রিপ্লেস করে RET বসানো
                    method.Body.Instructions.Clear();
                    method.Body.Instructions.Add(OpCodes.Ret.ToInstruction());
                    
                    Console.WriteLine("✅ Patched with dnlib and injected IFixAttribute!");
                }
            }
            module.Write("Assembly-CSharp_patched.dll");
            Console.WriteLine("✅ Saved Assembly-CSharp_patched.dll!");
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
