using dnlib.DotNet;
using dnlib.DotNet.Emit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDevGaurd.modules
{
    public class renamer:Protection
    {

        static Random random = new Random();

        public override void initalize(ModuleDefMD module)
        {


            int rand = random.Next(1, 2);
            
            

            
            foreach(var type in module.Types)
            {
                if (Settings.renamer)
                {

                    module.Name = "This file has been obfuscated by Taskmgr.lol";

                    switch (rand)
                    {
                        case 1:
                            type.Namespace = "Protect_by_Taskmgr.lol";
                            break;
                        case 2:
                            type.Namespace = "Capybaras_are_cute";
                            break;
                    }

                    type.Name = RandomString();

                    foreach(var method in type.Methods)
                    {

                        if(!method.IsRuntimeSpecialName && !method.DeclaringType.IsForwarder)
                        {
                            method.Name = RandomString();
                        }



                    }
                    foreach(var field in type.Fields)
                    {
                        field.Name = RandomString();
                    }
                }

            }

        }

        

        

        private static string RandomString()
        {
            string random = Path.GetRandomFileName();
            string rname = random.Replace(".", string.Empty);
            string rnamef = rname.ToUpper();
            return rnamef;
        }


    }
}
