using System;
using Mono.Cecil;
using System.Linq;

class OffsetFinder {
    static void Main() {
        try {
            var asm = AssemblyDefinition.ReadAssembly("Assembly-CSharp.dll");
            var targetType = asm.MainModule.GetType("AnimatorsRandomAnimController");
            if (targetType != null) {
                var targetMethod = targetType.Methods.FirstOrDefault(m => m.Name == "Update");
                if (targetMethod != null) {
                    Console.WriteLine("RVA:" + targetMethod.RVA);
                }
            }
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
