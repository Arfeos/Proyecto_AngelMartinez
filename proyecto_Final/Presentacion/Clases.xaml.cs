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
    public partial class Clases : Page
    {
        List<String> clases = new List<String>();
        Api api = new Api();

        public Clases()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarClases();
            clases = api.devolverlista();
            listaclase.ItemsSource = clases;
        }

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