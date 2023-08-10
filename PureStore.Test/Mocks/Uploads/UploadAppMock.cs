using AutoBogus;

namespace PureStore.Test.Mocks.Uploads
{
    internal class UploadAppMock
    {

        public static UploadedApplication GetSingle()
        {
            var personFaker = new AutoFaker<UploadedApplication>()
                      .Configure(builder =>
                      {
                          builder.WithRepeatCount(1);
                      })
                      .RuleFor(fake => fake.Id, fake => fake.Random.Guid().ToString())
                      .RuleFor(fake => fake.ImageUrl, fake => fake.Image.PlaceImgUrl())
                      .Generate();

            return personFaker;
        }

        public static IEnumerable<UploadedApplication> GetAll()
        {
            var enumerator = new AutoFaker<UploadedApplication>()
                      .Configure(builder =>
                      {
                          builder.WithRepeatCount(5);
                      })
                      .RuleFor(fake => fake.Id, fake => fake.Random.Guid().ToString())
                      .RuleFor(fake => fake.ImageUrl, fake => fake.Image.PicsumUrl())
                      .GenerateLazy(10);

            return enumerator;
        }

        public static IEnumerable<UploadedApplication> GetAll(int page, int size)
        {
            var enumerator = new AutoFaker<UploadedApplication>()
                      .Configure(builder =>
                      {
                          builder.WithRepeatCount(5);
                      })
                      .RuleFor(fake => fake.Id, fake => fake.Random.Guid().ToString())
                      .RuleFor(fake => fake.ImageUrl, fake => fake.Image.PicsumUrl())
                      .GenerateLazy(100);

            var data = enumerator.Skip((page - 1) * size).Take(size);

            return data;
        }
    }
}
