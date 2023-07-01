using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskDevGaurd.modules;

namespace TaskDevGaurd
{
    public abstract class Protection
    {

        public abstract void initalize(ModuleDefMD module);


        public static void execute() {

            Console.Write("Enter Executable: ");
            string path = Console.ReadLine();
            var module = ModuleDefMD.Load(path);

            var features = new List<Protection> {

                new renamer()
            
            };


            foreach(var f in features)
            {
                f.initalize(module);
            }



            module.Write(path.Remove(path.Length - 4) + "-opt.exe");
        }

    }

}
