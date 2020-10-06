using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("orders")] //renomear a tabela no banco
    public class Order
    {
        [Key]
        [Column("id")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("costumer_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public string CostumerUid { get; set; }


        [Column("company_id")] //Não é obrigatório pois a API é quem identifica e grava o ID pelo token (uid do firebase)
        // [Required(ErrorMessage = "Este campo é obrigatorio")]
        // [DataType("varchar")]
        public string CpnyUid { get; set; }



        [Column("status")] //0 CANCELADO, 1 ATIVO
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
        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime DateOrder { get; set; }


        [Column("date_update")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }


        //objetos completos
        public User Costumer { get; set; }
        public Company Cpny { get; set; }

        public PaymentMethod Payment { get; set; }




    }
}