using Notes.Data.Models;
using Notes.Data.Repositories;
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

        public async Task<IEnumerable<UploadFile>> SelectAllAsync()
        {
            return await _uploadFileRepository.ListAllWithoutDataAsync();
        }

        public async Task CreateAsync(UploadFile uploadFile)
        {
            await _uploadFileRepository.AddAsync(uploadFile);
        }
    }
}
