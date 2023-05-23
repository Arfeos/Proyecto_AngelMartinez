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

    public partial class razas : Page
    {
        List<String> Razas = new List<String>();
        Api api = new Api();
       
        public razas()
        {
            

            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await api.llamarRazas();
            Razas =api.devolverlista();
 
            listarazas.ItemsSource=Razas;  

        }

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
