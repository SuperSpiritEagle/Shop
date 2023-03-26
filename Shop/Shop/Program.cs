using System;
using System.Collections.Generic;

namespace Shop
{
    class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();

            shop.WorkProgram();
        }

        class Shop
        {
            public void WorkProgram()
            {
                const string ComandShowProduct = "1";
                const string ComandSellProduct = "2";
                const string ComandSeeYourStuff = "3";
                const string ComandExit = "exit";

                Seller seller = new Seller();

                Player player = new Player();

                List<Product> products = new List<Product>();
                List<Product> soldGoods = new List<Product>();

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
                            seller.SellProduct(products, soldGoods, seller);
                            break;

                        case ComandSeeYourStuff:
                            player.ShowPurchase(soldGoods);
                            break;

                        case ComandExit:
                            isWork = false;
                            break;
                    }
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

            public void SellProduct(List<Product> products, List<Product> soldGoods, Seller seller)
            {
                Console.WriteLine("Выберите товар который хотите купить");
                seller.ShowProducts(products);

                int.TryParse(Console.ReadLine(), out int sell);

                if (sell <= 0 || sell > products.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Товар не найден!!!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    soldGoods.Add(products[sell - 1]);

                    products.RemoveAt(sell - 1);
                }
            }
        }
    }

    class Product
    {
        private string _product;

        public Product(string product)
        {
            _product = product;
        }

        public void ShowInfo()
        {
            Console.WriteLine(_product);
        }
    }
}

