using BusinessLogic;

namespace UI
{
    /// <summary>Starts the app: checks the database, then shows the login window.</summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            try
            {
                // Show the database readiness issue, but keep the application available for troubleshooting.
                DbInitializer.EnsureDatabase(showMessage: true);
            }
            catch (Exception ex)
            {
                Logger.Error("Program.Startup failed", ex);
            }

            Application.Run(new LoginForm());
        }
    }
}
