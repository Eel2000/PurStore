using AutoBogus;
using Bogus;
using PureStore.Domain.Entities;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                      .RuleFor(fake => fake.Id, fake => fake.GetId().ToString())
                      .RuleFor(fake => fake.ImageUrl, fake => fake.Image.ToString())
                      .Generate();

            return personFaker;
        }
    }
}
