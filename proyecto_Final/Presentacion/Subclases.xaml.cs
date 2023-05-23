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
    public partial class subclases : Page
    {
        List<String> clases = new List<String>();
        Api api = new Api();

        public subclases()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarSubClases();
            clases = api.devolverlista();
            listaclase.ItemsSource = clases;
        }

        private async void listaclase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await api.llamarsubClase(listaclase.SelectedIndex);
            clase.Text = api.devolvernombre();
            desc.Text = api.devolverpgolpe();
        }
    }
}