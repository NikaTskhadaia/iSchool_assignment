using System;
using System.Collections.Generic;

#nullable disable

namespace SmartAcademy_ISchool_Assignment.Models
{
    public partial class StudentsToSubject
    {
        public string StudentId { get; set; }
        public string SubjectName { get; set; }
        public int? Point { get; set; }

        public virtual Student Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
