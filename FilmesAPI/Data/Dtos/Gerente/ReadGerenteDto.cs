using FilmesAPI.Models;
using System.Collections.Generic;

namespace FilmesAPI.Data.Dtos
{
    public class ReadGerenteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public object Cinemas { get; set; }
    }
}
