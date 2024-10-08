namespace OfficeRoomie.ViewModels;

public class AdministradorViewModel
{
    public int id { get; init; }
    public string nome { get; set; } = "";
    public string email { get; set; } = "";
    public string[] permissoes { get; set; } = [];
}