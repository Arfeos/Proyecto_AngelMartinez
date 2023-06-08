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
using System.Windows.Shapes;

namespace proyecto_Final.Presentacion
{/// <summary>
 /// logica de interaccion de Registrar.xaml
 /// </summary>
    public partial class Registrar : Window
    {
        private InicioSesion inicioSesion;
        private BBDD miBase;
        /// <summary>
        /// constructor de la clase registrar
        /// </summary>
        /// <param name="inicioSesion"> ventana inicio de sesion</param>
        /// <param name="miBase"> base de datos</param>
        public Registrar(InicioSesion inicioSesion, BBDD miBase)
        {
            InitializeComponent();
            this.inicioSesion = inicioSesion;
            this.miBase = miBase;
        }

        /// <summary>
        /// Maneja el evento TextChanged del campo de texto "usuarioForm".
        /// </summary>
        private void usuarioForm_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Maneja el evento Click del botón "btnAcceder". Intenta insertar un nuevo usuario en la base de datos utilizando los valores de los campos de texto "usuarioForm", "contraseñaForm" y "Correo". Si la operación tiene éxito, muestra la ventana de inicio de sesión y cierra la ventana actual. En caso contrario, muestra un mensaje de error.
        /// </summary>
        private async void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            miBase = new BBDD();
            if (await miBase.insertarusuarios(usuarioForm.Text, contraseñaForm.Password.ToString(), Correo.Text, 0))
            {
                inicioSesion.Visibility = Visibility.Visible;
                this.Close();
            }
            else
            {
                MessageBox.Show("No se puede insertar");
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón "volver". Cierra la ventana actual.
        /// </summary>
        public void volver_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del botón "registrar". Intenta insertar un nuevo usuario en la base de datos utilizando los valores de los campos de texto "usuarioForm", "contraseñaForm" y "Correo". Si la operación tiene éxito, muestra la ventana de inicio de sesión y cierra la ventana actual. En caso contrario, muestra un mensaje de error.
        /// </summary>
        public async void registrar_Click(object sender, RoutedEventArgs e)
        {
            miBase = new BBDD();
            if (await miBase.insertarusuarios(usuarioForm.Text, contraseñaForm.Password.ToString(), Correo.Text, 0))
            {
                inicioSesion.Visibility = Visibility.Visible;
                this.Close();
            }
            else
            {
                MessageBox.Show("No se puede insertar");
            }
        }

        /// <summary>
        /// Maneja el evento Closed de la ventana. Restablece los campos de texto y muestra la ventana de inicio de sesión.
        /// </summary>
        private void Window_Closed(object sender, EventArgs e)
        {
            inicioSesion.usuarioForm.Text = "";
            inicioSesion.contraseñaForm.Password = "";
            inicioSesion.Visibility = Visibility.Visible;
        }
    }
   }
