using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ContosoUniversity.Models
{
    // Base model class
    // class -- table
    public class Student
    {
        // properties -- colomns
        public int ID { get; set; }


        [Required(ErrorMessage = "This is Required")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name ="Last Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string LastName { get; set; }


        [Required]
        [StringLength(50 ,ErrorMessage ="First Name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name ="First Name")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string FirstMidName { get; set; }


        [DataType(DataType.Date)]
        [Display(Name ="Enrollment Date")]
        public DateTime EnrollmentDate { get; set; }



        public string ImagePath { get; set; }




        [Display(Name ="Full Name")]
        public string FullName { get { return LastName + " " + FirstMidName; } }



        // Navigation property 
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}