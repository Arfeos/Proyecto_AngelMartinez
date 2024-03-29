﻿using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace proyecto_Final.Presentacion
{
    /// <summary>
    /// Lógica de interacción para tirada.xaml
    /// </summary>
    public partial class tirada : Window
    {
        int carac, total , d1,d2;
        Random rand;

        private void me_MediaEnded(object sender, RoutedEventArgs e)
        {
            // Mostrar nuevamente el MediaElement
            me.Visibility = Visibility.Hidden;
            if (ventaja.IsChecked == true)
            {
                d1 = rand.Next(1, 21);
                d2 = rand.Next(1, 21);
                if (d1 < d2)
                {
                    if (d2 == 20) { MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")" + "\n you pass the challenge with a crital roll"); }
                    else if (d2 == 1) { MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")" + "\n you fail the challenge with a crital fail"); }
                    else
                    {
                        total = carac + d2 + Int32.Parse(mod.Text);
                        if (total >= Int32.Parse(challenge.Text))
                            MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")  result[" + d2 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou pass the challenge ", "Roll result");
                        else
                            MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")  result[" + d2 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou fail the challenge ", "Roll result");
                    }
                }
                else
                {
                    if (d1 == 20) { MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")" + "\n you pass the challenge with a crital roll"); }
                    else if (d1 == 1) { MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")" + "\n you fail the challenge with a crital fail"); }
                    else
                    {
                        total = carac + d1 + Int32.Parse(mod.Text);
                        if (total >= Int32.Parse(challenge.Text))
                            MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")  result[" + d1 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou pass the challenge ", "Roll result");
                        else
                            MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")  result[" + d1 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou fail the challenge ", "Roll result");
                    }
                }
            }
            else if (neutro.IsChecked == true)
            {
                d1 = rand.Next(1, 21);
                if (d1 == 20) { MessageBox.Show("1º dice(" + d1 + ")" + "\n you pass the challenge with a crital roll"); }
                else if (d1 == 1) { MessageBox.Show("1º dice(" + d1 + ")" + "\n you fail the challenge with a crital fail"); }
                else
                {
                    total = d1 + carac + Int32.Parse(mod.Text);
                    if (total >= Int32.Parse(challenge.Text))
                        MessageBox.Show("1º dice(" + d1 + ") result[" + d1 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou pass the challenge ", "Roll result");
                    else
                        MessageBox.Show("1º dice(" + d1 + ") result[" + d1 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou fail the challenge ", "Roll result");
                }
            }
            else if (desventaja.IsChecked == true)
            {
                d1 = rand.Next(1, 21);
                d2 = rand.Next(1, 21);
                if (d1 > d2)
                {
                    if (d2 == 20) { MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")" + "\n you pass the challenge with a crital roll"); }
                    else if (d2 == 1) { MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")" + "\n you fail the challenge with a crital fail"); }
                    else
                    {
                        total = carac + d2 + Int32.Parse(mod.Text);
                        if (total >= Int32.Parse(challenge.Text))
                            MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")  result[" + d2 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou pass the challenge ", "Roll result");
                        else
                            MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")  result[" + d2 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou fail the challenge ", "Roll result");
                    }
                }
                else
                {
                    if (d1 == 20) { MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")" + "\n you pass the challenge with a crital roll"); }
                    else if (d1 == 1) { MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")" + "\n you fail the challenge with a crital fail"); }
                    else
                    {
                        total = carac + d1 + Int32.Parse(mod.Text);
                        if (total >= Int32.Parse(challenge.Text))
                            MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")  result[" + d1 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou pass the challenge ", "Roll result");
                        else
                            MessageBox.Show("1º dice(" + d1 + ") 2º dice(" + d2 + ")  result[" + d1 + "+" + mod.Text + "+" + carac + "=" + total + "\nyou fail the challenge ", "Roll result");
                    }
                }
            }
        }

        /// <summary>
        /// contructor de la clase tirada
        /// </summary>
        /// <param name="cont"> contador de la puntuacion de la habilidad</param>
        public tirada(string cont)
        {
            InitializeComponent();
            rand = new Random();
            carac = Int32.Parse(cont);
        }
        /// <summary>
        /// Maneja el evento Click del botón "btn_tirada". Realiza una tirada de dados en función de la opción seleccionada (ventaja, neutro o desventaja) y muestra el resultado en un cuadro de mensaje.
        /// </summary>
        private async void btn_tirada_Click(object sender, RoutedEventArgs e)
        {
            string ruta = "https://i.imgur.com/R7nB674.mp4";
            me.Source = new Uri(ruta, UriKind.RelativeOrAbsolute);
            me.Visibility = Visibility.Visible;
            me.Play();
           
        }


        /// <summary>
        /// Maneja el evento PreviewTextInput del control "tx". Permite solo la entrada de caracteres numéricos y el signo de menos ("-").
        /// </summary>
        private void tx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
