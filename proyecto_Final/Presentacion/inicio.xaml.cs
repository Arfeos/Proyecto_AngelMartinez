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
{
    /// <summary>
    /// Página de inicio del programa.
    /// </summary>
    public partial class inicio : Page
    {
        bool invitado = false;
        persona per; // Objeto de persona que representa al usuario actual
        UserControl1 userControl;

        /// <summary>
        /// Constructor de la página de inicio.
        /// </summary>
        /// <param name="per">Objeto de persona que representa al usuario actual.</param>
        public inicio(persona per)
        {
            this.per = per; // Guardar el objeto de persona

            InitializeComponent();

            // Asignar manejadores de eventos para los botones del UserControl de navegación
            navegador.ButtonClickEventClases += HandleButtonClickClases;
            navegador.ButtonClickEventSubclases += HandleButtonClickSubClases;
            navegador.ButtonClickEventRazas += HandleButtonClickRazas;
            navegador.ButtonClickEventSubrazas += HandleButtonClickSubRazas;
        }
        public inicio(bool invitado)
        {
            this.invitado = invitado; // Guardar el objeto de persona

            InitializeComponent();

            // Asignar manejadores de eventos para los botones del UserControl de navegación
            navegador.ButtonClickEventClases += HandleButtonClickClases;
            navegador.ButtonClickEventSubclases += HandleButtonClickSubClases;
            navegador.ButtonClickEventRazas += HandleButtonClickRazas;
            navegador.ButtonClickEventSubrazas += HandleButtonClickSubRazas;
        }

        /// <summary>
        /// Abrir la página de ayuda al presionar el botón correspondiente.
        /// </summary>
        private void ayuda_Click(object sender, RoutedEventArgs e)
        {
            Ayuda ayu = new Ayuda();
            NavigationService.Navigate(ayu);
        }

        /// <summary>
        /// Mostrar la interfaz de administrador si el usuario actual es un administrador.
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (invitado)
            {
                Fichas.Visibility = Visibility.Collapsed;
                cerrar_sesion.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (per.EsAdmin == 1)
                    administrador.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Cerrar la sesión del usuario actual y salir del programa.
        /// </summary>
        private void cerrar_sesion_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow window = (NavigationWindow)Window.GetWindow(this);
            window.Close();
        }

        /// <summary>
        /// Manejador de eventos para el botón "Clases" del UserControl de navegación.
        /// </summary>
        public void HandleButtonClickClases(object sender, EventArgs e)
        {
            Clases clases = new Clases();
            NavigationService.Navigate(clases);
        }

        /// <summary>
        /// Manejador de eventos para el botón "Subclases" del UserControl de navegación.
        /// </summary>
        public void HandleButtonClickSubClases(object sender, EventArgs e)
        {
            subclases subc = new subclases();
            NavigationService.Navigate(subc);
        }

        /// <summary>
        /// Manejador de eventos para el botón "Razas" del UserControl de navegación.
        /// </summary>
        public void HandleButtonClickRazas(object sender, EventArgs e)
        {
            razas raz = new razas();
            NavigationService.Navigate(raz);
        }

        /// <summary>
        /// Manejador de eventos para el botón "Subrazas" del UserControl de navegación.
        /// </summary>
        public void HandleButtonClickSubRazas(object sender, EventArgs e)
        {
            subRazas subz = new subRazas();
            NavigationService.Navigate(subz);
        }

        /// <summary>
        /// Mostrar la pagina de administrador si esta existe
        /// </summary>
        private void administrador_Click(object sender, RoutedEventArgs e)
        {
            Administrador ad = new Administrador();
            NavigationService.Navigate(ad);
        }

        private void Fichas_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow window = (NavigationWindow)Window.GetWindow(this);
            fichas fic = new fichas(per, window);
            fic.Show();
            window.Visibility = Visibility.Collapsed;
        }
    }
}

