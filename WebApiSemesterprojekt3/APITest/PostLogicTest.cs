using APITest;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Data.SqlClient;
using WebApi.BuissnessLogiclayer;
using Xunit.Abstractions;

namespace WebApi.Database.Tests
{



    public class PostLogicTest : IDisposable
    {
        readonly private IPostDBAccess _postAccess;
        private readonly ITestOutputHelper _extraOutpuit;
        private readonly string aspNetUserIdTestData = "c811de3f-ab3c-4445-8d70-612e68d61c93",
                                _connectionString;
        private PostLogic _postLogic;

        public PostLogicTest(ITestOutputHelper output)
        {
            _extraOutpuit = output;
            IConfiguration inConfig = TestConfigHelper.GetConfigurationRoot();
            _postAccess = new PostDBAccess(inConfig);
            _connectionString = inConfig.GetConnectionString("hildur_prod");
            _postLogic = new(_postAccess);

        }

        public void Dispose()
        {

        }



       

        [Fact]
        public void GetAllPostsShouldReturnAllPost()
        {
            //Assign
            List<Post> posts;
            int expectedAmount = 0;
            string query = "SELECT COUNT(*) AS 'RowCount' FROM posts";
            //Act
            posts = _postLogic.GetAllPosts();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expectedAmount += (int)reader["RowCount"];
                        }
                    }
                }
            }
            //Assert
            if (posts == null)
            {
                Assert.Fail("No post was found");
            }

            Assert.Equal(posts.Count, expectedAmount);

        }

        [Fact]
        public void InsertValidOfferShouldReturnTheSameOffer()
        {
            //Assign
            AccountDBAccess accountDB = new(TestConfigHelper.GetConfigurationRoot());
            List<Post> list;
            Currency currency = new("USD");
            Post offer = new(100, 10, currency, -1, "Offer");
            Post? result;
            //Act

            result = _postLogic.InsertOffer(offer, aspNetUserIdTestData);

            //Assert
            //This assumes that GetOfferPost() works
            if(result == null)
            {
                Assert.Fail("Result is null");
            }
            Assert.Equal(result, offer);
        }





        
        public void InsertOfferWithUserIdShouldFail(string userId)
        {
            //Assign
            Currency currency = new("USD");
            Post offer = new(100, 10, currency, -1, "Offer");
            Post? result;
            //Act
            result = _postLogic.InsertOffer(offer, userId);
            //Assert

            if( result == null)
            {
                Assert.Fail("Result is null");
            }
            Assert.Equal(result, offer);
        }

        [Fact]
        public void InsertOfferWithInvalidIdShouldFail()
        {
            //Assign
            string userId = "InvalidId-123-123-123";
            //Act
            //Assert
            InsertOfferWithUserId(userId);
        }

        [Fact]
        public void GetRelatedTransactionsShouldReturnAllRelatedTransactions()
        {
            //Assign

            //Known post with transactions
            int postId1 = 9;
            int postId2 = 12;

            //Act
            //Assert
            GetRelatedTransactionsTest(postId1);
            GetRelatedTransactionsTest(postId2);
        }


        [Fact]
        public void GetRelatedTransactionsShouldNotReturnAnyWithPostId1()
        {
            //Assign
            int postId = 1;
            //Act
            //Assert
            GetRelatedTransactionsTest(postId);
        }
        [Fact]
        public void GetRelatedTransactionsShouldThrowExceptionWithNegativeInterger()
        {
            //Assign
            int postId1 = -1;
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => GetRelatedTransactionsTest(postId1));
        }
        [Fact]
        public void GetRelatedTransactionsShouldThrowExceptionWithMoreThanMaxIntegerValue()
        {
            //Assign
            int postId1 = int.MaxValue;
            //Act
            postId1 += 1;
            //Assert
            Assert.Throws<OverflowException>(() => GetRelatedTransactionsTest(postId1));
        }

        
        public void GetRelatedTransactionsTest(int postId)
        {
            //Assign
            string query = "SELECT COUNT(*) AS 'RowCount' FROM Transactions where Transactions.Post_offer_id_fk = ";
            List<TransactionLine> relatedTransactions;
            int expectedAmount = 0;

            //Act
            relatedTransactions = _postLogic.GetRelatedTransactions(postId);

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query + postId.ToString();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expectedAmount += (int)reader["RowCount"];
                        }
                    }

                }
            }

            //Assert
            Assert.Equal(relatedTransactions.Count, expectedAmount);
        }
    }
}