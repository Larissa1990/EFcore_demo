using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement
{
    [Table("student")]
    public class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public int grade_id { get; set; }
        public int address_id { get; set; }

        [NotMapped]
        public Address address { get; set; }

        [NotMapped]
        public List<Subject> subjects { get; set; }
        [NotMapped]
        public List<Teacher> teachers { get; set; }

    }
    [Table("teacher")]
    public class Teacher
    {
        public int id { get; set; }
        public string name { get; set; }
        public int subject_id { get; set; }
        public int depart_id { get; set; }
        public int address_id { get; set; }

        [NotMapped]
        public Address address { get; set; }

        [NotMapped]
        public List<Student> students { get; set; }

    }
    [Table("stu_tea")]
    public class StudentTeacher
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public int teacher_id { get; set; }
        [NotMapped]
        public Student student { get; set; }
        [NotMapped]
        public Teacher teacher { get; set; }
    }
    [Table("stu_subject")]
    public class StudentSubject
    {
        public int id { get; set; }
        public int student_id { get; set; }
        public int subject_id { get; set; }
        [NotMapped]
        public Student student { get; set; }
        [NotMapped]
        public Subject subject { get; set; }
    }
    [Table("grade")]
    public class Grade
    {
        public int id { get; set; }
        public string name { get; set; }
        public int depart_id { get; set; }
        [NotMapped]
        public List<Student> students { get; set; }

    }
    [Table("subject")]
    public class Subject
    {
        public int id { get; set; }
        public string name { get; set; }
        public int depart_id { get; set; }
        [NotMapped]
        public List<Teacher> teachers { get; set; }
        [NotMapped]
        public List<Student> students { get; set; }
    }
    [Table("depart")]
    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }

        [NotMapped]
        public List<Address> addresses { get; set; }
        [NotMapped]
        public List<Grade> grades { get; set; }
        [NotMapped]
        public List<Subject> subjects { get; set; }
        [NotMapped]
        public List<Teacher> teachers { get; set; }
    }

    [Table("address")]
    public class Address
    {
        public int id { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        [NotMapped]
        public List<Department> departs { get; set; }
        
    }

    [Table("depart_address")]
    public class Depart_Address
    {
        public int id { get; set; }
        public int depart_id { get; set; }
        public int address_id { get; set; }

        [NotMapped]
        public Department depart { get; set; }
        [NotMapped]
        public Address address { get; set; }
    }


}
