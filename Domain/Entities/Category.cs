using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Category : AggregateRoot<Guid>
    {
        public Title Title { get; set; }

        public Category(Guid id, Title title)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(id));

            if (title is null)
                throw new ArgumentNullException(nameof(title));

            Id = id;
            Title = title;
        }

        public Category(Title title) 
            : this(Guid.NewGuid(), title) 
        { 
        }
    }
}
