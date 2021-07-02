using System;
using System.Collections.Generic;

namespace SmartAcademy_ISchool_Assignment
{
    public class School : ISchool
    {
        private Dictionary<string, int> _people;

        public School()
        {
            _people = new(StringComparer.OrdinalIgnoreCase);
        }

        public void AddPerson(string name)
        {
            if (string.IsNullOrEmpty(name.Trim()))
            {
                Console.WriteLine("Name should not be empty");
                return;
            }
            _people.Add(name, default);
            Console.WriteLine($"{name} was added to the school.");
        }

        public void GetPersonPoint(string name)
        {
            if (_people.ContainsKey(name))
            {
                Console.WriteLine($"{name} has {_people[name]} point(s)");
                return;
            }
            Console.WriteLine($"There is no one with the name \"{name}\" in the school.");
        }

        public void RemovePerson(string name)
        {
            if (_people.ContainsKey(name))
            {
                _people.Remove(name);
                Console.WriteLine($"{name} was removed from the school.");
                return;
            }
            Console.WriteLine($"There is no one with the name \"{name}\" in the school.");
        }

        public void SetPersonPoint(string name, int point)
        {
            if (_people.ContainsKey(name))
            {
                _people[name] = point;
                return;
            }
            Console.WriteLine($"There is no one with the name \"{name}\" in the school.");
        }
    }
}
