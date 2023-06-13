using proyecto_Final.control;
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
{  /// <summary>
   /// Lógica de interacción para inicio.xaml
   /// </summary>
    public partial class inicio : Page
    {
        bool invitado = false;
        persona per; // Objeto de persona que representa al usuario actual
        UserControl1 userControl;

        /// <summary>
        /// Constructor de la clase inicio para un usuario autenticado.
        /// </summary>
        /// <param name="per">Objeto persona que representa al usuario actual.</param>
        public inicio(persona per)
        {
            this.per = per;
            InitializeComponent();

            navegador.ButtonClickEventClases += HandleButtonClickClases;
            navegador.ButtonClickEventSubclases += HandleButtonClickSubClases;
            navegador.ButtonClickEventRazas += HandleButtonClickRazas;
            navegador.ButtonClickEventSubrazas += HandleButtonClickSubRazas;
        }

        /// <summary>
        /// Constructor de la clase inicio para un usuario invitado.
        /// </summary>
        /// <param name="invitado">Indica si el usuario es invitado o no.</param>
        public inicio(bool invitado)
        {
            this.invitado = invitado;
            InitializeComponent();

            navegador.ButtonClickEventClases += HandleButtonClickClases;
            navegador.ButtonClickEventSubclases += HandleButtonClickSubClases;
            navegador.ButtonClickEventRazas += HandleButtonClickRazas;
            navegador.ButtonClickEventSubrazas += HandleButtonClickSubRazas;
        }

        /// <summary>
        /// Maneja el evento Click del botón "ayuda".
        /// Navega hacia la página de Ayuda.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void ayuda_Click(object sender, RoutedEventArgs e)
        {
            Ayuda ayu = new Ayuda();
            NavigationService.Navigate(ayu);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (invitado)
            {
                Fichas.Visibility = Visibility.Collapsed;
                cerrar_sesion.Visibility = Visibility.Collapsed;
                saludo.Content = "Buenos dias forastero";
                texto.Text = "como forastero podrá consultar datos de clases, subclases, razas y subrazas, pero si desea crear y consultar fichas tendrá que darle a cerrar sesión y registrarse";
            }
            else
            {
                if (per.EsAdmin == 1)
                {
                    administrador.Visibility = Visibility.Visible;
                    saludo.Content = "Buenos dias administrador";
                    texto.Text = "como administrador no solo tendrás los permisos que tienen el usuario registrado si no que tendrá acceso a la parte de administrador donde podrás cambiar los privilegios de usuario, crear nuevos usuarios o darlos de baja";
                }
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón "cerrar_sesion".
        /// Cierra la ventana actual y vuelve a la ventana de inicio de sesión.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void cerrar_sesion_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow window = (NavigationWindow)Window.GetWindow(this);
            window.Close();
        }

        /// <summary>
        /// Maneja el evento click clases
        /// Cierra la ventana actual y va ha la ventana clases.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        public void HandleButtonClickClases(object sender, EventArgs e)
        {
            Clases clases = new Clases();
            NavigationService.Navigate(clases);
        }
        /// <summary>
        /// Maneja el evento click subclases
        /// Cierra la ventana actual y va ha la ventana subclases.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        public void HandleButtonClickSubClases(object sender, EventArgs e)
        {
            subclases subc = new subclases();
            NavigationService.Navigate(subc);
        }
        /// <summary>
        /// Maneja el evento click razas
        /// Cierra la ventana actual y va ha la ventana Raza.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        public void HandleButtonClickRazas(object sender, EventArgs e)
        {
            razas raz = new razas();
            NavigationService.Navigate(raz);
        }
        /// <summary>
        /// Maneja el evento click subrazas
        /// Cierra la ventana actual y va ha la ventana subraza.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        public void HandleButtonClickSubRazas(object sender, EventArgs e)
        {
            subRazas subz = new subRazas();
            NavigationService.Navigate(subz);
        }

        /// <summary>
        /// Maneja el evento Click del botón "administrador".
        /// Navega hacia la página de administrador.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void administrador_Click(object sender, RoutedEventArgs e)
        {
            Administrador ad = new Administrador();
            NavigationService.Navigate(ad);
        }

        /// <summary>
        /// Maneja el evento Click del botón "Fichas".
        /// Abre la ventana de fichas asociada al usuario actual.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void Fichas_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow window = (NavigationWindow)Window.GetWindow(this);
            fichas fic = new fichas(per, window);
            fic.Show();
            window.Visibility = Visibility.Collapsed;
        }
    }
}