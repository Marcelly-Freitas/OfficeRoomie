using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRoomie.Models
{
    [Table("administradores")]
    public class Administrador
    {
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'nome' Obrigatório!")]
        public string nome { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'email' Obrigatório!")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Formato de email inválido!")]
        public string email { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'cpf' Obrigatório!")]
        public string cpf { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'senha' Obrigatório!")]
        public string senha { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'permissoes' Obrigatório!")]
        public string[] permissoes { get; set; } = [];

        public string created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

        public string updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    }
}
