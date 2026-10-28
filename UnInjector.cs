using System;
using Mono.Cecil;

class UnInjector {
    static void Main() {
        var asm = AssemblyDefinition.ReadAssembly("Assembly-CSharp.dll");
        var type = asm.MainModule.GetType("IFix.ILFixInterfaceBridge");
        if (type != null) {
            type.Name = "IFix.ILFixInterfaceBridge_Dummy";
            type.Namespace = "IFix";
            asm.Write("Assembly-CSharp_Uninjected.dll");
            Console.WriteLine("✅ Evil Hack Successful: Renamed IFix bridge type!");
        } else {
            Console.WriteLine("Type not found. Maybe already uninjected?");
        }
    }
}
