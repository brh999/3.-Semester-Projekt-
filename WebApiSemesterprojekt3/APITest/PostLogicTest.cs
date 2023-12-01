using APITest;
using Microsoft.Extensions.Configuration;
using Models;
using System.Data.SqlClient;
using Xunit.Abstractions;

namespace WebApi.Database.Tests
{



    public class PostLogicTest : IDisposable
    {


        readonly private IPostDBAccess _postAccess;
        private readonly ITestOutputHelper _extraOutpuit;
        private readonly string aspNetUserIdTestData = "c811de3f-ab3c-4445-8d70-612e68d61c93",
                                _connectionString;

        public PostLogicTest(ITestOutputHelper output)
        {
            _extraOutpuit = output;
            IConfiguration inConfig = TestConfigHelper.GetConfigurationRoot();
            _postAccess = new PostDBAccess(inConfig);
            _connectionString = inConfig.GetConnectionString("hildur_prod");


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
            Assert.ThrowsAny<NullReferenceException>(() => { _postAccess.InsertOffer(offer, aspNetUserIdTestData); });

        }

        [Fact]
        public void GetAllPostsShouldReturnAllPost()
        {
            //Assign
            List<Post> posts;
            int expectedAmount = 0;
            string query = "SELECT COUNT(*) AS 'RowCount' FROM posts";
            //Act
            posts = (List<Post>)_postAccess.GetAllPosts();
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
            List<Offer> list;
            Currency currency = new("USD");
            Offer offer = new(100, 10, currency, -1);
            Offer? result;
            //Act

            _postAccess.InsertOffer(offer, aspNetUserIdTestData);
            // At this moment, we do not have the functionality to find a specific offer.
            list = (List<Offer>)_postAccess.GetOfferPosts();
            result = list.Find(x =>
            {
                bool isEqual = false;
                isEqual = x.Price == offer.Price;
                isEqual = x.Currency.Equals(offer.Currency);
                isEqual = x.Amount == offer.Amount;
                isEqual = x.IsComplete == offer.IsComplete;

                return isEqual;
            });

            //Assert

            //This assumes that GetOfferPost() works
            Assert.NotNull(result);



        }

        [Fact]
        public void GetRelatedTransactionsShouldReturnAllRelatedTransactions()
        {
            //Assign
            int postId1 = 9;
            int postId2 = 12;

            //Act
            //Assert
            GetRelatedTransactionsTest(9);
            GetRelatedTransactionsTest(12);
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

        public void GetRelatedTransactionsShouldThrowExceptionWithNegativeInterger()
        {
            //Assign
            int postId1 = -1;
            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => GetRelatedTransactionsTest(postId1));
        }

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
            relatedTransactions = (List<TransactionLine>)_postAccess.GetTransactionLines(postId);

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

            Assert.Equal(relatedTransactions.Count, expectedAmount);

        }
    }
}
