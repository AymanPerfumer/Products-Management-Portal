using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class NewProductDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public double Price { get; set; }
        public string? Image { get; set; }
    }
}
