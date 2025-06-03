using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IReportRepository _reportRepository;
    
    public AdminService(IMovieRepository movieRepository, IReportRepository reportRepository)
    {
        _movieRepository = movieRepository;
        _reportRepository = reportRepository;
    }
    
    
    public int InsertMovie(NewMovieModel movie, string userName)
    {
        Movie res = _movieRepository.Insert(new Movie()
        {
            BackdropUrl = movie.BackdropUrl,
            Budget = movie.Budget,
            CreatedBy = userName,
            CreatedDate = DateTime.Now,
            ImdbUrl = movie.ImdbUrl,
            OriginalLanguage = movie.OriginalLanguage,
            Overview = movie.Overview,
            PosterUrl = movie.PosterUrl,
            Price = movie.Price,
            ReleaseDate = movie.ReleaseDate,
            Revenue = movie.Revenue,
            RunTime = movie.RunTime,
            Tagline = movie.Tagline,
            Title = movie.Title,
            TmdbUrl = movie.TmdbUrl,
            UpdatedBy = userName,
            UpdatedDate = DateTime.Now
        });
        return res.Id;
    }
    
    public bool DeleteMovie(int id)
    {
        var movie = _movieRepository.DeleteById(id);
        if (movie == null)
        {
            return false;
        }

        return true;
    }

    public bool UpdatePrice(MoviePriceModel moviePrice)
    {
        var movie = _movieRepository.GetById(moviePrice.MovieId);
        movie.Price = moviePrice.Price;
        var res = _movieRepository.Update(movie);
        if (res == null) return false;
        return true;
    }

    public PaginatedModel<ReportModel> GetSellReport(DateTime? start, DateTime? end, int page)
    {
        int itemsPerPage = 30;
    
        var purchases = _reportRepository.GetMovieSell(start, end);
        
        var grouped = purchases
            .GroupBy(p => p.MovieId)
            .Select(g => new
            {
                Movie = g.First().Movie.Title,
                MovieId = g.First().MovieId,
                Count = g.Count(),
            })
            .OrderByDescending(g => g.Count)
            .Select((item, index) => new
            {
                RowNumber = index + 1,
                Movie = item.Movie,
                MovieId = item.MovieId,
                Count = item.Count
            })
            .ToList();
        int totalItems = grouped.Count;
        int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
        
        var pagedData = grouped
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToList();

        var resultItems = pagedData.Select(item => new ReportModel
        {
            Id = item.RowNumber,
            Title = item.Movie,
            movieId = item.MovieId,
            Count = item.Count,
        }).ToList();

        return new PaginatedModel<ReportModel>
        {
            Items = resultItems,
            CurrentIndex = page,
            TotalPages = totalPages
        };
    }
}