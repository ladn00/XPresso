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
using System.IO;

namespace XPresso.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageCoffee.xaml
    /// </summary>
    public partial class PageCoffee : Page
    {
        FontFamily nirmala = new FontFamily("Nirmala UI");

        public PageCoffee()
        {
            InitializeComponent();
            try
            {
                // Создание меню
                for (int i = 0; i < MainWindow.coffees.Count; i++)
                {
                    StackPanel sp = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(50)};
                    var button = new Button { Content = "Выбрать", Tag = i, Style = this.Resources["ButtonStyle1"] as Style};
                    button.Click += CoffeeSelect_Click;
                    sp.Children.Add(new Image { Source = new BitmapImage(new Uri(MainWindow.coffees[i].ImagePath, UriKind.RelativeOrAbsolute)), Width = 200, Height  = 200 });
                    sp.Children.Add(new Label { Content = MainWindow.coffees[i].Name, FontFamily = nirmala, FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center, Tag = i});
                    sp.Children.Add(new Label { Content = MainWindow.coffees[i].Cost + " руб.", FontFamily = nirmala, FontSize = 16, HorizontalAlignment = HorizontalAlignment.Center, Tag = i });
                    sp.Children.Add(button);
                    wpCoffee.Children.Add(sp);
                }
                // Поля с крепостью и сахаром
                lbStrength.Content = MainWindow.coffeeStrength;
                lbSugar.Content = MainWindow.sugar;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Выбор кофе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoffeeSelect_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            MainWindow.selectedCoffee = MainWindow.coffees[(int)button.Tag];
            ResetButtons();
            // Изменение кнопки "Выбрать" на "Удалить"
            button.Content = "Удалить";
            button.Background = Brushes.Orange;
            button.Click -= CoffeeSelect_Click;
            button.Click += CoffeeBtDelete;
        }

        /// <summary>
        /// Удаление выбранного кофе из заказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CoffeeBtDelete(object sender, EventArgs e)
        {
            ResetButtons();
            
            MainWindow.selectedCoffee = null;
        }

        /// <summary>
        /// Сброс всех кнопок страницы "Кофе"
        /// </summary>
        public void ResetButtons()
        {
            foreach (StackPanel el in wpCoffee.Children)
            {
                foreach (var bt in el.Children)
                {
                    if (bt.GetType() == typeof(Button) && 
                        ((bt as Button).Content.ToString() == "Удалить"))
                    {
                        (bt as Button).Content = "Выбрать";
                        (bt as Button).Background = this.Resources["MainYellow"] as SolidColorBrush;
                        (bt as Button).Click -= CoffeeBtDelete;
                        (bt as Button).Click += CoffeeSelect_Click;
                    }
                }
            }
        }


        /// <summary>
        /// Увеличение крепости кофе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPlusStrength(object sender, RoutedEventArgs e)
        {
            if(MainWindow.coffeeStrength >=0 && MainWindow.coffeeStrength < 5)
                lbStrength.Content = ++MainWindow.coffeeStrength;
        }

        /// <summary>
        /// Уменьшение крепости кофе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btMinusStrength(object sender, RoutedEventArgs e)
        {
            if (MainWindow.coffeeStrength > 0 && MainWindow.coffeeStrength <= 5)
                lbStrength.Content = --MainWindow.coffeeStrength;
        }

        /// <summary>
        /// Увеличение кол-ва сахара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btPlusSugar(object sender, RoutedEventArgs e)
        {
            if (MainWindow.sugar >= 0 && MainWindow.sugar < 5)
                lbSugar.Content = ++MainWindow.sugar;
        }

        /// <summary>
        /// Уменьшение кол-ва сахара
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btMinusSugar(object sender, RoutedEventArgs e)
        {
            if (MainWindow.sugar > 0 && MainWindow.sugar <= 5)
                lbSugar.Content = --MainWindow.sugar;
        }
    }
}
