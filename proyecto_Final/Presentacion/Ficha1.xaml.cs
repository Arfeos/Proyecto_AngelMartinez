using proyecto_Final.control;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
using Xceed.Wpf.Toolkit;
using MessageBox = System.Windows.MessageBox;

namespace proyecto_Final.Presentacion
{
    /// <summary>
    /// Lógica de interacción para Ficha1.xaml
    /// </summary>
    public partial class Ficha1 : Page
    {
        string nom,bono;
        BBDD db;
        bool ignorar;
        Api api;
        int cont;


        /// <summary>
        /// Constructor de la clase Ficha1 que recibe el nombre del personaje.
        /// </summary>
        /// <param name="nom">Nombre del personaje</param>
        public Ficha1(string nom)
        {
            ignorar = true;
            this.nom = nom;
            db = new BBDD();
            api = new Api();
            InitializeComponent();
            Nombre.Content = nom;
        }

        /// <summary>
        /// Evento que se ejecuta cuando se carga la página.
        /// </summary>
        /// <param name="sender">Objeto que generó el evento</param>
        /// <param name="e">Argumentos del evento</param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            carga car = new carga();
            this.Visibility = Visibility.Collapsed;
            car.Show();
            await cargarmetodosini();
            compmod.Content = await api.añadirdatosclaseporniv(db.devolverclase(), nom, Int32.Parse(niv.Text));
            await ficheros.CargarDatosDesdeArchivo("fich" + nom + ".txt", nom, Tras, niv, rtxtInv, rdFue, rdDes, rdCon, rdInt, rdSab, rdCar, rdCar, rdAcro, rdArca, rdAthl, rdDece, rdHist, rdPerf, rdInti, rdInve, rdInve, rdSloH, rdMedi, rdNatu, rdPerc, rdPerc, rdPers, rdReli, rdStea, rdSurv, rdAnHa, rtxtRasPer, rtxtide, rtxtVin, rtxtDef, Ali, rtxtcomp, Exp, tempg, acpg);
            Fuebon.Content = sacarbono(STR.Text);
            Desbon.Content = sacarbono(DEX.Text);
            Conbon.Content = sacarbono(CON.Text);
            Intbon.Content = sacarbono(INT.Text);
            Sabbon.Content = sacarbono(WIS.Text);
            Carbon.Content = sacarbono(CHA.Text);
            actualizarsalvaciones();
            actualizarhablidades();
            await api.añadirdatossubclaseporniv(db.devolversubclase(), nom, Int32.Parse(niv.Text));        
            list.ItemsSource = await db.devolverRasgos(nom);
            car.Close();
            this.Visibility = Visibility.Visible;
            if (rdFue.IsChecked == false && rdDes.IsChecked == false && rdCon.IsChecked == false && rdInt.IsChecked == false && rdSab.IsChecked == false && rdCar.IsChecked == false)
            {
                await api.avisarsalvacion(db);
            }
            if (rdAcro.IsChecked == false && rdArca.IsChecked == false && rdAthl.IsChecked == false && rdDece.IsChecked == false && rdHist.IsChecked == false && rdPerf.IsChecked == false && rdInti.IsChecked == false && rdInve.IsChecked == false && rdSloH.IsChecked == false && rdMedi.IsChecked == false && rdNatu.IsChecked == false && rdPerc.IsChecked == false && rdPerc.IsChecked == false && rdPers.IsChecked == false && rdReli.IsChecked == false && rdStea.IsChecked == false && rdSurv.IsChecked == false && rdAnHa.IsChecked == false)
            {
                await api.avisarcompetencias(db);
            }
            perpas.Content = 10 + Int32.Parse(Sabbon.Content.ToString());
            ignorar = false;
        }

        /// <summary>
        /// Actualiza las habilidades del personaje.
        /// </summary>
        private void actualizarhablidades()
        {
            if ((bool)rdAcro.IsChecked)
            {
                Acro.Content = Int32.Parse((string)Desbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Acro.Content = Desbon.Content;
            }

            if ((bool)rdAnHa.IsChecked)
            {
                AnHa.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                AnHa.Content = Sabbon.Content;
            }

            if ((bool)rdArca.IsChecked)
            {
                Arca.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Arca.Content = Intbon.Content;
            }

            if ((bool)rdAthl.IsChecked)
            {
                Athl.Content = Int32.Parse((string)Fuebon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Athl.Content = Fuebon.Content;
            }

            if ((bool)rdDece.IsChecked)
            {
                Dece.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Dece.Content = Carbon.Content;
            }

            if ((bool)rdHist.IsChecked)
            {
                Hist.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Hist.Content = Intbon.Content;
            }

            if ((bool)rdInsi.IsChecked)
            {
                Insi.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Insi.Content = Carbon.Content;
            }

            if ((bool)rdInti.IsChecked)
            {
                Inti.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Inti.Content = Carbon.Content;
            }

            if ((bool)rdInve.IsChecked)
            {
                Inve.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Inve.Content = Intbon.Content;
            }

            if ((bool)rdMedi.IsChecked)
            {
                Medi.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Medi.Content = Sabbon.Content;
            }

            if ((bool)rdNatu.IsChecked)
            {
                Natu.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Natu.Content = Intbon.Content;
            }

            if ((bool)rdPerc.IsChecked)
            {
                Perc.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Perc.Content = Sabbon.Content;
            }

            if ((bool)rdPerf.IsChecked)
            {
                Perf.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Perf.Content = Carbon.Content;
            }

            if ((bool)rdPers.IsChecked)
            {
                Pers.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Pers.Content = Sabbon.Content;
            }

            if ((bool)rdReli.IsChecked)
            {
                Reli.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Reli.Content = Intbon.Content;
            }

            if ((bool)rdSloH.IsChecked)
            {
                SloH.Content = Int32.Parse((string)Desbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                SloH.Content = Desbon.Content;
            }

            if ((bool)rdStea.IsChecked)
            {
                Stea.Content = Int32.Parse((string)Desbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Stea.Content = Desbon.Content;
            }

            if ((bool)rdSurv.IsChecked)
            {
                Surv.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
            }
            else
            {
                Surv.Content = Sabbon.Content;
            }

            
        }

        /// <summary>
        /// Carga los métodos iniciales al iniciar la aplicación.
        /// </summary>
        private async Task cargarmetodosini()
        {
            db.cargarficha(Nombre.Content.ToString());
            STR.Text = db.devolverSTR();
            DEX.Text = db.devolverDEX();
            CON.Text = db.devolverCON();
            INT.Text = db.devolverINT();
            WIS.Text = db.devolverWIS();
            CHA.Text = db.devolverCAR();
            txtclase.Content = db.devolverclase() + " " + db.devolversubclase();
            due.Content = db.devolverdueño();
            velo.Content = (await api.devolvervelocidad(db.devolverRaza()));
            niv.Text = db.devolvernivel().ToString();
            cont = Int32.Parse(db.devolvernivel().ToString());
            BitmapImage imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.UriSource = new Uri(db.devolverimagen(db.devolverclase()), UriKind.RelativeOrAbsolute);
            imageSource.EndInit();
            img.Source = imageSource;
            await api.llamarClaseurl(db.devolverurlclase(db.devolverclase()));
            dg.Content = api.devolverpgolpe();
            perpas.Content = (10 + Int32.Parse((string)Sabbon.Content)).ToString();
            inic.Content = Desbon.Content;
            if (db.devolversubraza().Equals("None"))
            {
                txtRaza.Content = db.devolverRaza();
            }
            else
            {
                txtRaza.Content = db.devolverRaza() + " " + db.devolversubraza();
            }

            // Calcular y mostrar el valor máximo de puntos de golpe
            maxpg.Content = (Int32.Parse(Conbon.Content.ToString()) + Int32.Parse(dg.Content.ToString())) * Int32.Parse(niv.Text);
        }

        /// <summary>
        /// Actualiza las salvaciones del personaje.
        /// </summary>
        private void actualizarsalvaciones()
        {
            try
            {
                if ((bool)rdFue.IsChecked)
                {
                    SalFue.Content = Int32.Parse((string)Fuebon.Content) + Int32.Parse((string)compmod.Content);
                }
                else
                {
                    SalFue.Content = Fuebon.Content;
                }

                if ((bool)rdDes.IsChecked)
                {
                    SalDes.Content = Int32.Parse((string)Desbon.Content) + Int32.Parse((string)compmod.Content);
                }
                else
                {
                    SalDes.Content = Desbon.Content;
                }

                if ((bool)rdCon.IsChecked)
                {
                    SalCon.Content = Int32.Parse((string)Conbon.Content) + Int32.Parse((string)compmod.Content);
                }
                else
                {
                    SalCon.Content = Conbon.Content;
                }

                if ((bool)rdInt.IsChecked)
                {
                    SalInt.Content = Int32.Parse((string)Intbon.Content) + Int32.Parse((string)compmod.Content);
                }
                else
                {
                    SalInt.Content = Intbon.Content;
                }

                if ((bool)rdSab.IsChecked)
                {
                    SalSab.Content = Int32.Parse((string)Sabbon.Content) + Int32.Parse((string)compmod.Content);
                }
                else
                {
                    SalSab.Content = Sabbon.Content;
                }

                if ((bool)rdCar.IsChecked)
                {
                    SalCar.Content = Int32.Parse((string)Carbon.Content) + Int32.Parse((string)compmod.Content);
                }
                else
                {
                    SalCar.Content = Carbon.Content;
                }
            }
            catch (FormatException ex) { }
        }

        /// <summary>
        /// Calcula la bonificación de Carbono para un número específico.
        /// </summary>
        /// <param name="numero">Número para el cual se calculará la bonificación de Carbono.</param>
        /// <returns>La bonificación de Carbono correspondiente al número dado.</returns>
        private string sacarbono(string numero)
        {
            try
            {
                int i = Int32.Parse(numero);

                switch (i)
                {
                    case 1:
                        return "-5";
                    case int n when n < 4 && n > 1:
                        return "-4";
                    case int n when n < 6 && n > 3:
                        return "-3";
                    case int n when n < 8 && n > 5:
                        return "-2";
                    case int n when n < 10 && n > 7:
                        return "-1";
                    case int n when n < 12 && n > 9:
                        return "0";
                    case int n when n < 14 && n > 11:
                        return "1";
                    case int n when n < 16 && n > 13:
                        return "2";
                    case int n when n < 18 && n > 15:
                        return "3";
                    case int n when n < 20 && n > 17:
                        return "4";
                    case int n when n < 22 && n > 19:
                        return "5";
                    case int n when n < 24 && n > 21:
                        return "6";
                    case int n when n < 26 && n > 23:
                        return "7";
                    case int n when n < 28 && n > 25:
                        return "8";
                    case int n when n < 30 && n > 27:
                        return "9";
                    case 30:
                        return "10";
                    default:
                        return "ERR";
                }
            }
            catch (FormatException ex)
            {
                return "ERR";
            }
        }

        /// <summary>
        /// Maneja el evento de cambio de texto en los TextBox.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void textbox_textchanged(object sender, TextChangedEventArgs e)
        {

            if (ignorar) { }
            else
            {
                TextBox textBox = sender as TextBox;


                switch (textBox.Name)
                {
                    case "STR":
                        Fuebon.Content = sacarbono(STR.Text);
                        break;
                    case "DEX":
                        Desbon.Content = sacarbono(DEX.Text);
                        break;
                    case "CON":
                        Conbon.Content = sacarbono(CON.Text);
                        break;
                    case "INT":
                        Intbon.Content = sacarbono(INT.Text);
                        break;
                    case "WIS":
                        Sabbon.Content = sacarbono(WIS.Text);
                        break;
                    case "CHA":
                        Carbon.Content = sacarbono(CHA.Text);
                        break;
                    default:
                        break;
                }


                actualizarsalvaciones();
                actualizarhablidades();
                inic.Content = Desbon.Content;
                perpas.Content = 10 + Int32.Parse(Sabbon.Content.ToString());
                maxpg.Content = (Int32.Parse(Conbon.Content.ToString()) + Int32.Parse(dg.Content.ToString())) * Int32.Parse(niv.Text);
            }
        }
        /// <summary>
        /// Maneja el evento de verificación de CheckBox.
        /// Actualiza las salvaciones según el CheckBox modificado.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void ch_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chckbox = sender as CheckBox;

            switch (chckbox.Name)
            {
                case "rdFue":
                case "rdDes":
                case "rdCon":
                case "rdInt":
                case "rdSab":
                case "rdCar":
                    actualizarsalvaciones();
                    break;
                case "rdAcro":
                case "rdAnHa":
                case "rdArca":
                case "rdAthl":
                case "rdDece":
                case "rdHist":
                case "rdInsi":
                case "rdInti":
                case "rdInve":
                case "rdMedi":
                case "rdNatu":
                case "rdPerc":
                case "rdPerf":
                case "rdPers":
                case "rdReli":
                case "rdSloH":
                case "rdStea":
                case "rdSurv":

                    actualizarhablidades();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Maneja el evento de descarga de la página.
        /// Guarda los datos de la ficha en un archivo y los actualiza en la base de datos.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            await db.modificarficha((string)Nombre.Content.ToString(), STR.Text, DEX.Text, CON.Text, INT.Text, WIS.Text, CHA.Text, niv.Text);
            await ficheros.GuardarDatos("fich" + Nombre.Content.ToString() + ".txt", Nombre.Content.ToString(), Tras, niv, rtxtInv, rdFue, rdDes, rdCon, rdInt, rdSab, rdCar, rdCar, rdAcro, rdArca, rdAthl, rdDece, rdHist, rdPerf, rdInti, rdInve, rdInve, rdSloH, rdMedi, rdNatu, rdPerc, rdPerc, rdPers, rdReli, rdStea, rdSurv, rdAnHa, rtxtRasPer, rtxtide, rtxtVin, rtxtDef, Ali, rtxtcomp, Exp,tempg,acpg);
        }

        /// <summary>
        /// Maneja el evento de entrada de texto en los TextBox.
        /// Permite solo la entrada de números.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumber(e.Text))
            {
                e.Handled = true; // Evita que se procese el texto ingresado
            }
        }
        /// <summary>
        /// trata de transformar un texto a numero
        /// </summary>
        /// <param name="text">el texto a transformar</param>
        /// <returns>tru si pudo transformarlo, false si no</returns>
        private bool IsNumber(string text)
        {
            // Intenta convertir el texto en un número entero
            return int.TryParse(text, out _);
        }

        /// <summary>
        /// Maneja el evento de pérdida de enfoque del TextBox de nivel.
        /// Actualiza los rasgos de clase y subclase según el nivel ingresado.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void niv_LostFocus(object sender, RoutedEventArgs e)
        {
            carga car = new carga();
            car.Show();
            if (Int32.Parse(niv.Text) < cont)
            {
                do
                {
                    await api.eliminardatossubclaseporniv(db.devolversubclase(), Nombre.Content.ToString(), cont);
                    compmod.Content = await api.elimardatosclaseporniv(db.devolverclase(), nom, cont);
                    cont--;
                } while (cont > Int32.Parse(niv.Text));
                maxpg.Content = (Int32.Parse(Conbon.Content.ToString()) + Int32.Parse(dg.Content.ToString())) * Int32.Parse(niv.Text);
                list.ItemsSource = await db.devolverRasgos(nom);
                actualizarhablidades();
                actualizarsalvaciones();
            }
            if (Int32.Parse(niv.Text) >= cont)
            {
                do
                {
                    await api.añadirdatossubclaseporniv(db.devolversubclase(), nom, cont);
                    compmod.Content = await api.añadirdatosclaseporniv(db.devolverclase(), nom, cont);
                    cont++;
                } while (cont <= Int32.Parse(niv.Text));
                list.ItemsSource = await db.devolverRasgos(nom);
                maxpg.Content = (Int32.Parse(Conbon.Content.ToString()) + Int32.Parse(dg.Content.ToString())) * Int32.Parse(niv.Text);
                actualizarhablidades();
                actualizarsalvaciones();
            }
            car.Close();
        }

        /// <summary>
        /// Maneja el evento de doble clic en un elemento de la lista.
        /// Muestra la descripción del elemento seleccionado.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void list_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                string desc = await api.devolverdesc(list.SelectedItem.ToString());
                datos dat = new datos(list.SelectedItem.ToString(), desc);
                dat.Show();
            }
        }

        /// <summary>
        /// Maneja el evento de doble clic en una etiqueta.
        /// Muestra la ventana de tirada de dados.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Label label = sender as Label;
            tirada tir = new tirada(label.Content.ToString());
            tir.Show();
        }

        /// <summary>
        /// Maneja el evento de clic en el botón.
        /// Crea una nueva instancia de la página Ficha2 y navega a ella.
        /// </summary>
        /// <param name="sender">El objeto que desencadenó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Ficha2 fich;
            bono = await api.sacarbono(db.devolverclase());
            if (bono == null) { }
            else
            {

                 if (bono == "DEX")
                {
                    fich = new Ficha2(Nombre.Content.ToString(), db.devolverclase(), bono, Int32.Parse(Desbon.Content.ToString()) + Int32.Parse(compmod.Content.ToString()), niv.Text.ToString());
                }
                else if (bono == "CON")
                {
                    fich = new Ficha2(Nombre.Content.ToString(), db.devolverclase(), bono, Int32.Parse(Conbon.Content.ToString()) + Int32.Parse(compmod.Content.ToString()), niv.Text.ToString());
                }
                else if (bono == "INT")
                {
                    fich = new Ficha2(Nombre.Content.ToString(), db.devolverclase(), bono, Int32.Parse(Intbon.Content.ToString()) + Int32.Parse(compmod.Content.ToString()), niv.Text.ToString());
                }
                else if (bono == "WIS")
                {
                    fich = new Ficha2(Nombre.Content.ToString(), db.devolverclase(), bono, Int32.Parse(Sabbon.Content.ToString()) + Int32.Parse(compmod.Content.ToString()), niv.Text.ToString());
                }
                else if (bono == "CHA")
                {
                    fich = new Ficha2(Nombre.Content.ToString(), db.devolverclase(), bono, Int32.Parse(Carbon.Content.ToString()) + Int32.Parse(compmod.Content.ToString()), niv.Text.ToString());
                }
                else
                {
                    fich = null;
                }
                this.NavigationService.Navigate(fich);
            }
        }
    }
    }

       
    

    
