using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPresso
{
    public class Check
    {
        /// <summary>
        /// Возвращает стоимость заказа
        /// </summary>
        /// /// <param name="selectedCoffee">Выбранный кофе</param>
        /// <param name="selectedDesserts">Выбранные десерты</param>
        public decimal OrderCost(Coffee selectedCoffee, Dictionary<Dessert, int> selectedDesserts)
        {
            decimal orderCost = 0;

            // Стоимость десертов
            foreach (Dessert el in selectedDesserts.Keys)
                orderCost += el.Cost * selectedDesserts[el];

            // Стоимость кофе
            if (selectedCoffee != null)
                orderCost += (int)selectedCoffee.Cost;

            return orderCost;
        }

        /// <summary>
        /// возвращает строку с информацией для формирования чека
        /// </summary>
        /// /// <param name="selectedCoffee">Выбранный кофе</param>
        /// <param name="selectedDesserts">Выбранные десерты</param>
        /// <param name="orderPrice">Стоимость заказа</param>
        /// <param name="money">Внесеннные деньги</param>
        /// <param name="sugar">Количество сахара</param>
        /// <param name="coffeeStrength">Крепость кофе</param>
        public string DataForCheck(Coffee selectedCoffee, Dictionary<Dessert, int> selectedDesserts, decimal orderPrice, int money, int sugar, int coffeeStrength)
        {
            string orderData = $"\n--------------Товарный чек--------------\n";

            // Информация о десертах
            foreach (Dessert el in selectedDesserts.Keys)
                orderData += $"{el.Name}   {selectedDesserts[el]} шт. * {el.Cost}  руб.\n";

            // Информация о кофе
            if (selectedCoffee != null)
                orderData += $"{selectedCoffee.Name}   1шт. * {selectedCoffee.Cost} руб.\n   Сахар: {sugar} шт.\n   Крепость: {coffeeStrength}/5";

            // Информация о заказе
            orderData += $"\nСтоимость: {orderPrice} руб.\n-----------------------------------------------\n   Оплата: {money} руб.\n   Сдача: {money - orderPrice} руб.\n   Дата: {DateTime.Now:f}\n";

            return orderData;
        }
    }
}
