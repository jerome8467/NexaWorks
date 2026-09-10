using Microsoft.EntityFrameworkCore;
using NexaWorks.Data;
using NexaWorks.Dtos;
using NexaWorks.Entities;

namespace NexaWorks.Repositories
{
    public class RequestRepository
    {
        private ApplicationDbContext _dataBase;
        public RequestRepository(ApplicationDbContext database) 
        {
            _dataBase = database;
        }

        public IEnumerable<TicketDto> GetTicketsDto(
            int? productId = null,
            int? versionProductId = null,
            int? systemOsId = null,
            DateOnly? dateStart = null,
            DateOnly? dateEnd = null,
            string? keywords = null,
            int? statusTicket = null,
            bool isDelete = false)
        {
            IQueryable<Ticket> request = _dataBase.Tickets
                .Where(t => t.IsDeleted == isDelete);

            if (productId != null)
                request = request.Where(t => t.VersionOs.VersionProduct.ProductId == productId);

            if (versionProductId != null)
                request = request.Where(t => t.VersionOs.VersionProductId == versionProductId);

            if (systemOsId != null)
                request = request.Where(t => t.VersionOs.SystemOsId == systemOsId);

            if (dateStart != null)
                request = request.Where(t => t.CreationDate >= dateStart);

            if (dateEnd != null)
                request = request.Where(t => t.CreationDate <= dateEnd);

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                var keywordsList = keywords.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in keywordsList)
                {
                    request = request.Where(t => t.Description != null && t.Description.Contains(word));
                }
            }

            if (statusTicket != null)
                request = request.Where(t => t.StatusTicketId == statusTicket);

            return request
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    CreationDate = t.CreationDate,
                    Description = t.Description ?? string.Empty,
                    ProductName = t.VersionOs.VersionProduct.Product.NameProduct,
                    ProductVersion = t.VersionOs.VersionProduct.RefVersion,
                    SystemOsName = t.VersionOs.SystemOs.NameSystem,
                    StatusTitle = t.StatusTicket.Title,
                    ResolutionDate = t.Resolution != null ? t.Resolution.ResolutionDate : null,
                    Resolution = t.Resolution != null ? t.Resolution.Description : null

                })
                .ToList();
        }

        public IEnumerable<Ticket> GetTicketsWithInclude(
            int? productId = null, 
            int? versionProductId = null, 
            int? systemOsId = null, 
            DateOnly? dateStart = null, 
            DateOnly? dateEnd = null, 
            string? keywords = null, 
            int? statusTicket = null, 
            bool isDelete = false) 
        {
            IQueryable<Ticket> request = _dataBase.Tickets
                .Include(t => t.VersionOs)
                    .ThenInclude(v => v.VersionProduct)
                        .ThenInclude(p => p.Product)
                .Include(t => t.VersionOs)
                    .ThenInclude(v => v.SystemOs)
                .Include(t => t.Resolution)
                .Include(t => t.StatusTicket)
                .Where(t => t.IsDeleted == isDelete);

            if (productId != null)
                request = request.Where(t => t.VersionOs.VersionProduct.ProductId == productId);

            if (versionProductId != null)
                request = request.Where(t => t.VersionOs.VersionProductId == versionProductId);

            if(systemOsId != null) 
                request = request.Where(t => t.VersionOs.SystemOsId == systemOsId);

            if (dateStart != null)
                request = request.Where(t => t.CreationDate >= dateStart);

            if (dateEnd != null)
                request = request.Where(t => t.CreationDate <= dateEnd);

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                var keywordsList = keywords.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var word in keywordsList) 
                {
                    request = request.Where(t => t.Description != null && t.Description.Contains(word));
                }
            }

            if (statusTicket != null) 
                request = request.Where(t => t.StatusTicketId == statusTicket);

            var response = request.ToList();
            return response;
        }




    }
}
