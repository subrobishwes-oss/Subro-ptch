using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;

class CustomPatcher {
    static void Main() {
        try {
            Console.WriteLine("[*] Loading Assembly-CSharp.dll with Cecil...");
            var asm = AssemblyDefinition.ReadAssembly("Assembly-CSharp.dll");
            
            var targetType = asm.MainModule.GetType("AnimatorsRandomAnimController");
            if (targetType != null) {
                var targetMethod = targetType.Methods.FirstOrDefault(m => m.Name == "Update");
                if (targetMethod != null) {
                    Console.WriteLine("[+] Found Update Method! Patching...");
                    var il = targetMethod.Body.GetILProcessor();
                    targetMethod.Body.Instructions.Clear();
                    // সরাসরি রিটার্ন করে দেব (RET)
                    il.Emit(OpCodes.Ret);
                    Console.WriteLine("✅ Patched successfully to RET.");
                } else {
                    Console.WriteLine("❌ Update method not found!");
                }
            } else {
                Console.WriteLine("❌ Target class not found!");
            }

            asm.Write("Assembly-CSharp_patched.dll");
            Console.WriteLine("✅ Saved as Assembly-CSharp_patched.dll");
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
