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
        public void TransferProduct(Seller seller, Player player, List<Product> products)
        {
            seller.ShowProducts(products, ConsoleColor.Yellow);

            if (seller.TryGetProduct(out Product product, products) == false)
            {
                return;
            }

            if (player.CanPay(product.Price) == false)
            {
                return;
            }

            seller.Sell(product);
            player.Buy(product);
        }

        public void WorkProgram(Seller seller, Player player, List<Product> products)
        {
            const string ComandShowProduct = "1";
            const string ComandSellProduct = "2";
            const string ComandSeeYourStuff = "3";
            const string ComandExit = "exit";

            bool isWork = true;

            string userInput;

            seller.AddProducts(products);

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
                        TransferProduct(seller, player, products);
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
        protected int money = 1000;
        protected int cash = 0;

        public void AddProducts(List<Product> products)
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
        private List<Product> _soldGoods = new List<Product>();

        public bool CanPay(int price)
        {
            if (money >= price)
            {
                Console.WriteLine("Оплата прошла успешно");
                return true;
            }
            else
            {
                Console.WriteLine("No money no honey");
                return false;
            }
        }

        public void Buy(Product product)
        {
            money -= product.Price;
            _soldGoods.Add(product);
        }

        public void ShowPurchase()
        {
            if (_soldGoods.Count == 0)
            {
                ConsoleColorText("Покупок нет", ConsoleColor.Red);
            }
            else
            {
                Console.WriteLine("Мои покупки");
                ShowProducts(_soldGoods, ConsoleColor.Green);
                Console.WriteLine($"Баланс покупателя = {money}");
            }
        }
    }

    class Seller : Persone
    {
        public bool TryGetProduct(out Product product, List<Product> products)
        {
            Console.WriteLine("Выберите товар который хотите купить");

            int.TryParse(Console.ReadLine(), out int index);

            if (index <= 0 || index > products.Count)
            {
                Console.WriteLine("Товар не наиден");
                product = null;
                return false;
            }
            else
            {
                product = products[index - 1];
                product.ShowInfo();
                return true;
            }
        }

        public void Sell(Product product)
        {
            cash += product.Price;
            Console.WriteLine($"Баланс продовца = {cash}");
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
