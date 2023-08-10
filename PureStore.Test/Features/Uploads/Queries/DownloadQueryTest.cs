namespace PureStore.Test.Features.Uploads.Queries
{
    public class DownloadQueryTest
    {
        [Fact]
        public async Task Commnad_Should_Failed_when_idNoCorresponding()
        {
            //Arrange
            var serivce = new Mock<IUploadApplicationService>();

            var query = new DownloadQuery(It.IsAny<string>());

            var sut = new DownloadQueryHandler(serivce.Object);

            //Act
            var response = await sut.Handle(query, default);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<Response<UploadedApplication>>();
            response.Succeeded.Should().BeFalse();
        }


        [Fact]
        public async Task Commnad_Should_Succeed()
        {
            //Arrange
            var serivce = new Mock<IUploadApplicationService>();
            serivce.Setup(x => x.DownloadAppAsync(It.IsAny<string>())).ReturnsAsync(UploadAppMock.GetSingle());

            var query = new DownloadQuery(It.IsAny<string>());

            var sut = new DownloadQueryHandler(serivce.Object);

            //Act
            var response = await sut.Handle(query, default);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<Response<UploadedApplication>>();
            response.Succeeded.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }
    }
}
