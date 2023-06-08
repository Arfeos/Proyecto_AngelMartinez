using proyecto_Final.control;
using proyecto_Final.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Clase que representa la página de administrador en la aplicación.
    /// </summary>
    public partial class Administrador : Page
    {
        List<persona> lista = new List<persona>();
        BBDD bd = new BBDD();

        /// <summary>
        /// Constructor de la clase Administrador.
        /// </summary>
        public Administrador()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se dispara cuando la página se carga.
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Iniciar un hilo para actualizar la lista de personas
            Thread actualizarThread = new Thread(new ThreadStart(actualizar));
            actualizarThread.Start();
        }

        /// <summary>
        /// Método para actualizar la lista de personas en el UI.
        /// </summary>
        public void actualizar()
        {
            // Invocar en el hilo de la interfaz de usuario para evitar excepciones de subproceso cruzado
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                lista = bd.recoger();
                lista_bd.ItemsSource = bd.recoger();
            }));
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Borrar".
        /// </summary>
        private void Borrar_btn_Click(object sender, RoutedEventArgs e)
        {
            bd.borrar(usuario.Text);
            // Iniciar un hilo para actualizar la lista de personas
            Thread actualizarThread = new Thread(new ThreadStart(actualizar));
            actualizarThread.Start();
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Actualizar".
        /// </summary>
        private void Actualizar_btn_Click(object sender, RoutedEventArgs e)
        {
            string nom = lista[lista_bd.SelectedIndex].Usuario;
            int admin = lista[lista_bd.SelectedIndex].EsAdmin;
            NavigationWindow nav = (NavigationWindow)Window.GetWindow(this);
            modificar mod = new modificar(nom, 1, admin, nav);
            mod.Show();
            nav.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Añadir".
        /// </summary>
        private void añadir_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow nav = (NavigationWindow)Window.GetWindow(this);
            modificar mod = new modificar(2, nav);
            mod.Show();
            nav.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Reactivar".
        /// </summary>
        private void reactivar_btn_Click(object sender, RoutedEventArgs e)
        {
            reactivar rec = new reactivar(this);
            rec.Show();
        }
    }
}