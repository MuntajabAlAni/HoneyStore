using System.Reflection;
using System.Text.Json;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.ProductCollections!.Any())
            {
                var collectionsData = File.ReadAllText(path + @"/Data/SeedData/collections.json");
                var collections = JsonSerializer.Deserialize<List<ProductCollection>>(collectionsData);

                collections!.ForEach(item => { context.ProductCollections!.Add(item); });

                await context.SaveChangesAsync();
            }

            if (!context.ProductTypes!.Any())
            {
                var typesData = File.ReadAllText(path + @"/Data/SeedData/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                types!.ForEach(item => { context.ProductTypes!.Add(item); });

                await context.SaveChangesAsync();
            }

           /* if (!context.Products!.Any())
            {
                var productsData = File.ReadAllText(path + @"/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                products!.ForEach(item => { context.Products!.Add(item); });

                await context.SaveChangesAsync();
            }*/

            if (!context.DeliveryMethods!.Any())
            {
                var deliverMethodsData = File.ReadAllText(path + @"/Data/SeedData/delivery.json");
                var deliverMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliverMethodsData);

                deliverMethods!.ForEach(item => { context.DeliveryMethods!.Add(item); });

                await context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            logger.LogError(ex.Message);
        }
    }
}