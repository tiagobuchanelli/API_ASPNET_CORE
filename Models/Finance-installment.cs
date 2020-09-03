using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("FINANCE_INSTALLMENT")] //renomear a tabela no banco
    public class FinanceInstallment
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("FINANCE_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int FinanceId { get; set; }


        [Column("STATUS")] //1= PENDENTE, 2= QUITADO, 3= CANCELADO
        [Required(ErrorMessage = "ID Status - Este campo é obrigatorio")]
        [Range(1, 2, ErrorMessage = "Status Invalido - Os valores devem ser 1,2 ou 3")]
        [DataType("int")]
        public int Status { get; set; }

        [Column("TOTAL")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("decimal")]
        public decimal Total { get; set; }

        [Column("PAYMENT_OK")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("decimal")]
        public decimal PaymentOk { get; set; }


        [Column("NOTE")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string Note { get; set; }

        [Column("DATE_DUE")]
        [Required(ErrorMessage = "Obrigatório informar uma data de vencimento.")]
        public DateTime DateDue { get; set; }

        [Column("DATE_LAST_PAY")]
        [Required(ErrorMessage = "Obrigatório informar uma data de ultimo pagamento.")]
        public DateTime DateLastPay { get; set; }

        [Column("DATE_CREATED")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_UPDATE")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }

        [Column("ENTITY_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int EntityId { get; set; }


        //objetos completos
        public FinanceApAr Finance { get; set; }

        public Entity Entity { get; set; }




    }
}