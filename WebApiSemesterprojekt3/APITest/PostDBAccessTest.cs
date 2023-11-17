using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Xunit;
using Models;

namespace WebApi.Database.Tests
{
    public class PostDBAccessTests : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly string _testConnectionString;

        public PostDBAccessTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _testConnectionString = _configuration.GetConnectionString("hildur");
        }

        public void Dispose()
        {
            
        }

        [Fact]
        public void GetBidPosts_ReturnsListOfBids()
        {
            // Arrange
            using (SqlConnection connection = new SqlConnection(_testConnectionString))
            {
                connection.Open();

                // Insert test data into the database
                InsertTestData(connection, "bid");

                PostDBAccess postDBAccess = new PostDBAccess(_configuration);

                // Act
                IEnumerable<Bid> result = postDBAccess.GetBidPosts();

                // Assert
                Assert.IsType<List<Bid>>(result);
                
            }
        }
        [Fact]
        public void GetOffetPosts_returnListOfOffers()
        {
            //Arragenge
            using(SqlConnection connection = new SqlConnection(_testConnectionString))
            {
                connection.Open();
                InsertTestData(connection, "offer");

                PostDBAccess postDBAccss = new PostDBAccess(_configuration);

                //Act
                IEnumerable<Offer> result = postDBAccss.GetOfferPosts();

                //Assert
                Assert.IsType<List<Offer>>(result);

            }
        }

        [Fact]
        public void GetCurrencyType_returnTheTypeOfCurrency()
        {
            //Arrange
            using(SqlConnection connection=new SqlConnection(_testConnectionString))
            {
                Enum res = null;
               

                connection.Open();
                PostDBAccess postDBAccess= new PostDBAccess(_configuration);

                //Act
                IEnumerable<Offer> result = postDBAccess.GetOfferPosts().ToList();
                Offer offerResult = result.FirstOrDefault();
                res = offerResult.Currency.Type;

                //assert
                Assert.Equal(CurrencyEnum.USD, res);

                
            }
        }

        private void InsertTestData(SqlConnection connection, string type)
        {
            // Insert test data into the database for the specified type (bid/offer)
            // You should implement this method based on your database schema
            // For simplicity, you can use SqlCommand to execute an INSERT statement
            // Ensure to clean up the data in the Dispose method

        }
    }
}
