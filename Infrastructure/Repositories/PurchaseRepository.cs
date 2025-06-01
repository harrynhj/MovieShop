using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class PurchaseRepository : Repository<Purchase>, IPurchaseRepository
{
    public PurchaseRepository(MovieShopDbContext dbContext): base(dbContext)
    {
        
    }
}