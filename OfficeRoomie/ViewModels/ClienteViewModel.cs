using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRoomie.ViewModels;

public class ClienteViewModel
{
    public int id { get; set; }

    public string nome { get; set; } = "";

    public string email { get; set; } = "";

    public string cpf { get; set; } = "";

    public string endereco_logradouro { get; set; } = "";

    public string endereco_numero { get; set; } = "";

    public string endereco_complemento { get; set; } = "";

    public string endereco_cep { get; set; } = "";

    public string endereco_bairro { get; set; } = "";

    public string endereco_cidade { get; set; } = "";

    public string endereco_estado { get; set; } = "";

    public string endereco_pais { get; set; } = "";
}

