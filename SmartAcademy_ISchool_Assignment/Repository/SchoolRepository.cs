using SmartAcademy_ISchool_Assignment.Data;
using SmartAcademy_ISchool_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SmartAcademy_ISchool_Assignment.Repository
{
    public class SchoolRepository : ISchoolRepository
    {
        private SchoolManagementContext _db;

        public SchoolRepository()
        {
            _db = new();
        }

        public int AddStudent(Student student)
        {
            try
            {
                _db.Students.Add(student);
                _db.SaveChanges();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
        }

        public int RemoveStudent(string studentId)
        {
            Student student = _db.Students.Where(s => s.Id == studentId).FirstOrDefault();
            if (student is not null)
            {
                _db.Students.Remove(student);
                _db.SaveChanges();
                return 0;
            }
            return 1;
        }

        public int AddSubject(Subject subject)
        {
            try
            {
                _db.Subjects.Add(subject);
                _db.SaveChanges();
                return 0;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public int AddStudentToSubject(Subject subject, string studentId)
        {
            Student student = _db.Students.Find(studentId);
            if (student is not null && _db.Subjects.Contains(subject))
            {
                try
                {
                    StudentsToSubject studentsToSubject = new() { StudentId = studentId, SubjectName = subject.Name, Point = default };
                    _db.StudentsToSubjects.Add(studentsToSubject);
                    _db.SaveChanges();
                    return 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
            return 1;
        }

        public int SetStudentPoint(string studentId, Subject subject, int point)
        {
            var record = _db.StudentsToSubjects.Where(q => q.StudentId == studentId && q.SubjectName == subject.Name).FirstOrDefault();
            if (record is not null)
            {
                try
                {
                    record.Point = point;
                    _db.SaveChanges();
                    return 0;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
            return 1;
        }

        public int? GetStudentPoint(string studentId, Subject subject)
        {
            var record = _db.StudentsToSubjects.Where(q => q.StudentId == studentId && q.SubjectName == subject.Name).FirstOrDefault();
            return record?.Point;
        }

        public Dictionary<Subject, int?> GetStudentPoints(string studentId)
        {
            var result = _db.StudentsToSubjects.Where(q => q.StudentId == studentId).Include(q => q.Subject).ToDictionary(q => q.Subject, q => q.Point);
            return result;
        }

        public Dictionary<Student, int?> GetStudentsPoints(Subject subject)
        {
            var result = _db.StudentsToSubjects.Where(q => q.SubjectName == subject.Name).Include(q => q.Student).ToDictionary(q => q.Student, q => q.Point);
            return result;
        }

        public Dictionary<string, Dictionary<Student, int?>> GetStudentsPoints()
        {
            Dictionary<string, Dictionary<Student, int?>> finalresultCollection = new();

            var result = _db.StudentsToSubjects.Include(q => q.Student).AsEnumerable().GroupBy(q => q.SubjectName);  

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
