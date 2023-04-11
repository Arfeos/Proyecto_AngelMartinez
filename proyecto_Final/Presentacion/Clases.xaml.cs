using proyecto_Final.Recursos;
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
    /// Clase para mostrar la lista de clases disponibles y los datos de cada una.
    /// </summary>
    public partial class Clases : Page
    {
        // Lista para almacenar los nombres de las clases
        List<String> clases = new List<String>();

        // Instancia de la clase Api para llamar a los métodos necesarios
        Api api = new Api();

        /// <summary>
        /// Constructor de la clase, llama al método llamarClases() de la clase Api y llama al método InitializeComponent().
        /// </summary>
        public Clases()
        {
            

            InitializeComponent();
        }

        /// <summary>
        /// Método que se ejecuta cuando se carga la página. Limpia la lista de clases, llama al método devolverlista() de la clase Api
        /// para obtener los nombres de las clases y los muestra en la lista listaclase.
        /// </summary>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarClases();
            clases = api.devolverlista();
            listaclase.ItemsSource = clases;
        }

        /// <summary>
        /// Método que se ejecuta cuando se selecciona una clase de la lista. Llama al método llamarClase() de la clase Api para
        /// obtener los datos de la clase seleccionada y los muestra en la interfaz.
        /// </summary>
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



