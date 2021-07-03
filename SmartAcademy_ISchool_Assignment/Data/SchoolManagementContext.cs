using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartAcademy_ISchool_Assignment.Models;

#nullable disable

namespace SmartAcademy_ISchool_Assignment.Data
{
    public partial class SchoolManagementContext : DbContext
    {
        public SchoolManagementContext()
        {
        }

        public SchoolManagementContext(DbContextOptions<SchoolManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentsToSubject> StudentsToSubjects { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=SchoolManagementSystem;integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .Property(s => s.Gender)
                .HasConversion<string>();

            modelBuilder.Entity<StudentsToSubject>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.SubjectName })
                    .HasName("PK_StudentsToSubjects_1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentsToSubjects)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentsToSubjects_Students");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.StudentsToSubjects)
                    .HasForeignKey(d => d.SubjectName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentsToSubjects_Subjects");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
