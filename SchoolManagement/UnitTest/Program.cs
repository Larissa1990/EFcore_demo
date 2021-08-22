using System;
using SchoolManagement;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace UnitTest
{
    class Program
    {
        
        static void Main(string[] args)
        {
            string connectionString = "Server=localhost;uid=root;pwd=xxx;port=3306;Database=db_efcore;charset=utf8";
            var context = new EntityContext(connectionString);
            /*
            #region Test1 Build Department
            Department depart = context.departs
                .Include(d => d.subjects)
                .ThenInclude(s=>s.students)
                .ThenInclude(s=>s.teachers)
                .Include(d=>d.addresses)
                .Include(d => d.teachers)
                .ThenInclude(t=>t.students)
                .Include(d => d.grades)
                .ThenInclude(g => g.students).First();

            Console.WriteLine("department name: " + depart.name);
            Console.WriteLine();
            Console.WriteLine("department subjects:");
            foreach(var item in depart.subjects)
            {
                Console.WriteLine((depart.subjects.IndexOf(item)+1).ToString()+"." + item.name);
                Console.WriteLine("Students choose this subject:");
                foreach (var stu in item.students)
                    Console.Write(stu.name+",");
                Console.WriteLine();
                Console.WriteLine("Teachers who teach this subject:");
                foreach (var t in item.teachers)
                    Console.Write(t.name+",");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("department teachers:");
            foreach(var item in depart.teachers)
            {
                Console.WriteLine((depart.teachers.IndexOf(item)+1).ToString()+"."+item.name);
                Console.WriteLine("his or her students:");
                foreach (var stu in item.students)
                    Console.Write(stu.name + ",");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("department grades:");
            foreach(var item in depart.grades)
            {
                Console.WriteLine((depart.grades.IndexOf(item) + 1).ToString() + "." + item.name);
                Console.WriteLine("students in this grade:");
                foreach (var stu in item.students)
                    Console.Write(stu.name + ",");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("department addresses:");
            foreach(var item in depart.addresses)
            {
                Console.WriteLine(item.country + "." + item.city);
            }
            #endregion*/

            #region Test2 Build Students
            List<Student> students = context.students
                .Include(s => s.subjects)
                .Include(s => s.teachers)
                .Include(s => s.address).ToList();

            Console.WriteLine("Students List:");
            foreach(var item in students)
            {
                Console.WriteLine((students.IndexOf(item) + 1).ToString() + "." + item.name);
                Console.WriteLine("his or her subjects:");
                foreach(var sub in item.subjects)
                {
                    Console.Write(sub.name + ",");
                }
                Console.WriteLine();
                Console.WriteLine("his or her teachers:");
                foreach (var t in item.teachers)
                    Console.Write(t.name + ",");
                Console.WriteLine();
                Console.WriteLine("his or her address:" + item.address.country + "." + item.address.city);
            }
            #endregion
            Console.Read();

        }

        
    }
}
