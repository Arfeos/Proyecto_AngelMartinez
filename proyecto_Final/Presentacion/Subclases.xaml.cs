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
    public partial class subclases : Page
    {
        List<String> clases = new List<String>();


        Api api = new Api();
       
        public subclases()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método que se ejecuta cuando se carga la página. Carga la lista de clases y la muestra en pantalla.
        /// </summary>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarSubClases();
            clases = api.devolverlista();
            listaclase.ItemsSource = clases;
        }
        /// <summary>
        /// Método que se ejecuta cuando se selecciona una clase de la lista. Carga los datos de la subclase seleccionada y los muestra en pantalla.
        /// </summary>
        private async void listaclase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await api.llamarsubClase(listaclase.SelectedIndex);
            clase.Text = api.devolvernombre();
            desc.Text = api.devolverpgolpe();

        }

    }
}
