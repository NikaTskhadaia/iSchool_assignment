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
            _people.Add(name, default);
            Console.WriteLine($"{name} was added to the school.");
        }

        public void GetPersonPoint(string name)
        {
            try
            {
                Console.WriteLine($"{name} has {_people[name]} point(s)");
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"There is no one with the name \"{name}\" in the school.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Something went wrong {ex.GetType()}. Please try again.");
            }
        }

        public void RemovePerson(string name)
        {
            _people.Remove(name);
            Console.WriteLine($"{name} was removed from the school.");
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
