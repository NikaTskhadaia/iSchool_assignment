using System;
using SmartAcademy_ISchool_Assignment.Models;
using SmartAcademy_ISchool_Assignment.Repository;

namespace SmartAcademy_ISchool_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            ISchoolRepository repo = new SchoolRepository();

            Student student = new() { FullName = "Nika Tskhadaia", Id = "123456", Age = 12, Gender = Gender.Male};

            repo.AddStudent(student);

            Console.ReadKey();
        }
    }
}