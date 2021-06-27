using System;

namespace SmartAcademy_ISchool_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            School school = new();
            school.AddPerson("Nika");
            school.SetPersonPoint("Nika", 10);

            school.GetPersonPoint("nika");

            school.RemovePerson("Nika");

            school.GetPersonPoint("nika");
            school.SetPersonPoint("Nika", 8);

            Console.ReadKey();
        }
    }
}
