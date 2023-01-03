using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Brand:IEntity
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public List<Car> Cars { get; set; }
        public List<BrandColor> BrandColors { get; set; }
        
    }
}
