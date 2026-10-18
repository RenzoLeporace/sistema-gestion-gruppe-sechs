using System;
using System.Windows.Forms;

namespace UI
{
    internal static class Program
    {
        /// <summary>
        ///  Punto de entrada principal de la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            // Mostrar login antes de inicializar la ventana principal
            using (var login = new Login())
            {
                var dr = login.ShowDialog();
                if (dr != DialogResult.OK)
                {
                    // Si el usuario cierra/cancela el login, salir de la app
                    return;
                }
            }

            // Si el login fue correcto, abrir la ventana principal (Form1)
            Application.Run(new Form1());
        }
    }
}