using Microsoft.Extensions.Logging;
using System.Text.Json;
using Talabat.core.Entities;
using Talabat.core.Entities.OrderAggregate;

namespace Talabat.Repositery.Data
{
    public class AppContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var BrandsData = File.ReadAllText("../Talabat.Repositery/Data/DataSeed/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);

                    if (brands is not null)
                    {
                        foreach (var B in brands)
                        {
                            context.Set<ProductBrand>().Add(B);
                        }
                    }
                }
                if (!context.productTypes.Any())
                {
                    var ProductTypessData = File.ReadAllText("../Talabat.Repositery/Data/DataSeed/Types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypessData);

                    if (ProductTypes is not null)
                    {
                        foreach (var T in ProductTypes)
                        {
                            context.Set<ProductType>().Add(T);
                        }
                    }
                }
                if (!context.products.Any())
                {
                    var ProductsData = File.ReadAllText("../Talabat.Repositery/Data/DataSeed/Products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                    if (Products is not null)
                    {
                        foreach (var P in Products)
                        {
                            context.Set<Product>().Add(P);
                        }
                    }
                }

                if (!context.DeliveryMethods.Any())
                {
                    var DeliveryMethodData = File.ReadAllText("../Talabat.Repositery/Data/DataSeed/delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodData);

                    if(DeliveryMethods is not null)
                    {
                        foreach (var DM in DeliveryMethods)
                        {
                            context.Set<DeliveryMethod>().Add(DM);
                        }
                    }
                }



                    await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppContextSeed>();
                logger.LogError(ex, ex.Message);
            }
        }

    }
}
