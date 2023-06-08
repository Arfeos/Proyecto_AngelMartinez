using proyecto_Final.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proyecto_Final.Presentacion
{
    /// <summary>
    /// logica de interaccion de usuario.xaml
    /// </summary>
    public partial class usuario : NavigationWindow
    {
        bool inicio = false;
        InicioSesion mibase;
        persona per;

        /// <summary>
        /// contructor de la clase usuario
        /// </summary>
        /// <param name="f1"> ventana inicio de sesion</param>
        /// <param name="per">objeto persona</param>
        public usuario(InicioSesion f1, persona per)
        {
            this.per = per;
            this.mibase = f1;
            inicio = true;
            InitializeComponent();
        }
        /// <summary>
        /// contructor de la clase usuario
        /// </summary>
        /// <param name="f1"> ventana inicio de sesion</param>
        public usuario(InicioSesion f1)
        {
            this.mibase = f1;
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento Closed de la ventana NavigationWindow. Restablece los campos de nombre de usuario y contraseña en la ventana InicioSesion y la hace visible nuevamente.
        /// </summary>
        private void NavigationWindow_Closed(object sender, EventArgs e)
        {
            mibase.usuarioForm.Text = "";
            mibase.contraseñaForm.Password = "";
            mibase.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Maneja el evento Loaded de la ventana NavigationWindow. Navega a la página de inicio o muestra la página de inicio según el valor de la variable "inicio".
        /// </summary>
        private void NavigationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (inicio)
                this.Navigate(new inicio(per));
            else
                this.Navigate(new inicio(true));
        }

        /// <summary>
        /// Abre la ventana de ayuda cuando se hace clic en el botón correspondiente.
        /// </summary>
        private void ayuda(object sender, RoutedEventArgs e)
        {
            Ayuda ayu = new Ayuda();
            NavigationService.Navigate(ayu);
        }

        /// <summary>
        /// Maneja el comando ejecutado para cerrar la ventana.
        /// </summary>
        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}