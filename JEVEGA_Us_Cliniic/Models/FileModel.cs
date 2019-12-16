using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace JEVEGA_Us_Cliniic.Models
{
    public class FileModel
    {
        [Required(ErrorMessage = "Please select file.")]
        [Display(Name = "Browse File")]
        [Key]
        public HttpPostedFileBase[] files { get; set; }
    }

}