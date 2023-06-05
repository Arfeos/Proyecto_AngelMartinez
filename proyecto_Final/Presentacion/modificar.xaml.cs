using proyecto_Final.control;
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

    public partial class modificar : Window
    {
        string nom;
        int esAdmin;
        NavigationWindow user;
        int categoria,admin;
        BBDD db;

        public modificar()
        {
            db= new BBDD(); 
            InitializeComponent();
        }
        public modificar(string nom,int categoria, int esAdmin, NavigationWindow user)
        {
            db = new BBDD();
            InitializeComponent();
            this.esAdmin = esAdmin;
            this.nom = nom;
            this.categoria = categoria;
            this.user = user;
        }
        public modificar(int categoria, NavigationWindow user)
        {
            db = new BBDD();
            this.user= user;
            this.categoria = categoria;
            InitializeComponent();
        }
        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_Finalizar_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)CkAdmin.IsChecked)
                admin = 1;
            else
                admin = 0;

            if (categoria == 1) {
                db.modificar(nombre.Text, admin, nom);
            }
            if (categoria == 2) {
                db.insertarusuarios(nombre.Text,cont.Text,"a",admin);
            }
            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Administrador ad = new Administrador();
            user.Navigate(ad);
            user.Visibility = Visibility.Visible;
        }

        private void CkAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (CkAdmin.IsChecked == true)
            {
                CkAdmin.IsChecked = false;
            }
            else
                CkAdmin.IsChecked = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
       
            if (categoria == 1) {
                nombre.Text = nom;
                if(esAdmin==1)
                    CkAdmin.IsChecked= true;
            }
            if (categoria == 2)
            {
                txt_cont.Visibility = Visibility.Visible;
                cont.Visibility = Visibility.Visible;
            }
        }
    }
}
