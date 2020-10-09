using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("sale_products")]
    public class OrderProduct
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Column("prod_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int ProductId { get; set; }


        [Column("order_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int OrderId { get; set; }

        [Column("quantity")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "A quanitade deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Quantity { get; set; }


        [Column("price")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("decimal")]
        public decimal Price { get; set; }

        [Column("total")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O valor da venda deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Total { get; set; }

        [Column("date_created")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }

        //objetos completos
        public Order Order { get; set; }

        public Product Product { get; set; }
    }
}