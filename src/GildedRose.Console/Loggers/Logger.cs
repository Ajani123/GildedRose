namespace GildedRose.Console.Loggers
{
    public static class Logger
    {
        public static void Log(string message)
        {
#if (DEBUG)
            System.Console.WriteLine(message);
#endif
        }
    }
}
