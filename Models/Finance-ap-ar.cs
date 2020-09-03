using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("FINANCE_AP_AR")] //renomear a tabela no banco
    public class FinanceApAr
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("ORIGIN_STATUS")] //1= VENDAS, 2= COMPRAS, 3= OUTROS
        [Required(ErrorMessage = "ID Status - Este campo é obrigatorio")]
        [Range(1, 3, ErrorMessage = "Origem Invalida - Os valores devem ser 1,2 ou 3")]
        [DataType("int")]
        public int OriginStatus { get; set; }


        [Column("STATUS")] //1= PENDENTE, 2= QUITADO, 3= CANCELADO
        [Required(ErrorMessage = "ID Status - Este campo é obrigatorio")]
        [Range(1, 2, ErrorMessage = "Status Invalido - Os valores devem ser 1,2 ou 3")]
        [DataType("int")]
        public int Status { get; set; }


        [Column("TYPE")] //0 A PAGAR - 1 A RECEBER
        [Required(ErrorMessage = "Tipo obrigatorio (Pagar/Receber)")]
        [Range(0, 1, ErrorMessage = "Tipo Invalido - Os valores devem ser 0 ou 1")]
        [DataType("int")]
        public int Type { get; set; }

        [Column("ENTITY_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int EntityId { get; set; }


        [Column("TOTAL")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(0.01, 999999, ErrorMessage = "O total do lançamento deve ser maior que zero")]
        [DataType("decimal")]
        public decimal Total { get; set; }

        [Column("PAYMENT_ID")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int PaymentId { get; set; }

        [Column("NOTE")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string Note { get; set; }

        [Column("DATE_CREATED")]
        [Required(ErrorMessage = "Obrigatório informar uma data de Criação.")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_UPDATE")]
        [Required(ErrorMessage = "Obrigatório informar uma data de atualização.")]
        public DateTime DateUpdate { get; set; }


        //objetos completos
        public Entity Entity { get; set; }
        public PaymentMethod Payment { get; set; }




    }
}