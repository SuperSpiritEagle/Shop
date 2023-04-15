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

            products.Add(new Product("фото"));
            products.Add(new Product("ноут"));
            products.Add(new Product("тел"));

            shop.WorkProgram(seller, player, products);
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
                            seller.ShowProducts(products,ConsoleColor.Blue);
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
            public void ShowProducts(List<Product> products,ConsoleColor colorText)
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
            public void BuyProduct(List<Product> products, int sell)
            {
                soldGoods.Add(products[sell - 1]);
            }

            public void ShowPurchase()
            {
                if (soldGoods.Count == 0)
                {
                    ConsoleColorText("Покупок нет",ConsoleColor.Red);
                }
                else
                {
                    Console.WriteLine("Мои покупки");
                    ShowProducts(soldGoods,ConsoleColor.Green);
                }
            }
        }

        class Seller : Persone
        {
            public void SellProduct(List<Product> products, Seller seller, Player player)
            {
                Console.WriteLine("Выберите товар который хотите купить");
                seller.ShowProducts(products,ConsoleColor.Cyan);

                int.TryParse(Console.ReadLine(), out int sell);

                if (sell <= 0 || sell > products.Count)
                {
                    ConsoleColorText("Товар не найден!!!", ConsoleColor.Red);
                }
                else
                {
                    player.BuyProduct(products, sell);
                    Console.WriteLine("Товар продан!!!");
                }
            }
        }
    }

    class Product
    {
        private string _nameProduct;

        public Product(string nameProduct)
        {
            _nameProduct = nameProduct;
        }

        public void ShowInfo()
        {
            Console.WriteLine(_nameProduct);
        }
    }
}

