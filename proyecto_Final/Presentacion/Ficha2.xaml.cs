
using Org.BouncyCastle.Asn1.X500;
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
    /// Lógica de interacción para Ficha2.xaml
    /// </summary>
    public partial class Ficha2 : Page
    {
        string nom, clas, bon, niv;
        int at;
        Api api;
        BBDD db;
        List<string> lista;
        /// <summary>
        /// Maneja el evento al descargar la pagina.
        /// guarda los datos en la base de datos
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            await ficheros.GuardarDatos("hech" + nom + ".txt", nom, act1, act2, act3, act4, act5, act6, act7, act8, act9);
        }

        /// <summary>
        /// Constructor de la clase Ficha2.
        /// </summary>
        /// <param name="nombre">Nombre del personaje.</param>
        /// <param name="clase">Clase del personaje.</param>
        /// <param name="bono">Bono del personaje.</param>
        /// <param name="ataque">Valor de ataque del personaje.</param>
        /// <param name="niv">Nivel del personaje.</param>
        public Ficha2(string nombre, string clase, string bono, int ataque, string niv)
        {
            InitializeComponent();
            this.nom = nombre;
            this.clas = clase;
            this.bon = bono;
            this.niv = niv;
            this.at = ataque;
            db = new BBDD();
            api = new Api();
        }
        /// <summary>
        /// Maneja el evento de carga de la pagina.
        /// carga los datos del documento
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            carga car = new carga();
            car.Show();
            Nombre.Content = clas;
            spcas.Content = bon;
            spsa.Content = 8 + at;
            spcAtt.Content = at;
            await ficheros.CargarDatosDesdeArchivo("hech" + nom + ".txt", nom, act1, act2, act3, act4, act5, act6, act7, act8, act9);
            this.lista = await api.sacarniveles(clas, niv);
            for (int i = 0; i < lista.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        slt1.Content = lista[i];
                        break;
                    case 1:
                        slt2.Content = lista[i];
                        break;
                    case 2:
                        slt3.Content = lista[i];
                        break;
                    case 3:
                        slt4.Content = lista[i];
                        break;
                    case 4:
                        slt5.Content = lista[i];
                        break;
                    case 5:
                        slt6.Content = lista[i];
                        break;
                    case 6:
                        slt7.Content = lista[i];
                        break;
                    case 7:
                        slt8.Content = lista[i];
                        break;
                    case 8:
                        slt9.Content = lista[i];
                        break;
                }
            }
            cantrip.ItemsSource = await db.devolverhechizos(nom, 0);
            niv1.ItemsSource = await db.devolverhechizos(nom, 1);
            niv2.ItemsSource = await db.devolverhechizos(nom, 2);
            niv3.ItemsSource = await db.devolverhechizos(nom, 3);
            niv4.ItemsSource = await db.devolverhechizos(nom, 4);
            niv5.ItemsSource = await db.devolverhechizos(nom, 5);
            niv6.ItemsSource = await db.devolverhechizos(nom, 6);
            niv7.ItemsSource = await db.devolverhechizos(nom, 7);
            niv8.ItemsSource = await db.devolverhechizos(nom, 8);
            niv9.ItemsSource = await db.devolverhechizos(nom, 9);
            car.Close();
            this.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Maneja el evento de doble clic en un hechizo.
        /// Muestra los datos del hechizo en una ventana emergente.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private async void cantrip_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listbox = sender as ListBox;
            string desc = await api.devolverdeschech(listbox.SelectedItem.ToString());
            datos dat = new datos(listbox.SelectedItem.ToString(), desc);
            dat.Show();
        }
    }
}
