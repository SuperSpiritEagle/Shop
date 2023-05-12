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

            List<Product> products = new List<Product>();

            shop.WorkProgram(seller, player, products);
        }
    }

    class Shop
    {
        private void TransferProduct(Seller seller, Player player, List<Product> products)
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
                        seller.ShowProducts(products, ConsoleColor.Cyan);
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
        protected int Money = 1000;
        protected int Cash = 0;

        public virtual void ShowProducts(List<Product> products, ConsoleColor colorText)
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
                ConsoleColorText("Мои покупки", ConsoleColor.Yellow);
                base.ShowProducts(_soldGoods, ConsoleColor.Green);
                ConsoleColorText($"Баланс = {Money}", ConsoleColor.Blue);
            }
        }
    }

    class Seller : Persone
    {
        public void AddProducts(List<Product> products)
        {
            products.Add(new Product("фото", 900));
            products.Add(new Product("ноут", 200));
            products.Add(new Product("тел", 50));
        }

        public bool TryGetProduct(out Product product, List<Product> products)
        {
            Console.WriteLine("Выберите товар который хотите купить");

            int.TryParse(Console.ReadLine(), out int index);

            if (index <= 0 || index > products.Count)
            {
                ConsoleColorText("Товар не найден", ConsoleColor.Red);
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
            Cash += product.Price;
            ConsoleColorText($"Баланс продавца = {Cash}", ConsoleColor.Green);
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
