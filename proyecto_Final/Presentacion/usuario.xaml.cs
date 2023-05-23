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

    public partial class usuario : NavigationWindow
    {
        bool inicio = false;
        InicioSesion mibase;
        persona per;


        public usuario(InicioSesion f1, persona per)
        {
            this.per = per;
            this.mibase = f1;
            inicio = true;
            InitializeComponent();
        }
        public usuario(InicioSesion f1)
        {
            this.mibase = f1;
            InitializeComponent();
        }

        private void NavigationWindow_Closed(object sender, EventArgs e)
        {
            mibase.usuarioForm.Text = "";
            mibase.contraseñaForm.Password = "";
            mibase.Visibility = Visibility.Visible;
        }

        private void NavigationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (inicio)
                this.Navigate(new inicio(per));
            else
                this.Navigate(new inicio(true));
        }
        private void ayuda(object sender, RoutedEventArgs e)
        {
            Ayuda ayu = new Ayuda();
            NavigationService.Navigate(ayu);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }
    }
}