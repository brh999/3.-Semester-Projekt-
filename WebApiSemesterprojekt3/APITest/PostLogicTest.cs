using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Xunit;
using Models;
using System.Runtime.CompilerServices;
using WebApi.BuissnessLogiclayer;
using APITest;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Xunit.Abstractions;

namespace WebApi.Database.Tests
{



    public class PostLogicTest : IDisposable
    {


        readonly private IPostDBAccess _postAccess;
        private readonly ITestOutputHelper _extraOutpuit;


        public PostLogicTest(ITestOutputHelper output)
        {
            _extraOutpuit = output;
            IConfiguration inConfig = TestConfigHelper.GetConfigurationRoot();
            _postAccess = new PostDBAccess(inConfig);


        }

        public void Dispose()
        {

        }



        [Fact]
        public void InsertNullOfferShouldReturnError()
        {
            //Assign
            Offer offer = null;
            //Act
            //Assert
            Assert.ThrowsAny<ArgumentNullException>(() => { _postAccess.InsertOffer(offer); });

        }

        [Fact]
        public void InsertValidOfferShouldReturnTheSameOffer()
        {
            //Assign
            Currency currency = new("USD");
            Offer offer = new(100,10,currency,-1);
            Offer? result = null;
            //Act
            _postAccess.InsertOffer(offer);
            
            //This assumes that GetOfferPost() works
            _postAccess.GetOfferPosts();
            //Assert
            
        }
    }
}
