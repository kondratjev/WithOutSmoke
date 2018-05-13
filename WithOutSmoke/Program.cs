using System;
using System.Windows.Forms;

namespace WithOutSmoke
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Properties.Settings.Default.InPack != 0) Application.Run(new Form1());
            else
            {
                const string message = "Здравствуйте!\n\nЭта программа поможет Вам бросить курить.\n\n" +
                                       "После запуска, перейдите на вкладку \"Настройки\" и заполните необходимую информацию.\n\n" +
                                       "Если Вас интересует какая-либо информация по программе, можете написать мне личное сообщение.\n\n" +
                                       "Спасибо за использование!";
                MessageBox.Show(message, "Приветствие!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Properties.Settings.Default.DateTimeQuit = DateTime.Now;
                // Properties.Settings.Default.TimeToSleep = DateTime.Now;
                // Properties.Settings.Default.TimeToWake = DateTime.Now;
                Properties.Settings.Default.ForDay = 20;
                Properties.Settings.Default.InPack = 20;
                Properties.Settings.Default.InAshtray = 40;
                Properties.Settings.Default.TimeToCig = 5;
                Properties.Settings.Default.Puffs = 15;
                Properties.Settings.Default.Breath = 3;
                Properties.Settings.Default.Resin = 6;
                Properties.Settings.Default.Nicotine = 0.5;
                Properties.Settings.Default.CarbonMonoxide = 7;
                Properties.Settings.Default.StartPrice = 50;
                Properties.Settings.Default.EndPrice = 80;
                Properties.Settings.Default.HowOld = 1;
                Properties.Settings.Default.Currency = "руб.";
                Properties.Settings.Default.StartMinimized = false;
                Properties.Settings.Default.CheckUpdate = true;
                Properties.Settings.Default.HideToTray = false;
                Properties.Settings.Default.Save();
                Application.Run(new Form1());
            }
        }
    }
}
