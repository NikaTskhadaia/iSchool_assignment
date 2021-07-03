using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace SmartAcademy_ISchool_Assignment.Models
{
    public partial class Subject
    {
        public Subject()
        {
            StudentsToSubjects = new HashSet<StudentsToSubject>();
        }

        [Key]
        public string Name { get; set; }

        public virtual ICollection<StudentsToSubject> StudentsToSubjects { get; set; }
    }
}
