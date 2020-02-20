
namespace JEVEGA_Us_Cliniic.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ExamVideo")]
    public partial class ExamVideo
    {
        private JEVEGA_UsDbEntities dbUSClinic = new JEVEGA_UsDbEntities();
        public int Id { get; set; }

        [Display(Name = "Patient")]
        public Nullable<int> Patient_IdKey { get; set; }

        [Display(Name = "Exam Id")]
        [Required(ErrorMessage = "*")]
        public string Patient_ExamId { get; set; }

        [Display(Name = "Video Title")]
        [Required(ErrorMessage = "*")]
        public string VideoName { get; set; }

        [Display(Name = "File Name")]
        public string VideoFilename { get; set; }

        [Display(Name = "Upload Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateUpload { get; set; }

        public string getPatientName {

            get {
                string patientName = "";
                PatientData patientData = dbUSClinic.PatientDatas.Find(Patient_IdKey);
                patientName = patientData.Lastname.ToString() + ", " + patientData.Firstname;

                return patientName;
            }
        } //-- 

        public string getPatientEmail {
            get {
                string patientEmail = "";
                PatientData patientData = dbUSClinic.PatientDatas.Find(Patient_IdKey);
                patientEmail = patientData.Email.ToString();

                return patientEmail;
            }
        } //-- 

    }
}