using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskDevGaurd.modules
{
    public class strenc:Protection
    {
        private static byte[] privateKey;
        public override void initalize(ModuleDefMD module)
        {

            //Generating private key
            privateKey = Encoding.UTF8.GetBytes(module.Types.ToString() + module.Is32BitPreferred + module.Is32BitRequired);
            
            foreach(var type in module.Types)
            {
                if (Settings.strenc)
                {
                    foreach(var method in type.Methods)
                    {
                        if (!method.IsPublic)
                        {
                            //This is not working rn
                            /*
                            if (ShouldDecrypt(module))
                            {
                                DecryptStrings(method);
                            }
                            else
                            {
                                EncryptString(method);
                            }
                            */

                            EncryptStrings(method);
                            //DecryptStrings(method);
                            
                        }
                    }
                }
            }
        }

        private static void EncryptStrings(MethodDef method)
        {
            for (int i = 0; i < method.Body.Instructions.Count; i++)
            {
                Instruction inst = method.Body.Instructions[i];

                if (inst.OpCode == OpCodes.Ldstr && inst.Operand is string stringValue)
                {
                    string encryptedString = EncryptString(stringValue);

                    method.Body.Instructions[i] = Instruction.Create(OpCodes.Ldstr, encryptedString);
                }
            }
        }



        private static string EncryptString(string input)
        {
            //Geting private key
            byte[] key = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(privateKey).ToCharArray());
            byte[] encB = new byte[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                encB[i] = (byte)(input[i] ^ key[i % key.Length]);
            }
            return Convert.ToBase64String(encB);
        }

        private static string DecryptString(string input)
        {
            //Getting private key for decryption
            byte[] key = Encoding.UTF8.GetBytes(Encoding.UTF8.GetString(privateKey).ToCharArray());
            byte[] encryptedBytes = Convert.FromBase64String(input);
            byte[] decryptedBytes = new byte[encryptedBytes.Length];
            for(int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ key[i % key.Length]);
            }
            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private static void DecryptStrings(MethodDef method)
        {
            for(int i = 0; i < method.Body.Instructions.Count; i++)
            {
                Instruction instruction = method.Body.Instructions[i];
                if(instruction.OpCode == OpCodes.Ldstr && instruction.Operand is string stringValue)
                {
                    string decryptedString = DecryptString(stringValue);
                    method.Body.Instructions[i] = Instruction.Create(OpCodes.Ldstr, decryptedString);
                }
            }
        }

        private static bool ShouldDecrypt(ModuleDefMD module)
        {
            //Checks if entryPoint has been executed.
            /*
            if(module.EntryPoint == null)
            {
                return false;
            }

            MethodDef entryPoint = module.EntryPoint;

            return entryPoint.IsStatic && entryPoint.ReturnType == module.CorLibTypes.Void;
            */
            var entryAssembly = System.Reflection.Assembly.GetEntryAssembly();
            return entryAssembly?.GetManifestResourceNames().Contains("EntryPointMarker") ?? false;
        }



    }
}
