namespace PureStore.Test.Features.Uploads.Commands
{
    public class RemoveAppCommandTest
    {

        [Fact]
        public async Task Commnad_Should_Failed_when_idNoCorresponding()
        {
            //Arrange
            var serivce = new Mock<IUploadApplicationService>();

            var commnad = new RemoveAppCommand(It.IsAny<string>());

            var sut = new RemoveAppCommandHandler(serivce.Object);

            //Act
            var response = await sut.Handle(commnad, default);

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
            serivce.Setup(x => x.RemoveAsync(It.IsAny<string>())).ReturnsAsync(UploadAppMock.GetSingle());

            var commnad = new RemoveAppCommand(It.IsAny<string>());

            var sut = new RemoveAppCommandHandler(serivce.Object);

            //Act
            var response = await sut.Handle(commnad, default);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<Response<UploadedApplication>>();
            response.Succeeded.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }
    }
}
