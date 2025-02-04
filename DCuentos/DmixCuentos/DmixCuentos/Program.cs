using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;

namespace DmixCuentos
{
    public static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var miForm = new DCuentos();
            Application.Run(miForm);

        }
    }

    public class Cliente()
    {
        public void Main(string[] args)
        {
            
        }

        
    }
}