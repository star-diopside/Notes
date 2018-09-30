using Microsoft.EntityFrameworkCore;
using Notes.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Data.Repositories
{
    public class UploadFileRepository : IUploadFileRepository
    {
        private readonly NotesDbContext _notesDbContext;

        public UploadFileRepository(NotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }

        public async Task<IEnumerable<UploadFile>> ListAllAsync()
        {
            return await _notesDbContext.UploadFiles.ToListAsync();
        }

        public async Task AddAsync(UploadFile uploadFile)
        {
            _notesDbContext.UploadFiles.Add(uploadFile);
            await _notesDbContext.SaveChangesAsync();
        }
    }
}
