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
        ISoftDeletableGenericRepositoryService<Ticket> Tickets { get; }
        ISoftDeletableGenericRepositoryService<Company> Companies { get; }
        ISoftDeletableGenericRepositoryService<Facility> Facilities { get; }
        ISoftDeletableGenericRepositoryService<Guard> Guards { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task SaveChangesAsync();
    }
}
