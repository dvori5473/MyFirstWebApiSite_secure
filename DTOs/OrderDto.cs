using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFirstWebApiSite;

namespace DTOs
{
    public class OrderDto
    {
        [Required, DataType("dd-mm-yyyy")]
        public DateTime OrderDate { get; set; }
        [Required]
        public int OrderSum { get; set; }
        [Required]
        public int UserId { get; set; }

        public virtual ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
