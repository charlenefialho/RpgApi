
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace RpgApi.Controllers
{
    [Authorize(Roles = "Jogador, Admin")]
    [ApiController]
    [Route("[Controller]")]
    public class PersonagensController : ControllerBase
    {
        //Programação de toda a classe ficará aqui abaixo
        private readonly DataContext _context;

        private readonly IHttpContextAccessor _httpContextoAccessor;

        public PersonagensController(DataContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
             _httpContextoAccessor = httpContextAccessor;
        }
        [HttpGet("GetByPerfil")]
        public async Task<IActionResult> GetByPerfilAsync()
        {
            try
            {
                List<Personagem> lista = new List<Personagem>();
                
                if(ObterPerfilUsuario() == "Admin")
                    lista = await _context.Personagens.ToListAsync();
                else
                {
                    lista = await _context.Personagens
                            .Where(p => p.Usuario.Id == ObterUsuarioId()).ToListAsync();
                }
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private string ObterPerfilUsuario()
        {
            return _httpContextoAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
        }



        [HttpGet("GetByUser")]
        public async Task<IActionResult> GetByUserAsync()
        {
            try
            {
                int id = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                List<Personagem> lista = await _context.Personagens
                .Where(u => u.Usuario.Id == id).ToListAsync();

                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                Personagem p = await _context.Personagens
                    .Include(ar => ar.Arma)
                    .Include(us => us.Usuario)
                    .Include(ph => ph.PersonagemHabilidades)
                        .ThenInclude(h => h.Habilidade)                    
                    .FirstOrDefaultAsync(pBusca => pBusca.Id == id);

                return Ok(p);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Identação: SHTIFT + ALT + F

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Personagem> lista = await _context.Personagens.ToListAsync();
                return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(Personagem novoPersonagem)
        {
            try
            {
                if (novoPersonagem.PontosVida > 100)
                {
                    throw new System.Exception("Pontos de vida não pode ser maior que 100");
                }                

                await _context.Personagens.AddAsync(novoPersonagem);
                await _context.SaveChangesAsync();

                return Ok(novoPersonagem.Id);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Personagem novoPersonagem)
        {
            try
            {
                if (novoPersonagem.PontosVida > 100)
                {
                    throw new System.Exception("Pontos de vida não pode ser maior que 100");
                }                

                _context.Personagens.Update(novoPersonagem);
                int linhaAfetadas = await _context.SaveChangesAsync();

                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                 Personagem pRemover = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == id);

                _context.Personagens.Remove(pRemover);
                int linhaAfetadas = await _context.SaveChangesAsync();

                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);                
            }
        }


          private int ObterUsuarioId()
            {
                return int.Parse(_httpContextoAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));            
            }






    }//Fim da classe do tipo controller
}




