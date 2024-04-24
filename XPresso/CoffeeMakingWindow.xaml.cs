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
    /// Логика взаимодействия для CoffeeMakingWindow.xaml
    /// </summary>
    public partial class CoffeeMakingWindow : Window
    {
        public CoffeeMakingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка окна. Начало анимации приготовления кофе
        /// </summary>
        private async void Form_Loaded(object sender, RoutedEventArgs e)
        {
            // Анимация налития кофе длительностью 8 сек.
            string path = "imgs/jet";
            int index = 1;
            var start = DateTime.UtcNow;
            var diff = TimeSpan.FromSeconds(8);
            StatusBar_Change(start, diff);
            while ((DateTime.UtcNow - start) < diff)
            {
                index++;
                Coffee_Jet.Source = new BitmapImage(new Uri(String.Concat(path, index.ToString(), ".png"), UriKind.Relative));
                if (index == 3)
                    index = 0;
                await Task.Delay(TimeSpan.FromSeconds(0.1));
            }
            Coffee_Jet.Source = null;
            await Task.Delay(TimeSpan.FromSeconds(1.5));

            // Анимация падения сахара
            if(MainWindow.sugar != 0)
            {
                Storyboard sugar = this.Resources["Sugar_Falling"] as Storyboard;
                sugar.Begin();
            }
        }

        /// <summary>
        /// Строка готовности кофе
        /// </summary>
        public async void StatusBar_Change(DateTime start, TimeSpan diff)
        {
            int index = 0;
            // Статус приготовления
            while ((DateTime.UtcNow - start) < diff + TimeSpan.FromSeconds(3))
            {
                lbStatus.Content += "  | ";
                if(index == 4)
                {
                    lbStatus.Content = "ПРИГОТОВЛЕНИЕ";
                    index = -1;
                }
                index++;
                await Task.Delay(TimeSpan.FromSeconds(0.5));
            }
            index = 0;
            // Кофе готов
            while (true)
            {
                if(index == 0)
                {
                    lbStatus.Content = "ГОТОВО";
                }
                else
                {
                    lbStatus.Content = "";
                    index = -1;
                }
                index++;
                await Task.Delay(TimeSpan.FromSeconds(0.6));
            }
        }
    }
}
