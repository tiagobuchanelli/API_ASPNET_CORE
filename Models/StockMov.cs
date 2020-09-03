using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("STOCK_MOV")] //renomear a tabela no banco
    public class StockMov
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("PROD_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int ProductId { get; set; }

        [Column("OPERATION_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int OperationId { get; set; }

        [Column("DATE_MOV")]
        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime DateMov { get; set; }


        [Column("QUANTITY")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("decimal")]
        public decimal Quantity { get; set; }


        //objetos completos
        public Product Product { get; set; }
        public OperationFinance Operation { get; set; }
    }
}