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
    /// <summary>
    /// logica de interaccion de modificar.xaml
    /// </summary>
    public partial class modificar : Window
    {
        string nom;
        int esAdmin;
        NavigationWindow user;
        int categoria,admin;
        BBDD db;
        /// <summary>
        /// Constructor de la  clase modificar
        /// </summary>
        public modificar()
        {
            db= new BBDD(); 
            InitializeComponent();
        }
        /// <summary>
        /// Constructor de la clase que recibe parámetros para inicializar los campos correspondientes. Crea una instancia de la clase BBDD y establece los valores de los campos según los parámetros recibidos. Inicializa los componentes de la ventana.
        /// </summary>
        /// <param name="nom">Nombre</param>
        /// <param name="categoria">Categoría</param>
        /// <param name="esAdmin">Indica si es administrador</param>
        /// <param name="user">Ventana de navegación</param>
        public modificar(string nom, int categoria, int esAdmin, NavigationWindow user)
        {
            db = new BBDD();
            InitializeComponent();
            this.esAdmin = esAdmin;
            this.nom = nom;
            this.categoria = categoria;
            this.user = user;
        }

        /// <summary>
        /// Constructor de la clase que recibe parámetros para inicializar los campos correspondientes. Crea una instancia de la clase BBDD y establece los valores de los campos según los parámetros recibidos. Inicializa los componentes de la ventana.
        /// </summary>
        /// <param name="categoria">Categoría</param>
        /// <param name="user">Ventana de navegación</param>
        public modificar(int categoria, NavigationWindow user)
        {
            db = new BBDD();
            this.user = user;
            this.categoria = categoria;
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento Click del botón "Cancelar". Cierra la ventana actual.
        /// </summary>
        private void btn_cancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Maneja el evento Click del botón "Finalizar". Obtiene el valor seleccionado en el checkbox "CkAdmin" para determinar si es administrador o no. Según la categoría, realiza las acciones correspondientes en la base de datos (modificar nombre de usuario o insertar nuevo usuario). Luego, cierra la ventana actual.
        /// </summary>
        private void btn_Finalizar_Click(object sender, RoutedEventArgs e)
        {
            int admin = 0;
            if ((bool)CkAdmin.IsChecked)
                admin = 1;

            if (categoria == 1)
            {
                db.modificar(nombre.Text, admin, nom);
            }
            if (categoria == 2)
            {
                db.insertarusuarios(nombre.Text, cont.Text, "a", admin);
            }
            Close();
        }

        /// <summary>
        /// Maneja el evento Closing de la ventana. Crea una instancia de la ventana "Administrador" y realiza la navegación hacia ella. Luego, muestra la ventana de navegación y oculta la ventana actual.
        /// </summary>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Administrador ad = new Administrador();
            user.Navigate(ad);
            user.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Maneja el evento Click del checkbox "CkAdmin". Invierte el valor de la selección cuando se hace clic en el checkbox.
        /// </summary>
        private void CkAdmin_Click(object sender, RoutedEventArgs e)
        {
            if (CkAdmin.IsChecked == true)
            {
                CkAdmin.IsChecked = false;
            }
            else
                CkAdmin.IsChecked = true;
        }

        /// <summary>
        /// Maneja el evento Loaded de la ventana. Según la categoría, realiza las acciones correspondientes de inicialización de los campos y visibilidad de elementos de la ventana.
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (categoria == 1)
            {
                nombre.Text = nom;
                if (esAdmin == 1)
                    CkAdmin.IsChecked = true;
            }
            if (categoria == 2)
            {
                txt_cont.Visibility = Visibility.Visible;
                cont.Visibility = Visibility.Visible;
            }
        }
    }
}
