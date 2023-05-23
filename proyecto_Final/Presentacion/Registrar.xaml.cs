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
using System.Windows.Shapes;

namespace proyecto_Final.Presentacion
{
    public partial class Registrar : Window
    {
        private InicioSesion inicioSesion;
        private BBDD miBase;

        public Registrar(InicioSesion inicioSesion, BBDD miBase)
        {
            InitializeComponent();
            this.inicioSesion = inicioSesion;
            this.miBase = miBase;
        }

        private void usuarioForm_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            miBase = new BBDD();
            if (miBase.insertarusuarios(usuarioForm.Text, contraseñaForm.Password.ToString(), Correo.Text, 0))
            {
                inicioSesion.Visibility = Visibility.Visible;
                this.Close();
            }
            else
            {
                MessageBox.Show("No se puede insertar");
            }
        }

        public void volver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void registrar_Click(object sender, RoutedEventArgs e)
        {
            miBase = new BBDD();
            if (miBase.insertarusuarios(usuarioForm.Text, contraseñaForm.Password.ToString(), Correo.Text, 0))
            {
                inicioSesion.Visibility = Visibility.Visible;
                this.Close();
            }
            else
            {
                MessageBox.Show("No se puede insertar");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            inicioSesion.usuarioForm.Text = "";
            inicioSesion.contraseñaForm.Password = "";
            inicioSesion.Visibility = Visibility.Visible;
        }
    }
}
