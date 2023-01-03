using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class BrandColor
    {
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ColorId { get; set; }
        public Color Color { get; set; }
    }
}
