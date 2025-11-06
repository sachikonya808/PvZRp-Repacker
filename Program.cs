namespace Repacker
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                CommandLineHandler.CLHMain(args);
            }
            else
            {
                CommandLineHandler.PrintHelp();
            }
        }
    }
}