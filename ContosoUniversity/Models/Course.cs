using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// To use Mapping
using System.ComponentModel.DataAnnotations.Schema;
// To use decoration
using System.ComponentModel.DataAnnotations;


namespace ContosoUniversity.Models
{
    public enum Level : byte
    {
        Intermediate = 1, Middle = 2 , Advancede = 3
    }


    public enum Rating : byte
    {
        Bad = 1 , Accepted =2 , Good =3 , Very_Good =4 ,Excellent =5
    }



    // Base model class
    // class -- table
    public class Course
    {
        // properties -- colomns


        // Attributes always put it   Up   the property or class  to effect on it
        [DatabaseGenerated(DatabaseGeneratedOption.None)]  // None   To do Mapping to make th ID not identity    to write it by my hand
        [Display(Name = "Number")]
        public int CourseID { get; set; }


        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }


        [Range(0, 5)]
        public int Credits { get; set; }


        public Level Level { get; set; }

        
        public Rating Rating { get; set; }



        // FK
        public int DepartmentID { get; set; }



        // navigation property 
        public virtual ICollection<Enrollment> Enrollments { get; set; }



        public virtual ICollection<Instructor> Instructors { get; set; }
        public virtual Department Department { get; set; }

    }
}