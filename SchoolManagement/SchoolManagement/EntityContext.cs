using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace SchoolManagement
{
    public class EntityContext:DbContext
    {
        private readonly string connectionString;
        public DbSet<Student> students { get; set; }
        public DbSet<Department> departs { get; set; }
        public DbSet<Teacher> teachers { get; set; }


        public EntityContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public static readonly LoggerFactory loggerFactory = new LoggerFactory(
            new[] { new DebugLoggerProvider() }
            );

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseMySQL(connectionString).UseLoggerFactory(loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Student Relationships
            // students-teachers : multi-to-multi
            builder.Entity<Student>()
                .HasMany(s => s.teachers)
                .WithMany(t => t.students)
                .UsingEntity<StudentTeacher>(
                j => j.HasOne(pt => pt.teacher)
                .WithMany()
                .HasForeignKey(pt => pt.teacher_id),
                j => j.HasOne(pt => pt.student)
                .WithMany()
                .HasForeignKey(pt => pt.student_id),
                j => j.HasKey(t => new { t.student_id, t.teacher_id }));

            // students-subjects: multi-to-multi
            builder.Entity<Student>()
                .HasMany(s => s.subjects)
                .WithMany(su => su.students)
                .UsingEntity<StudentSubject>(
                j => j.HasOne(pt => pt.subject)
                .WithMany()
                .HasForeignKey(pt => pt.subject_id),
                j => j.HasOne(pt => pt.student)
                .WithMany()
                .HasForeignKey(pt => pt.student_id),
                j => j.HasKey(t => new { t.student_id, t.subject_id }));

            // student-address: multi-to-one
            builder.Entity<Student>()
                .HasOne(s => s.address)
                .WithMany()
                .HasForeignKey(s => s.address_id);
            #endregion

            #region Teacher Relationships
            // teachers-address: multi-to-one
            builder.Entity<Teacher>()
                .HasOne(t => t.address)
                .WithMany()
                .HasForeignKey(t => t.address_id);

            builder.Entity<Teacher>()
                .HasMany(t => t.students)
                .WithMany(s => s.teachers)
                .UsingEntity<StudentTeacher>(
                j => j.HasOne(pt => pt.student)
                .WithMany().HasForeignKey(pt => pt.student_id),
                j => j.HasOne(pt => pt.teacher)
                .WithMany().HasForeignKey(pt => pt.teacher_id),
                j => j.HasKey(t => new { t.teacher_id, t.student_id }));
            #endregion

            #region Department Relationships
            // department-grades: one-to-multi
            builder.Entity<Department>()
                .HasMany(d => d.grades)
                .WithOne().HasForeignKey(g => g.depart_id);

            // department-teachers: one-to-multi
            builder.Entity<Department>()
                .HasMany(d => d.teachers)
                .WithOne().HasForeignKey(t => t.depart_id);

            // department-subjects: one-to-multi
            builder.Entity<Department>()
                .HasMany(d => d.subjects)
                .WithOne().HasForeignKey(s => s.depart_id);

            // departments-addresses: multi-to-multi
            builder.Entity<Department>()
                .HasMany(d => d.addresses)
                .WithMany(a => a.departs)
                .UsingEntity<Depart_Address>(
                j => j.HasOne(pt => pt.address)
                .WithMany().HasForeignKey(pt => pt.address_id),
                j => j.HasOne(pt => pt.depart)
                .WithMany().HasForeignKey(pt => pt.depart_id),
                j => j.HasKey(t => new { t.depart_id, t.address_id }));
            #endregion

            #region Grade Relationships
            // grade-students: one-to-multi
            builder.Entity<Grade>()
                .HasMany(g => g.students)
                .WithOne().HasForeignKey(s => s.grade_id);
            #endregion

            #region Subject Relationships
            // subjects-students: multi-to-multi
            builder.Entity<Subject>()
                .HasMany(s => s.students)
                .WithMany(stu => stu.subjects)
                .UsingEntity<StudentSubject>(
                j => j.HasOne(pt => pt.student)
                .WithMany()
                .HasForeignKey(pt => pt.student_id),
                j => j.HasOne(pt => pt.subject)
                .WithMany()
                .HasForeignKey(pt => pt.subject_id),
                j => j.HasKey(t => new { t.subject_id, t.student_id }));

            builder.Entity<Subject>()
                .HasMany(s => s.teachers)
                .WithOne().HasForeignKey(t => t.subject_id);
            #endregion
        }
    }
}
