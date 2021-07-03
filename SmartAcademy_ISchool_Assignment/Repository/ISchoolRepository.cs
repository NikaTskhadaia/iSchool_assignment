using SmartAcademy_ISchool_Assignment.Models;
using System.Collections.Generic;

namespace SmartAcademy_ISchool_Assignment.Repository
{
    public interface ISchoolRepository
    {
        /// <summary>
        /// ამატებს სტუდენტს ბაზაში
        /// </summary>
        /// <param name="student">ბაზაში დასამატებელი სტუდენტი</param>
        /// <returns>სტუდენტის </returns>
        void AddStudent(Student student);

        /// <summary>
        /// წაშლის სტუდენტს ბაზიდან
        /// </summary>
        /// <param name="studentiId">სტუდენტის პირადი ნომერი</param>
        void RemoveStudent(string studentiId);

        /// <summary>
        /// ახალი საგნის დამატება ბაზაში
        /// </summary>
        /// <param name="subject"></param>
        void AddSubject(Subject subject);

        /// <summary>
        /// სტუდენტისთვის საგნის დამატება
        /// </summary>
        /// <param name="subject">საგანი</param>
        /// <param name="studentId">სტუდენტის პირადი ნომერი</param>
        void AddStudentToSubject(Subject subject, string studentId);

        /// <summary>
        /// სტუდენტისთვის ქულის მინიჭება მითითბულ საგანში
        /// </summary>
        /// <param name="studentId">სტუდენტის პირადი ნომერი</param>
        /// <param name="subject">საგანი</param>
        /// <param name="point">ქულა</param>
        void SetStudentPoint(string studentId, Subject subject, int point);

        /// <summary>
        /// მოძებნის ბაზაში სტუდენტის ქულებს
        /// </summary>
        /// <param name="studentId">სტუდენტის პირადი ნომერი</param>
        /// <returns>სტუდენტის ქულები ყველა საგანში</returns>
        Dictionary<Subject, int?> GetStudentPoints(string studentId);

        /// <summary>
        /// მოძებნის ბაზაში სტუდენტის ქულას მითითებულ საგანში
        /// </summary>
        /// <param name="studentId">სტუდენტის პირადი ნომერი</param>
        /// <param name="subject">საგანი</param>
        /// <returns>ქულა შესაბამის საგანში</returns>
        int? GetStudentPoint(string studentId, Subject subject);

        /// <summary>
        /// მოძებნის ბაზაში მოსწავლეების სიას შესაბამისი ქულით ამ საგანში
        /// </summary>
        /// <param name="subject"></param>
        /// <returns>მოსწავლეების სია შესაბამისი ქულით ამ საგანში</returns>
        Dictionary<Student, int?> GetStudentsPoints(Subject subject);

        /// <summary>
        /// ამოიღებს ბაზიდან ყველა საგნის დასახელებას და ამ საგანში ჩაწერილი სტუდენტების სია შესაბამისი ქულებით
        /// </summary>
        /// <returns>ყველა საგნის დასახელება და ამ საგანში ჩაწერილი სტუდენტების სია შესაბამისი ქულებით</returns>
        Dictionary<Subject, Dictionary<Student, int?>> GetStudentsPoints();
    }
}
