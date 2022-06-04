using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Title : ValueObject
    {
        public string Value { get; }

        public Title(string value)
        {
            if(string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));

            if(value.Length > 100)
                throw new ArgumentOutOfRangeException(nameof(value), "Title length cannot be longer than 100");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
