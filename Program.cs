using System.Text;

namespace _3D_Model_Accuracy_Checker
{
    internal static class Program
    {

        // Define a unique mutex name for your application
        private static Mutex mutex = new Mutex(true, "3DModelAccuracyCheckerMutex");


        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Check if the mutex can be acquired
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                // Register the encoding provider
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                // To customize application configuration such as set high DPI settings or default font,
                // see https://aka.ms/applicationconfiguration.
                ApplicationConfiguration.Initialize();
                Application.Run(new Main());
            }
            else
            {
                // Show message to user if the application is already running
                MessageBox.Show("The application '3D Model Accuracy Checker' is already running.",
                                "Instance Already Running",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            }
        }
    }
}