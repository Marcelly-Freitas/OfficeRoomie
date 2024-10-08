using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRoomie.Models
{
    [Table("clientes")]
    public class Cliente
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

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_logradouro' Obrigatório!")]
        public string endereco_logradouro { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_numero' Obrigatório!")]
        public string endereco_numero { get; set; } = "";

        public string endereco_complemento { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_cep' Obrigatório!")]
        public string endereco_cep { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_bairro' Obrigatório!")]
        public string endereco_bairro { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_cidade' Obrigatório!")]
        public string endereco_cidade { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_estado' Obrigatório!")]
        public string endereco_estado { get; set; } = "";

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_pais' Obrigatório!")]
        public string endereco_pais { get; set; } = "";

        public string created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

        public string updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    }
}
