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

    public partial class PatientData
    {
        private JEVEGA_UsDbEntities dbUSClinic = new JEVEGA_UsDbEntities();
        public int Id { get; set; }

        [Display(Name = "Patient ID")]
        [Required(ErrorMessage = "*")]
        public string Patient_Id { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "*")]
        public string Lastname { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "*")]
        public string Firstname { get; set; }

        [Display(Name = "Age")]
        [Required(ErrorMessage = "*")]
        [Range(7, 80, ErrorMessage = "*age must be 7 to 80")]
        public Nullable<short> Age { get; set; }

        [Display(Name = "Sex")]
        [Required(ErrorMessage = "*")]
        public string Sex { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "*")]
        public string Address { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "*invalid E-mail ...")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "*invalid phone no. ...")]
        [Display(Name = "Mobile #")]
        public string Phone { get; set; }

        public string LoginPassword { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "*")]
        public string Status { get; set; }

        [Display(Name = "LMP")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> LMP { get; set; }

        public string GetIDFullname
        {
            get
            { return Patient_Id + " - " + Lastname + ", " + Firstname; }
        }

        public string GetFullname
        {
            get
            { return Lastname + ", " + Firstname; }
        }

        public string getStatusDesc
        {
            get
            {
                string status_desc = "";
                switch (Status)
                {
                    case "S":
                        status_desc = "Single"; break;
                    case "M":
                        status_desc = "Married"; break;
                }
                return status_desc;
            }
        } //--

    }
}
