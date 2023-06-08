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
    /// logica de interaccion de subrazas.xaml
    /// </summary>
    public partial class subRazas : Page
    {
        List<String> subrazas = new List<String>();
        Api api = new Api();
        /// <summary>
        /// constructor de la clase subrazas
        /// </summary>
        public subRazas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento Loaded de la página. Realiza una llamada asincrónica a la API para obtener la lista de subrazas y la asigna como origen de datos del control "listarazas".
        /// </summary>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarsubRazas();
            subrazas = api.devolverlista();
            listarazas.ItemsSource = subrazas;
        }

        /// <summary>
        /// Maneja el evento SelectionChanged del control "listarazas". Realiza una llamada asincrónica a la API para obtener la información de la subraza seleccionada y muestra los detalles en los controles de texto "clase" y "descripcion".
        /// </summary>
        private async void listaclase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await api.llamarsubRaza(listarazas.SelectedIndex);
            clase.Text = api.devolvernombre();
            descripcion.Text = api.devolverpgolpe();
        }
    }
}