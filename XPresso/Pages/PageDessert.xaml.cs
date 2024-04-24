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
    /// Логика взаимодействия для PageDessert.xaml
    /// </summary>
    public partial class PageDessert : Page
    {
        public PageDessert()
        {
            InitializeComponent();

            // Формирование меню
            try
            {
                // Считывание данных из файла Desserts.txt и добавление в меню
                for (int i = 0; i < MainWindow.desserts.Count; i++)
                {
                    StackPanel sp = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center, Margin = new Thickness(50), Tag = i };
                    var button = new Button { Content = "Выбрать", Tag = i, Style = this.Resources["ButtonStyle1"] as Style };
                    button.Click += new RoutedEventHandler(dessertSelect_Click);
                    // Добавление позиции в меню
                    sp.Children.Add(new Image { Source = new BitmapImage(new Uri(MainWindow.desserts[i].ImagePath, UriKind.RelativeOrAbsolute)), Width = 200, Height = 200 });
                    sp.Children.Add(new Label { Content = MainWindow.desserts[i].Name, FontFamily = new FontFamily("Nirmala UI"), FontSize = 18, HorizontalAlignment = HorizontalAlignment.Center, Tag = i });
                    sp.Children.Add(new Label { Content = MainWindow.desserts[i].Cost + " руб.", FontFamily = new FontFamily("Nirmala UI"), FontSize = 16, HorizontalAlignment = HorizontalAlignment.Center, Tag = i });
                    sp.Children.Add(new Label { Content = MainWindow.desserts[i].Kcal + " ккал в 100г", FontFamily = new FontFamily("Nirmala UI"), FontSize = 15, HorizontalAlignment = HorizontalAlignment.Center, Tag = i });
                    sp.Children.Add(button);
                    wpDesserts.Children.Add(sp);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Выбор десерта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dessertSelect_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int tag = (int)button.Tag;
            // Создание элементов управления для редактирования количества выбранного десерта
            StackPanel spSelect = new StackPanel
            {
                Tag = tag,
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center,
                // Кнопки - и + 
                Children =
                {
                new Button { Content = new Image{ Source = new BitmapImage(new Uri("imgs/-.png", UriKind.RelativeOrAbsolute)), Width = 25}, BorderBrush = Brushes.White, Height = 35, Width = 39, Tag = tag, Background = Brushes.White },
                new Label { Content = "1", FontSize = 20, VerticalAlignment = VerticalAlignment.Center} ,
                new Button { Content = new Image{ Source = new BitmapImage(new Uri("imgs/+.png", UriKind.RelativeOrAbsolute))}, BorderBrush = Brushes.White, Height = 39, Width = 39, Tag = tag, Background = Brushes.White}
                }
            };
            Border br = new Border { BorderBrush = Brushes.Black, BorderThickness = new Thickness(1), Height = 40, Width = 120, CornerRadius = new CornerRadius(8)};
            MainWindow.selectedDesserts.Add(MainWindow.desserts[(int)button.Tag], 1);
            br.Child = spSelect;
            button.Visibility = Visibility.Collapsed;
            StackPanel sp = wpDesserts.Children[tag] as StackPanel;
            sp.Children.Add(br);
            (spSelect.Children[0] as Button).Click -= CountMinus;
            (spSelect.Children[2] as Button).Click -= CountPlus;
            (spSelect.Children[0] as Button).Click += CountMinus;
            (spSelect.Children[2] as Button).Click += CountPlus;
            
        }

        /// <summary>
        /// Увеличение кол-ва выбранного десерта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountPlus(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int tag = (int)button.Tag;
            Label lb1 = (((wpDesserts.Children[tag] as StackPanel).Children[5] as Border).Child as StackPanel).Children[1] as Label;
            if (Int32.Parse(lb1.Content.ToString()) + 1 <= 10)
            {
                MainWindow.selectedDesserts[MainWindow.desserts[tag]]++;
                lb1.Content = MainWindow.selectedDesserts[MainWindow.desserts[tag]];
            }
        }

        /// <summary>
        /// Уменьшение кол-ва выбранного десерта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountMinus(object sender, EventArgs e)
        {
            var button = (Button)sender;
            int tag = (int)button.Tag;
            Label lb1 = (((wpDesserts.Children[tag] as StackPanel).Children[5] as Border).Child as StackPanel).Children[1] as Label;
            if (Int32.Parse(lb1.Content.ToString()) - 1 > 0)
            {
                MainWindow.selectedDesserts[MainWindow.desserts[tag]]--;
                lb1.Content = MainWindow.selectedDesserts[MainWindow.desserts[tag]];
                return;
            }
            // Удаление десерта из корзины
            if (Int32.Parse(lb1.Content.ToString()) - 1 == 0)
            {
                MainWindow.selectedDesserts.Remove(MainWindow.desserts[tag]);
                (wpDesserts.Children[tag] as StackPanel).Children.RemoveAt(5);
                ((wpDesserts.Children[tag] as StackPanel).Children[4] as Button).Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Сброс всех кнопок страницы "Десерты"
        /// </summary>
        public void ResetButtons()
        {
            foreach (StackPanel el in wpDesserts.Children)
            {
                (el.Children[4] as Button).Visibility = Visibility.Visible;
                if (el.Children.Count == 6)
                {
                    el.Children.RemoveAt(5);
                }
            }
        }
    }
}
