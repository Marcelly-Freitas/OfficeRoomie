using System.ComponentModel.DataAnnotations;

namespace OfficeRoomie.DTO;
public class AdministradorDto
{
    public int id { get; set; }

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
    public string nome { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string email { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! {0}")]
    [StringLength(100, ErrorMessage = "Nome não pode ter mais que 100 caracteres")]
    public string senha { get; set; } = "";
    public string[] permissoes { get; set; } = [];
}