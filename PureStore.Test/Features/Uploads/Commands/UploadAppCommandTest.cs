using PureStore.Domain.Common;

namespace PureStore.Test.Features.Uploads.Commands
{
    public class UploadAppCommandTest
    {
        [Fact]
        public void Command_Should_Throw_ArgumentException_When_Request_Null()
        {
            //Arrange
            var serice = new Mock<IUploadApplicationService>();

            var command = new UploadAppCommand(It.IsAny<UploadApp>());

            var sut = new UploadAppCommandHandler(serice.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);


            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }


        [Fact]
        public void Command_Should_Throw_ArgumentException_When_Request_Title_Missing()
        {
            //Arrange
            var serice = new Mock<IUploadApplicationService>();

            var req = new UploadApp()
            {
                Title = null,
                AppUrl = "string",
                Description = "description",
                Author = "Me",
                Size = 2000
            };

            var command = new UploadAppCommand(req);

            var sut = new UploadAppCommandHandler(serice.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);


            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public void Command_Should_Throw_ArgumentException_When_Request_Description_Missing()
        {
            //Arrange
            var serice = new Mock<IUploadApplicationService>();

            var req = new UploadApp()
            {
                Title = "test",
                AppUrl = "string",
                Description = default,
                Author = "Me",
                Size = 2000
            };

            var command = new UploadAppCommand(req);

            var sut = new UploadAppCommandHandler(serice.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);


            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public void Command_Should_Throw_ArgumentException_When_Request_AppUrl_Missing()
        {
            //Arrange
            var serice = new Mock<IUploadApplicationService>();

            var req = new UploadApp()
            {
                Title = "title",
                AppUrl = It.IsAny<string>(),
                Description = "description",
                Author = "Me",
                Size = 2000
            };

            var command = new UploadAppCommand(req);

            var sut = new UploadAppCommandHandler(serice.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);


            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public void Command_Should_Throw_ArgumentException_When_Request_StringWord_Proibided()
        {
            //Arrange
            var serice = new Mock<IUploadApplicationService>();

            var req = new UploadApp()
            {
                Title = "string",
                AppUrl = "string",
                Description = "string",
                Author = "Me",
                Size = 2000
            };

            var command = new UploadAppCommand(req);

            var sut = new UploadAppCommandHandler(serice.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);


            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Command_Should_Succeed()
        {
            //Arrange
            var serice = new Mock<IUploadApplicationService>();
            serice.Setup(x => x.UploadNewAsync(It.IsAny<UploadApp>()))
                .Returns(ValueTask.CompletedTask);

            var req = new UploadApp()
            {
                Title = "MyApp",
                AppUrl = "https://google.com",
                Description = "Help to dowlaod free apk",
                Author = "Me",
                Size = 2000
            };

            var command = new UploadAppCommand(req);

            var sut = new UploadAppCommandHandler(serice.Object);

            //Act
            var response = await sut.Handle(command, default);

            //Assert
            response.Succeeded.Should().BeTrue();
            response.Should().BeOfType<Response<string>>();
            response.Data.Should().BeOfType<string>();
        }
    }
}
