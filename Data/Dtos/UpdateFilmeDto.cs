using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateFilmeDto
    {
        [Required (ErrorMessage = "O campo título é obrigatório!")]
        public string Titulo {get; set;}
        [Required (ErrorMessage = "O campo diretor é obrigatório!")]
        public string Diretor {get; set;}
        [StringLength (15, ErrorMessage ="Limite de 15 caracteres!")]
        public string Genero {get; set;}
        [Range(1, 400, ErrorMessage ="A duração deve ter no mínimo 1 e no máximo 400 minutos!")]
        public int Duracao {get; set;}
    }
}