using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SmartAcademy_ISchool_Assignment.Models
{
    public partial class Student
    {
        public Student()
        {
            StudentsToSubjects = new HashSet<StudentsToSubject>();
        }

        [Key]
        public string Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public virtual ICollection<StudentsToSubject> StudentsToSubjects { get; set; }
    }

    public enum Gender
    {
        Female = 1,
        Male = 2
    }
}
