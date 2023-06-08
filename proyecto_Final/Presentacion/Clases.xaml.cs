using proyecto_Final.control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
    /// Clase parcial que representa la página "Clases" en la aplicación.
    /// </summary>
    public partial class Clases : Page
    {
        List<String> clases = new List<String>();
        Api api = new Api();

        /// <summary>
        /// Constructor de la clase Clases.
        /// </summary>
        public Clases()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Manejador del evento "Loaded" de la página.
        /// Se llama cuando la página se carga y se utiliza para cargar las clases desde la API y asignarlas a un control de lista.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarClases();
            clases = api.devolverlista();
            listaclase.ItemsSource = clases;
        }

        /// <summary>
        /// Manejador del evento "SelectionChanged" del control de lista de clases.
        /// Se llama cuando se selecciona una clase de la lista y se utiliza para cargar los detalles de la clase seleccionada desde la API y mostrarlos en controles de texto.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private async void listaclase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await api.llamarClase(listaclase.SelectedIndex);
            nombre.Text = api.devolvernombre();
            pgolpe.Text = api.devolverpgolpe();
            Ventajas.Text = api.devolverventajas();
            objetos_inciales.Text = api.devolverobjetos();
        }
    }
}