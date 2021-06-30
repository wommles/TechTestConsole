using Microsoft.EntityFrameworkCore;
using TechTestConsole.Data.Entities;

namespace TechTestConsole.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=techtestconsole;Trusted_Connection=True;MultipleActiveResultSets=true");
          
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Student>(e =>
            {
                e.HasKey(st => st.Id);
                e.HasMany(st => st.Subjects)
                    .WithMany(su => su.Students)
                    .UsingEntity<ExamResult>(
                        e => e.HasOne(er => er.Subject).WithMany(),
                        e => e.HasOne(er => er.Student).WithMany());
            });

            builder.Entity<Subject>(e =>
            {
                e.HasKey(su => su.Id);
                e.HasMany(su => su.Students)
                    .WithMany(st => st.Subjects)
                    .UsingEntity<ExamResult>(
                        e => e.HasOne(ss => ss.Student).WithMany(),
                        e => e.HasOne(ss => ss.Subject).WithMany());

                e.HasData(new[]
                {
                    new Subject { Id = 1, Title = "Science" },
                    new Subject { Id = 2, Title = "Technology" },
                    new Subject { Id = 3, Title = "Engineering" },
                    new Subject { Id = 4, Title = "Maths" }
                });
            });

            builder.Entity<ExamResult>(e =>
            {
                e.HasKey(er => new { StudentID = er.StudentId, SubjectID = er.SubjectId });

                e.HasOne(er => er.Student)
                    .WithMany(st => st.ExamResults)
                    .HasForeignKey(er => er.StudentId);
                e.HasOne(er => er.Subject)
                    .WithMany()
                    .HasForeignKey(er => er.SubjectId);

                e.HasCheckConstraint("CK_ExamResults_Grade", "Grade >= 0 AND Grade <= 100");
                e.HasCheckConstraint("CK_ExamResults_AdjustedGrade", "AdjustedGrade >= 0 AND AdjustedGrade <= 100");
            });
        }
    }
}
