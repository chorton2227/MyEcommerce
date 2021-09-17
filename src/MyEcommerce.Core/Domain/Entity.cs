namespace MyEcommerce.Core.Domain
{
    using System.Collections.Generic;

    public abstract class Entity<TId> : IEntity
        where TId : Identifier
    {
        private readonly List<IDomainEvent> _domainEvents = new();

        private int? _hashCode;

        public virtual TId Id { get; protected set; }

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public bool IsTransient()
        {
            return Id.Equals(default(TId));
        }

        public override int GetHashCode()
        {
            if (IsTransient())
            {
                return base.GetHashCode();
            }

            if (_hashCode == null)
            {
                // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                _hashCode = Id.GetHashCode() ^ 31;
            }

            return _hashCode.Value;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity<TId>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            var entity = (Entity<TId>)obj;
            if (IsTransient() || entity.IsTransient())
            {
                return false;
            }

            return Id.Equals(entity.Id);
        }

        public static bool operator ==(Entity<TId> left, Entity<TId> right)
        {
            if (left.Equals(null))
            {
                return right.Equals(null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId> left, Entity<TId> right)
        {
            return !(left == right);
        }
    }
}