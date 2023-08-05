using PureStore.Domain.Common;

namespace PureStore.Test.Features.Identity.Commands;

public class AuthenticationCommandTest
{
    private readonly Mock<IAuthenticationService> _authenticationService;
    public AuthenticationCommandTest()
    {
        _authenticationService = new Mock<IAuthenticationService>();
        _authenticationService.Setup(a => a.AuthenticateAsync(AuthenticationMock.Credentials()))
            .ReturnsAsync(AuthenticationMock.AuthenticationResult());
    }

    [Fact]
    public void Command_Should_Throw_Exception_When_Params_Are_Missing()
    {
        //Arrange
        var command = new AuthenticationCommand(It.IsAny<Auth>());
        var sut = new AuthenticationCommandHandler(_authenticationService.Object);

        //Act
        Func<Task> response = () => sut.Handle(command, default);

        //Assert
        response.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public void Command_Should_Throw_Exception_When_One_Param_Field_IsMissing()
    {
        //Arrange
        Auth auth = new(It.IsAny<string>(), "12345");
        var command = new AuthenticationCommand(auth);

        var sut = new AuthenticationCommandHandler(_authenticationService.Object);

        //Act
        Func<Task> response = () => sut.Handle(command, default);

        //Assert
        response.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task Command_Should_Succeed()
    {
        //Arrange
        Auth auth = new("eliezerbwana", "123456");
        var command = new AuthenticationCommand(auth);

        var sut = new AuthenticationCommandHandler(_authenticationService.Object);

        //Act
        var response = await sut.Handle(command, default);

        //Assert
        response.Should().BeOfType<Response<AuthResponse>>();
        response.Succeeded.Should().BeTrue();
    }
}
