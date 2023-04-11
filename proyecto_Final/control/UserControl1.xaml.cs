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

namespace proyecto_Final.control
{
    /// <summary>
    /// Lógica de interacción para UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public event EventHandler ButtonClickEventClases;
        public event EventHandler ButtonClickEventSubclases;
        public event EventHandler ButtonClickEventRazas;
        public event EventHandler ButtonClickEventSubrazas;
        public UserControl1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Lanza el evento de para ir a clases
        /// </summary>
        private void Clases_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickEventClases?.Invoke(this, e);
        }
        /// <summary>
        /// Lanza el evento de para ir a subclases
        /// </summary>
        private void Subclases_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickEventSubclases?.Invoke(this, e);
        }
        /// <summary>
        /// Lanza el evento de para ir a razas
        /// </summary>
        private void Razas_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickEventRazas?.Invoke(this, e);
        }
        /// <summary>
        /// Lanza el eventeo de para ir a subrazas
        /// </summary>
        private void Subrazas_Click(object sender, RoutedEventArgs e)
        {
            ButtonClickEventSubrazas?.Invoke(this,e);
        }
    }
}
