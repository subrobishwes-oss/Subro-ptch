using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;

class Injector {
    static void Main() {
        try {
            Console.WriteLine("[*] Reading original Assembly-CSharp.dll...");
            var asm = AssemblyDefinition.ReadAssembly("Assembly-CSharp.dll");
            
            // ১. SakuraMenu ক্লাস বানানো
            var newType = new TypeDefinition("SakuraPatch", "SakuraMenu", TypeAttributes.Public | TypeAttributes.Class, asm.MainModule.TypeSystem.Object);
            asm.MainModule.Types.Add(newType);
            var onUpdate = new MethodDefinition("OnUpdate", MethodAttributes.Public | MethodAttributes.Static, asm.MainModule.TypeSystem.Void);
            newType.Methods.Add(onUpdate);
            var ilUpdate = onUpdate.Body.GetILProcessor();
            ilUpdate.Emit(OpCodes.Ret); // ফাঁকা মেথড

            // ২. AnimatorsRandomAnimController.Update() খুঁজে বের করা
            var targetType = asm.MainModule.GetType("AnimatorsRandomAnimController");
            if (targetType != null) {
                var targetMethod = targetType.Methods.FirstOrDefault(m => m.Name == "Update");
                if (targetMethod != null) {
                    var il = targetMethod.Body.GetILProcessor();
                    targetMethod.Body.Instructions.Clear();
                    il.Emit(OpCodes.Call, onUpdate); // আমাদের SakuraMenu.OnUpdate() কল করা
                    il.Emit(OpCodes.Ret);
                    Console.WriteLine("✅ Successfully injected SakuraMenu into Update method!");
                } else {
                    Console.WriteLine("❌ Update method not found!");
                }
            } else {
                Console.WriteLine("❌ Target class not found!");
            }

            // ৩. মডিফাই করা DLL সেভ করা
            asm.Write("Assembly-CSharp_mod.dll");
            Console.WriteLine("✅ Saved as Assembly-CSharp_mod.dll");
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
