using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Description : ValueObject
    {
        public string Value { get; }

        public Description(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if (value.Length > 1000)
                throw new ArgumentOutOfRangeException(nameof(value), 
                    "Description length cannot be longer than 1000");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
