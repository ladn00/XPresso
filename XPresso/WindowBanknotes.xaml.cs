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

namespace XPresso
{
    /// <summary>
    /// Логика взаимодействия для WindowBanknotes.xaml
    /// </summary>
    public partial class WindowBanknotes : Window
    {
        MainWindow mw;

        public WindowBanknotes(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;
        }

        /// <summary>
        /// Купюра 10 р.
        /// </summary>
        private void bt10_Click(object sender, RoutedEventArgs e)
        {
            mw.MoneyUpdate(10);
        }

        /// <summary>
        /// Купюра 50 р.
        /// </summary>
        private void bt50_Click(object sender, RoutedEventArgs e)
        {
            mw.MoneyUpdate(50);
        }

        /// <summary>
        /// Купюра 100 р.
        /// </summary>
        private void bt100_Click(object sender, RoutedEventArgs e)
        {
            mw.MoneyUpdate(100);
        }

        /// <summary>
        /// Купюра 200 р.
        /// </summary>
        private void bt200_Click(object sender, RoutedEventArgs e)
        {
            mw.MoneyUpdate(200);
        }

        /// <summary>
        /// Купюра 500 р.
        /// </summary>
        private void bt500_Click(object sender, RoutedEventArgs e)
        {
            mw.MoneyUpdate(500);
        }

        /// <summary>
        /// Купюра 1000 р.
        /// </summary>
        private void bt1000_Click(object sender, RoutedEventArgs e)
        {
            mw.MoneyUpdate(1000);
        }

        /// <summary>
        /// Купюра 2000 р.
        /// </summary>
        private void bt2000_Click(object sender, RoutedEventArgs e)
        {
            mw.MoneyUpdate(2000);
        }

        /// <summary>
        /// Купюра 5000 р.
        /// </summary>
        private void bt5000_Click(object sender, RoutedEventArgs e)
        {
            mw.MoneyUpdate(5000);
        }
    }
}
