using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("STOCK")] //renomear a tabela no banco
    public class Stock
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("PROD_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int ProductId { get; set; }


        [Column("QUANTITY")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O estoque deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Quantity { get; set; }

        [Column("DATE_CREATED")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_UPDATE")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }


        public Product Product { get; set; }

    }
}