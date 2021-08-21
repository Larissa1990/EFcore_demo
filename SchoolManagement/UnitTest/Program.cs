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
            string connectionString = "Server=localhost;uid=root;pwd=hzbp;port=3306;Database=db_reference;charset=utf8";
            var context = new EntityContext(connectionString);
            Department depart = context.departs
                .Include(d => d.subjects)
                .Include(d => d.teachers)
                .Include(d => d.grades)
                .ThenInclude(g => g.students).First();

            foreach(var item in depart.grades)
            {
                Console.WriteLine(item.name);
                foreach (var student in item.students)
                    Console.WriteLine(student.name);
                Console.WriteLine("-------------------------------------");
            }

            Console.Read();
            

            /*Grade g1 = new Grade() { address = "B1" };
            Grade g2 = new Grade() { address = "B2" };
            Grade g3 = new Grade() { address = "B3" };

            Subject s1 = new Subject() { name = "English" };
            Subject s2 = new Subject() { name = "Computer" };
            Subject s3 = new Subject() { name = "Math" };
            Subject s4 = new Subject() { name = "Music" };
            Subject s5 = new Subject() { name = "physics" };

            Teacher t1 = new Teacher() { name = "spencer", subject = s1 };
            Teacher t2 = new Teacher() { name = "lisa", subject = s2 };
            Teacher t3 = new Teacher() { name = "sheldon", subject = s5 };
            Teacher t4 = new Teacher() { name = "penny", subject = s1 };
            Teacher t5 = new Teacher() { name = "howard", subject = s3 };
            Teacher t6 = new Teacher() { name = "raj", subject = s4 };

            Department d1 = new Department()
            {
                name = "Star",
                grades = new List<Grade>() { g1, g2, g3 },
                subjects = new List<Subject>() { s1, s2, s3, s4, s5 },
                teachers = new List<Teacher>() { t1, t2, t3, t4, t5, t6 }
            };

            Student stu1 = new Student()
            {
                name = "tom",
                grade = g1,
                subjects = new List<Subject>() { s1, s2 },
                teachers = new List<Teacher>() { t1, t2 }
            };

            Student stu2 = new Student()
            {
                name = "lily",
                grade = g2,
                subjects = new List<Subject>() { s1, s3 },
                teachers = new List<Teacher>() { t4, t3 }
            };

            context.departs.Add(d1);
            context.students.Add(stu1);
            context.students.Add(stu2);
            context.SaveChanges();*/
        }

        
    }
}
