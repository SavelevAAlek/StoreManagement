using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace StoreManagement.Data
{
    class DbInitializer
    {
        private readonly StoreManagementDB _db;
        private readonly ILogger<DbInitializer> _logger;

        public DbInitializer(StoreManagementDB db, ILogger<DbInitializer> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация БД...");

            _logger.LogInformation("Удаление существующей БД...");
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            _logger.LogInformation("Удаление существующей БД выполнено за {0} мс", timer.ElapsedMilliseconds);

            _db.Database.EnsureCreated();

            _logger.LogInformation("Миграция БД...");
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            _logger.LogInformation("Миграция БД выполнена за {0} мс", timer.ElapsedMilliseconds);

            if (await _db.Products.AnyAsync()) return;

            await InitializeProducts();
            await InitializeClient();
            await InitializePurchases();

            _logger.LogInformation("Инициализация БД выполнена за {0} с", timer.Elapsed.TotalSeconds);
        }

        private const int _purchasesCount = 100;
        private async Task InitializePurchases()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация покупок...");

            var rnd = new Random();

            var deals = Enumerable.Range(1, _purchasesCount)
               .Select(i => new Purchase
               {
                   Client = rnd.NextItem(_clients),
                   Product = rnd.NextItem(_products)
               });

            await _db.Purchases.AddRangeAsync(deals);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация покупок выполнена за {0} мс", timer.ElapsedMilliseconds);
        }


        private const int _clientCount = 10;
        private Client[] _clients;
        private async Task InitializeClient()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация клиентов...");

            _clients = Enumerable.Range(1, _clientCount)
                .Select(i => new Client
                {
                    Name = $"Клиент-Имя {i}",
                    Surname = $"Клиент-Фамилия {i}",
                    Patronymics = $"Клиент-Отчество {i}",
                    Email = $"Клиент-Email mail{i}@test.ru",
                    Phone = $"Клиент-Имя 8999-459-55-{i}{i+1}",
                })
                .ToArray();

            await _db.Clients.AddRangeAsync(_clients);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация клиентов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }

        private const int _productsCount = 10;
        private string[] _productNames = ["Хлеб", "Молоко", "Масло", "Сок", "Чай", "Кофе", "Печенье", "Сахар", "Соль", "Перец"];
        private Product[] _products;
        private async Task InitializeProducts()
        {
            var timer = Stopwatch.StartNew();
            _logger.LogInformation("Инициализация продуктов...");

            _products = Enumerable.Range(0, _productsCount)
                .Select(i => new Product
                {
                    Name = _productNames[i]
                })
                .ToArray();

            await _db.Products.AddRangeAsync(_products);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Инициализация продуктов выполнена за {0} мс", timer.ElapsedMilliseconds);
        }
    }
}
