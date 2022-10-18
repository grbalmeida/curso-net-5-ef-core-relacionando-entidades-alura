using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class ReadCinemaDto
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Name = "nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        public ReadEnderecoDto Endereco { get; set; }
    }
}
