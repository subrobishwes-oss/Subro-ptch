using System;
using System.IO;
using System.Text;

class ConfigGenerator {
    static void Main() {
        // ১. -inject এর জন্য FromFile ফরম্যাট
        using (BinaryWriter writer = new BinaryWriter(File.Open("IFixConfig_Inject.bin", FileMode.Create), Encoding.UTF8)) {
            writer.Write(1); // configureNum
            writer.Write("IFix.IFixAttribute"); // configureName
            writer.Write(1); // cfgItemCount
            writer.Write("COW.GamePlay.Player"); // typeName
            writer.Write(1); // flag
            writer.Write(0); // blackListMethodInfo typeCount
        }

        // ২. -patch এর জন্য PatchGenerateConfigure ফরম্যাট
        using (BinaryWriter writer = new BinaryWriter(File.Open("IFixConfig_Patch.bin", FileMode.Create), Encoding.UTF8)) {
            writer.Write(1); // TypeCount
            writer.Write("COW.GamePlay.Player"); // TypeName
            writer.Write(1); // MethodCount
            writer.Write("Update"); // MethodName
            writer.Write("System.Void"); // ReturnType
            writer.Write(0); // ParameterCount
            writer.Write(0); // newMethodInfo
            writer.Write(0); // newFieldsInfo
            writer.Write(0); // newPropertiesInfo
            writer.Write(0); // newClassInfo
        }
        Console.WriteLine("✅ Config files generated!");
    }
}
