using Microsoft.VisualStudio.TestPlatform.Utilities;
using NexaWorks.Data;
using NexaWorks.Dtos;
using NexaWorks.Entities;
using NexaWorks.Repositories;
using Xunit.Abstractions;

namespace NexaWorks.Test
{
    public class GetTicketDto_Test
    {
        private readonly ApplicationDbContext _database;
        private readonly RequestRepository _requestRepository;
        private readonly ITestOutputHelper _output;

        public GetTicketDto_Test(ITestOutputHelper output)
        {
            _database = new ApplicationDbContext();
            _requestRepository = new RequestRepository(_database);
            _output = output;
        }

        //// REQUEST 1
        /// Définition : Obtenir tous les problèmes en cours (tous les produits)
        [Fact]
        public void Request1()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(t => t.StatusTicketId == 1).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: null,
                versionProductId: null,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: null,
                statusTicket: 1);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("En cours", t.StatusTitle));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 2
        /// Définition : Obtenir tous les problèmes en cours pour un produit (toutes les versions)
        [Fact]
        public void Request2()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 1 &&
                t.VersionOs.VersionProduct.ProductId == 1
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: null,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: null,
                statusTicket: 1);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("En cours", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 3
        /// Définition : Obtenir tous les problèmes en cours pour un produit (une seule version)
        [Fact]
        public void Request3()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 1 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.VersionOs.VersionProduct.Id == 4
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: 4,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: null,
                statusTicket: 1);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("En cours", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Equal("1.3", t.ProductVersion));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 4
        /// Définition : Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit (toutes les versions)
        [Fact]
        public void Request4()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.VersionOs.VersionProduct.ProductId == 1 &&
                t.CreationDate >= new DateOnly(2023, 1, 10) &&
                t.CreationDate <= new DateOnly(2023, 1, 20)
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: null,
                systemOsId: null,
                dateStart: new DateOnly(2023, 1, 10),
                dateEnd: new DateOnly(2023, 1, 20),
                keywords: null,
                statusTicket: null);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.InRange(t.CreationDate, new DateOnly(2023, 1, 10), new DateOnly(2023, 1, 20)));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 5
        /// Définition : Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit (une seule version)
        [Fact]
        public void Request5()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.VersionOs.VersionProduct.ProductId == 1 &&
                t.VersionOs.VersionProductId == 4 &&
                t.CreationDate >= new DateOnly(2023, 04, 20) &&
                t.CreationDate <= new DateOnly(2023, 04, 30)
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: 4,
                systemOsId: null,
                dateStart: new DateOnly(2023, 04, 20),
                dateEnd: new DateOnly(2023, 04, 30),
                keywords: null,
                statusTicket: null);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("1.3", t.ProductVersion));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.InRange(t.CreationDate, new DateOnly(2023, 04, 20), new DateOnly(2023, 04, 30)));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 6
        /// Définition : Obtenir tous les problèmes en cours contenant une liste de mots-clés (tous les produits)
        [Fact]
        public void Request6()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 1 &&
                t.Description != null && t.Description.Contains("aucune notification")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: null,
                versionProductId: null,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: "aucune notification",
                statusTicket: 1);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("En cours", t.StatusTitle));
            Assert.All(tickets, t => Assert.Contains("aucune notification", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 7
        /// Définition : Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés (Toutes les versions)
        [Fact]
        public void Request7()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 1 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.Description != null && t.Description.Contains("aucune notification")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: null,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: "aucune notification",
                statusTicket: 1);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("En cours", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Contains("aucune notification", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 8
        /// Définition : Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés (Une seule version)
        [Fact]
        public void Request8()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 1 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.VersionOs.VersionProductId == 4 &&
                t.Description != null && t.Description.Contains("aucune notification")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: 4,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: "aucune notification",
                statusTicket: 1);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("En cours", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Equal("1.3", t.ProductVersion));
            Assert.All(tickets, t => Assert.Contains("aucune notification", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 9
        /// Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit contenant une liste de mots-clés(toutes les versions)
        [Fact]
        public void Request9()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.VersionOs.VersionProduct.ProductId == 1 &&
                t.CreationDate >= new DateOnly(2023, 3, 5) &&
                t.CreationDate <= new DateOnly(2023, 3, 15) &&
                t.Description != null && t.Description.Contains("aucune notification")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: null,
                systemOsId: null,
                dateStart: new DateOnly(2023, 3, 5),
                dateEnd: new DateOnly(2023, 3, 15),
                keywords: "aucune notification",
                statusTicket: null);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.InRange(t.CreationDate, new DateOnly(2023, 3, 5), new DateOnly(2023, 3, 15)));
            Assert.All(tickets, t => Assert.Contains("aucune notification", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 10
        /// Obtenir tous les problèmes rencontrés au cours d’une période donnée pour un produit contenant une liste de mots-clés(Une seule version)
        [Fact]
        public void Request10()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.VersionOs.VersionProduct.ProductId == 2 &&
                t.VersionOs.VersionProductId == 5 &&
                t.CreationDate >= new DateOnly(2023, 5, 5) &&
                t.CreationDate <= new DateOnly(2023, 5, 15) &&
                t.Description != null && t.Description.Contains("ventilateur")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 2,
                versionProductId: 5,
                systemOsId: null,
                dateStart: new DateOnly(2023, 5, 5),
                dateEnd: new DateOnly(2023, 5, 15),
                keywords: "ventilateur",
                statusTicket: null);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Maître des Investissements", t.ProductName));
            Assert.All(tickets, t => Assert.Equal("1.0", t.ProductVersion));
            Assert.All(tickets, t => Assert.InRange(t.CreationDate, new DateOnly(2023, 5, 5), new DateOnly(2023, 5, 15)));
            Assert.All(tickets, t => Assert.Contains("ventilateur", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 11
        /// Obtenir tous les problèmes résolus (tous les produits)
        [Fact]
        public void Request11()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: null,
                versionProductId: null,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: null,
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 12
        /// Définition : Obtenir tous les problèmes résolus pour un produit (toutes les versions)
        [Fact]
        public void Request12()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.VersionOs.VersionProduct.ProductId == 1
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: null,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: null,
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 13
        /// Définition : Obtenir tous les problèmes résolus pour un produit (une seule version)
        [Fact]
        public void Request13()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.VersionOs.VersionProduct.Id == 4
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: 4,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: null,
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Equal("1.3", t.ProductVersion));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 14
        /// Définition : Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit (toutes les versions)
        [Fact]
        public void Request14()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.CreationDate >= new DateOnly(2023, 1, 10) &&
                t.CreationDate <= new DateOnly(2023, 1, 20)
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: null,
                systemOsId: null,
                dateStart: new DateOnly(2023, 1, 10),
                dateEnd: new DateOnly(2023, 1, 20),
                keywords: null,
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.InRange(t.CreationDate, new DateOnly(2023, 1, 10), new DateOnly(2023, 1, 20)));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 15
        /// Définition : Obtenir tous les problèmes résolus au cours d’une période donnée pour un produit (une seule version)
        [Fact]
        public void Request15()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.VersionOs.VersionProductId == 4 &&
                t.CreationDate >= new DateOnly(2023, 4, 25) &&
                t.CreationDate <= new DateOnly(2023, 5, 1)
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: 4,
                systemOsId: null,
                dateStart: new DateOnly(2023, 4, 25),
                dateEnd: new DateOnly(2023, 5, 1),
                keywords: null,
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Equal("1.3", t.ProductVersion));
            Assert.All(tickets, t => Assert.InRange(t.CreationDate, new DateOnly(2023, 4, 25), new DateOnly(2023, 5, 1)));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 16
        /// Définition : Obtenir tous les problèmes résolus contenant une liste de mots-clés (tous les produits)
        [Fact]
        public void Request16()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.Description != null && t.Description.Contains("gris foncé")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: null,
                versionProductId: null,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: "gris foncé",
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Contains("gris foncé", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 17
        /// Définition : Obtenir tous les problèmes résolus pour un produit contenant une liste de mots-clés (Toutes les versions)
        [Fact]
        public void Request17()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.Description != null && t.Description.Contains("gris foncé")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: null,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: "gris foncé",
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Contains("gris foncé", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 18
        /// Définition : Obtenir tous les problèmes en cours pour un produit contenant une liste de mots-clés (Une seule version)
        [Fact]
        public void Request18()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.VersionOs.VersionProductId == 4 &&
                t.Description != null && t.Description.Contains("compte bancaire")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: 4,
                systemOsId: null,
                dateStart: null,
                dateEnd: null,
                keywords: "compte bancaire",
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Equal("1.3", t.ProductVersion));
            Assert.All(tickets, t => Assert.Contains("compte bancaire", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 19
        /// Obtenir tous les problèmes rencontrés résolus d'une période donnée pour un produit contenant une liste de mots-clés(toutes les versions)
        [Fact]
        public void Request19()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.CreationDate >= new DateOnly(2023, 1, 10) &&
                t.CreationDate <= new DateOnly(2023, 1, 20) &&
                t.Description != null && t.Description.Contains("icône")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: null,
                systemOsId: null,
                dateStart: new DateOnly(2023, 1, 10),
                dateEnd: new DateOnly(2023, 1, 20),
                keywords: "icône",
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Contains("icône", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

        //// REQUEST 20
        /// Obtenir tous les problèmes rencontrés résolus d'une période donnée pour un produit contenant une liste de mots-clés(Une seule version)
        [Fact]
        public void Request20()
        {
            // Arrange
            int countNoResolve = _database.Tickets.Where(
                t => t.StatusTicketId == 2 &&
                t.VersionOs.VersionProduct.ProductId == 1 &&
                t.VersionOs.VersionProductId == 4 &&
                t.CreationDate >= new DateOnly(2023, 4, 25) &&
                t.CreationDate <= new DateOnly(2023, 5, 1) &&
                t.Description != null && t.Description.Contains("liaison a échoué")
                ).Count();

            // Act
            IEnumerable<TicketDto> tickets = _requestRepository.GetTicketsDto(
                productId: 1,
                versionProductId: 4,
                systemOsId: null,
                dateStart: new DateOnly(2023, 4, 25),
                dateEnd: new DateOnly(2023, 5, 1),
                keywords: "liaison a échoué",
                statusTicket: 2);

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(countNoResolve, tickets.Count());
            Assert.All(tickets, t => Assert.Equal("Résolu", t.StatusTitle));
            Assert.All(tickets, t => Assert.Equal("Trader en Herbe", t.ProductName));
            Assert.All(tickets, t => Assert.Equal("1.3", t.ProductVersion));
            Assert.All(tickets, t => Assert.Contains("liaison a échoué", t.Description));
            _output.WriteLine($"Résultat : {tickets.Count()}");
        }

    }
}
