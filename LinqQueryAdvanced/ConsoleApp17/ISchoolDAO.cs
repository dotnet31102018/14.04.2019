using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    interface ISchoolDAO
    {
        Dictionary<ClassRoom, List<Student>> GetMapClassToStudentsDictionary(Int64 n);
        List<Student> GetStudentsFromClass(Int64 id);
    }
}
