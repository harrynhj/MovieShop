using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReportRepository : Repository<Purchase>, IReportRepository
{
    public ReportRepository(MovieShopDbContext dbContext): base(dbContext)
    {
        
    }
    
    public IEnumerable<Purchase> GetMovieSell(DateTime? startDate, DateTime? endDate)
    {
        var query = _movieShopDbContext.Purchases
            .Include(p => p.Movie)
            .AsQueryable();

        if (startDate.HasValue)
        {
            query = query.Where(p => p.PurchaseDateTime >= startDate.Value);
        }

        if (endDate.HasValue)
        {
            query = query.Where(p => p.PurchaseDateTime <= endDate.Value);
        }

        return query.ToList();
    }
}