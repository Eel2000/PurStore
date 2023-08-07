using ServiceStack;

namespace PureStore.Test.Services.Identity;

public class AuthenticationServiceTest
{

    [Fact]//wrong test
    public async Task Auth_Should_Throw_Exception_When_Param_Null()
    {
        //Arrange
        var service = new Mock<IAuthenticationService>();

        //Act
        var response = await service.Object.AuthenticateAsync(null);

        //Assert
        response.Should().ThrowIfNull();
    }

    [Fact]
    public async Task Auth_Should_Succeed()
    {
        //Arrange
        var service = new Mock<IAuthenticationService>();
        service.Setup(x => x.AuthenticateAsync(It.IsAny<Auth>())).ReturnsAsync(AuthenticationMock.AuthenticationResult());

        //Act
        var response = await service.Object.AuthenticateAsync(AuthenticationMock.Credentials());

        //act
        response.Should().NotBeNull();
        response.Should().BeOfType<AuthResponse>();
        response.Email.Should().NotBeNullOrWhiteSpace();
        response.Username.Should().NotBeNullOrWhiteSpace();
        response.Token.Should().NotBeNullOrWhiteSpace();
    }
}
