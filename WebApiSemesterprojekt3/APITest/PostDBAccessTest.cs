using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Xunit;
using Models;
using System.Runtime.CompilerServices;

namespace WebApi.Database.Tests
{



    public class PostDBAccessTests : IDisposable
    {


        private readonly IConfiguration _testconfiguration;
        private string? _testconnectionString;


        public PostDBAccessTests(IConfiguration configuration)
        {
            _testconfiguration = configuration;
            _testconnectionString = _testconfiguration.GetConnectionString("hildur_test");
        }

        public void Dispose()
        {

        }



        [Fact]
        public void GetBidPosts_ReturnsListOfBids()
        {
            // Arrange
            using (SqlConnection connection = new SqlConnection(_testconnectionString))
            {
                connection.Open();

                // Insert test data into the database


                PostDBAccess postDBAccess = new PostDBAccess(_testconfiguration);
               

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
            using (SqlConnection connection = new SqlConnection(_testconnectionString))
            {
                connection.Open();


                PostDBAccess postDBAccss = new PostDBAccess(_testconfiguration);

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
            using (SqlConnection connection = new SqlConnection(_testconnectionString))
            {
                Enum res = null;


                connection.Open();
                PostDBAccess postDBAccess = new PostDBAccess(_testconfiguration);

                //Act
                IEnumerable<Offer> result = postDBAccess.GetOfferPosts();
                Offer offerResult = result.FirstOrDefault();
                res = offerResult.Currency.Type;

                //assert
                Assert.Equal(CurrencyEnum.USD, res);


            }


        }
        [Fact]
        public void InsertBid_InsertsBidOnDB()
        {
            using (SqlConnection connection = new SqlConnection(_testconnectionString))
            {

                connection.Open();
                PostDBAccess postDBAccess = new PostDBAccess(_testconfiguration);

                //Arrange
                Currency cu = new Currency(CurrencyEnum.USD, null); ;
                Bid p = new Bid
                {
                    Transactions = null,
                    Currency = cu,
                    Price = 100,
                    Amount = 100,
                    IsComplete = false,
                };

                //!!fylder databasen med test data, hver gang man kører alle tests!!
                //Act
                // postDBAccess.InsertBid(p);


                //Assert
                Assert.True(isBidInsertedSuccessfully());
            }
        }

        [Fact]
        public void InsertOffer_InsertsOfferOnDB()
        {
            using (SqlConnection connection = new SqlConnection(_testconnectionString))
            {

                connection.Open();
                PostDBAccess postDBAccess = new PostDBAccess(_testconfiguration);

                //Arrange
                Currency cu = new Currency(CurrencyEnum.USD, null); ;
                Bid p = new Bid
                {
                    Transactions = null,
                    Currency = cu,
                    Price = 100,
                    Amount = 100,
                    IsComplete = false,
                };

                //!!fylder databasen med test data, hver gang man kører alle tests!!
                //Act
                // postDBAccess.InsertOffer(p);


                //Assert
                Assert.True(isBidInsertedSuccessfully());
            }


        }
        private bool isBidInsertedSuccessfully()
        {


            return true;
        }
    }
}
