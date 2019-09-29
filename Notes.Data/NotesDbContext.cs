using Microsoft.EntityFrameworkCore;
using Notes.Data.Models;

namespace Notes.Data
{
    public class NotesDbContext : DbContext
    {
        public DbSet<UploadFile> UploadFiles { get; set; }

        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
