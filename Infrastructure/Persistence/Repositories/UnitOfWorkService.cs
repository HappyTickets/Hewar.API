
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    internal class UnitOfWorkService: IUnitOfWorkService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UnitOfWorkService(AppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;

            Attendances = new SoftDeletableGenericRepositoryService<Attendance>(_context, _currentUserService);
            Customers = new SoftDeletableGenericRepositoryService<Customer>(_context, _currentUserService);
            Guards = new SoftDeletableGenericRepositoryService<Guard>(_context, _currentUserService);
            Payrolls = new SoftDeletableGenericRepositoryService<Payroll>(_context, _currentUserService);
            PerformanceReviews = new SoftDeletableGenericRepositoryService<PerformanceReview>(_context, _currentUserService);
            Policies = new SoftDeletableGenericRepositoryService<Policy>(_context, _currentUserService);
            Reports = new SoftDeletableGenericRepositoryService<Report>(_context, _currentUserService);
            Shifts = new SoftDeletableGenericRepositoryService<Shift>(_context, _currentUserService);
            Tenants = new SoftDeletableGenericRepositoryService<Tenant>(_context, _currentUserService);
            Tickets = new SoftDeletableGenericRepositoryService<Ticket>(_context, _currentUserService);
        }

        #region repos
        public ISoftDeletableGenericRepositoryService<Attendance> Attendances { get; }
        public ISoftDeletableGenericRepositoryService<Customer> Customers { get; }
        public ISoftDeletableGenericRepositoryService<Guard> Guards { get; }
        public ISoftDeletableGenericRepositoryService<Payroll> Payrolls { get; }
        public ISoftDeletableGenericRepositoryService<PerformanceReview> PerformanceReviews { get; }
        public ISoftDeletableGenericRepositoryService<Policy> Policies { get; }
        public ISoftDeletableGenericRepositoryService<Report> Reports { get; }
        public ISoftDeletableGenericRepositoryService<Shift> Shifts { get; }
        public ISoftDeletableGenericRepositoryService<Tenant> Tenants { get; }
        public ISoftDeletableGenericRepositoryService<Ticket> Tickets { get; }
        #endregion

        #region transaction methods
        public Task BeginTransactionAsync()
            => _context.Database.BeginTransactionAsync();

        public Task CommitTransactionAsync()
            => _context.Database.CommitTransactionAsync();

        public Task RollbackTransactionAsync()
            => _context.Database.RollbackTransactionAsync();

        public Task SaveChangesAsync()
            => _context.SaveChangesAsync();
        #endregion
    }
}
