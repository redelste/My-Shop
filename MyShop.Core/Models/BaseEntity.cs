using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    // Def: Abstract: we can never create an instance of the base entity class on its own. We can only create a class that implements it.
    public abstract class BaseEntity
    {
        public string Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
}
