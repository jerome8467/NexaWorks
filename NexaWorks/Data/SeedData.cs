using NexaWorks.Data.SeedDataContent;
using Microsoft.EntityFrameworkCore;

namespace NexaWorks.Data
{
    public static class SeedData
    {

        public static void Run(ModelBuilder modelBuilder)
        {
            SeedStatusTicket.CreateStatusTicket(modelBuilder);
            SeedProduct.CreateSeedProduct(modelBuilder);
            SeedTicket.CreateSeedTicket(modelBuilder);
        }

    }
}
