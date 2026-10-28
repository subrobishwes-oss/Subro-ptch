using System;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System.Linq;

class Injector {
    static void Main() {
        try {
            var asm = AssemblyDefinition.ReadAssembly("Assembly-CSharp.dll");
            
            var newType = new TypeDefinition("SakuraPatch", "SakuraMenu", TypeAttributes.Public | TypeAttributes.Class, asm.MainModule.TypeSystem.Object);
            asm.MainModule.Types.Add(newType);
            var onUpdate = new MethodDefinition("OnUpdate", MethodAttributes.Public | MethodAttributes.Static, asm.MainModule.TypeSystem.Void);
            newType.Methods.Add(onUpdate);
            var ilUpdate = onUpdate.Body.GetILProcessor();
            ilUpdate.Emit(OpCodes.Ret); 

            var targetType = asm.MainModule.GetType("AnimatorsRandomAnimController");
            if (targetType != null) {
                var targetMethod = targetType.Methods.FirstOrDefault(m => m.Name == "Update");
                if (targetMethod != null) {
                    var il = targetMethod.Body.GetILProcessor();
                    targetMethod.Body.Instructions.Clear();
                    il.Emit(OpCodes.Call, onUpdate);
                    il.Emit(OpCodes.Ret);
                    Console.WriteLine("✅ Successfully injected SakuraMenu into Update method!");
                }
            }
            asm.Write("Assembly-CSharp_mod.dll");
        } catch (Exception e) { Console.WriteLine("Error: " + e.Message); }
    }
}
