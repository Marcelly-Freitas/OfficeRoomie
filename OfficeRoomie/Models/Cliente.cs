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
        [Display(Name = "Nome")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'email' Obrigatório!")]
        [Display(Name = "E-mail")]
        public string email { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'cpf' Obrigatório!")]
        [Display(Name = "CPF")]
        public string cpf { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_logradouro' Obrigatório!")]
        [Display(Name = "Logradouro")]
        public string endereco_logradouro { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_numero' Obrigatório!")]
        [Display(Name = "Número")]
        public string endereco_numero { get; set; }

        [Display(Name ="Complemento")]
        public string? endereco_complemento { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_cep' Obrigatório!")]
        [Display(Name = "CEP")]
        public string endereco_cep { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_bairro' Obrigatório!")]
        [Display(Name = "Bairro")]
        public string endereco_bairro { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_cidade' Obrigatório!")]
        [Display(Name = "Cidade")]
        public string endereco_cidade { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_estado' Obrigatório!")]
        [Display(Name = "Estado")]
        public string endereco_estado { get; set; }

        [Required(ErrorMessage = "Preenchimento do Campo 'endereco_pais' Obrigatório!")]
        [Display(Name = "País")]
        public string endereco_pais { get; set; }

        public string? created_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

        public string? updated_at { get; set; } = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    }
}
