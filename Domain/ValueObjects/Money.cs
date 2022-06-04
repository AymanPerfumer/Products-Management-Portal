using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Money : ValueObject
    {
        public double Value { get; }

        public Money(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "value cannot be less then 0");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
