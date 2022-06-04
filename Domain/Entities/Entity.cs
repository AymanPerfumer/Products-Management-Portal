using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var entity = obj as IEntity<TKey>;

            if (entity == null)
                return false;

            if (GetType() != entity.GetType())
                return false;

            return Id.Equals(entity.Id);
        }

        public static bool operator ==(Entity<TKey> entity1, Entity<TKey> entity2)
        {
            return Equals(entity1, entity2);
        }

        public static bool operator !=(Entity<TKey> entity1, Entity<TKey> entity2)
        {
            return !(entity1 == entity2);
        }
    }
}
