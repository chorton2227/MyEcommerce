namespace MyEcommerce.Data.EntityFrameworkCore
{
    using System;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    using MyEcommerce.Core.Domain;

    public abstract class BaseDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        private IDbContextTransaction _currentTransaction;

        protected BaseDbContext(DbContextOptions options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Guid> BeginTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                _currentTransaction = await Database
                    .BeginTransactionAsync(IsolationLevel.ReadCommitted)
                    .ConfigureAwait(false);
            }

            return _currentTransaction.TransactionId;
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                throw new InvalidOperationException("Transaction must first be started");
            }

            try
            {
                await ((IUnitOfWork)this).SaveChangesAsync().ConfigureAwait(false);
                await _currentTransaction.CommitAsync().ConfigureAwait(false);
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public bool HasActiveTransction()
        {
            return _currentTransaction != null;
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                _currentTransaction?.Dispose();
                _currentTransaction = null;
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.PublishDomainEventsAsync(this).ConfigureAwait(false);
            return  await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}