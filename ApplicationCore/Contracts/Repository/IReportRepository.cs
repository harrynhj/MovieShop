using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repository;

public interface IReportRepository : IRepository<Purchase>
{
    IEnumerable<Purchase> GetMovieSell(DateTime? startDate, DateTime? endDate);
}