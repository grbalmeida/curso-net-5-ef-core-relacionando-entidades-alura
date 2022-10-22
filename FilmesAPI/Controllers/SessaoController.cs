using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SessaoController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionaSessao(CreateSessaoDto sessaoDto)
        {
            var sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Sessoes.Add(sessao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(RecuperaSessaoPorId), new { sessao.Id }, sessao);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> RecuperaSessaoPorId(int id)
        {
            var sessao = await _context.Sessoes.FirstOrDefaultAsync(sessao => sessao.Id == id);

            if (sessao != null)
            {
                var sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
                return Ok(sessaoDto);
            }

            return NotFound();
        }
    }
}
