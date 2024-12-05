

namespace Application.Common.Interfaces.Repositories
{
    public interface IUnitOfWorkService
    {
        ISoftDeletableGenericRepositoryService<Attendance> Attendances { get; }
        ISoftDeletableGenericRepositoryService<Customer> Customers { get; }
        ISoftDeletableGenericRepositoryService<Guard> Guards { get; }
        ISoftDeletableGenericRepositoryService<Payroll> Payrolls { get; }
        ISoftDeletableGenericRepositoryService<PerformanceReview> PerformanceReviews { get; }
        ISoftDeletableGenericRepositoryService<Policy> Policies { get; }
        ISoftDeletableGenericRepositoryService<Report> Reports { get; }
        ISoftDeletableGenericRepositoryService<Shift> Shifts { get; }
        ISoftDeletableGenericRepositoryService<Tenant> Tenants { get; }
        ISoftDeletableGenericRepositoryService<Ticket> Tickets { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}
