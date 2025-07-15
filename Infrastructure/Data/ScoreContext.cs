using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ScoreContext : DbContext
    {
        public ScoreContext(DbContextOptions<ScoreContext> options) : base (options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");
            modelBuilder.Entity<Nota>().ToTable("Notas");

            modelBuilder.Entity<Student>().
                HasIndex(s => s.Identification).IsUnique();
            modelBuilder.Entity<Teacher>()
                .HasIndex(t => t.Identification).IsUnique();

            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Teacher)
                .WithMany(p => p.Notas)
                .HasForeignKey(n => n.TeacherId);

            modelBuilder.Entity<Nota>()
                .HasOne(n => n.Student)
                .WithMany(e => e.Notas)
                .HasForeignKey(n => n.StudentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
