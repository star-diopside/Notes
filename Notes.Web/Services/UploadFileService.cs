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

        public ValueTask<IEnumerable<UploadFileViewModel>> ListAsync()
        {
            return _uploadFileRepository.FindAllAsync(u => new UploadFileViewModel(u));
        }

        public ValueTask<UploadFileViewModel> GetDetailsAsync(int id)
        {
            return _uploadFileRepository.FindByIdAsync(id, u => new UploadFileViewModel(u));
        }

        public ValueTask<UploadFile> GetDownloadDataAsync(int id)
        {
            return _uploadFileRepository.FindByIdAsync(id);
        }

        public async ValueTask CreateAsync(UploadFileViewModel uploadFile)
        {
            await _uploadFileRepository.AddAsync(await uploadFile.ToUploadFileAsync());
        }

        public async ValueTask EditAsync(int id, UploadFileViewModel uploadFile)
        {
            var model = await _uploadFileRepository.FindByIdAsync(id);
            await _uploadFileRepository.UpdateAsync(await uploadFile.UpdateUploadFileAsync(model));
        }

        public async ValueTask DeleteAsync(int id, uint version)
        {
            await _uploadFileRepository.RemoveAsync(new UploadFile { Id = id, Version = version });
        }
    }
}
