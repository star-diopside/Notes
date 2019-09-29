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

        public Task<IEnumerable<UploadFileViewModel>> ListAsync()
        {
            return _uploadFileRepository.FindAllAsync(u => new UploadFileViewModel(u));
        }

        public Task<UploadFileViewModel> GetDetailsAsync(int id)
        {
            return _uploadFileRepository.FindByIdAsync(id, u => new UploadFileViewModel(u));
        }

        public ValueTask<UploadFile> GetDownloadDataAsync(int id)
        {
            return _uploadFileRepository.FindByIdAsync(id);
        }

        public Task CreateAsync(UploadFileViewModel uploadFile)
        {
            return _uploadFileRepository.AddAsync(uploadFile.ToUploadFile());
        }

        public async Task EditAsync(int id, UploadFileViewModel uploadFile)
        {
            var model = await _uploadFileRepository.FindByIdAsync(id);
            await _uploadFileRepository.UpdateAsync(uploadFile.UpdateUploadFile(model), uploadFile.Version);
        }

        public Task DeleteAsync(int id, uint version)
        {
            return _uploadFileRepository.RemoveAsync(new UploadFile { Id = id }, version);
        }
    }
}
