using CashFlow.Application.Dtos;

namespace CashFlow.Application.Repositories
{
    public interface ICashFlowReportRepostory
    {
        Task<IList<CashFlowReportDto>> GetReport();
    }
}
