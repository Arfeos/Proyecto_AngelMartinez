using proyecto_Final.control;
using proyecto_Final.Recursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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

    public partial class InicioSesion : Window
    {
        private BBDD miBase;
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            miBase = new BBDD();
            if (miBase.Conectar(usuarioForm.Text, contraseñaForm.Password.ToString()))
            {
                persona per = new persona(miBase.EsUsuario(), miBase.EsAdministrador());
                usuario user = new usuario(this, per);
                user.Show();
                Visibility = Visibility.Collapsed;


            }
            else
            {
                MessageBox.Show("No se puede acceder");
            }
        }

        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Registrar reg = new Registrar(this, miBase);
            reg.Show();
            Visibility = Visibility.Collapsed;
        }

        private void Acceder_Click(object sender, RoutedEventArgs e)
        {
            miBase = new BBDD();
            if (miBase.Conectar(usuarioForm.Text, contraseñaForm.Password.ToString()))
            {
                persona per = new persona(miBase.EsUsuario(), miBase.EsAdministrador());
                usuario user = new usuario(this, per);
                user.Show();
                Visibility = Visibility.Collapsed;


            }
            else
            {
                MessageBox.Show("No se puede acceder");
            }
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            usuario user = new usuario(this);
            user.Show();
            Visibility = Visibility.Collapsed;

        }



        //private async void btn_rellenar_Click(object sender, RoutedEventArgs e)
        //{
        //    Api api = new Api();
        //    BBDD db = new BBDD();
        //    db.conectar();
        //      await api.llenarClase(db);
        //    await api.llenarHechizos(db);
        //    await api.llenarRasgos(db);
        //       await api.llenarRaza(db);
        //      await api.llenarsubclases(db);
        //       await api.llenarsubrazas(db);
        //        await api.llenarrazarasgo(db);
        //    db.desconectar();

            
        //}
        
    } 
}


    