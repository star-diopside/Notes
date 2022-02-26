using Moq;
using Notes.Data.Models;
using Notes.Data.Repositories;
using Notes.Web.Models;
using Notes.Web.Services;
using System.Linq.Expressions;
using Xunit;

namespace Notes.Web.Tests.Services;

public class UploadFileServiceTest
{
    [Fact(DisplayName = "ListAsync() : Not Found")]
    public void ListAsyncTest1()
    {
        var mock = new Mock<IUploadFileRepository>();
        mock.Setup(m => m.FindAllAsync(It.IsAny<Expression<Func<UploadFile, UploadFileViewModel>>>()))
            .Returns(Enumerable.Empty<UploadFileViewModel>().ToAsyncEnumerable());

        var service = new UploadFileService(mock.Object);
        var files = service.ListAsync();

        Assert.Empty(files.ToEnumerable());
    }

    [Fact(DisplayName = "ListAsync() : 1 Hit")]
    public void ListAsyncTest2()
    {
        var mock = new Mock<IUploadFileRepository>();
        mock.Setup(m => m.FindAllAsync(It.IsAny<Expression<Func<UploadFile, UploadFileViewModel>>>()))
            .Returns(new[] { new UploadFileViewModel() { Id = 0 } }.ToAsyncEnumerable());

        var service = new UploadFileService(mock.Object);
        var files = service.ListAsync();

        Assert.Single(files.ToEnumerable());
    }

    [Fact(DisplayName = "ListAsync() : 10 Hits")]
    public async Task ListAsyncTest3()
    {
        var mock = new Mock<IUploadFileRepository>();
        mock.Setup(m => m.FindAllAsync(It.IsAny<Expression<Func<UploadFile, UploadFileViewModel>>>()))
            .Returns(Enumerable.Range(0, 10).Select(i => new UploadFileViewModel() { Id = i }).ToAsyncEnumerable());

        var service = new UploadFileService(mock.Object);
        var files = service.ListAsync();

        Assert.Equal(10, await files.CountAsync());
    }
}
