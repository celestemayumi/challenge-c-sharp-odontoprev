using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace challenge_c_sharp.Dtos
{
    public class EstadoDto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sigla { get; set; }
    }

}
