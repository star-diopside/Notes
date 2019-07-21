using Moq;
using Notes.Data.Models;
using Notes.Data.Repositories;
using Notes.Web.Models;
using Notes.Web.Services;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Notes.Web.Tests.Services
{
    public class UploadFileServiceTest
    {
        [Fact(DisplayName = "ListAsync() : Not Found")]
        public async Task ListAsyncTest1()
        {
            var mock = new Mock<IUploadFileRepository>();
            mock.Setup(m => m.FindAllAsync(It.IsAny<Expression<Func<UploadFile, UploadFileViewModel>>>()))
                .ReturnsAsync(Enumerable.Empty<UploadFileViewModel>());

            var service = new UploadFileService(mock.Object);
            var files = await service.ListAsync();

            Assert.Empty(files);
        }

        [Fact(DisplayName = "ListAsync() : 1 Hit")]
        public async Task ListAsyncTest2()
        {
            var mock = new Mock<IUploadFileRepository>();
            mock.Setup(m => m.FindAllAsync(It.IsAny<Expression<Func<UploadFile, UploadFileViewModel>>>()))
                .ReturnsAsync(new[] { new UploadFileViewModel() { Id = 0 } });

            var service = new UploadFileService(mock.Object);
            var files = await service.ListAsync();

            Assert.Single(files);
        }

        [Fact(DisplayName = "ListAsync() : 10 Hits")]
        public async Task ListAsyncTest3()
        {
            var mock = new Mock<IUploadFileRepository>();
            mock.Setup(m => m.FindAllAsync(It.IsAny<Expression<Func<UploadFile, UploadFileViewModel>>>()))
                .ReturnsAsync(Enumerable.Range(0, 10).Select(i => new UploadFileViewModel() { Id = i }));

            var service = new UploadFileService(mock.Object);
            var files = await service.ListAsync();

            Assert.NotEmpty(files);
            Assert.Equal(10, files.Count());
        }
    }
}
