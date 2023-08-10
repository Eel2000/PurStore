namespace PureStore.Test.Features.Uploads.Queries
{
    public class GetPagedDataQueryTest
    {
        [Fact]
        public async Task Command_Should_Return_Emty_Enum()
        {
            //Arrange
            var service = new Mock<IUploadApplicationService>();
            service.Setup(x => x.GetAllAsync()).ReturnsAsync(Enumerable.Empty<UploadedApplication>());

            var query = new GetPagedDataQuery(1, 10);
            var sut = new GetPagedDataQueryHandler(service.Object);

            //Act
            var response = await sut.Handle(query, default);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<Response<IEnumerable<UploadedApplication>>>();
            response.Data.Should().NotBeNull();
            response.Data.Should().BeEmpty();
        }

        [Fact]
        public async Task Command_Should_Return_data()
        {
            //Arrange
            var service = new Mock<IUploadApplicationService>();
            service.Setup(x => x.GetAllAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(UploadAppMock.GetAll(1, 10));

            var query = new GetPagedDataQuery(1, 10);
            var sut = new GetPagedDataQueryHandler(service.Object);

            //Act
            var response = await sut.Handle(query, default);

            //Assert
            response.Should().NotBeNull();
            response.Should().BeOfType<Response<IEnumerable<UploadedApplication>>>();
            response.Data.Should().NotBeNull();
            response.Data.Should().NotBeEmpty();
            response.Data.Should().HaveCountGreaterThan(0);
        }
    }
}
