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
using System.Windows.Media.Animation;

namespace XPresso
{
    /// <summary>
    /// Логика взаимодействия для SdachaGiving.xaml
    /// </summary>
    public partial class SdachaGiving : Window
    {
        public SdachaGiving()
        {
            InitializeComponent();

            tbSdacha.Text = $"Ваша сдача: {MainWindow.sdacha} руб.";

        }
        BitmapImage[] imgs =
        {
            new BitmapImage(new Uri("imgs/Монеты1.jpg", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri("imgs/монеты2.png", UriKind.RelativeOrAbsolute)),
            new BitmapImage(new Uri("imgs/монеты3.png", UriKind.RelativeOrAbsolute))
        };
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            double sdacha = MainWindow.sdacha;
            Storyboard st = this.Resources["CoinFalling"] as Storyboard;
            Storyboard st2 = this.Resources["Coin2Falling"] as Storyboard;
            Storyboard st3 = this.Resources["Coin3Falling"] as Storyboard;

            if (sdacha > 0 && sdacha <= 100)
            {
                imgCoins.Source = imgs[0];
                st.Begin();
            }
            else if(sdacha > 100 && sdacha <= 1000)
            {
                imgCoins.Source = imgs[1];
                st.Begin();
                await Task.Delay(TimeSpan.FromSeconds(0.3));
                st2.Begin();
            }
            else
            {
                imgCoins.Source = imgs[2];
                st.Begin();
                await Task.Delay(TimeSpan.FromSeconds(0.2));
                st2.Begin();
                st3.Begin();
            }
        }
    }
}
