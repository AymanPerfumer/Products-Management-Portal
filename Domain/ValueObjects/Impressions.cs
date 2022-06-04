using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Impressions : ValueObject
    {
        public int Value { get; }

        public Impressions(int value)
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Impressions cannot be less then 0");

            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
