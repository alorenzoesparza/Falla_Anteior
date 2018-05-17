namespace Falla.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserType
    {
        [Key]
        public int UserTypeID { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener como máximo {1} caracteres.")]
        [Index("UserType_Name_Index", IsUnique = true)]
        public string Name { get; set; }
    }
}
