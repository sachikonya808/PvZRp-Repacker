using PvZRp_Repacker;
using System.Reflection;

namespace Repacker
{
    public class CommandLineHandler
    {
        public static void PrintHelp()
        {
            Console.WriteLine("PvZ Replanter");
            Console.WriteLine("No Input");
        }

        public static HashSet<string> GetFlags(string[] args)
        {
            HashSet<string> flags = new HashSet<string>();
            for (int i = 1; i < args.Length; i++)
            {
                if (args[i].StartsWith("-"))
                    flags.Add(args[i]);
            }
            return flags;
        }

        public static void CLHMain(string[] args)
        {
            if (args.Length < 1)
            {
                PrintHelp();
                return;
            }

            string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configPath = Path.GetFullPath(Path.Combine(currentPath, @"../config/config.json"));
            string command = args[0];

            if (command == "repack")
            {
                string file = FlagUtil.FlagChecker(args, configPath);
                BundleRepacker.RepackBundle(file);
            }
        }
    }
}
