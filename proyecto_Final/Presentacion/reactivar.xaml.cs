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
        public reactivar(Administrador ad)
        {
            this.ad = ad;
            InitializeComponent();
        }

        private void btn_cacelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listusu.ItemsSource = db.recogerusuariosinactivos();
        }

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
