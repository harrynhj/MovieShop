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
        var cast = _movieShopDbContext.Set<Cast>()
            .Include(c => c.MovieCast) 
            .ThenInclude(mc => mc.Movie)
            .FirstOrDefault(c => c.Id == id);
        return cast;
    }

}