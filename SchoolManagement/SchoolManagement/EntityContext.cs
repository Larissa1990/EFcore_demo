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
            // students-teachers
            builder.Entity<Student>()
                .HasMany(s => s.teachers)
                .WithMany(t => t.students)
                .UsingEntity<StudentTeacher>(
                j => j.HasOne(pt => pt.teacher)
                .WithMany(t => t.studentRelations)
                .HasForeignKey(pt => pt.teacher_id),
                j => j.HasOne(pt => pt.student)
                .WithMany(t => t.teacherRelations)
                .HasForeignKey(pt => pt.student_id),
                j => j.HasKey(t => new { t.student_id, t.teacher_id }));

            // students-grade
            builder.Entity<Student>()
                .HasOne(s => s.grade)
                .WithMany()
                .HasForeignKey(s => s.grade_id);

            // students-subjects
            builder.Entity<Student>()
                .HasMany(s => s.subjects)
                .WithMany(su => su.students)
                .UsingEntity<StudentSubject>(
                j => j.HasOne(pt => pt.subject)
                .WithMany(t => t.studentRelations)
                .HasForeignKey(pt => pt.subject_id),
                j => j.HasOne(pt => pt.student)
                .WithMany(t => t.subjectRelations)
                .HasForeignKey(pt => pt.student_id),
                j => j.HasKey(t => new { t.student_id, t.subject_id }));

            builder.Entity<Teacher>()
                .HasOne(t => t.subject)
                .WithMany()
                .HasForeignKey(t => t.subject_id);

            builder.Entity<Department>()
                .HasMany(d => d.grades)
                .WithOne(g => g.depart).HasForeignKey(g => g.depart_id);

            builder.Entity<Department>()
                .HasMany(d => d.teachers)
                .WithOne(t => t.depart).HasForeignKey(t => t.depart_id);

            builder.Entity<Department>()
                .HasMany(d => d.subjects)
                .WithOne(s => s.depart).HasForeignKey(s => s.depart_id);

            builder.Entity<Grade>()
                .HasMany(g => g.students)
                .WithOne(s => s.grade).HasForeignKey(s => s.grade_id);


        }
    }
}
