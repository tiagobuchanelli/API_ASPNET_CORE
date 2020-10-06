using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("payment_methods")] //renomear a tabela no banco
    public class PaymentMethod
    {
        [Key]
        [Column("id")] //renomear a tabela no banco.
        public int Id { get; set; }

        [Column("company_id")] //Não é obrigatório pois a API é quem identifica e grava o ID pelo token (uid do firebase)
        // [Required(ErrorMessage = "Este campo é obrigatorio")]
        // [DataType("varchar")]
        public string CpnyUid { get; set; }

        [Column("desc")]
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [DataType("varchar")]
        public string Desc { get; set; }


        [Column("interval")]
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [Range(1, 999, ErrorMessage = "Intervalo deve ser maior que zero")]
        [DataType("int")]
        public int Interval { get; set; }

        [Column("repeat_nr")]
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [Range(1, 999, ErrorMessage = "Número de repetições deve ser maior que zero")]
        [DataType("int")]
        public int RepeatNr { get; set; }


        [Column("date_created")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }

        public Company Cpny { get; set; }


    }
}