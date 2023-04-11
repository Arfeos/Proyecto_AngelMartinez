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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class InicioSesion : Window
    {
        private BBDD miBase;
        public InicioSesion()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Acceder".
        /// Crea una instancia de la clase BBDD para conectarse a la base de datos con los datos de usuario y contraseña.
        /// Si se puede conectar, crea un objeto persona y usuario y muestra la ventana de usuario. Oculta la ventana actual.
        /// Si no se puede conectar, muestra un mensaje de error.
        /// </summary>
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

        /// <summary>
        /// Evento que se dispara al hacer clic en el botón "Registrar".
        /// Crea una instancia de la clase Registrar y la muestra en una ventana. Oculta la ventana actual.
        /// </summary>
        private void btnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            Registrar reg = new Registrar(this, miBase);
            reg.Show();
            Visibility = Visibility.Collapsed;
        }
        /// <summary>
        /// Evento que se dispara al darle click  a acceder en el contextmenu.
        /// Crea una instancia de la clase BBDD para conectarse a la base de datos con los datos de usuario y contraseña.
        /// Si se puede conectar, crea un objeto persona y usuario y muestra la ventana de usuario. Oculta la ventana actual.
        /// Si no se puede conectar, muestra un mensaje de error.
        /// </summary>
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
    }
}