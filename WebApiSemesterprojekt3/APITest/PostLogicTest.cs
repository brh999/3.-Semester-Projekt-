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
            _postLogic = new(_postAccess, inConfig);

        }

        public void Dispose()
        {

        }

        [Fact]
        public void GetAllPostsShouldReturnAllPostOffers()
        {
            //try to make sure this is the last test.
            System.Threading.Thread.Sleep(3000);
            //Assign
            List<Post> posts;
            int expectedAmount = 0;
            string query = "SELECT COUNT(*) AS 'RowCount' FROM posts WHERE posts.type = 'Offer'";
            //Act
            posts = (List<Post>)_postLogic.GetAllOffers();
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
                            expectedAmount = (int)reader["RowCount"];
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

            List<Post> list;
            Currency currency = new("BTC");
            Post offer = new(100, 10, currency, 1000, "Offer");
            Post? result;
            //Act

            result = _postLogic.InsertOffer(offer, aspNetUserIdTestData);

            //Assert
            //This assumes that GetOfferPost() works
            if (result == null)
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

            if (result == null)
            {
                Assert.Fail("Result is null");
            }
            Assert.Equal(result, offer);
        }

        [Fact]
        public void InsertOfferWithInvalidIdShouldFail()
        {
            //Assign
            Currency currency = new("USD");
            Post offer = new(100, 10, currency, -1, "Offer");

            string userId = "Invalid-id-123-123-123";
            //Act
            //Assert

            Assert.Throws<ArgumentException>(() => _postLogic.InsertOffer(offer, userId));

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
            Assert.Throws<ArgumentException>(() => GetRelatedTransactionsTest(postId1));
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

        //Concurrency Test
        [Theory]
        [InlineData(null)]

        public void BuyOfferTest(Post offer = null, int delay = 0)
        {
            //Assign
            List<Post> posts;
            bool isBought = false;
            

            bool isCompleteOffer = false,
                isCompleteBid = false;

            //It is known that the account id with the test aspNetId is 1;
            int accountId = 1,
                   foundAccountId = 0;


            //Act

            //This method inserts a new offer into the database
            //We assume that the offer is inserted.
            InsertValidOfferShouldReturnTheSameOffer();

            posts = (List<Post>)_postLogic.GetAllOffers();

            if (posts.Count > 0 && offer == null)
            {
                for (int i = 0; i < posts.Count; i++)
                {
                    if (posts[i].IsComplete == false)
                    {
                        offer = posts[i];
                        i = posts.Count + 1;
                    }
                }
            }
            else if (offer == null)
            {
                Assert.Fail("No offer was found");
            }



            isBought = _postLogic.BuyOffer(offer, aspNetUserIdTestData);

            //Confirm that a transaction has been made and the column isComplete is true
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = "SELECT isComplete FROM posts WHERE id =" + offer.Id.ToString();
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                isCompleteOffer = (bool)reader["isComplete"];
                            }
                        }
                    }

                    //Find the bid id from the transaction
                    query = "SELECT * FROM Transactions WHERE Post_offer_id_fk =" + offer.Id.ToString();
                    int foundBidId = 0;
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                foundBidId = (int)reader["post_bid_id_fk"];
                            }
                        }

                    }

                    if (foundBidId == 0)
                    {
                        Assert.Fail("No bid id was found");
                    }

                    query = "SELECT * FROM posts WHERE id =" + foundBidId.ToString();

                    //Find the bid, and check that is associated with the correct account
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                isCompleteBid = (bool)reader["isComplete"];
                                foundAccountId = (int)reader["account_id_fk"];
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }


            //Assert
            Assert.True(isBought);
            Assert.True(isCompleteOffer);
            Assert.True(isCompleteBid);
            Assert.True(accountId == foundAccountId);
        }


        [Fact]
        public async void BuyOfferTestConcurrency()
        {
            //Assign
            _postLogic.InsertOffer(new Post(10, 2, new Currency("BTC"), 0, "Offer"),aspNetUserIdTestData);
            Post offer = null;
            bool t1Result = false,
                t2Result = false;

            Random rnd = new Random();
            int delay = rnd.Next(100);

            List<Post> posts = (List<Post>)_postLogic.GetAllOffers();



            //Act
            if (posts.Count > 0 && offer == null)
            {
                for (int i = 0; i < posts.Count; i++)
                {
                    if (posts[i].IsComplete == false)
                    {
                        offer = posts[i];
                        i = posts.Count + 1;
                    }
                }
            }
            else if (offer == null)
            {
                Assert.Fail("No offer was found");
            }

            try
            {
                Task<bool> t1 = new Task<bool>(() => _postLogic.BuyOffer(offer, aspNetUserIdTestData));
                Task<bool> t2 = new Task<bool>(() => _postLogic.BuyOffer(offer, aspNetUserIdTestData, delay));

                t1.Start();
                t2.Start();

                Task<bool>[] tasks = { t1, t2 };

                
                Task.WhenAll(tasks).Wait();


                t1Result = tasks[0].Result;
                t2Result = tasks[1].Result;
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            

            // Assert

            Assert.True(t1Result != t2Result);

        }









    }
}