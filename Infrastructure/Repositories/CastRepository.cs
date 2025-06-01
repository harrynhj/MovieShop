using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CastRepository : Repository<Cast>, ICastRepository
{
    public CastRepository(MovieShopDbContext dbContext): base(dbContext)
    {
        
    }

    public override Cast GetById(int id)
    {
        return _movieShopDbContext.Set<Cast>().Include(m => m.MovieCast).FirstOrDefault(c => c.Id == id);
    }

}