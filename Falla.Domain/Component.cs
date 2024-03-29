﻿namespace Falla.Domain
{
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public class Component
    {
        [Key]
        public int ComponentId { get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe contener como máximo {1} caracteres.")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe contener como máximo {1} caracteres.")]
        public string LastName { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe contener como máximo {1} caracteres.")]
        [Index("Component_Email_Index", IsUnique = true)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(20, ErrorMessage = "El campo {0} debe contener como máximo {1} caracteres.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }

        [Display(Name = "Foto")]
        [DataType(DataType.ImageUrl)]
        public string Foto { get; set; }

        [DataType(DataType.ImageUrl)]
        public string Foto500 { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public HttpPostedFileBase FotoFile { get; set; }

        [Display(Name = "Foto")]
        public string FotoFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(FotoFullPath))
                {
                    return "No hay Foto";
                }

                return string.Format(
                    "http://api.antoniole.com/{0}",
                    Foto.Substring(1));
            }
        }

        [Display(Name = "Nombre")]
        public string FullName
        {
            get
            {
                return string.Format("{0}, {1}", LastName, FirstName);
            }
        }
    }
}
