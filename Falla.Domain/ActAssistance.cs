namespace Falla.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ActAssistance
    {
        [Key]
        public int IdAsistencia { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Acto")]
        [Index("IdAsistencia_IdAct_Index", 1, IsUnique = true)]
        public int IdAct { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Componente")]
        [Index("IdAsistencia_IdFallero_Index", 2, IsUnique = true)]
        public int IdFallero { get; set; }
    }
}
