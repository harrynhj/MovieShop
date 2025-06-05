using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class CastService : ICastService
{
    private readonly ICastRepository _castRepository;
    
    public CastService(ICastRepository castRepository)
    {
        _castRepository = castRepository;
    }
    
    
    public async Task<CastDetailsModel> GetCastDetails(int id)
    {
        var castDetail = await _castRepository.GetById(id);
        var movies = new List<MovieCardModel>();
        foreach (var movie in castDetail.MovieCast)
        {
            movies.Add(new MovieCardModel()
            {
                Id = movie.Movie.Id,
                Title = movie.Movie.Title,
                PosterURL = movie.Movie.PosterUrl,
            });
        }

        var res = new CastDetailsModel()
        {
            Id = castDetail.Id,
            Gender = castDetail.Gender,
            Name = castDetail.Name,
            ProfilePath = castDetail.ProfilePath,
            TmdbUrl = castDetail.TmdbUrl,
            Movies = movies
        };
        return res;
    }
}