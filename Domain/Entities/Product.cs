using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : AggregateRoot<Guid>
    {
        virtual public Category Category { get; set; }
        public Title Title { get; set; }
        public Description? Description { get; set; }
        public Money Price { get; set; }
        public DietaryFlags? Dietaries { get; set; }
        public Image? Image { get; set; }
        public Impressions Impressions { get; set; }

        public Product(Guid id, Title title)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Guid cannot be empty", nameof(id));

            if (title is null)
                throw new ArgumentNullException(nameof(title));

            Id = id;
            Title = title;
            Price = new Money(0);
            Impressions = new Impressions(0);
        }

        public Product(Title title)
            : this(Guid.NewGuid(), title)
        {
        }
    }
}
