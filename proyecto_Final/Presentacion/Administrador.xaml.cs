using proyecto_Final.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class Administrador : Page
    {
        List<persona> lista = new List<persona>();
        BBDD bd = new BBDD();

        public Administrador()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Thread actualizarThread = new Thread(new ThreadStart(actualizar));
            actualizarThread.Start();
        }

        public void actualizar()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                lista = bd.recoger();
                lista_bd.ItemsSource = bd.recoger();
            }));
        }

        private void Borrar_btn_Click(object sender, RoutedEventArgs e)
        {
            bd.borrar(usuario.Text);
            Thread actualizarThread = new Thread(new ThreadStart(actualizar));
            actualizarThread.Start();
        }

        private void Actualizar_btn_Click(object sender, RoutedEventArgs e)
        {
            string nom = lista[lista_bd.SelectedIndex].Usuario;
            int admin = lista[lista_bd.SelectedIndex].EsAdmin;
            NavigationWindow nav = (NavigationWindow)Window.GetWindow(this);
            modificar mod = new modificar(nom, 1, admin, nav);
            mod.Show();
            nav.Visibility = Visibility.Collapsed;
        }

        private void añadir_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow nav = (NavigationWindow)Window.GetWindow(this);
            modificar mod = new modificar(2, nav);
            mod.Show();
            nav.Visibility = Visibility.Collapsed;
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}