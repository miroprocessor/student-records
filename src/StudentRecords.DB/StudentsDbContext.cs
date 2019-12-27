using Microsoft.EntityFrameworkCore;
using StudentRecords.Entities;

namespace StudentRecords.DB
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
            : base(options)
        {}

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentFile> StudentsFiles { get; set; }
    }
}
