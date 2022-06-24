using Microsoft.AspNetCore.Mvc;
using RpgApi.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using RpgApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;
using System.Linq;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class DisputasController : ControllerBase
    {
        //Construtores e métodos aqui.

        private readonly DataContext _context;

        public DisputasController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("Arma")]
        public async Task<IActionResult> AtaqueComArmaAsync(Disputa d)
        {
            try
            {
                //Programe aqui por favor.
                Personagem atacante = await _context.Personagens
                .Include(p=> p.Arma)
                .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);

                Personagem oponente = await _context.Personagens
                .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                int dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));

                dano = dano - new Random().Next(oponente.Defesa);

                if(dano > 0)
                    oponente.PontosVida = oponente.PontosVida - (int)dano;
                if(oponente.PontosVida <= 0)
                     d.Narracao = $"{oponente.Nome} foi derrotado!";

                _context.Personagens.Update(oponente);
                await _context.SaveChangesAsync();

                StringBuilder dados = new StringBuilder();
                dados.AppendFormat(" Atacante: {0}. ", atacante.Nome);
                dados.AppendFormat(" Oponente:  {0}. ", oponente.Nome);
                dados.AppendFormat(" Pontos de vida do atacante: {0}. ", atacante.PontosVida);
                dados.AppendFormat(" Pontos de vida do oponente: {0}. ", oponente.PontosVida);
                dados.AppendFormat(" Arma Utilizada: {0}. ", atacante.Arma.Nome);
                dados.AppendFormat(" Dano: {0}. ", dano);

                d.Narracao += dados.ToString();
                d.DataDisputa = DateTime.Now;
                _context.Disputas.Add(d);
                _context.SaveChanges();
                return Ok(d);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Habilidade")]

        public async Task<IActionResult> AtaqueComHabilidadeAsync(Disputa d)
        {
            try
            {
                Personagem atacante = await _context.Personagens
                    .Include(p => p.PersonagemHabilidades).ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);
                
                Personagem oponente = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                PersonagemHabilidade ph = await _context.PersonagemHabilidades
                    .Include(p => p.Habilidade)
                    .FirstOrDefaultAsync(phBusca => phBusca.HabilidadeId == d.HabilidadeId
                    && phBusca.PersonagemId == d.AtacanteId);

                if(ph == null)
                    d.Narracao = $"{atacante.Nome} não possui esta habilidade";
                else
                {
                    int dano = ph.Habilidade.Dano + (new Random().Next(atacante.Inteligencia));
                    dano = dano - new Random().Next(oponente.Defesa);

                    if(dano > 0)
                        oponente.PontosVida = oponente.PontosVida - dano;
                    if(oponente.PontosVida <= 0)
                        d.Narracao += $"{oponente.Nome} foi derrotado";
                    
                    _context.Personagens.Update(oponente);
                    await _context.SaveChangesAsync();

                StringBuilder dados = new StringBuilder();
                dados.AppendFormat(" Atacante: {0}. ", atacante.Nome);
                dados.AppendFormat(" Oponente: {0}. ", oponente.Nome);
                dados.AppendFormat(" Pontos de vida do atacante: {0}. ", atacante.PontosVida);
                dados.AppendFormat(" Pontos de vida do oponente: {0}. ", oponente.PontosVida);
                dados.AppendFormat(" Habilidade utilizada: {0}. ", ph.Habilidade.Nome);
                dados.AppendFormat(" Dano: {0}. ", dano);

                d.Narracao += dados.ToString();
                d.DataDisputa = DateTime.Now;
                _context.Disputas.Add(d);
                _context.SaveChanges();

                }
               return Ok(d); 
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*
         public async Task<IActionResult> AtaqueComHabilidadeAsync(Disputa d)
        {
            try
            {
                Personagem atacante = await _context.Personagens
                    .Include(p => p.PersonagemHabilidades).ThenInclude(ph => ph.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == d.AtacanteId);
                
                Personagem oponente = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == d.OponenteId);

                PersonagemHabilidade ph = await _context.PersonagemHabilidades
                    .Include(p => p.Habilidade)
                    .FirstOrDefaultAsync(phBusca => phBusca.HabilidadeId == d.HabilidadeId
                    && phBusca.PersonagemId == d.AtacanteId);
                    
                if(ph == null)
                    d.Narracao = $"{atacante.Nome} não possui esta habilidade";
                else
                {
                    int dano = ph.Habilidade.Dano + (new Random().Next(atacante.Inteligencia));
                     dano = dano - new Random().Next(oponente.Defesa);
                    if(dano > 0)
                        oponente.PontosVida = oponente.PontosVida - dano;
                    if(oponente.PontosVida <= 0)
                        d.Narracao += $"{oponente.Nome} foi derrotado!";
                        
                        _context.Personagens.Update(oponente);
                        await _context.SaveChangesAsync();
                        
                        StringBuilder dados = new StringBuilder();
                        dados.AppendFormat(" Atacante: {0}. ", atacante.Nome);
                        dados.AppendFormat(" Oponente:  {0}. ", oponente.Nome);
                        dados.AppendFormat(" Pontos de vida do atacante: {0}. ", atacante.PontosVida);
                        dados.AppendFormat(" Pontos de vida do oponente: {0}. ", oponente.PontosVida);
                        dados.AppendFormat(" Arma Utilizada: {0}. ", atacante.Arma.Nome);
                        dados.AppendFormat(" Dano: {0}. ", dano);

                        d.Narracao += dados.ToString();
                        d.DataDisputa = DateTime.Now;
                        _context.Disputas.Add(d);
                        _context.SaveChanges();
                    }
                    
                    return Ok(d);
                }
                catch (System.Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            */
                [HttpGet("PersonagemRandom")]
                public async Task<IActionResult> Sorteio()
                {
                    List<Personagem> personagens =
                        await _context.Personagens.ToListAsync();

                    //Sorteio com numero da quaqntidade de personagens
                    int sorteio = new Random().Next(personagens.Count);

                    //busca na lista pelo indice sorteado (não é ID)
                    Personagem p = personagens[sorteio];

                    string msg =
                        string.Format("Nº Sorteado {0}. Personagem: {1}", sorteio, p.Nome);
                    
                    return Ok(msg);
                }

                [HttpPost("DisputaEmGrupo")]
                public async Task<IActionResult> DisputaEmGrupoAsync(Disputa d)
                {
                    try
                    {
                        //Busca na base dos personagens informados no parametro incluindo Armas e Habilidade
                        List<Personagem> personagens = await _context.Personagens
                            .Include(p => p.Arma)
                            .Include(p => p.PersonagemHabilidades).ThenInclude(ph => ph.Habilidade)
                            .Where(p => d.ListaIdPersonagens.Contains(p.Id)).ToListAsync();
                        
                        //contagem de personagens vivos na lista obtida do banco de dados
                        int qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count;

                        //Enquanto houver mais de um personagem vivo haverá disputa
                        while(qtdPersonagensVivos > 1)
                        {
                            //Seleciona personagem com pontos vida edepois faz sorteio.
                            List<Personagem> atacantes = personagens.Where(p => p.PontosVida > 0).ToList();
                            Personagem atacante = atacantes[new Random().Next(atacantes.Count)];
                            d.AtacanteId = atacante.Id;

                            //Seleciona personagens com pontos vida positivos,exceto o atacante escolhido edepois faz sorteio
                            List<Personagem> oponentes = personagens.Where(p => p.Id != atacante.Id  && p.PontosVida > 0).ToList();
                            Personagem oponente = oponentes[new Random().Next(oponentes.Count)];
                            d.OponenteId = oponente.Id;

                            //Sorteia e redefine a cada passagem do while o valor das variaveis que serão usados.
                            int dano = 0;
                            string ataqueUsado = string.Empty;
                            string resultado = string.Empty;

                            //Sorteia entre 0 e 1 : 0 é um ataque com arma e 1 é um ataque com habilidades
                            bool ataqueUsaArma = (new Random().Next(1) == 0);

                            if (ataqueUsaArma && atacante.Arma != null)
                            {
                                //programacao ataque com arma caso o atacante possua arma (o != null) do if

                                //sorteio da Força
                                dano = atacante.Arma.Dano + (new Random().Next(atacante.Forca));
                                dano = dano - new Random().Next(oponente.Defesa); //sorteio da defesa
                                ataqueUsado = atacante.Arma.Nome;

                                if(dano > 0)
                                    oponente.PontosVida = oponente.PontosVida - (int)dano;
                                
                                //Formata a mensagem
                                resultado =
                                    string.Format("{0} atacou {1} usando {2} com o dano {3}.", atacante.Nome,oponente.Nome,ataqueUsado, dano);
                                
                                d.Narracao += resultado; //concatena o resultado com as narrações existentes.
                                d.Resultados.Add(resultado);//Adiciona o resultadoatual na lista de resultados.
                            }
                            else if (atacante.PersonagemHabilidades.Count != 0)//verifica se o personagem tem habilidades
                            {
                                //Programação do ataque com habilidade

                                //Realiza o sorteio entre as habilidade existentes e na linha seguinte a seleciona.
                                int sorteioHabilidadeId = new Random().Next(atacante.PersonagemHabilidades.Count);
                                Habilidade habilidadeEscolhida = atacante.PersonagemHabilidades[sorteioHabilidadeId].Habilidade;

                                //Sorteio de inteligencia somada ao dano
                                dano = habilidadeEscolhida.Dano + (new Random().Next(atacante.Inteligencia));
                                dano = dano - new Random().Next(oponente.Defesa);//sorteio da defesa

                                if(dano > 0)
                                    oponente.PontosVida = oponente.PontosVida - (int)dano;
                                
                                resultado =
                                    string.Format("{0} atacou {1} usando {2} com o dano {3}.",atacante.Nome, oponente.Nome, ataqueUsado, dano);
                                
                                d.Narracao += resultado;
                                d.Resultados.Add(resultado);
                            }

                            //programcao da verificação do ataque usado e verificacao se existe mais de um personagem vivo
                            if (!string.IsNullOrEmpty(ataqueUsado))
                            {
                                //Incrementa os dados dos combates
                                atacante.Vitorias++;
                                oponente.Derrotas++;
                                atacante.Disputas++;
                                oponente.Disputas++;

                                d.Id = 0;//Zera o Id para poder salvar os dados de disputa sem erro de chave.
                                d.DataDisputa = DateTime.Now;
                                _context.Disputas.Add(d);
                                await _context.SaveChangesAsync();
                            }

                            qtdPersonagensVivos = personagens.FindAll(p => p.PontosVida > 0).Count;

                            if(qtdPersonagensVivos == 1)//Havendo só um personagem vivo,existe um CAMPEAO!
                            {
                                string resultadoFinal =
                                    $"{atacante.Nome.ToUpper()} é CAMPEÃO com {atacante.PontosVida} pontos de vida restantes!";
                                
                                d.Narracao += resultadoFinal;
                                d.Resultados.Add(resultadoFinal);//Concatena o resultado final com os demais resultados.

                                break; //break vai parar o while
                            }
                        }//fim while

                        //Código que irá atualizar os pontos de vida,
                        //disputas,vitorias e derrotas de todos personagens ao fiunal das batalhas
                        _context.Personagens.UpdateRange(personagens);
                        await _context.SaveChangesAsync();

                        return Ok(d); //retorna dados de disputas
                    }
                    catch (System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    } 
                }

                [HttpDelete("ApagarDisputas")]
                public async Task<IActionResult> DeleteAsync()
                {
                    try
                    {
                        List<Disputa> disputas = await _context.Disputas.ToListAsync();

                        _context.Disputas.RemoveRange(disputas);
                        await _context.SaveChangesAsync();

                        return Ok("Disputas Apagadas");
                    }
                    catch(SystemException ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                [HttpGet("Listar")]
                public async Task<IActionResult> ListarAsync()
                {
                    try
                    {
                        List<Disputa> disputas =
                        await _context.Disputas.ToListAsync();

                        return Ok(disputas);
                    }
                    catch (System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                [HttpPut("RestaurarPontosVida")]
                public async Task<IActionResult> RestaurarPontosVidaAsync(Personagem p)
                {
                    try
                    {
                        int linhasAfetadas= 0;
                        Personagem pEncontrado =
                        await _context.Personagens.FirstOrDefaultAsync(pBusca => pBusca.Id == p.Id);
                        pEncontrado.PontosVida = 100;

                         bool atualizou = await TryUpdateModelAsync<Personagem>(pEncontrado, "p",
                            pAtualizar => pAtualizar.PontosVida);
                         //EF vai detectar e atualizar apenas as coluynas que foram alteradas.
                         if(atualizou)
                             linhasAfetadas = await _context.SaveChangesAsync();
                            
                         return Ok(linhasAfetadas);
                     }
                    catch (System.Exception ex)
                    {
                            return BadRequest(ex.Message);
                    }
                }

                //Alterar foto
                [HttpPut("AlterarFoto")]
                public async Task<IActionResult> AtualizarFoto(Personagem p)
                {
                    try
                    {
                        Personagem personagem = await _context.Personagens
                            .FirstOrDefaultAsync(x => x.Id == p.Id);
                        personagem.FotoPersonagem = p.FotoPersonagem;
                        var attach = _context.Attach(personagem);
                        attach.Property(x => x.Id).IsModified = false;
                        attach.Property(x => x.FotoPersonagem).IsModified = true;
                        int linhasAfetadas = await _context.SaveChangesAsync();
                        return Ok(linhasAfetadas);
                    }
                    catch(System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                [HttpPut("ZerarRanking")]
                public async Task<IActionResult> ZerarRankingAsync(Personagem p)
                {
                    try
                    {
                        Personagem pEncontrado =
                            await _context.Personagens.FirstOrDefaultAsync(pBusca => pBusca.Id == p.Id);
                        pEncontrado.Disputas = 0;
                        pEncontrado.Vitorias = 0;
                        pEncontrado.Derrotas = 0;
                        int linhasAfetadas = 0;

                        bool atualizou = await TryUpdateModelAsync<Personagem>(pEncontrado, "p",
                            pAtualizar => pAtualizar.Disputas,
                            pAtualizar => pAtualizar.Vitorias,
                            pAtualizar => pAtualizar.Derrotas);

                        //EF vai detectar e atualizar apenas as colunas que forma alteradas.
                        if(atualizou)
                            linhasAfetadas = await _context.SaveChangesAsync();
                        
                        return Ok(linhasAfetadas);
                    }
                    catch(System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                [HttpPut("ZerarRankingRestaurarVidas")]
                public async Task<IActionResult> ZerarRankingRestaurarVidasAsync()
                {
                    try
                    {
                        List<Personagem> lista = await _context.Personagens.ToListAsync();

                        foreach (Personagem p in lista)
                        {
                            await ZerarRankingAsync(p);
                            await RestaurarPontosVidaAsync(p);
                        }
                        return Ok();
                    }
                    catch (System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                [HttpGet("{usuarioId}")]
                public async Task<IActionResult> GetUsuario(int usuarioId)
                {
                    try
                    {
                        Usuario usuario = await _context.Usuarios //Busca o usuario no banco atraves do iD
                            .FirstOrDefaultAsync(x => x.Id == usuarioId);
                        return Ok(usuario);
                    }
                    catch(System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }

                [HttpGet("GetByLogin/{login}")]
                public async Task<IActionResult> GetUsuario(string login)
                {
                    try
                    {
                         Usuario usuario = await _context.Usuarios //Busca o usuario no banco atraves do login
                            .FirstOrDefaultAsync(x => x.Username.ToLower() == login.ToLower());
                        return Ok(usuario);
                    }
                    catch(System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                //Atualizar geolocalização
                [HttpPut("AtualizarLocalizacao")]
                public async Task<IActionResult> AtualizarLocalizacao(Usuario u)
                {
                    try
                    {
                        Usuario usuario = await _context.Usuarios //Busca o usuario no banco atraves do Id
                            .FirstOrDefaultAsync(x => x.Id == u.Id);
                        
                        usuario.Latitude = u.Latitude;
                        usuario.Longitude = u.Longitude;

                        var attach = _context.Attach(usuario);
                        attach.Property(x => x.Id).IsModified = false;
                        attach.Property(x => x.Latitude).IsModified = true;
                        attach.Property(x => x.Longitude).IsModified = true;

                        int linhasAfetadas = await _context.SaveChangesAsync();//confirma alteração no banco
                        return Ok(linhasAfetadas); //retorna as linhas afetadas
                    }
                    catch (System.Exception ex)
                    {
                        return BadRequest(ex.Message);   
                    }
                }
                

                //Metodo alterar fot usuario
                [HttpPut("AtualizarFoto")]
                public async Task<IActionResult> AtualizarFoto(Usuario u)
                {
                    try
                    {
                        Usuario usuario = await _context.Usuarios
                            .FirstOrDefaultAsync(x => x.Id == u.Id);

                        usuario.FotoPersonagem = u.Foto;

                        var attach = _context.Attach(usuario);
                        attach.Property(x => x.Id).IsModified = false;
                        attach.Property(x => x.Foto).IsModified = true;

                        int linhasAfetadas = await _context.SaveChangesAsync();
                        return Ok(linhasAfetadas); 
                    }
                    catch(System.Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }


               
    }
}
