using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class CreateCinemaDto
    {
        [Display(Name = "nome")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
        public int GerenteId { get; set; }
    }
}
