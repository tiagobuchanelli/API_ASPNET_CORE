using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("category_products")] //renomear a tabela no banco
    public class Category
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

        [Column("satus")] //0 Cancelado - 1 Ativo
        [Required(ErrorMessage = "Status - Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }

        public Company Cpny { get; set; }

    }
}