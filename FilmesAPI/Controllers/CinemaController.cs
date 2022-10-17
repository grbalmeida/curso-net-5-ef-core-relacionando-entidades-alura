using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Data.Dtos.Cinema;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CinemaController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            var cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(RecuperaCinemaPorId), new { cinema.Id }, cinema);
        }

        [HttpGet]
        public async Task<IEnumerable<Cinema>> RecuperaCinemas([FromQuery] string nomeDoFilme)
        {
            return await _context.Cinemas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaCinemaPorId(int id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(cinema => cinema.Id == id);

            if (cinema != null)
            {
                var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
                return Ok(cinemaDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }

            _mapper.Map(cinemaDto, cinema);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaCinema(int id)
        {
            var cinema = await _context.Cinemas.FirstOrDefaultAsync(cinema => cinema.Id == id);

            if (cinema == null)
            {
                return NotFound();
            }

            _context.Remove(cinema);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
