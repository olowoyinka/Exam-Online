using Exam_Online.Models.Question;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exam_Online.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Choice> Choices { get; set; }

        public DbSet<Exhibit> Exhibits { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionCategory> QuestionCategories { get; set; }

        public DbSet<QuestionDuration> QuestionDurations { get; set; }

        public DbSet<Registration> Registrations { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Test> Tests { get; set; }

        public DbSet<TestPaper> TestPapers { get; set; }

        public DbSet<TestQuestion> TestQuestions { get; set; }
    }
}
