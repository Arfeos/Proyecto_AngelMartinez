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
    /// Lógica de interacción para Clases.xaml
    /// </summary>
    public partial class razas : Page
    {
        List<String> Razas = new List<String>();
        Api api = new Api();
       
        public razas()
        {
            api.llamarRazas();

            InitializeComponent();
        }

        /// <summary>
        /// evento que carga la lista al cargar la pagina
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Razas =api.devolverlista();
            MessageBox.Show("En la siguiente pantalla al elegir una raza apreceran sus datos");
            listarazas.ItemsSource=Razas;  

        }
        /// <summary>
        /// evento que al seleccionar una raza carga sus datos
        /// </summary>
        private void listaclase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            api.llamarRaza(listarazas.SelectedIndex);

            MessageBox.Show("elegido");
            mejoras_stats.Text = api.devolverventajas();
            lenguaje.Text = api.devolvernombre();
            descripcion.Text = api.devolverpgolpe();
            rasgos.Text = api.devolverobjetos();

        }

        
    }
}
