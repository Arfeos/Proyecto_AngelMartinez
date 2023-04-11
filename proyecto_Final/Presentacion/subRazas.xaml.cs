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
    /// Página que muestra las subrazas disponibles.
    /// </summary>
    public partial class subRazas : Page
    {
        List<String> subrazas = new List<String>();
        Api api = new Api();

        public subRazas()
        {
            api.llamarsubRazas();
            InitializeComponent();
        }


        /// <summary>
        /// Carga las subrazas disponibles en la lista de subrazas.
        /// </summary>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            subrazas = api.devolverlista();
            MessageBox.Show("En la siguiente pantalla al elegir una subraza aparecerán sus datos");
            listarazas.ItemsSource = subrazas;
        }

        /// <summary>
        /// Muestra los datos de la subraza seleccionada.
        /// </summary>
        private void listaclase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            api.llamarsubRaza(listarazas.SelectedIndex);
            MessageBox.Show("elegido");
            clase.Text = api.devolvernombre();
            descripcion.Text = api.devolverpgolpe();
        }
    }
}