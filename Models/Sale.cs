using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("SALES")] //renomear a tabela no banco
    public class Sale
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("COSTUMER_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int CostumerId { get; set; }

        [Column("FINANCE_AR_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int FinanceArId { get; set; }

        [Column("STATUS")] //0 CANCELADO, 1 ATIVO
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }


        [Column("TOTAL_SALE")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O valor da venda deve ser maior que zero")]
        [DataType("decimal")]
        public decimal TotalSale { get; set; }


        [Column("PAYMENT_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int PaymentId { get; set; }

        [Column("NOTE")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string Note { get; set; }

        [Column("DATE_SALE")]
        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime DateSales { get; set; }


        [Column("DATE_UPDATE")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }


        //objetos completos
        public Entity Costumer { get; set; }

        public PaymentMethod Payment { get; set; }


    }
}