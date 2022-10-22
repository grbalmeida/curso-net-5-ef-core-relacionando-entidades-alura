using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FilmeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
            var filme = _mapper.Map<Filme>(filmeDto);

            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(RecuperaFilmePorId), new { id = filme.Id }, filme);
        }

        [HttpGet]
        public async Task<IActionResult> RecuperaFilmes([FromQuery] int? classificacaoEtaria = null)
        {
            List<Filme> filmes;

            if (classificacaoEtaria == null)
            {
                filmes = await _context.Filmes.ToListAsync();
            }
            else
            {
                filmes = await _context.Filmes.Where(filme => filme.ClassificacaoEtaria <= classificacaoEtaria).ToListAsync();
            }

            if (filmes != null && filmes.Count > 0)
            {
                var filmesDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
                return Ok(filmesDto);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaFilmePorId(int id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme != null)
            {
                var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                return Ok(filmeDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _mapper.Map(filmeDto, filme);
            _context.Filmes.Update(filme);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaFilme(int id)
        {
            var filme = await _context.Filmes.FirstOrDefaultAsync(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            _context.Filmes.Remove(filme);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
