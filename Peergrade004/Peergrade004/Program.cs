using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peergrade004
{
    /// <summary>
    ///  Основной класс, содержащий точку входа.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Главная входная точка в приложение.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Перехватываем все исключения, поднятые основным ядром.
            Application.ThreadException +=
                new System.Threading.ThreadExceptionEventHandler(ApplicationThreadException);

            // Перехватываем все исключения, поднятые другими ядрами.
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomainUnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        /// <summary>
        /// Событие, ответственное за работу с исключениями в основном ядре приложения.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // Показываем информацию.
            ShowExceptionDetails(e.Exception);
        }

        /// <summary>
        /// Событие, ответственное за работу с исключениями в побочных ядрах приложения.
        /// </summary>
        /// <param name="sender">Обьект который инициализировал событие.</param>
        /// <param name="e">Переменная, содержащая информацию для использования при реализации события.</param>
        static void CurrentDomainUnhandledException
            (object sender, UnhandledExceptionEventArgs e)
        {
            // Показываем информацию.
            ShowExceptionDetails(e.ExceptionObject as Exception);

            // Приостанавливаем работу побочного ядра.
            Thread.CurrentThread.Suspend();
        }

        /// <summary>
        /// Метод, выводящий на экран информацию об ошибке на экран.
        /// </summary>
        /// <param name="ex">Переменная, содержащая информацию об ошибке.</param>
        static void ShowExceptionDetails(Exception ex)
        {
            // Выводим детали ошибки.
            MessageBox.Show(ex.Message, ex.TargetSite.ToString(),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

