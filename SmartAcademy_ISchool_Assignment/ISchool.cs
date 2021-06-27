using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAcademy_ISchool_Assignment
{
    public interface ISchool
    {
        void AddPerson(string name);

        void SetPersonPoint(string name, int point);

        void RemovePerson(string name);

        void GetPersonPoint(string name);
    }
}
