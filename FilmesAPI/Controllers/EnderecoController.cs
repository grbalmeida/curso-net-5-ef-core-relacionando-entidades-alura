using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EnderecoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaEndereco([FromBody] CreateEnderecoDto enderecoDto)
        {
            var endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(RecuperaEnderecoPorId), new { endereco.Id }, endereco);
        }

        [HttpGet]
        public async Task<IEnumerable<Endereco>> RecuperaEnderecos()
        {
            return await _context.Enderecos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaEnderecoPorId(int id)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(endereco => endereco.Id == id);
        
            if (endereco != null)
            {
                var enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
                return Ok(enderecoDto);
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizaEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var endereco = await _context.Enderecos.FirstOrDefaultAsync(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return NotFound();
            }

            _mapper.Map(enderecoDto, endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletaEndereco(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefaultAsync(endereco => endereco.Id == id);

            if (endereco == null)
            {
                return NotFound();
            }

            _context.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
