using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{


    // Enumeration      We should make it   Public   to use it
    public enum Grade
    {
        A, B, C, D, F
    }



    // Base model class
    // class -- table
    public class Enrollment
    {
        // properties -- colomns
        public int EnrollmentID { get; set; }
        // FK
        public int CourseID { get; set; }
        // FK
        public int StudentID { get; set; }



        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }    // We use  just 5 grade  so we put  Enumeration



        // Navigation property 
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }


    }
}