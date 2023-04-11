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
    /// <summary>
    /// Lógica de interacción para Registrar.xaml
    /// </summary>
    public partial class Registrar : Window
    {
        private InicioSesion inicioSesion;
        private BBDD miBase;

        /// <summary>
        /// Inicializa una nueva instancia de la clase Registrar
        /// </summary>
        /// <param name="inicioSesion">Instancia de la ventana de inicio de sesión</param>
        /// <param name="miBase">Instancia de la base de datos</param>
        public Registrar(InicioSesion inicioSesion, BBDD miBase)
        {
            InitializeComponent();
            this.inicioSesion = inicioSesion;
            this.miBase = miBase;
        }

        /// <summary>
        /// Maneja el evento TextChanged del control usuarioForm
        /// </summary>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void usuarioForm_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Maneja el evento Click del control btnAcceder
        /// </summary>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void btnAcceder_Click(object sender, RoutedEventArgs e)
        {
            miBase = new BBDD(); // Crea una nueva instancia de la base de datos
            if (miBase.insertar(usuarioForm.Text, contraseñaForm.Password.ToString()) )// Inserta el usuario y la contraseña en la base de datos
            {
                inicioSesion.Visibility = Visibility.Visible; // Hace visible la ventana de inicio de sesión
                this.Close(); // Cierra la ventana actual

            }
            else
            {
                MessageBox.Show("No se puede insertar"); // Muestra un mensaje de error si no se puede insertar en la base de datos
            }
        }
        /// <summary>
        /// Maneja el evento Click del control que al pulsar volver en el context menu devuelve a inicio sesion
        /// </summary>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        public void volver_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }
        /// <summary>
        /// Maneja el evento Click del control que al pulsar registrar en el context menu registra al usuario en la base de datos
        /// </summary>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        public void registrar_Click(object sender, RoutedEventArgs e) {
            miBase = new BBDD(); // Crea una nueva instancia de la base de datos
            if (miBase.insertar(usuarioForm.Text, contraseñaForm.Password.ToString())) // Inserta el usuario y la contraseña en la base de datos
            {
                inicioSesion.Visibility = Visibility.Visible; // Hace visible la ventana de inicio de sesión
                this.Close(); // Cierra la ventana actual

            }
            else
            {
                MessageBox.Show("No se puede insertar"); // Muestra un mensaje de error si no se puede insertar en la base de datos
            }
        }

        /// <summary>
        /// Maneja el evento Closed de la ventana
        /// </summary>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            inicioSesion.usuarioForm.Text = "";
            inicioSesion.contraseñaForm.Password = "";
            inicioSesion.Visibility = Visibility.Visible;
        }
    }
}