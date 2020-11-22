using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Core.Entities;

namespace Ordering.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryforAvailability = retry.Value;

            try
            {
                orderContext.Database.Migrate();

                if (!orderContext.Orders.Any())
                {
                    orderContext.Orders.AddRange(GetPreConfiguredOrders());
                    await orderContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                if (retryforAvailability < 3)
                {
                    retryforAvailability++;
                    var log = loggerFactory.CreateLogger<OrderContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(orderContext, loggerFactory, retryforAvailability);
                }

            }
        }

        private static IEnumerable<Order> GetPreConfiguredOrders()
        {
            return new List<Order>()
            {
                new Order() { Username = "gbubemi", FirstName = "Gbubemi", LastName = "Smith", EmailAddress = "gsmith@smith.com", AddressLine = "Moon", TotalPrice = 4500 },
                new Order() { Username = "walter", FirstName = "Walter", LastName = "Smith", EmailAddress ="walter@smith.com", AddressLine = "Sun", TotalPrice = 6000 }
            };
        }
    }
}