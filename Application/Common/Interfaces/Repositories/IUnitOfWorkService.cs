using Domain.Entities.PriceRequestAggregates;
using Domain.Entities.TicketAggregates;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Common.Interfaces.Repositories
{
    public interface IUnitOfWorkService
    {
        ISoftDeletableGenericRepositoryService<Attendance> Attendances { get; }
        ISoftDeletableGenericRepositoryService<Payroll> Payrolls { get; }
        ISoftDeletableGenericRepositoryService<PerformanceReview> PerformanceReviews { get; }
        ISoftDeletableGenericRepositoryService<Policy> Policies { get; }
        ISoftDeletableGenericRepositoryService<Report> Reports { get; }
        ISoftDeletableGenericRepositoryService<Shift> Shifts { get; }
        ISoftDeletableGenericRepositoryService<Company> Companies { get; }
        ISoftDeletableGenericRepositoryService<Facility> Facilities { get; }
        ISoftDeletableGenericRepositoryService<Guard> Guards { get; }
        ISoftDeletableGenericRepositoryService<Ticket> Tickets { get; }
        ISoftDeletableGenericRepositoryService<TicketMessage> TicketMessages { get; }
        ISoftDeletableGenericRepositoryService<PriceRequest> PriceRequests { get; }
        ISoftDeletableGenericRepositoryService<PriceRequestFacilityDetails> PriceRequestFacilityDetails { get; }
        ISoftDeletableGenericRepositoryService<PriceRequestOffer> PriceRequestOffers { get; }
        ISoftDeletableGenericRepositoryService<Notification> Notifications { get; }
        ISoftDeletableGenericRepositoryService<InsuranceAd> InsuranceAds { get; }
        ISoftDeletableGenericRepositoryService<InsuranceAdOffer> InsuranceAdOffers { get; }
        ISoftDeletableGenericRepositoryService<InsuranceAdOfferMessage> InsuranceAdOfferMessages { get; }
        ISoftDeletableGenericRepositoryService<PriceRequestMessage> PriceRequestMessages { get; }

        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}
