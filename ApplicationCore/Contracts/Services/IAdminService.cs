
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IAdminService
{
    int InsertMovie(NewMovieModel movie, string userName);
    bool DeleteMovie(int id);
    bool UpdatePrice(MoviePriceModel moviePrice);
    PaginatedModel<ReportModel> GetSellReport(DateTime? start, DateTime? end, int page);
}