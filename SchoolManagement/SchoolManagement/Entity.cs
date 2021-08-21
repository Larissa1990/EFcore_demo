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

        [NotMapped]
        public Grade grade { get; set; }
        [NotMapped]
        public List<Subject> subjects { get; set; }
        [NotMapped]
        public List<Teacher> teachers { get; set; }
        [NotMapped]
        public List<StudentTeacher> teacherRelations { get; set; }

        [NotMapped]
        public List<StudentSubject> subjectRelations { get; set; }

    }
    [Table("teacher")]
    public class Teacher
    {
        public int id { get; set; }
        public string name { get; set; }
        public int subject_id { get; set; }
        public int depart_id { get; set; }
        [NotMapped]
        public Subject subject { get; set; }
        [NotMapped]
        public List<Student> students { get; set; }
        [NotMapped]
        public List<StudentTeacher> studentRelations { get; set; }
        [NotMapped]
        public Department depart { get; set; }
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
        public string address { get; set; }
        public int depart_id { get; set; }
        [NotMapped]
        public List<Student> students { get; set; }
        [NotMapped]
        public Department depart { get; set; }
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
        [NotMapped]
        public List<StudentSubject> studentRelations { get; set; }
        [NotMapped]
        public Department depart { get; set; }
    }
    [Table("depart")]
    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
        [NotMapped]
        public List<Grade> grades { get; set; }
        [NotMapped]
        public List<Subject> subjects { get; set; }
        [NotMapped]
        public List<Teacher> teachers { get; set; }
    }
}
