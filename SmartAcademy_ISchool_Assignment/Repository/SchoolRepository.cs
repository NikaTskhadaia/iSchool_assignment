using SmartAcademy_ISchool_Assignment.Data;
using SmartAcademy_ISchool_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartAcademy_ISchool_Assignment.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private SchoolManagementContext _db;

        public SchoolRepository()
        {
            _db = new();
        }

        public void AddStudent(Student student)
        {
            _db.Students.Add(student);
            _db.SaveChanges();
        }

        public void RemoveStudent(string studentId)
        {
            Student student = _db.Students.Where(s => s.Id == studentId).FirstOrDefault();
            if (student is not null)
            {
                _db.Students.Remove(student);
                _db.SaveChangesAsync();
            }
        }

        public void AddSubject(Subject subject)
        {
            _db.Subjects.Add(subject);
            _db.SaveChangesAsync();
        }

        public void AddStudentToSubject(Subject subject, string studentId)
        {
            Student student = _db.Students.Find(studentId);
            if (student is not null && _db.Subjects.Contains(subject))
            {
                StudentsToSubject studentsToSubject = new() { Student = student, Subject = subject, Point = default };
                _db.StudentsToSubjects.Add(studentsToSubject);
                _db.SaveChangesAsync();
            }
        }

        public void SetStudentPoint(string studentId, Subject subject, int point)
        {
            var record = _db.StudentsToSubjects.Where(q => q.StudentId == studentId && q.SubjectName == subject.Name).FirstOrDefault();
            if (record is not null)
            {
                record.Point = point;
                _db.SaveChangesAsync();
            }
        }

        public int? GetStudentPoint(string studentId, Subject subject)
        {
            var record = _db.StudentsToSubjects.Where(q => q.StudentId == studentId && q.SubjectName == subject.Name).FirstOrDefault();
            return record?.Point;
        }

        public Dictionary<Subject, int?> GetStudentPoints(string studentId)
        {
            var result = _db.StudentsToSubjects.Where(q => q.StudentId == studentId);
            Dictionary<Subject, int?> resultCollection = new();
            foreach (var item in result)
            {
                resultCollection.Add(item.Subject, item.Point);
            }
            return resultCollection;
        }

        public Dictionary<Student, int?> GetStudentsPoints(Subject subject)
        {
            var result = _db.StudentsToSubjects.Where(q => q.SubjectName == subject.Name);
            Dictionary<Student, int?> resultCollection = new();
            foreach (var item in result)
            {
                resultCollection.Add(item.Student, item.Point);
            }
            return resultCollection;
        }

        public Dictionary<Subject, Dictionary<Student, int?>> GetStudentsPoints()
        {
            Dictionary<Subject, Dictionary<Student, int?>> finalresultCollection = new();

            var result = _db.StudentsToSubjects.GroupBy(q => q.Subject);

            foreach (var item in result)
            {
                Dictionary<Student, int?> studentPoint = new();

                foreach (var record in item)
                {
                    studentPoint.Add(record.Student, record.Point);
                }

                finalresultCollection.Add(item.Key, studentPoint);
            }

            return finalresultCollection;
        }
    }
}
