using proyecto_Final.Recursos;
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
        string usuario;
        int fuerza, destreza, constitucion, sabiduria, inteligencia, carisma, contador;
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
            InitializeComponent();

        }
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button botonPresionado = (Button)sender;
            switch (botonPresionado.Name)
            {

                case "btnmasFu":
                    if (fuerza < 15 && contador > 0) {
                        if (fuerza < 13) {
                            fuerza++;
                            contador--;
                        }                    
                        else {
                            if (contador >= 2)
                            {
                                fuerza++;
                                contador -= 2;
                            }
                            else
                                MessageBox.Show("no hay puntos suficientes");
                        }
                    }
                    else if (contador <= 0)
                    {
                        MessageBox.Show("no quedan puntas");
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 15, no puede subir del mismo");
                    }
                    break;

                case "btnmenFu":
                    if (fuerza > 8) {
                        if (fuerza < 13)
                        {
                            fuerza--;
                            contador++;
                        }
                        else
                        {
                            fuerza--;
                            contador += 2;
                        }
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 8, no puede bajar de esto");
                    }
                    break;

                case "btnmasDe":
                    if (destreza < 15 && contador > 0)
                    {
                        if (destreza < 13)
                        {
                            destreza++;
                            contador--;
                        }
                        else
                        {
                            if (contador >= 2)
                            {
                                destreza++;
                                contador -= 2;
                            }
                            else
                                MessageBox.Show("no hay puntos suficientes");
                        }
                    }
                    else if (contador <= 0)
                    {
                        MessageBox.Show("no quedan puntas");
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 15");
                    }
                    break;

                case "btnmenDe":
                    if (destreza > 8)
                    {
                        if (destreza < 13)
                        {
                            destreza--;
                            contador++;
                        }
                        else
                        {
                            destreza--;
                            contador += 2;
                        }
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 8, no puede bajar de esto");
                    }
                    break;

                case "btnmasCo":
                    if (constitucion < 15 && contador > 0)
                    {
                        if (constitucion < 13)
                        {
                            constitucion++;
                            contador--;
                        }
                        else
                        {
                            if (contador >= 2)
                            {
                                constitucion++;
                                contador -= 2;
                            }
                            else
                                MessageBox.Show("no hay puntos suficientes");
                        }
                    }
                    else if (contador <= 0)
                    {
                        MessageBox.Show("no quedan puntas");
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 15");
                    }
                    break;

                case "btnmenCo":
                    if (constitucion > 8)
                    {
                        if (constitucion < 13)
                        {
                            constitucion--;
                            contador++;
                        }
                        else
                        {
                            constitucion--;
                            contador += 2;
                        }
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 8, no puede bajar de esto");
                    }
                    break;

                case "btnmasSa":
                    if (sabiduria < 15 && contador > 0)
                    {
                        if (sabiduria < 13)
                        {
                            sabiduria++;
                            contador--;
                        }
                        else
                        {
                            if (contador >= 2)
                            {
                                sabiduria++;
                                contador -= 2;
                            }
                            else
                                MessageBox.Show("no hay puntos suficientes");
                        }
                    }
                    else if (contador <= 0)
                    {
                        MessageBox.Show("no quedan puntas");
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 15");
                    }
                    break;

                case "btnmenSa":
                    if (sabiduria > 8)
                    {
                        if (sabiduria < 13)
                        {
                            sabiduria--;
                            contador++;
                        }
                        else
                        {
                            sabiduria--;
                            contador += 2;
                        }
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 8, no puede bajar de esto");
                    }
                    break;

                case "btnmasIn":
                    if (inteligencia < 15 && contador > 0)
                    {
                        if (inteligencia < 13)
                        {
                            inteligencia++;
                            contador--;
                        }
                        else
                        {
                            if (contador >= 2)
                            {
                                inteligencia++;
                                contador -= 2;
                            }
                            else
                                MessageBox.Show("no hay puntos suficientes");
                        }
                    }
                    else if (contador <= 0)
                    {
                        MessageBox.Show("no quedan puntas");
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 15");
                    }
                    break;

                case "btnmenIn":
                    if (inteligencia > 8)
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

                case "btnmasCa":
                    if (carisma < 15 && contador > 0)
                    {
                        if (carisma < 13)
                        {
                            carisma++;
                            contador--;
                        }
                        else
                        {
                            if (contador >= 2)
                            {
                                carisma++;
                                contador -= 2;
                            }
                            else
                                MessageBox.Show("no hay puntos suficientes");
                        }
                    }
                    else if (contador <= 0)
                    {
                        MessageBox.Show("no quedan puntas");
                    }
                    else
                    {
                        MessageBox.Show("el limite de creacion es de 15");
                    }
                    break;
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

        private void btn_rand_Click(object sender, RoutedEventArgs e)
        {
            randomizar();
        }

        private void btncrear_Click(object sender, RoutedEventArgs e)
        {
            if (CBRaza.Items[CBRaza.SelectedIndex].ToString()=="None") {
                MessageBox.Show("seleccione una raza valida");
            }
            else { 
            db.insertarficha(CBClase.Text,CBSubclase.Text,CBRaza.Text,CBSubraza.Text,nombre.Text,usuario,fuerza,destreza,constitucion,sabiduria,inteligencia,carisma);
            Close();
            }
        }



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
        private  async void randomizar() {
            Api api = new Api();
            nombre.Text = await api.devolvernombre(CBRaza.Items[CBRaza.SelectedIndex].ToString().ToLower());
            
        }

        private void CBRaza_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBSubraza.ItemsSource = null;
            CBSubraza.Items.Clear();
            CBSubraza.ItemsSource = db.devolversubrazas(db.devolverRazas()[CBRaza.SelectedIndex]);
            if (CBSubraza.Items.Count == null || CBSubraza.Items.Count == 0) {
                CBSubraza.ItemsSource =null;
                CBSubraza.Items.Add("None"); }
            CBSubraza.SelectedIndex=0;
        }

        private void CBClase_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CBSubraza.ItemsSource = "";
            CBSubclase.ItemsSource = db.devolversubclase(db.devolverClases()[CBClase.SelectedIndex]);
        }

        BBDD db;

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
