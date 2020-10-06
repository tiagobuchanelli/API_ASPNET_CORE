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



        [Column("desc")]
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [DataType("varchar")]
        public string Desc { get; set; }


        [Column("type")] //cartao/dinheiro (0,1)
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [Range(1, 999, ErrorMessage = "Intervalo deve ser maior que zero")]
        [DataType("int")]
        public int Type { get; set; }

        [Column("type_desc")] //desc type: cartao/dinheiro
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [DataType("varchar")]
        public string TypeDesc { get; set; }




        [Column("date_created")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateCreated { get; set; }

        [Column("date_update")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }




    }
}