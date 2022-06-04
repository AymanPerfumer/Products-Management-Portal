using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Domain
{
    public class ProductTest
    {
        [Fact]
        public void Product_Cannot_Be_Without_Title()
        {
            Assert.Throws<ArgumentNullException>(() => new Product(null));
        }

        [Fact]
        public void Product_Id_Cannot_Be_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Product(Guid.Empty, null));
        }
    }
}
