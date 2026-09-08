using Microsoft.EntityFrameworkCore;
using NexaWorks.Entities;

namespace NexaWorks.Data.SeedDataContent
{
    public static class SeedProduct
    {

        public static void CreateSeedProduct(ModelBuilder modelBuilder)
        {

            //// TABLE PRODUCT
            modelBuilder.Entity<Product>()
                .HasData(

                    new Product { Id = 1, NameProduct = "Trader en Herbe" },
                    new Product { Id = 2, NameProduct = "Maître des Investissements" },
                    new Product { Id = 3, NameProduct = "Planificateur d’Entraînement" },
                    new Product { Id = 4, NameProduct = "Planificateur d’Anxiété Sociale" }

                 );


            //// TABLE VERSIONPRODUCT
            modelBuilder.Entity<VersionProduct>()
                .HasData(

                    //PRODUCT : "Trader en Herbe"
                    new VersionProduct { Id = 1, RefVersion = "1.0", ProductId = 1 },
                    new VersionProduct { Id = 2, RefVersion = "1.1", ProductId = 1 },
                    new VersionProduct { Id = 3, RefVersion = "1.2", ProductId = 1 },
                    new VersionProduct { Id = 4, RefVersion = "1.3", ProductId = 1 },

                    //PRODUCT : "Maître des Investissements"
                    new VersionProduct { Id = 5, RefVersion = "1.0", ProductId = 2 },
                    new VersionProduct { Id = 6, RefVersion = "2.0", ProductId = 2 },
                    new VersionProduct { Id = 7, RefVersion = "2.1", ProductId = 2 },

                    //PRODUCT : "Planificateur d’Entraînement"
                    new VersionProduct { Id = 8, RefVersion = "1.0", ProductId = 3 },
                    new VersionProduct { Id = 9, RefVersion = "1.1", ProductId = 3 },
                    new VersionProduct { Id = 10, RefVersion = "2.0", ProductId = 3 },

                    //PRODUCT : "Planificateur d’Anxiété Sociale"
                    new VersionProduct { Id = 11, RefVersion = "1.0", ProductId = 4 },
                    new VersionProduct { Id = 12, RefVersion = "1.1", ProductId = 4 }

                );


            //// TABLE SYSTEMOS
            modelBuilder.Entity<SystemOs>()
                .HasData(

                    new SystemOs { Id = 1, NameSystem = "Linux" },
                    new SystemOs { Id = 2, NameSystem = "MacOS" },
                    new SystemOs { Id = 3, NameSystem = "Windows" },
                    new SystemOs { Id = 4, NameSystem = "Android" },
                    new SystemOs { Id = 5, NameSystem = "iOS" },
                    new SystemOs { Id = 6, NameSystem = "Windows Mobile" }

                );


            //// TABLE VERSIONOS
            modelBuilder.Entity<VersionOs>()
                .HasData(

                    //PRODUCT : "Trader en Herbe"
                    //Version : 1.0
                    new VersionOs { Id = 1, VersionProductId = 1, SystemOsId =  1 },
                    new VersionOs { Id = 2, VersionProductId = 1, SystemOsId = 3 },
                    //Version : 1.1
                    new VersionOs { Id = 3, VersionProductId = 2, SystemOsId = 1 },
                    new VersionOs { Id = 4, VersionProductId = 2, SystemOsId = 2 },
                    new VersionOs { Id = 5, VersionProductId = 2, SystemOsId = 3 },
                    //Version : 1.2
                    new VersionOs { Id = 6, VersionProductId = 3, SystemOsId = 1 },
                    new VersionOs { Id = 7, VersionProductId = 3, SystemOsId = 2 },
                    new VersionOs { Id = 8, VersionProductId = 3, SystemOsId = 3 },
                    new VersionOs { Id = 9, VersionProductId = 3, SystemOsId = 4 },
                    new VersionOs { Id = 10, VersionProductId = 3, SystemOsId = 5 },
                    new VersionOs { Id = 11, VersionProductId = 3, SystemOsId = 6 },
                    //Version : 1.3
                    new VersionOs { Id = 12, VersionProductId = 4, SystemOsId = 2 },
                    new VersionOs { Id = 13, VersionProductId = 4, SystemOsId = 3 },
                    new VersionOs { Id = 14, VersionProductId = 4, SystemOsId = 4 },
                    new VersionOs { Id = 15, VersionProductId = 4, SystemOsId = 5 },

                    //PRODUCT : "Maître des Investissements"
                    //Version : 1.0
                    new VersionOs { Id = 16, VersionProductId = 5, SystemOsId = 2 },
                    new VersionOs { Id = 17, VersionProductId = 5, SystemOsId = 5 },
                    //Version : 2.0
                    new VersionOs { Id = 18, VersionProductId = 6, SystemOsId = 2 },
                    new VersionOs { Id = 19, VersionProductId = 6, SystemOsId = 4 },
                    new VersionOs { Id = 20, VersionProductId = 6, SystemOsId = 5 },
                    //Version : 2.1
                    new VersionOs { Id = 21, VersionProductId = 7, SystemOsId = 2 },
                    new VersionOs { Id = 22, VersionProductId = 7, SystemOsId = 3 },
                    new VersionOs { Id = 23, VersionProductId = 7, SystemOsId = 4 },
                    new VersionOs { Id = 24, VersionProductId = 7, SystemOsId = 5 },

                    //PRODUCT : "Planificateur d’Entraînement"
                    //Version : 1.0
                    new VersionOs { Id = 25, VersionProductId = 8, SystemOsId = 1 },
                    new VersionOs { Id = 26, VersionProductId = 8, SystemOsId = 2 },
                    //Version : 1.1
                    new VersionOs { Id = 27, VersionProductId = 9, SystemOsId = 1 },
                    new VersionOs { Id = 28, VersionProductId = 9, SystemOsId = 2 },
                    new VersionOs { Id = 29, VersionProductId = 9, SystemOsId = 3 },
                    new VersionOs { Id = 30, VersionProductId = 9, SystemOsId = 4 },
                    new VersionOs { Id = 31, VersionProductId = 9, SystemOsId = 5 },
                    new VersionOs { Id = 32, VersionProductId = 9, SystemOsId = 6 },
                    //Version : 2.0
                    new VersionOs { Id = 33, VersionProductId = 10, SystemOsId = 2 },
                    new VersionOs { Id = 34, VersionProductId = 10, SystemOsId = 3 },
                    new VersionOs { Id = 35, VersionProductId = 10, SystemOsId = 4 },
                    new VersionOs { Id = 36, VersionProductId = 10, SystemOsId = 5 },

                    //PRODUCT : "Planificateur d’Anxiété Sociale"
                    //Version : 1.0
                    new VersionOs { Id = 37, VersionProductId = 11, SystemOsId = 2 },
                    new VersionOs { Id = 38, VersionProductId = 11, SystemOsId = 3 },
                    new VersionOs { Id = 39, VersionProductId = 11, SystemOsId = 4 },
                    new VersionOs { Id = 40, VersionProductId = 11, SystemOsId = 5 },
                    //Version : 1.1
                    new VersionOs { Id = 41, VersionProductId = 12, SystemOsId = 2 },
                    new VersionOs { Id = 42, VersionProductId = 12, SystemOsId = 3 },
                    new VersionOs { Id = 43, VersionProductId = 12, SystemOsId = 4 },
                    new VersionOs { Id = 44, VersionProductId = 12, SystemOsId = 5 }

                );




        }


    }
}
