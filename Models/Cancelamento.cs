using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeRoomie.Models
{
    [Table("Cancelamentos")] 

    public class Cancelamento
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Obrigatório informar o nome")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatório informar a data")]
        public string Data { get; set; }

        [Required(ErrorMessage = "Obrigatório informar o horário")]
        public string Horario { get; set; }


    }
}
