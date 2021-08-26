using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using System;
using AutoMapper;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route ("[controller]")]
    public class FilmeController : ControllerBase
    {
       private FilmeContext _context;
       private IMapper _mapper;

       public FilmeController(FilmeContext context, IMapper mapper)
       {
           _context = context;
           _mapper = mapper;
       }

         /*private static List <Filme> filmes = new List<Filme>();
        private static int Id = 1; //id para incrementação
          Depois da conexão não será mais necessário, porque o banco faz isso pra gente */  

        [HttpPost] //Criar
         public IActionResult AdicionaFilme([FromBody] CreateFilmeDto filmeDto)
        {
           Filme filme = _mapper.Map<Filme>(filmeDto); //conversão com mapeamento

        /*  Filme filme = new Filme 
           {
              Titulo = filmeDto.Titulo,
              Genero = filmeDto.Genero,
              Diretor = filmeDto.Diretor,
              Duracao = filmeDto.Duracao
           };*/ //Criação de um objeto com um construtor implicíto 

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
          {
             ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme); //conversão com mapeamento


              /*   ReadFilmeDto filmeDto = new ReadFilmeDto
               {
                  Id = filme.Id,
                  Titulo = filme.Titulo,
                  Genero = filme.Genero,
                  Diretor = filme.Diretor,
                  Duracao = filme.Duracao,
                  HoraDaConsulta = DateTime.Now  
               }; */ //estamos pegando do filme e colocando no dto 
               //colocando uma informação extra

               return Ok(filmeDto);
            }
            return NotFound();
         }
         [HttpPut("{id}")]
            public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
               {
                  Filme filme = _context.Filmes.FirstOrDefault (filme => filme.Id == id);
                  if(filme == null)
                  return NotFound();

                  _mapper.Map(filmeDto, filme);

                 /* filme.Titulo = filmeDto.Titulo;
                  filme.Genero = filmeDto.Genero;
                  filme.Diretor = filmeDto.Diretor;
                  filme.Duracao = filmeDto.Duracao;*/ //essa parte atualiza campo a campo o filme 
                  _context.SaveChanges();
                  return NoContent(); 

               }
               
               [HttpDelete("{id}")]
               public IActionResult DeletaFilmes(int id)
               {
                  Filme filme = _context.Filmes.FirstOrDefault (filme => filme.Id == id);
                  if(filme == null)
                  return NotFound();
                  _context.Remove(filme);
                  _context.SaveChanges();
                  return NoContent();
               }
         
    }
}