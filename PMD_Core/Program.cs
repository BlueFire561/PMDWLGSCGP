using System;

namespace PMD_Core
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new PMD_Lib.Game1())
                game.Run();
        }
    }
}
