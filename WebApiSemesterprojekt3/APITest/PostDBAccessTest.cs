using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Xunit;
using Models;
using System.Runtime.CompilerServices;
using Xunit.Abstractions;
using APITest;

namespace WebApi.Database.Tests
{



    public class PostDBAccessTests : IDisposable
    {


        readonly private IPostDBAccess _postAccess;
        private readonly ITestOutputHelper _extraOutpuit;


        public PostDBAccessTests(ITestOutputHelper output)
        {
            _extraOutpuit = output;
            IConfiguration inConfig = TestConfigHelper.GetConfigurationRoot();
            _postAccess = new PostDBAccess(inConfig);

        }

        public void Dispose()
        {

        }



        [Fact]
        public void GetBidPosts_ReturnsListOfBids()
        {


            // Act
            IEnumerable<Bid> result = _postAccess.GetBidPosts();

            // Assert
            Assert.IsType<List<Bid>>(result);



        }
        [Fact]
        public void GetOffetPosts_returnListOfOffers()
        {

            //Act
            IEnumerable<Offer> result = _postAccess.GetOfferPosts();

            //Assert
            Assert.IsType<List<Offer>>(result);


        }








        //[Fact]
        //public void InsertBid_InsertsBidOnDB()
        //{


        //    //Arrange
        //    Currency cu = new Currency("USD"); ;

        //    Bid p = new Bid
        //    {
        //        Transactions = null,
        //        Currency = cu,
        //        Price = 100,
        //        Amount = 100,
        //        IsComplete = false,
        //    };

        //    //!!fylder databasen med test data, hver gang man kører alle tests!!
        //    //Act
        //    _postAccess.InsertBid(p);


        //    //Assert
        //    Assert.True(isBidInsertedSuccessfully());

        //}

        //[Fact]
        //public void InsertOffer_InsertsOfferOnDB()
        //{

        //    //Arrange

        //    Currency cu = new Currency("USD"); ;

        //    Offer p = new Offer
        //    {
        //        Transactions = null,
        //        Currency = cu,
        //        Price = 100,
        //        Amount = 100,
        //        IsComplete = false,
        //    };

        //    //!!fylder databasen med test data, hver gang man kører alle tests!!
        //    //Act
        //    _postAccess.InsertOffer(p);


        //    //Assert
        //    Assert.True(isBidInsertedSuccessfully());
        //}

         private bool isBidInsertedSuccessfully()
    {


        return true;
    }

    }
   
}

