using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("SALES_PRODUCTS")] //renomear a tabela no banco
    public class SaleProduct
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("PROD_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int ProductId { get; set; }


        [Column("SALE_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int SaletId { get; set; }

        [Column("QUANTITY")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "A quanitade deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Quantity { get; set; }


        [Column("PRICE")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("decimal")]
        public decimal Price { get; set; }

        [Column("TOTAL")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O valor da venda deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Total { get; set; }



        //objetos completos
        public Sale Sale { get; set; }

        public Product Product { get; set; }
    }
}