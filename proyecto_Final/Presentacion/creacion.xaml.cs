using proyecto_Final.control;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para creacion.xaml
    /// </summary>
    public partial class creacion : Window
    {
        BBDD db;
        string usuario;
        int fuerza, destreza, constitucion, sabiduria, inteligencia, carisma, contador;
        Api api;
        /// <summary>
        /// Clase parcial que representa la ventana de creación de personajes.
        /// </summary>
        public creacion(string us)
        {
            this.usuario = us;
            fuerza = 8;
            destreza = 8;
            constitucion = 8;
            sabiduria = 8;
            inteligencia = 8;
            carisma = 8;
            contador = 27;
            db = new BBDD();
            api = new Api();
            InitializeComponent();
        }

        /// <summary>
        /// Manejador del evento "Click" del botón principal.
        /// Se llama cuando se hace clic en uno de los botones de la ventana y realiza diferentes acciones según el botón presionado.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button botonPresionado = (Button)sender;
            switch (botonPresionado.Name)
            {
                // ...

                case "btnmenCa":
                    if (carisma > 8)
                    {
                        if (inteligencia < 13)
                        {
                            inteligencia--;
                            contador++;
                        }
                        else
                        {
                            inteligencia--;
                            contador += 2;
                        }
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 8, no puede bajar de esto");
                    }
                    break;
            }
            Thread actualizarThread = new Thread(new ThreadStart(actualizar));
            actualizarThread.Start();
        }

        /// <summary>
        /// Manejador del evento "Click" del botón "Randomizar".
        /// Se llama cuando se hace clic en el botón "Randomizar" y genera valores aleatorios para los atributos del personaje.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private async void btn_rand_Click(object sender, RoutedEventArgs e)
        {
            randomizar();
        }

        /// <summary>
        /// Manejador del evento "Click" del botón "Crear".
        /// Se llama cuando se hace clic en el botón "Crear" y realiza la creación del personaje en la base de datos.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private async void btncrear_Click(object sender, RoutedEventArgs e)
        {
            if (CBRaza.Text == "Half-Elf") {
                MessageBox.Show("al cargar la ficha por primera vez tienes 1+ en dos caracteristicas a elegir entre Fuerza,destreza,constitucion,inteligencia o sabiduria");
            }
            if (contador != 0) {
                MessageBox.Show("faltan puntos por gastar");
            }
            else { 
            if (CBRaza.Items[CBRaza.SelectedIndex].ToString()=="None") {
                MessageBox.Show("seleccione una raza valida");
            }
            else {
                    carga car = new carga();
                    car.Show();
                    List<string> comp = new List<string> { };
            db.insertarficha(CBClase.Text,CBSubclase.Text,CBRaza.Text,CBSubraza.Text,nombre.Text,usuario,fuerza,destreza,constitucion,sabiduria,inteligencia,carisma);
                    await api.añadirdatosraza(CBRaza.Text,nombre.Text);
                    await api.añadirdatossubraza(CBSubraza.Text, nombre.Text);
                    await api.añadirdatossubclase(CBSubclase.Text, nombre.Text);
                    await api.añadirdatosclase(CBClase.Text, nombre.Text);
                    car.Close();
            Close();
            }
            }
        }


        /// <summary>
        /// Actualiza los valores mostrados en la ventana de creación de personajes.
        /// </summary>
        private void actualizar()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                contFu.Content = fuerza.ToString();
                contDe.Content = destreza.ToString();
                contCo.Content = constitucion.ToString();
                contSa.Content = sabiduria.ToString();
                contIn.Content = inteligencia.ToString();
                contCa.Content = carisma.ToString();
                txtcontador.Content = "quedan " + contador.ToString() + " a repartir";
            }));
        }

        /// <summary>
        /// Genera un nombre aleatorio para el personaje basado en la raza seleccionada.
        /// </summary>
        private async void randomizar()
        {
            Api api = new Api();
            nombre.Text = await api.devolvernombre(CBRaza.Items[CBRaza.SelectedIndex].ToString().ToLower());
        }

        /// <summary>
        /// Manejador del evento de cambio de selección en el ComboBox de raza.
        /// Actualiza las opciones disponibles en el ComboBox de subraza según la raza seleccionada.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void CBRaza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBSubraza.ItemsSource = null;
            CBSubraza.Items.Clear();
            CBSubraza.ItemsSource = db.devolversubrazas(db.devolverRazas()[CBRaza.SelectedIndex]);
            if (CBSubraza.Items.Count == null || CBSubraza.Items.Count == 0)
            {
                CBSubraza.ItemsSource = null;
                CBSubraza.Items.Add("None");
            }
            CBSubraza.SelectedIndex = 0;
        }

        /// <summary>
        /// Manejador del evento de cambio de selección en el ComboBox de clase.
        /// Actualiza las opciones disponibles en el ComboBox de subclase según la clase seleccionada.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void CBClase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBSubclase.ItemsSource = null;
            CBSubclase.ItemsSource = db.devolversubclase(db.devolverClases()[CBClase.SelectedIndex]);
            CBSubclase.SelectedIndex = 0;
        }

        /// <summary>
        /// Manejador del evento "Loaded" de la ventana de creación de personajes.
        /// Inicializa los valores mostrados en la ventana y los ComboBox de clase y raza.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Argumentos del evento.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            contFu.Content = fuerza.ToString();
            contDe.Content = destreza.ToString();
            contCo.Content = constitucion.ToString();
            contSa.Content = sabiduria.ToString();
            contIn.Content = inteligencia.ToString();
            contCa.Content = carisma.ToString();
            txtcontador.Content = "quedan " + contador.ToString() + " a repartir";
            CBClase.ItemsSource = db.devolverClases();
            CBRaza.ItemsSource = db.devolverRazas();
            CBRaza.SelectedIndex = 8;
        }

    }
}
