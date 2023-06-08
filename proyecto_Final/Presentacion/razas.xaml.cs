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
    /// logica de interaccion de razas.xaml
    /// </summary>
    public partial class razas : Page
    {
        List<String> Razas = new List<String>();
        Api api = new Api();
       /// <summary>
       /// contrusctor de la clase razas
       /// </summary>
        public razas()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Maneja el evento Loaded de la página. Realiza una llamada asíncrona a la API para obtener las razas. Luego, asigna la lista de razas devuelta por la API como origen de datos para el control "listarazas".
        /// </summary>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarRazas();
            Razas = api.devolverlista();
            listarazas.ItemsSource = Razas;
        }

        /// <summary>
        /// Maneja el evento SelectionChanged del control "listarazas". Realiza una llamada asíncrona a la API para obtener información detallada de la raza seleccionada. Luego, asigna los valores devueltos por la API a los elementos correspondientes de la interfaz de usuario (mejoras_stats, lenguaje, descripcion y rasgos).
        /// </summary>
        private async void listaclase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await api.llamarRaza(listarazas.SelectedIndex);
            mejoras_stats.Text = api.devolverventajas();
            lenguaje.Text = api.devolvernombre();
            descripcion.Text = api.devolverpgolpe();
            rasgos.Text = api.devolverobjetos();
        }


    }
}
