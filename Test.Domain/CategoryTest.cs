using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Domain
{
    public class CategoryTest
    {
        [Fact]
        public void Category_Cannot_Be_Without_Title()
        {
            Assert.Throws<ArgumentNullException>(() => new Category(null));
        }

        [Fact]
        public void Category_Id_Cannot_Be_Empty()
        {
            Assert.Throws<ArgumentException>(() => new Category(Guid.Empty, null));
        }
    }
}
