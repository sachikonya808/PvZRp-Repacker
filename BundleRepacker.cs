using AssetsTools.NET;
using AssetsTools.NET.Extra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PvZRp_Repacker
{
    public class BundleRepacker
    {
        public static void RepackBundle(string bundleName)
        {
            string bundleFolder = Path.GetFileName(bundleName);
            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string decompPath = Path.GetFullPath(Path.Combine(currentPath, @"../unpacked"));
            decompPath = Path.GetFullPath(Path.Combine(decompPath, bundleFolder));

            if (!Directory.Exists(decompPath))
            {
                Console.WriteLine("NO DECOMP FOLDER");
                return;
            }

            string bundleRebuild = Path.GetFullPath(Path.Combine(decompPath, bundleFolder));
            bundleRebuild = bundleRebuild + $".decomp";

            
            var manager = new AssetsManager();

            var bunInst = manager.LoadBundleFile(bundleRebuild,true);
            var bun = bunInst.file;
            
            int entryCount = bun.BlockAndDirInfo.DirectoryInfos.Count;
            Console.WriteLine(entryCount);

            foreach (string file in Directory.EnumerateFiles(decompPath))
            {
                string matchName = Path.GetFileName(file);

                for (int i = 0; i < entryCount; i++)
                {
                    string name = bun.BlockAndDirInfo.DirectoryInfos[i].Name;

                    if (matchName == name && Path.GetExtension(matchName) != ".resS" && Path.GetExtension(matchName) != ".resource")
                    {
                        Console.WriteLine($"\t*** Block [{i}]: Match! ***");
                        Console.WriteLine($"Loading {file}...");

                        var aFileInst = manager.LoadAssetsFile(file, false);
                        var aFile = aFileInst.file;
                        Console.WriteLine("Load Successful! Writing to block...");

                        bun.BlockAndDirInfo.DirectoryInfos[i].SetNewData(aFile);
                        Console.WriteLine($"\t*** Success! ***");
                    }

                }
            }

            using (AssetsFileWriter writer = new AssetsFileWriter(Path.GetFullPath(Path.Combine(decompPath, bundleFolder))))
            {
                bun.Write(writer);
            }
        }
    }
}
