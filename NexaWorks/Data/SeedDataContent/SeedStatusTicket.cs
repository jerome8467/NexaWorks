using Microsoft.EntityFrameworkCore;
using NexaWorks.Entities;

namespace NexaWorks.Data.SeedDataContent
{
    public static class SeedStatusTicket
    {

        public static void CreateStatusTicket(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusTicket>()
                .HasData(
                    new StatusTicket { 
                        Id = 1,
                        Title = "En cours",
                    },
                    new StatusTicket
                    {
                        Id = 2,
                        Title = "Résolu",
                    }

                );
        }


    }
}
