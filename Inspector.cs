using System;
using Mono.Cecil;

class Inspector {
    static void Main() {
        try {
            var asm = AssemblyDefinition.ReadAssembly("Assembly-CSharp.dll");
            var type = asm.MainModule.GetType("AnimatorsRandomAnimController");
            if (type != null) {
                Console.WriteLine("Class AnimatorsRandomAnimController found!");
                foreach (var m in type.Methods) {
                    Console.WriteLine("Method: " + m.FullName);
                }
            } else {
                Console.WriteLine("Class NOT found! Searching for other classes...");
            }
        } catch (Exception e) {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
