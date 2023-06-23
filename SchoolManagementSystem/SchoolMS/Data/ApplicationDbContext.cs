using Microsoft.EntityFrameworkCore;
using SchoolMS.Models;

namespace SchoolMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet for Student model
        public DbSet<Student> Students { get; set; }

        // DbSet for Lecturer model
        //public DbSet<Lecturer> Lecturers { get; set; }

        // DbSet for PendingStudent model
        public DbSet<PendingStudent> PendingStudents { get; set; }

        // DbSet for PendingLecturer model
        public DbSet<PendingLecturer> PendingLecturers { get; set; }

        // DbSet for School model
        public DbSet<School> Schools { get; set; }

        // DbSet for Program model
        public DbSet<StudyProgram> Programs { get; set; }

        // DbSet for Course model
        public DbSet<Courses> Courses { get; set; }

        // DbSet for Course model
        public DbSet<Lecturer> Lecturer { get; set; }

        public DbSet<Admin> Administrators { get; set; }

        public DbSet<NoticeBoard>? NoticeBoard { get; set; }

        public DbSet<Club>? Clubs { get; set; }
        public DbSet<ClubMember>? ClubsMembers { get; set; }
    }
}
