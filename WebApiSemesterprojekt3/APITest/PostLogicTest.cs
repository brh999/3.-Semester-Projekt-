using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Xunit;
using Models;
using System.Runtime.CompilerServices;
using WebApi.BuissnessLogiclayer;

namespace WebApi.Database.Tests
{



    public class PostLogicTest : IDisposable
    {


        private readonly IConfiguration _testconfiguration;
        IPostDBAccess dBAccess;
        private string? _testconnectionString;


        public PostLogicTest(IConfiguration configuration)
        {
            _testconfiguration = configuration;
            _testconnectionString = _testconfiguration.GetConnectionString("hildur_test");
            dBAccess = new PostDBAccess();
           

        }

        public void Dispose()
        {

        }



        [Fact]
        public void InsertNullValueShouldReturnError()
        {
            //Assign
            PostLogic postLogic = new(dBAccess);
            Offer offer = null;
            //Act
            //Assert
            Assert.Null(postLogic.InsertOffer(offer));

            
        }
    }
}
