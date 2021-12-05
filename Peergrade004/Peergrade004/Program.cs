using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Peergrade004
{
    /// <summary>
    ///  �������� �����, ���������� ����� �����.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// ������� ������� ����� � ����������.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // ������������� ��� ����������, �������� �������� �����.
            Application.ThreadException +=
                new System.Threading.ThreadExceptionEventHandler(ApplicationThreadException);

            // ������������� ��� ����������, �������� ������� ������.
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomainUnhandledException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        /// <summary>
        /// �������, ������������� �� ������ � ������������ � �������� ���� ����������.
        /// </summary>
        /// <param name="sender">������ ������� ��������������� �������.</param>
        /// <param name="e">����������, ���������� ���������� ��� ������������� ��� ���������� �������.</param>
        static void ApplicationThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            // ���������� ����������.
            ShowExceptionDetails(e.Exception);
        }

        /// <summary>
        /// �������, ������������� �� ������ � ������������ � �������� ����� ����������.
        /// </summary>
        /// <param name="sender">������ ������� ��������������� �������.</param>
        /// <param name="e">����������, ���������� ���������� ��� ������������� ��� ���������� �������.</param>
        static void CurrentDomainUnhandledException
            (object sender, UnhandledExceptionEventArgs e)
        {
            // ���������� ����������.
            ShowExceptionDetails(e.ExceptionObject as Exception);

            // ���������������� ������ ��������� ����.
            Thread.CurrentThread.Suspend();
        }

        /// <summary>
        /// �����, ��������� �� ����� ���������� �� ������ �� �����.
        /// </summary>
        /// <param name="ex">����������, ���������� ���������� �� ������.</param>
        static void ShowExceptionDetails(Exception ex)
        {
            // ������� ������ ������.
            MessageBox.Show(ex.Message, ex.TargetSite.ToString(),
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

