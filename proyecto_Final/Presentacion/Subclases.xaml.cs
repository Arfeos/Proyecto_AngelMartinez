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
{/// <summary>
/// logica de interaccion de subclases.xaml
/// </summary>
    public partial class subclases : Page
    {
        List<String> clases = new List<String>();
        Api api = new Api();
        /// <summary>
        /// constructor de la clase subclases
        /// </summary>
        public subclases()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento Loaded de la página. Realiza una llamada asincrónica a la API para obtener la lista de subclases y la asigna como origen de datos del control "listaclase".
        /// </summary>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarSubClases();
            clases = api.devolverlista();
            listaclase.ItemsSource = clases;
        }

        /// <summary>
        /// Maneja el evento SelectionChanged del control "listaclase". Realiza una llamada asincrónica a la API para obtener la información de la subclase seleccionada y muestra los detalles en los controles de texto "clase" y "desc".
        /// </summary>
        private async void listaclase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await api.llamarsubClase(listaclase.SelectedIndex);
            clase.Text = api.devolvernombre();
            desc.Text = api.devolverpgolpe();
        }
    }
}