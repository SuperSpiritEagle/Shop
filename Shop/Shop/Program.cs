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

            shop.WorkProgram(seller, player);
        }
    }

    class Shop
    {
        public void WorkProgram(Seller seller, Player player)
        {
            const string CommandShowProduct = "1";
            const string CommandSellProduct = "2";
            const string CommandSeeYourStuff = "3";
            const string CommandExit = "exit";

            bool isWork = true;

            string userInput;

            while (isWork)
            {
                Console.WriteLine($"команда [{CommandShowProduct}] показать товар\n" +
                                  $"команда [{CommandSellProduct}] продать товар\n" +
                                  $"команда [{CommandSeeYourStuff}] посмотреть свой покупки\n" +
                                  $"команда [{CommandExit}] для выхода из программы");

                userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case CommandShowProduct:
                        seller.ShowProducts(ConsoleColor.Cyan);
                        break;

                    case CommandSellProduct:
                        TransferProduct(seller, player);
                        break;

                    case CommandSeeYourStuff:
                        player.ShowProducts(ConsoleColor.Green);
                        break;

                    case CommandExit:
                        isWork = false;
                        break;
                }
            }
        }

        private void TransferProduct(Seller seller, Player player)
        {
            seller.ShowProducts(ConsoleColor.Yellow);

            if (seller.TryGetProduct(out Product product) == false)
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
    }

    class Persone
    {
        protected int Money = 1000;
        protected List<Product> Products = new List<Product>();

        public void ShowProducts(ConsoleColor colorText)
        {
            if (Products.Count == 0)
            {
                ConsoleColorText("Покупок нет", ConsoleColor.Red);
                return;
            }

            for (int i = 0; i < Products.Count; i++)
            {
                Console.ForegroundColor = colorText;
                Console.Write(i + 1 + " ");
                Products[i].ShowInfo();
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
        public bool CanPay(int price)
        {
            if (Money >= price)
            {
                ConsoleColorText("Оплата прошла успешно!", ConsoleColor.Green);
                return true;
            }
            else
            {
                ConsoleColorText("No money no honey", ConsoleColor.Red);
                return false;
            }
        }

        public void Buy(Product product)
        {
            Money -= product.Price;
            Products.Add(product);
        }
    }

    class Seller : Persone
    {
        public void AddProducts()
        {
            Products.Add(new Product("фото", 900));
            Products.Add(new Product("ноут", 200));
            Products.Add(new Product("тел", 50));
        }

        public Seller()
        {
            AddProducts();
        }

        public bool TryGetProduct(out Product product)
        {
            Console.WriteLine("Выберите товар который хотите купить");

            int.TryParse(Console.ReadLine(), out int index);

            if (index <= 0 || index > Products.Count)
            {
                ConsoleColorText("Товар не найден", ConsoleColor.Red);
                product = null;
                return false;
            }
            else
            {
                product = Products[index - 1];
                product.ShowInfo();
                return true;
            }
        }

        public void Sell(Product product)
        {
            Money += product.Price;
            ConsoleColorText($"Баланс продавца = {Money}", ConsoleColor.Green);
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
