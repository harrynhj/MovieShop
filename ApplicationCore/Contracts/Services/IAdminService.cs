
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAdminService
{
    Task<int> InsertMovie(NewMovieModel movie, string userName);
    Task<bool> DeleteMovie(int id);
    Task<bool> UpdatePrice(MoviePriceModel moviePrice);
    Task<PaginatedModel<ReportModel>> GetSellReport(DateTime? start, DateTime? end, int page);
}