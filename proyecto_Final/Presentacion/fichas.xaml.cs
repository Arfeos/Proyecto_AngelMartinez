using proyecto_Final.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Lógica de interacción para fichas.xaml
    /// </summary>
    public partial class fichas : Window
    {
        persona usuario;
        BBDD db;
        List<string> listfichas;
        NavigationWindow wind;
        public fichas(persona us,NavigationWindow pag)
        {
            this.wind = pag;
            this.usuario = us;
            db = new BBDD();
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            listfichas = db.devolverfichas(usuario.Usuario);
            lista_fichas.ItemsSource = listfichas;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            wind.Visibility = Visibility.Visible;
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            db.borrarficha(listfichas[lista_fichas.SelectedIndex]);
            lista_fichas.ItemsSource = listfichas;
        }

        private void Btncrear_Click(object sender, RoutedEventArgs e)
        {
            creacion creacion = new creacion();
            creacion.Show();
        }
    }
}
