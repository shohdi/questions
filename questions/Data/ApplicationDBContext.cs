using Microsoft.EntityFrameworkCore;

using questions.Data.Models;
namespace questions.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base()
        {
        }
        public ApplicationDbContext(DbContextOptions options)
        : base(options)
        {
        }
        public DbSet<EXAM_REPOSITORY> ExamRepositories => Set<EXAM_REPOSITORY>();
        public DbSet<QUESTION> Questions => Set<QUESTION>();
        public DbSet<SELECTION> Selections => Set<SELECTION>();

        public DbSet<EXAM> Exams => Set<EXAM>();
        public DbSet<EXAM_QUETION> ExamQuestions => Set<EXAM_QUETION>();


    }
}
