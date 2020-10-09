using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("orders")]
    public class Order
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }


        [Column("costumer_id")]
        public int CostumerId { get; set; }


        //UID da Empresa. Utilizado para identificar o CLIENTE da venda, por meio do uid_user que está presente no token.
        [Column("costumer_uid")]
        // [DataType("varchar")]
        public string CostumerUid { get; set; }


        [Column("company_id")]
        // [DataType("varchar")]
        public int CpnyId { get; set; }


        //UID da Empresa. Utilizado para identificar o VENDEDOR da venda, por meio do uid_user que está presente no token.
        [Column("company_uid")]
        // [DataType("varchar")]
        public string CpnyUid { get; set; }


        //0 CANCELADO, 1 ATIVO
        [Column("status")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }


        [Column("total_sale")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O valor da venda deve ser maior que zero")]
        [DataType("decimal")]
        public decimal TotalSale { get; set; }


        [Column("payment_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int PaymentId { get; set; }

        [Column("payment_desc")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string PaymentDesc { get; set; }


        [Column("note")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string Note { get; set; }

        [Column("date_created")]
        public DateTime DateOrder { get; set; }


        [Column("date_update")]
        public DateTime DateUpdate { get; set; }


        //objetos completos
        public User Costumer { get; set; }
        public Company Cpny { get; set; }

        public PaymentMethod Payment { get; set; }




    }
}