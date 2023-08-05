using PureStore.Application.DTOs.Identity;
using PureStore.Domain.Common;

namespace PureStore.Test.Features.Identity.Commands
{
    public class RegistrationCommandTest
    {
        private readonly Mock<IAuthenticationService> _authenticationService;

        public RegistrationCommandTest()
        {
            _authenticationService = new Mock<IAuthenticationService>();
            _authenticationService.Setup(s => s.RegisterAsync(RegistrationMock.Register()))
                .ReturnsAsync(RegistrationMock.ResgistrationResponse());
        }


        [Fact]
        public void Command_Should_Failed_When_Param_Missing()
        {
            //Arrange
            var command = new RegistrationCommand(It.IsAny<RegisterDTO>());
            var sut = new RegistrationCommandHandler(_authenticationService.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);

            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public void Command_Should_Failed_When_Username_Missing()
        {
            //Arrange
            var param = new RegisterDTO(It.IsAny<string>(), "password", "test@gmail.com");

            var command = new RegistrationCommand(param);
            var sut = new RegistrationCommandHandler(_authenticationService.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);

            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public void Command_Should_Failed_When_Email_Missing()
        {
            //Arrange
            var param = new RegisterDTO("test@gmail.com", "password", It.IsAny<string>());

            var command = new RegistrationCommand(param);
            var sut = new RegistrationCommandHandler(_authenticationService.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);

            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public void Command_Should_Failed_When_Password_Missing()
        {
            //Arrange
            var param = new RegisterDTO("test@gmail.com", It.IsAny<string>(), "test@gmail.com");

            var command = new RegistrationCommand(param);
            var sut = new RegistrationCommandHandler(_authenticationService.Object);

            //Act
            Func<Task> response = () => sut.Handle(command, default);

            //Assert
            response.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Command_Should_Succeed()
        {
            //Arrange
            var command = new RegistrationCommand(RegistrationMock.Register());
            var sut = new RegistrationCommandHandler(_authenticationService.Object);

            //Act
            var response = await sut.Handle(command, default);

            //Assert
            response.Should().BeOfType<Response<object>>();
            response.Succeeded.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }
    }
}
