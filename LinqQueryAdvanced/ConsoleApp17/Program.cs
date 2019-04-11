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
            mapClassToListStudents.Keys.ToList().ForEach(c => c.Age_Average = mapClassToListStudents[c].Where(s => s.Vip == "yes").Count());
            
            var result = from oneClass in mapClassToListStudents.Keys
                         select new { Class = oneClass , VIPCount =  (from oneStudent in mapClassToListStudents[oneClass]
                                                                        where oneStudent.Vip == "yes"
                                                                        select oneStudent.Id).Count()};
            return result;


        }

        static object GetAverageAgePerClass(Dictionary<ClassRoom, List<Student>> mapClassToListStudents)
        {
            mapClassToListStudents.Keys.ToList().ForEach(c => c.Age_Average = (long)mapClassToListStudents[c].Select(s => s.Age).Average());

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
            mapClassToListStudents.Keys.ToList().ForEach(c => c.Youngest_Vip = mapClassToListStudents[c].ToList().Where(s2 => s2.Vip == "yes").Select(s3 => s3.Age).Min());

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
            mapClassToListStudents.Keys.ToList().ForEach(c => c.Most_Popular_City = (mapClassToListStudents[c].GroupBy(s => s.Address_City).ToList().
                Where(g => g.Count() == mapClassToListStudents[c].GroupBy(s => s.Address_City).ToList().Select(g2 => g2.Count()).Max()).Select(g => g.Key).FirstOrDefault()));

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
