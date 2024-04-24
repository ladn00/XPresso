//***********************************************************************
//*Название программы: "XPresso"                                        *
//*                                                                     *
//*Назначение программы: имитация работы кофейного автомата с десертами *
//*                                                                     *
//*Разработчик: студент группы ПР-330/б Зуев А.А.                       *
//*                                                                     *
//* версия: 1.0                                                         *
//***********************************************************************

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

namespace XPresso
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Coffee> coffees = new List<Coffee>();
        public static List<Dessert> desserts = new List<Dessert>();
        public static Coffee selectedCoffee;
        public static Dictionary<Dessert, int> selectedDesserts = new Dictionary<Dessert, int>();
        public static int sugar = 3;
        public static int coffeeStrength = 3;
        public static decimal orderPrice = 0;
        public static int money = 0;
        public static double sdacha = 0;
        Pages.PageCoffee pageCoffee;
        Pages.PageDessert pageDesserts;

        public MainWindow()
        {
            InitializeComponent();

            // Заполнение меню с кофе информацией из Coffee.txt
            var sw = new StreamReader("Coffee.txt");
            string line = sw.ReadLine();

            while (line != null)
            {
                string coffeeName = line.Split('|')[0];
                decimal cost = decimal.Parse(line.Split('|')[1]);
                string imageName = line.Split('|')[2];

                coffees.Add(new Coffee(coffeeName, cost, "imgs/" + imageName, 3, 3));
                line = sw.ReadLine();
            }
            sw.Close();

            // Заполнение меню с десертами информацией из Desserts.txt
            var sw1 = new StreamReader("Desserts.txt");
            line = sw1.ReadLine();
            while (line != null)
            {
                string dessertName = line.Split('|')[0];
                decimal cost = decimal.Parse(line.Split('|')[1]);
                string imageName = line.Split('|')[2];
                double kcal = Double.Parse(line.Split('|')[3]);

                desserts.Add(new Dessert(dessertName, cost, "imgs/" + imageName, kcal));
                line = sw1.ReadLine();
            }
            sw1.Close();

            // Создание страниц и установка меню с кофе при запуске
            pageCoffee = new Pages.PageCoffee();
            pageDesserts = new Pages.PageDessert();

            frame.NavigationService.Navigate(pageCoffee);
        }

        /// <summary>
        /// Переход на страницу с десертами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dessertmenu_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(pageDesserts);
        }

        /// <summary>
        /// Переход на страницу с кофе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void coffeemenu_Click(object sender, RoutedEventArgs e)
        {
            frame.NavigationService.Navigate(pageCoffee);
        }

        /// <summary>
        /// Монета 10 рублей
        /// </summary>
        private void money10_Click(object sender, RoutedEventArgs e)
        {
            MoneyUpdate(10);
        }

        /// <summary>
        /// Монета 5 рублей
        /// </summary>
        private void money5_Click(object sender, RoutedEventArgs e)
        {
            MoneyUpdate(5);
        }

        /// <summary>
        /// Монета 2 рубля
        /// </summary>
        private void money2_Click(object sender, RoutedEventArgs e)
        {
            MoneyUpdate(2);
        }

        /// <summary>
        /// Монета 1 рубль
        /// </summary>
        private void money1_Click(object sender, RoutedEventArgs e)
        {
            // Монета 1 рубль
            MoneyUpdate(1);
        }

        /// <summary>
        /// Обновление информации о внесенной сумме денег
        /// </summary>
        /// <param name="given">На сколько увеличить внесенную сумму</param>
        public void MoneyUpdate(int given)
        {
            if (money + given <= 5000)
            {
                money += given;
                lbMoney.Content = $"Внесено: {money} руб.";
            }
            else
            {
                MessageBox.Show("Сумма внесенных денежных средств не должна превышать 5 000 руб.");
            }
        }

        /// <summary>
        /// Начало приготовления заказа
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                btGetOrder.IsEnabled = false;

                // Не пустой ли заказ
                if (selectedDesserts.Count != 0 || selectedCoffee != null)
                {
                    Check check = new Check();
                    orderPrice = check.OrderCost(selectedCoffee, selectedDesserts);
                    sdacha = money - (double)orderPrice;
                    string order = check.DataForCheck(selectedCoffee, selectedDesserts, orderPrice, money, sugar, coffeeStrength); // Строка для чека

                    // Достаточно ли средств
                    if (orderPrice > money)
                    {
                        btGetOrder.IsEnabled = true;
                        throw new Exception("Недостаточно средств!");
                    }
                    // Сброс внесенных денежных средств
                    lbMoney.Content = "Внесено: 0 руб.";
                    money = 0;
                    orderPrice = 0;
                    if (selectedCoffee != null)
                    {
                        // Вызов приготовления кофе
                        selectedCoffee.Sugar = sugar;
                        selectedCoffee.CoffeeStrength = coffeeStrength;

                        CoffeeMakingWindow coffeeWindow = new CoffeeMakingWindow();
                        coffeeWindow.Show();

                        // Ведение статистики в файле Desserts popularity.txt
                        string[] statCoffee = File.ReadAllText("Coffee popularity.txt").Split('\n');
                        var sw = new StreamWriter("Coffee popularity.txt");

                        foreach (string s in statCoffee)
                        {
                            if (!String.IsNullOrEmpty(s))
                            {
                                int count = Int32.Parse(s.Split('|')[1].Trim());
                                if (s.Split('|')[0].Trim() == selectedCoffee.Name)
                                    count++;

                                sw.WriteLine(String.Format("{0,-15} | {1, -3}", s.Split('|')[0].Trim(), count));
                            }
                        }
                        sw.Close();

                        pageCoffee.ResetButtons();
                        selectedCoffee = null;

                        await Task.Delay(TimeSpan.FromSeconds(13));
                    }
                    if (selectedDesserts.Count != 0)
                    {
                        // Вызов выдачи десертов
                        FoodGivingWindow foodWindow = new FoodGivingWindow();
                        foodWindow.Show();

                        // Ведение статистики популярности десертов за все время
                        string[] statDesserts = File.ReadAllText("Desserts popularity.txt").Split('\n');
                        var sw1 = new StreamWriter("Desserts popularity.txt");

                        foreach (string s in statDesserts)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                int count = 0;

                                foreach (Dessert el in selectedDesserts.Keys)
                                {
                                    if (el.Name == s.Split('|')[0].Trim())
                                        count = selectedDesserts[el];
                                }

                                count += Int32.Parse(s.Split('|')[1].Trim());
                                sw1.WriteLine(String.Format("{0,-20} | {1, -3}", s.Split('|')[0].Trim(), count));
                            }
                        }
                        sw1.Close();

                        pageDesserts.ResetButtons();

                        await Task.Delay(TimeSpan.FromSeconds(selectedDesserts.Count * 2.5));
                        selectedDesserts = new Dictionary<Dessert, int>();
                    }
                    if(sdacha != 0)
                    {
                        // Выдача сдачи
                        SdachaGiving sdachaWin = new SdachaGiving();
                        sdachaWin.Show();
                    }
                    // Чек
                    await Task.Delay(TimeSpan.FromSeconds(1.5));
                    MessageBox.Show(order);
                }
                else
                {
                    btGetOrder.IsEnabled = true;
                    throw new Exception("Выберите напиток или десерт!");
                }
                btGetOrder.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Открытие окна с банкнотами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btBanknoteAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowBanknotes banknotes = new WindowBanknotes(this);
            banknotes.ShowDialog();
        }

        /// <summary>
        /// Открытие справки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSpravka_Click(object sender, RoutedEventArgs e)
        {
            Spravka sp = new Spravka();
            sp.ShowDialog();
        }

        /// <summary>
        /// Горячие клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hotkeys_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                //Справка
                case Key.F1:
                    btSpravka_Click(sender, e);
                    break;
                //Страница "Десерты"
                case Key.D:
                    dessertmenu_Click(sender, e);
                    break;
                //Страница "Кофе"
                case Key.C:
                    coffeemenu_Click(sender, e);
                    break;
            }
        }

        /// <summary>
        /// Статистика популярности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btStats_Click(object sender, RoutedEventArgs e)
        {
            string coffeePopularity = File.ReadAllText("Coffee popularity.txt");
            string dessertsPopularity = File.ReadAllText("Desserts popularity.txt");

            MessageBox.Show($"Популярность кофе:\n{coffeePopularity}\nПопулярность десертов:\n{dessertsPopularity}", "Статистика", MessageBoxButton.OK,MessageBoxImage.Information, MessageBoxResult.None, MessageBoxOptions.RightAlign);
        }
    }
}
