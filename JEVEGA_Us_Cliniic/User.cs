//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JEVEGA_Us_Cliniic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        enum userTypes { Admin, Sonographer, Radiologist, Viewer };

        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "*")]
        public string Firstname { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*")]
        public string Lastname { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*invalid E-mail ...")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "*invalid phone no. ...")]
        public string MobileNo { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password, ErrorMessage = "*")]
        public string Password { get; set; }

        [Display(Name = "User Type")]
        [Required(ErrorMessage = "*")]
        public Nullable<short> UserType { get; set; }

        public string GetFullName
        {
            get { return Lastname + ", " + Firstname; }
        }

        public string UserTypeDesc
        {
            get
            {
                string typedesc = "";
                switch (UserType)
                {
                    case 1: typedesc = "Admin"; break;
                    case 2: typedesc = "Radiologist"; break;
                    case 3: typedesc = "Admin"; break;
                    case 4: typedesc = "Viewer"; break;
                };
                return typedesc;
            }
        }
    }
}

