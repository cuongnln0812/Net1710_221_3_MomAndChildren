using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MomAndChildren.Data.Models.DTO
{
    public class ProductCart
    {
        public Product? Product { get; set; }
        public int QuantityCart { get; set; }
    }
}
