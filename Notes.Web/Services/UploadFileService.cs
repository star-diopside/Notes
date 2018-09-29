using Microsoft.EntityFrameworkCore;
using Notes.Data;
using Notes.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Web.Services
{
    class UploadFileService : IUploadFileService
    {
        public readonly NotesDbContext _notesDbContext;

        public UploadFileService(NotesDbContext notesDbContext)
        {
            _notesDbContext = notesDbContext;
        }

        public async Task<IEnumerable<UploadFile>> SelectAllAsync()
        {
            return await _notesDbContext.UploadFiles.ToListAsync();
        }

        public async Task CreateAsync(UploadFile uploadFile)
        {
            _notesDbContext.UploadFiles.Add(uploadFile);
            await _notesDbContext.SaveChangesAsync();
        }
    }
}
