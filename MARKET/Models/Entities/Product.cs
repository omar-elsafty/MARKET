using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MARKET.Models.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public string Discription { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public int Quantity { get; set; }

        //one to many relation
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }


        //many to many relation
        public IList<ProductPayment> PaymentTypes { get; set; }
        public IList<ProductTag> Tags { get; set; }


    }

}
