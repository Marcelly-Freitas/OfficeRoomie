using System.ComponentModel.DataAnnotations;

namespace OfficeRoomie.DTO;
public class ClienteDto
{
    public int id { get; init; }

    [Required(ErrorMessage = "Campo obrigatório! [nome]")]
    [StringLength(100, ErrorMessage = "O campo nome não pode ter mais que 100 caracteres")]
    public string nome { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [email]")]
    [StringLength(100, ErrorMessage = "O campo email não pode ter mais que 100 caracteres")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    public string email { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [cpf]")]
    [StringLength(100, ErrorMessage = "O campo cpf não pode ter mais que 100 caracteres")]
    public string cpf { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [endereco]")]
    [StringLength(100, ErrorMessage = "O campo endereco não pode ter mais que 100 caracteres")]
    public string endereco_logradouro { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [numero]")]
    [StringLength(100, ErrorMessage = "O campo numero não pode ter mais que 100 caracteres")]
    public string endereco_numero { get; set; } = "";

    public string endereco_complemento { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [cep]")]
    [StringLength(100, ErrorMessage = "O campo cep não pode ter mais que 100 caracteres")]
    public string endereco_cep { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [bairro]")]
    [StringLength(100, ErrorMessage = "O campo bairro não pode ter mais que 100 caracteres")]
    public string endereco_bairro { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [cidade]")]
    [StringLength(100, ErrorMessage = "O campo cidade não pode ter mais que 100 caracteres")]
    public string endereco_cidade { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [estado]")]
    [StringLength(100, ErrorMessage = "O campo estado não pode ter mais que 100 caracteres")]
    public string endereco_estado { get; set; } = "";

    [Required(ErrorMessage = "Campo obrigatório! [pais]")]
    [StringLength(100, ErrorMessage = "O campo pais não pode ter mais que 100 caracteres")]
    public string endereco_pais { get; set; } = "";
}