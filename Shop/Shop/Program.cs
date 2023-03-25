using System;
using System.Collections.Generic;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            const string ComandShowProduct = "1";
            const string ComandSellProduct = "2";
            const string ComandSeeYourStuff = "3";
            const string ComandExit = "exit";

            Seller seller = new Seller();

            Player player = new Player();

            List<Product> products = new List<Product>();
            List<Product> SoldGoods = new List<Product>();

            bool isWork = true;

            string userInput;

            products.Add(new Product("фото"));
            products.Add(new Product("ноут"));
            products.Add(new Product("тел"));

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
                        seller.ShowProducts(products);
                        break;

                    case ComandSellProduct:
                        int sell;

                        Console.WriteLine("Выберите товар который хотите купить");
                        seller.ShowProducts(products);

                        int.TryParse(Console.ReadLine(), out sell);

                        if (sell < 0 || sell > products.Count)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Товар не найден!!!");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }

                        SoldGoods.Add(products[sell - 1]);

                        products.RemoveAt(sell - 1);
                        break;

                    case ComandSeeYourStuff:
                        player.ShowPurchase(SoldGoods);
                        break;

                    case ComandExit:
                        isWork = false;
                        break;
                }
            }
        }

        class Player
        {
            public void ShowPurchase(List<Product> myProduct)
            {
                if (myProduct.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("нет покупок");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                for (int i = 0; i < myProduct.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(i + 1 + " ");
                    myProduct[i].ShowInfo();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        class Seller
        {
            public void ShowProducts(List<Product> product)
            {
                for (int i = 0; i < product.Count; i++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(i + 1 + " ");
                    product[i].ShowInfo();
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }

    class Product
    {
        private string _Product;
        public Product(string product)
        {
            _Product = product;
        }

        public void ShowInfo()
        {
            Console.WriteLine(_Product);
        }
    }
}

