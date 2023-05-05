using System;
using System.Collections.Generic;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();
            Seller seller = new Seller();
            Player player = new Player();

            List<Product> products = new List<Product>(); ;

            shop.WorkProgram(seller, player, products);
        }
    }

    class Shop
    {
        public void WorkProgram(Seller seller, Player player, List<Product> products)
        {
            const string ComandShowProduct = "1";
            const string ComandSellProduct = "2";
            const string ComandSeeYourStuff = "3";
            const string ComandExit = "exit";

            bool isWork = true;

            string userInput;

            seller.Products(products);

            while (isWork)
            {
                Console.WriteLine($"команда [{ComandShowProduct}] показать товар\n" +
                                  $"команда [{ComandSellProduct}] продать товар\n" +
                                  $"команда [{ComandSeeYourStuff}] посмотреть свой покупки\n" +
                                  $"команда [{ComandExit}] для выхода из программы");

                userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case ComandShowProduct:
                        seller.ShowProducts(products, ConsoleColor.Green);
                        break;

                    case ComandSellProduct:

                        seller.SellProduct(products, seller, player);
                        break;

                    case ComandSeeYourStuff:
                        player.ShowPurchase();
                        break;

                    case ComandExit:
                        isWork = false;
                        break;
                }
            }
        }
    }

    class Persone
    {
        public void Products(List<Product> products)
        {
            products.Add(new Product("фото", 900));
            products.Add(new Product("ноут", 200));
            products.Add(new Product("тел", 50));
        }

        public void ShowProducts(List<Product> products, ConsoleColor colorText)
        {
            for (int i = 0; i < products.Count; i++)
            {
                Console.ForegroundColor = colorText;
                Console.Write(i + 1 + " ");
                products[i].ShowInfo();
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void ConsoleColorText(string text, ConsoleColor foreground)
        {
            Console.ForegroundColor = foreground;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class Player : Persone
    {
        private List<Product> soldGoods = new List<Product>();
        private int _money = 1000;

        public int GetMoney() => _money;

        public int PayGoods(int price) => _money -= price;

        public void BuyProduct(List<Product> products, int index)
        {
            soldGoods.Add(products[index - 1]);
        }

        public void ShowPurchase()
        {
            if (soldGoods.Count == 0)
            {
                ConsoleColorText("Покупок нет", ConsoleColor.Red);
            }
            else
            {
                Console.WriteLine("Мои покупки");
                ShowProducts(soldGoods, ConsoleColor.Green);
            }
        }
    }

    class Seller : Persone
    {
        private int _cash = 0;

        public void SellProduct(List<Product> products, Seller seller, Player player)
        {
            Console.WriteLine("Выберите товар который хотите купить");
            seller.ShowProducts(products, ConsoleColor.Cyan);

            int.TryParse(Console.ReadLine(), out int index);

            if (index <= 0 || index > products.Count)
            {
                ConsoleColorText("Товар не найден!!!", ConsoleColor.Red);
            }
            else
            {
                Console.WriteLine("Внесите деньги");
                int.TryParse(Console.ReadLine(), out int price);

                if (products[index - 1].Price == price)
                {
                    int balance = player.GetMoney();

                    if (balance >= price)
                    {
                        balance = player.PayGoods(price);
                        _cash += price;
                        player.BuyProduct(products, index);
                        Console.WriteLine($"Баланс покупателя = {balance} Баланс продавца = {_cash}");
                        Console.WriteLine("Товар продан!!!");
                    }
                    else
                    {
                        Console.WriteLine($"Баланс покупателя = {balance}");
                        Console.WriteLine("не достаточно денег");
                    }
                }
                else
                {
                    Console.WriteLine("Товар не найден!");
                }
            }
        }
    }

    class Product
    {
        private string _commodity;

        public Product(string commodity, int price)
        {
            _commodity = commodity;
            Price = price;
        }

        public int Price { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"{_commodity} : {Price}");
        }
    }
}
