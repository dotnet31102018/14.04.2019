using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class Program
    {
        static object GetVIPPerClass(Dictionary<ClassRoom, List<Student>> mapClassToListStudents)
        {

            var result = from oneClass in mapClassToListStudents.Keys
                         select new { Class = oneClass , VIPCount =  (from oneStudent in mapClassToListStudents[oneClass]
                                                                        where oneStudent.Vip == "yes"
                                                                        select oneStudent.Id).Count()};
            return result;
        }

        static object GetAverageAgePerClass(Dictionary<ClassRoom, List<Student>> mapClassToListStudents)
        {

            var result = from oneClass in mapClassToListStudents.Keys
                         select new
                         {
                             Class = oneClass,
                             AverageAge = (from oneStudent in mapClassToListStudents[oneClass]
                                         select oneStudent.Age).Average()
                         };
            return result;
        }

        static object GetVIPYoungestPerClass(Dictionary<ClassRoom, List<Student>> mapClassToListStudents)
        {

            var result = from oneClass in mapClassToListStudents.Keys
                         select new
                         {
                             Class = oneClass,
                             VIPCount = (from oneStudent in mapClassToListStudents[oneClass]
                                         where oneStudent.Vip == "yes"
                                         select oneStudent.Age).Min()
                         };
            return result;
        }

        static object GetMostPopularCityPerClass(Dictionary<ClassRoom, List<Student>> mapClassToListStudents)
        {

            var result = from oneClass in mapClassToListStudents.Keys
                         select new
                         {
                             Class_ID = oneClass.Id,
                             Max = (from counter in
                                                 (from oneStudent in mapClassToListStudents[oneClass]
                                                  group oneStudent by oneStudent.Address_City into cities
                                                  select new { cities.ToList()[0].Address_City, cities.ToList().Count })
                                    where counter.Count == ((from oneStudent in mapClassToListStudents[oneClass]
                                                             group oneStudent by oneStudent.Address_City into cities
                                                             select cities.ToList().Count).Max())
                                    select counter.Address_City).FirstOrDefault()
                         };
            return result;
        }

        static void Main(string[] args)
        {
            ISchoolDAO school = new SchoolDAO();
            Dictionary<ClassRoom, List<Student>> mapClassToListStudents = school.GetMapClassToStudentsDictionary(1);
            List<Student> students1 = school.GetStudentsFromClass(1);
            List<Student> students2 = school.GetStudentsFromClass(2);

            GetVIPPerClass(mapClassToListStudents);
            GetAverageAgePerClass(mapClassToListStudents);
            GetVIPYoungestPerClass(mapClassToListStudents);
            GetMostPopularCityPerClass(mapClassToListStudents);

            Console.WriteLine(school);

        }
    }
}
