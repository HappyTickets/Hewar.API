using Domain.Entities;
using Infrastructure.Persistence.Repositories.Generic;

namespace Infrastructure.Persistence.Repositories
{
    internal class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly AppDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UnitOfWorkService(AppDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;

            Attendances = new SoftDeletableGenericRepositoryService<Attendance>(_context, _currentUserService);
            Payrolls = new SoftDeletableGenericRepositoryService<Payroll>(_context, _currentUserService);
            PerformanceReviews = new SoftDeletableGenericRepositoryService<PerformanceReview>(_context, _currentUserService);
            Policies = new SoftDeletableGenericRepositoryService<Policy>(_context, _currentUserService);
            Reports = new SoftDeletableGenericRepositoryService<Report>(_context, _currentUserService);
            Shifts = new SoftDeletableGenericRepositoryService<Shift>(_context, _currentUserService);
            Tenants = new SoftDeletableGenericRepositoryService<TenantBase>(_context, _currentUserService);
            Facilities = new SoftDeletableGenericRepositoryService<Facility>(_context, _currentUserService);
            Companies = new SoftDeletableGenericRepositoryService<Company>(_context, _currentUserService);
            Guards = new SoftDeletableGenericRepositoryService<Guard>(_context, _currentUserService);
            Tickets = new SoftDeletableGenericRepositoryService<Ticket>(_context, _currentUserService);
            TicketMessages = new SoftDeletableGenericRepositoryService<TicketMessage>(_context, _currentUserService);
            PriceRequests = new SoftDeletableGenericRepositoryService<PriceRequest>(_context, _currentUserService);
            PriceRequestFacilityDetails = new SoftDeletableGenericRepositoryService<PriceRequestFacilityDetails>(_context, _currentUserService);
            PriceRequestResponses = new SoftDeletableGenericRepositoryService<PriceRequestResponse>(_context, _currentUserService);
            Notifications = new SoftDeletableGenericRepositoryService<Notification>(_context, _currentUserService);
        }

        #region repos
        public ISoftDeletableGenericRepositoryService<Attendance> Attendances { get; }
        public ISoftDeletableGenericRepositoryService<Payroll> Payrolls { get; }
        public ISoftDeletableGenericRepositoryService<PerformanceReview> PerformanceReviews { get; }
        public ISoftDeletableGenericRepositoryService<Policy> Policies { get; }
        public ISoftDeletableGenericRepositoryService<Report> Reports { get; }
        public ISoftDeletableGenericRepositoryService<Shift> Shifts { get; }
        public ISoftDeletableGenericRepositoryService<TenantBase> Tenants { get; }
        public ISoftDeletableGenericRepositoryService<Company> Companies { get; }
        public ISoftDeletableGenericRepositoryService<Facility> Facilities { get; }
        public ISoftDeletableGenericRepositoryService<Guard> Guards { get; }
        public ISoftDeletableGenericRepositoryService<Ticket> Tickets { get; }
        public ISoftDeletableGenericRepositoryService<TicketMessage> TicketMessages { get; }
        public ISoftDeletableGenericRepositoryService<PriceRequest> PriceRequests { get; }
        public ISoftDeletableGenericRepositoryService<PriceRequestFacilityDetails> PriceRequestFacilityDetails { get; }
        public ISoftDeletableGenericRepositoryService<PriceRequestResponse> PriceRequestResponses { get; }
        public ISoftDeletableGenericRepositoryService<Notification> Notifications { get; }
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
