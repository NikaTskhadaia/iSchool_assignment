using SmartAcademy_ISchool_Assignment.Models;
using SmartAcademy_ISchool_Assignment.Repository;
using SmartAcademy_ISchool_Assignment.Services;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartAcademy_ISchool_Assignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press 'Esc' button to exit the program.");

            ConsoleHelper consoleHelper = new(new SchoolRepository());


            while(true)
            {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Escape)
                {
                    break;
                }

                string command = key.ToString() + Console.ReadLine();
                consoleHelper.EvalConsoleCommand(command);
                Console.WriteLine("\nDone!");
            }
            


        }

        
    }
}