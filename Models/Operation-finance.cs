using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("OPERATIONS_FINANCE")] //renomear a tabela no banco
    public class OperationFinance
    {
        [Key]
        [Column("ID")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("DESC")]
        [Required(ErrorMessage = "Este campo é obrigatório")] //como tem MinLength não seria necessario
        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [DataType("varchar")]
        public string Desc { get; set; }

    }


    //operações
    //ex: 1 - venda 2 - ajuste de estoque

}