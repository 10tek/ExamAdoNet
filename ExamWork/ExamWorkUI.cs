using ExamWork.DataAccess;
using ExamWork.Domain;
using ExamWork.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExamWork.UI
{
    internal class ExamWorkUI
    {
        private ExamWorkContext context;
        private SearchUI searchUI;
        private StockService stockService;

        public ExamWorkUI()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot configurationRoot = builder.Build();
            var ConnectionString = configurationRoot.GetConnectionString("DebugConnectionString");
            var ProviderName = configurationRoot.GetSection("AppConfig").GetChildren().Single(item => item.Key == "ProviderName").Value;
            context = new ExamWorkContext(ConnectionString, ProviderName);
            stockService = new StockService(context);
            searchUI = new SearchUI(context);
        }

        public void Action()
        {
            FillTables();
            var isExit = false;
            while (!isExit)
            {
                Console.WriteLine("1 - Составить накладную");
                Console.WriteLine("2 - Посмотреть все товары со склада");
                Console.WriteLine("4 - Накладная на приход");
                Console.WriteLine("5 - Накладная на вывоз");
                Console.WriteLine("0 - Выход");
                if (int.TryParse(Console.ReadLine(), out var menu) && menu > 0 && menu <= 4)
                {
                    switch (menu)
                    {
                        case 0: isExit = true; break;
                        case 1:  break;
                        case 2:  break;
                        case 3:  break;
                        case 4:  break;
                        case 5:  break;
                    }
                }
            }
        }

        private void MakeWaybill()
        {
            Console.Clear();
            var products = context.Products.GetAll().ToList();
            Console.Write("Введите количество товаров в накладной:");
            while(!(int.TryParse(Console.ReadLine(), out var number)) || number > products.Count || number < 1)
            {
                Console.Write("Выберите заново:");
            }
            Console.WriteLine("Вы создаете накладную.\nВыберите продукт: ");
            var waybill = new Dictionary<Product, int>();
            searchUI.ShowProducts(products);
            

        }

        private void FillTables()
        {
            context.Products.Add(new Product
            {
                ProductName = "Помидоры"
            });
            context.Products.Add(new Product
            {
                ProductName = "Огурцы"
            });
            context.Products.Add(new Product
            {
                ProductName = "Свёкла"
            });
            context.Products.Add(new Product
            {
                ProductName = "Капуста"
            });
            context.Products.Add(new Product
            {
                ProductName = "Морковь"
            });
            context.Products.Add(new Product
            {
                ProductName = "Лук"
            });
            context.Products.Add(new Product
            {
                ProductName = "Тыква"
            });

            context.Storekeepers.Add(new Storekeeper
            {
                FullName = "Сагадиев Ернар Габитович"
            });
            context.Storekeepers.Add(new Storekeeper
            {
                FullName = "Скидан Олег Сергеевич"
            });

        }
    }
}