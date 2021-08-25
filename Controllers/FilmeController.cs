using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI.Data;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class FilmeController : ControllerBase
    {
       private FilmeContext _context;

       public FilmeController(FilmeContext context)
       {
           _context = context;
       }

         /*private static List <Filme> filmes = new List<Filme>();
        private static int Id = 1; //id para incrementação
          Depois da conexão não será mais necessário, porque o banco faz isso pra gente */  

        [HttpPost] //Criar
         public IActionResult AdicionaFilme([FromBody]Filme filme)
        {
           _context.Filmes.Add(filme);
           _context.SaveChanges();
          /* filme.Id = Id++; //incrementação do id para diferenciar os registros 
           filmes.Add(filme); Não será necessário depois da conexão*/
           return CreatedAtAction(nameof(RecuperaFilmesPorId), new {Id = filme.Id}, filme); //mostra a location
        }

        [HttpGet] //Retornar
        public IEnumerable<Filme> RecuperaFilme()
        {
            /* return Ok(filmes); Não será necessário depois da conexão*/
            return _context.Filmes;
        }
        
         [HttpGet("{id}")] //com parâetros de chamada 
         public IActionResult RecuperaFilmesPorId(int id) 
         {
            /* Filme filme = filmes.FirstOrDefault (filme => filme.Id == id); Não será necessário depois da conexão*/
            Filme filme = _context.Filmes.FirstOrDefault (filme => filme.Id == id); //linha com o _context inserido
            if(filme != null)
               return Ok(filme);
            return NotFound();
         }
         [HttpPut("{id}")]
            public IActionResult AtualizaFilme(int id, [FromBody] Filme filmeNovo)
               {
                  Filme filme = _context.Filmes.FirstOrDefault (filme => filme.Id == id);
                  if(filme == null)
                  return NotFound();

                  filme.Titulo = filmeNovo.Titulo;
                  filme.Genero = filmeNovo.Genero;
                  filme.Diretor = filmeNovo.Diretor;
                  filme.Duracao = filmeNovo.Duracao; //essa parte atualiza campo a campo o filme 
                  _context.SaveChanges();
                  return NoContent();

               }
         
    }
}