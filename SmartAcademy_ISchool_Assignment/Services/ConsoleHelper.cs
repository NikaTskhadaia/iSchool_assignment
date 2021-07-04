using System;
using System.Text.RegularExpressions;
using SmartAcademy_ISchool_Assignment.Models;
using SmartAcademy_ISchool_Assignment.Repository;

namespace SmartAcademy_ISchool_Assignment.Services
{
    class ConsoleHelper
    {
        #region MyRegexes
        private Regex addPersonRegex = new(@"Add\sPerson\s{[a-zA-Z]+\s[a-zA-Z]+},{\d{11}},{\d{1,3}},{[1,2]}");
        private Regex removePersonRegex = new(@"Remove\sPerson\s{\d{11}}");
        private Regex addSubjectRegex = new(@"Add\sSubject\s{[a-zA-Z\s]+}");
        private Regex addPersonToSubjectRegex = new(@"Add\sPerson\sTo\sSubject\s{[a-zA-Z\s]+},{\d{11}}");
        private Regex setPointRegex = new(@"Set\sPoint\s{\d{11}},{[a-zA-Z\s]+},{\d{1,2}}");
        private Regex getPersonPointsByIdRegex = new(@"^Get\sPerson\sPoint\s{\d{11}}$");
        private Regex getPersonSubjectPointRegex = new(@"Get\sPerson\sPoint\s{\d{11}},{[a-zA-Z\d\s]+}");
        private Regex getPersonsPointBySubjectRegex = new(@"Get\sPersons\sPoint\s{[a-zA-Z\d\s]+}");
        private Regex getPersonsPointsRegex = new(@"Get\sPersons\sPoint");
        #endregion

        private readonly ISchoolRepository _repo;

        public ConsoleHelper(ISchoolRepository repository)
        {
            _repo = repository;
        }
        public void EvalConsoleCommand(string s)
        {
            switch (s)
            {
                case string command when addPersonRegex.IsMatch(command):
                    Student student = GetStudent(command);
                    if (_repo.AddStudent(student) == 1) 
                    {
                        Console.WriteLine("Command not executed.");
                    }
                    break;

                case string command when removePersonRegex.IsMatch(command):
                    Console.WriteLine("Are you sure to delete the student?\nY - Yes\nN - No");
                    var answer = Console.ReadKey();
                    string studentId = GetStudentId(command);
                    if (answer.Key == ConsoleKey.Y)
                    {
                        if (_repo.RemoveStudent(studentId) == 1)
                        {
                            Console.WriteLine("The student was not found in DB.");
                        }
                    }
                    break;

                case string command when addSubjectRegex.IsMatch(command):
                    if (_repo.AddSubject(GetSubject(command)) == 1)
                    {
                        Console.WriteLine("Command not executed.");
                    }
                    break;

                case string command when addPersonToSubjectRegex.IsMatch(command):
                    Subject subject = GetSubject(command);
                    studentId = GetStudentId(command);
                    if (_repo.AddStudentToSubject(subject, studentId) == 1)
                    {
                        Console.WriteLine("Command not executed.");
                    }
                    break;

                case string command when setPointRegex.IsMatch(command):
                    studentId = GetStudentId(command);
                    subject = GetSubject(command);
                    int point = GetPoint(command);
                    if (_repo.SetStudentPoint(studentId, subject, point) == 1)
                    {
                        Console.WriteLine("Command not executed.");
                    }
                    break;

                case string command when getPersonSubjectPointRegex.IsMatch(command):
                    studentId = GetStudentId(command);
                    subject = GetSubject(command);
                    int? nullablePoint = _repo.GetStudentPoint(studentId, subject);
                    if (nullablePoint is null)
                    {
                        Console.WriteLine("The student has no point yet.");
                    }
                    else
                    {
                        Console.WriteLine(nullablePoint);
                    }
                    break;

                case string command when getPersonPointsByIdRegex.IsMatch(command):
                    studentId = GetStudentId(command);
                    var collection = _repo.GetStudentPoints(studentId);
                    foreach (var item in collection)
                    {
                        Console.WriteLine($"{item.Key.Name} - {item.Value}");
                    }
                    break;

                case string command when getPersonsPointBySubjectRegex.IsMatch(command):
                    subject = GetSubject(command);
                    var coll = _repo.GetStudentsPoints(subject);
                    foreach (var item in coll)
                    {
                        Console.WriteLine($"{item.Key.FullName} - {item.Value}");
                    }
                    break;

                case string command when getPersonsPointsRegex.IsMatch(command):
                    var db = _repo.GetStudentsPoints();
                    foreach (var item in db)
                    {
                        Console.WriteLine($"\n{item.Key}:");
                        foreach (var subItem in item.Value)
                        {
                            Console.WriteLine($"\t{subItem.Key.FullName} - {subItem.Value}");
                        }
                    }
                    break;

                default:
                    Console.WriteLine("You entered wrong command");
                    break;
            }
        }

        private Student GetStudent(string s)
        {
            s = s.Remove(0, s.IndexOf('{'));

            Student student = new();

            student.FullName = new Regex(@"[a-zA-Z]+\s[a-zA-Z]+").Match(s).Value;
            student.Id = new Regex(@"\d{11}").Match(s).Value;
            student.Age = int.Parse(new Regex(@"{\d{1,3}}").Match(s).Value.Remove(0, 1).Replace("}", ""));
            student.Gender = (Gender)int.Parse(new Regex(@"{[1,2]}").Match(s).Value.Remove(0, 1).Replace("}", ""));
            return student;
        }

        private string GetStudentId(string s)
        {
            Regex studentId = new(@"\d{11}");
            return studentId.Match(s).Value;
        }

        private Subject GetSubject(string s)
        {
            Subject subject = new();
            subject.Name = new Regex(@"{[a-zA-Z\s]+}").Match(s).Value.Remove(0, 1).Replace("}", "");
            return subject;
        }

        private int GetPoint(string s)
        {
            int point = int.Parse(new Regex(@"{\d{1,2}}").Match(s).Value.Remove(0, 1).Replace("}", ""));
            return point;
        }
    }
}
