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

    public partial class fichas : Window
    {
        persona usuario;
        BBDD db;
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
            lista_fichas.ItemsSource = db.devolverfichas(usuario.Usuario);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            wind.Visibility = Visibility.Visible;
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            db.borrarficha(lista_fichas.Items[lista_fichas.SelectedIndex].ToString());
            lista_fichas.ItemsSource = db.devolverfichas(usuario.Usuario);
        }

        private void Btncrear_Click(object sender, RoutedEventArgs e)
        {
            creacion creacion = new creacion(usuario.Usuario);
            creacion.ShowDialog();
            lista_fichas.ItemsSource = db.devolverfichas(usuario.Usuario);
        }

        private void BtnCargar_Click(object sender, RoutedEventArgs e)
        {
            VentFich ventfich = new VentFich();
            Ficha1 fich = new Ficha1(lista_fichas.Items[lista_fichas.SelectedIndex].ToString());
            ventfich.NavigationService.Navigate(fich);
            ventfich.Show();
        }
    }
}






