using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("PAYMENT_METHODS")] //renomear a tabela no banco
    public class PaymentMethod
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int IdProduct { get; set; }


        [Column("DESC")]
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [DataType("varchar")]
        public string Desc { get; set; }


        [Column("INTERVAL")]
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [Range(1, 999999, ErrorMessage = "Intervalo deve ser maior que zero")]
        [DataType("int")]
        public int Interval { get; set; }


        [Column("DATE_CREATED")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("DATE_UPDATE")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }



    }
}