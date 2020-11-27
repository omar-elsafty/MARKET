using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Models.Entities
{
    public class ProductPayment
    {
        [ForeignKey(nameof(PaymentType))]
        public int PaymentId { get; set; }
        public PaymentType PaymentType { get; set; }


        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
