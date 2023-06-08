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
{
    /// <summary>
    /// Lógica de interacción para reactivar.xaml
    /// </summary>
    public partial class reactivar : Window
    {
        Administrador ad;
        BBDD db= new BBDD();
        /// <summary>
        /// constructor de la clase reactivar
        /// </summary>
        /// <param name="ad">ventana administrador</param>
        public reactivar(Administrador ad)
        {
            this.ad = ad;
            InitializeComponent();
        }
        /// <summary>
        /// Maneja el evento Loaded de la ventana. Carga la lista de usuarios inactivos desde la base de datos y la asigna como origen de datos para el control "listusu".
        /// </summary>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listusu.ItemsSource = db.recogerusuariosinactivos();
        }

        /// <summary>
        /// Maneja el evento Click del botón "btn_cacelar". Cierra la ventana actual.
        /// </summary>
        private void btn_cacelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Maneja el evento Click del botón "btn_reactivar". Intenta reactivar el usuario seleccionado en la lista "listusu" llamando al método "reactivar" de la base de datos. Luego, actualiza la interfaz de usuario del administrador y cierra la ventana actual.
        /// </summary>
        private void btn_reactivar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.reactivar(listusu.SelectedItem.ToString());
                ad.actualizar();
                Close();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("elige un usuario");
            }
        }
    }
}
