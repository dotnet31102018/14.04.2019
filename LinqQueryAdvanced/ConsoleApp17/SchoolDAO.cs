using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17
{
    class SchoolDAO : ISchoolDAO
    {

        static SQLiteConnection connection;
        public static string dbName = @"E:\sqlite\4.db";

        static SchoolDAO()
        {
            connection = new SQLiteConnection($"Data Source = {dbName}; Version=3;");
            connection.Open();
        }
        public Dictionary<ClassRoom, List<Student>> GetMapClassToStudentsDictionary(Int64 n)
        {
            Dictionary<ClassRoom, List<Student>> diclass = new Dictionary<ClassRoom, List<Student>>();

            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT * FROM class", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClassRoom classRoom = new ClassRoom
                        {
                            Id = (Int64)reader["ID"],
                            Name = (string)reader["NAME"],
                            Code = (Int64)reader["code"],
                            Number_Of_Students = (Int64)reader["number_of_students"],
                            Number_Of_Vip = (Int64)reader["number_of_vip"],
                            Age_Average = (Int64)reader["age_average"],
                            Most_Popular_City = (string)reader["most_popular_city"],
                            Oldest_Vip = (Int64)reader["oldest_vip"],
                            Youngest_Vip = (Int64)reader["youngest_vip"]
                        };
                        if (!diclass.ContainsKey(classRoom))
                        {
                            diclass.Add(classRoom, new List<Student>());
                        }
                        diclass[classRoom] = GetStudentsFromClass(classRoom.Id);


                    }
                }
            }
            return diclass;
        }


        public List<Student> GetStudentsFromClass(Int64 id)
        {
            List<Student> students = new List<Student>();
            using (SQLiteCommand cmd = new SQLiteCommand($"SELECT *,class.NAME as class_NAME, students.NAME as students_NAME,class.id as class_id, students.id as students_id FROM students JOIN class ON students.class_id = class.id WHERE class.ID = {id} ", connection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            Id = (Int64)reader["students_id"],
                            Name = (string)reader["students_NAME"],
                            Age = (Int64)reader["AGE"],
                            Address_City = (string)reader["ADDRESS_CITY"],
                            Vip = (string)reader["VIP"],
                            Class_Id = (Int64)reader["class_id"]
                        };

                        ClassRoom ClassRoom = new ClassRoom
                        {
                            Id = (Int64)reader["class_id"],
                            Name = (string)reader["class_NAME"],
                            Code = (Int64)reader["code"],
                            Number_Of_Students = (Int64)reader["number_of_students"],
                            Number_Of_Vip = (Int64)reader["number_of_vip"],
                            Age_Average = (Int64)reader["age_average"],
                            Most_Popular_City = (string)reader["most_popular_city"],
                            Oldest_Vip = (Int64)reader["oldest_vip"],
                            Youngest_Vip = (Int64)reader["youngest_vip"]
                        };
                        students.Add(student);


                    }
                }
            }

            return students;

        }
    }
}
