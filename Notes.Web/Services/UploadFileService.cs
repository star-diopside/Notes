using Notes.Data.Models;
using Notes.Data.Repositories;
using Notes.Web.Models;

namespace Notes.Web.Services;

public class UploadFileService : IUploadFileService
{
    private readonly IUploadFileRepository _uploadFileRepository;

    public UploadFileService(IUploadFileRepository uploadFileRepository)
    {
        _uploadFileRepository = uploadFileRepository;
    }

    public IAsyncEnumerable<UploadFileViewModel> ListAsync()
    {
        return _uploadFileRepository.FindAllAsync(u => new UploadFileViewModel(u));
    }

    public ValueTask<UploadFileViewModel?> GetDetailsAsync(int id)
    {
        return _uploadFileRepository.FindByIdAsync(id, u => new UploadFileViewModel(u));
    }

    public ValueTask<UploadFile?> GetDownloadDataAsync(int id)
    {
        return _uploadFileRepository.FindByIdAsync(id);
    }

    public async ValueTask<UploadFile> CreateAsync(UploadFileViewModel uploadFile)
    {
        var model = await uploadFile.ToUploadFileAsync();
        await _uploadFileRepository.AddAsync(model);
        return model;
    }

    public async ValueTask<UploadFile?> EditAsync(int id, UploadFileViewModel uploadFile)
    {
        var model = await _uploadFileRepository.FindByIdAsync(id);

        if (model is null)
        {
            return null;
        }

        model = await uploadFile.UpdateUploadFileAsync(model);
        await _uploadFileRepository.UpdateAsync(model);
        return model;
    }

    public async ValueTask DeleteAsync(int id, uint version)
    {
        await _uploadFileRepository.RemoveAsync(new UploadFile { Id = id, Version = version });
    }
}
