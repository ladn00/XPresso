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
    /// Логика взаимодействия для FoodGivingWindow.xaml
    /// </summary>
    public partial class FoodGivingWindow : Window
    {
        public FoodGivingWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка окна. Начало анимации выдачи десертов
        /// </summary>
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Анимация выдачи десертов
            Storyboard st = this.Resources["DessertFalling"] as Storyboard;
            Storyboard st2 = this.Resources["ImageGoBack"] as Storyboard;

            // Повторение анимации для каждого выбранного десерта
            foreach (Dessert el in MainWindow.selectedDesserts.Keys)
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                imgDessert.Source = new BitmapImage(new Uri("Pages/" + el.ImagePath, UriKind.RelativeOrAbsolute));
                st.Begin();
                
                await Task.Delay(TimeSpan.FromSeconds(1.5));
                imgDessert.Source = null;
                st2.Begin();
            }
        }
    }
}
