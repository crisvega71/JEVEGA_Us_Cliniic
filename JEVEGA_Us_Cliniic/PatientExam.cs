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

    public partial class PatientExam
    {
        private JEVEGA_USDB_Entities dbUSClinic = new JEVEGA_USDB_Entities();
        public int Id { get; set; }

        [Display(Name = "Patient")]
        [Required(ErrorMessage = "*")]
        public string PatientID { get; set; }

        [Display(Name = "Exam Type")]
        [Required(ErrorMessage = "*")]
        public Nullable<short> ExamType { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> ExamDate { get; set; }

        [Display(Name = "Sonographer")]
        [Required(ErrorMessage = "*")]
        public Nullable<short> Sonographer { get; set; }

        [Display(Name = "Radiologist")]
        [Required(ErrorMessage = "*")]
        public Nullable<short> Radiologist { get; set; }

        [Display(Name = "Medical Report")]
        public string ExamReport { get; set; }

        [Display(Name = "Signed")]
        public string SignByDoctor { get; set; }

        [Display(Name = "Date Signed")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateSigned { get; set; }

        public Nullable<bool> Image1 { get; set; }
        public Nullable<bool> Image2 { get; set; }
        public Nullable<bool> Image3 { get; set; }
        public Nullable<bool> Image4 { get; set; }
        public Nullable<bool> Image5 { get; set; }
        public Nullable<bool> Image6 { get; set; }
        public Nullable<bool> Image7 { get; set; }
        public Nullable<bool> Image8 { get; set; }
        public Nullable<bool> Image9 { get; set; }
        public Nullable<bool> Image10 { get; set; }

        public string getPatientIdName
        {
            get
            {
                string lastName = dbUSClinic.PatientDatas.Find(PatientID).Lastname.ToString();
                string firstName = dbUSClinic.PatientDatas.Find(PatientID).Firstname.ToString();
                string patiendIdName = PatientID + " - " + lastName + ", " + firstName;
                return patiendIdName;
            }
        } //**

        public string getDoctorName
        {
            get
            {
                string lastName = dbUSClinic.RadiologistDoctors.Find(Radiologist).LastName.ToString();
                string firstName = dbUSClinic.RadiologistDoctors.Find(Radiologist).FirstName.ToString();
                return lastName + ", " + firstName;
            }
        }

        public string getSonographerName
        {
            get
            {
                string lastName = dbUSClinic.Sonographers.Find(Radiologist).LastName.ToString();
                string firstName = dbUSClinic.Sonographers.Find(Radiologist).FirstName.ToString();
                return lastName + ", " + firstName;
            }
        }

        public string getExamName
        {
            get
            {
                string examtype_name = dbUSClinic.DiagnosticExams.Find(ExamType).GetfullExamName;
                return examtype_name;
            }
        }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime? formattedExamDate
        {
            get { return ExamDate; }
        }

        public string getSignDefinition
        {
            get
            {
                return (SignByDoctor == "Y" ? "Yes" : "No");
            }
        }
    }
}
