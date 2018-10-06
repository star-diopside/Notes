using Notes.Data.Models;
using Notes.Data.Repositories;
using Notes.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Notes.Web.Services
{
    public class UploadFileService : IUploadFileService
    {
        private readonly IUploadFileRepository _uploadFileRepository;

        public UploadFileService(IUploadFileRepository uploadFileRepository)
        {
            _uploadFileRepository = uploadFileRepository;
        }

        public async Task<IEnumerable<UploadFileViewModel>> ListAsync()
        {
            return await _uploadFileRepository.FindAllAsync(u => new UploadFileViewModel
            {
                Id = u.Id,
                FileName = u.FileName,
                ContentType = u.ContentType,
                Length = u.Length
            });
        }

        public async Task<UploadFileViewModel> GetDetailsAsync(int id)
        {
            return await _uploadFileRepository.FindByIdAsync(id, u => new UploadFileViewModel
            {
                Id = u.Id,
                FileName = u.FileName,
                ContentType = u.ContentType,
                Length = u.Length
            });
        }

        public async Task<UploadFile> GetDownloadDataAsync(int id)
        {
            return await _uploadFileRepository.FindByIdAsync(id);
        }

        public async Task CreateAsync(UploadFileViewModel uploadFile)
        {
            await _uploadFileRepository.AddAsync(uploadFile.ToUploadFile());
        }
    }
}
