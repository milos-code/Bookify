using Bogus;
using Bookify.Application.Abstractions.Data;
using Bookify.Domain.Apartments;

namespace Bookify.Api.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();
            using var connection = sqlConnectionFactory.CreateConnection();

            var faker = new Faker();

            List<object> apartments = new();
            for(var i = 0; i < 100; i++)
            {
                apartments.Add(new
                {
                    Id = Guid.NewGuid(),
                    Name = faker.Company.CompanyName(),
                    Description = "Amazing view",
                    Country = faker.Address.Country(),
                    State = faker.Address.State(),
                    ZipCode = faker.Address.ZipCode(),
                    City = faker.Address.City(),
                    Street = faker.Address.StreetAddress(),
                    PriceAmount = faker.Random.Decimal(50,1000),
                    PriceCurrnecy = "USD",
                    CleaningFeeAmount = faker.Random.Decimal(25,200),
                    CleaningFeeCurrency = "USD",
                    Amenities = new List<int> { (int)Amenity.Parking, (int)Amenity.MountainView},
                    LastBookedOn = DateTime.MinValue
                });
            }
        }
    }
}
