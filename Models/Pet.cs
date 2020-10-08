using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lojax.Models
{
    [Table("pets")] //renomear a tabela no banco
    public class Pet
    {
        [Key]
        [Column("id")] //renomear a tabela no banco.
        public int Id { get; set; }


        [Column("user_id")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [Range(1, 999999, ErrorMessage = "O valor da venda deve ser maior que zero")]
        public int UserId { get; set; }

        [Column("user_uid")] //Não é obrigatório pois a API é quem identifica e grava o ID pelo token (uid do firebase)
        //[Required(ErrorMessage = "Este campo é obrigatorio")]
        // [DataType("varchar")]
        public string UserUid { get; set; }



        [Column("status")] //0 CANCELADO, 1 ATIVO
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Status { get; set; }


        [Column("name_pet")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string NamePet { get; set; }

        [Column("color_pet")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string ColorPet { get; set; }

        [Column("age")] //0 CANCELADO, 1 ATIVO
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Age { get; set; }

        [Column("species")] //0 CANCELADO, 1 ATIVO
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Species { get; set; }

        [Column("race")] //0 CANCELADO, 1 ATIVO
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Race { get; set; }

        [Column("pelage")] //0 CANCELADO, 1 ATIVO
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Pelage { get; set; }

        [Column("gender")] //1 MACHO, 2 FEMEA, 3 INDEFINIDO
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        [DataType("int")]
        public int Gender { get; set; }

        [Column("microship_nr")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string MicroshipNr { get; set; }

        [Column("pedigree_nr")]
        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [DataType("varchar")]
        public string PedigreeNr { get; set; }

        [Column("birth_date")]
        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime BirthDate { get; set; }

        [Column("date_created")]
        [Required(ErrorMessage = "Obrigatório informar uma data.")]
        public DateTime DateCreated { get; set; }


        [Column("date_update")]
        [Required(ErrorMessage = "Este campo é obrigatorio")]
        public DateTime DateUpdate { get; set; }


        //objetos completos
        public User User { get; set; }




    }
}