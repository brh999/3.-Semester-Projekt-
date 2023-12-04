using APITest;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Data.SqlClient;
using WebApi.BuissnessLogiclayer;
using WebApi.Database;
using Xunit.Abstractions;


namespace APITest
{
    public class AccountLogicTest : IDisposable
    {

        
        private readonly ITestOutputHelper _extraOutpuit;
        private readonly string aspNetUserId,
                                _connectionString;
        private AccountLogic _accountLogic;
        private readonly IAccountDBAccess _accountDBAccess;




        public AccountLogicTest(ITestOutputHelper output)
        {
            _extraOutpuit = output;
            IConfiguration inConfig = TestConfigHelper.GetConfigurationRoot();
            _connectionString = inConfig.GetConnectionString("hildur_prod");
            aspNetUserId = "c811de3f-ab3c-4445-8d70-612e68d61c93";
            _accountDBAccess = new AccountDBAccess(inConfig);
            _accountLogic = new AccountLogic(_accountDBAccess);


        }
        public void Dispose()
        {
            
        }

        /// <summary>
        /// This test can fail, if the test overlap with the test
        /// InsertValidOfferShouldReturnTheSameOffer.
        /// </summary>
        [Fact]
        public void GetAccountByIdWithValidIdShouldReturnAccountWithThatId()
        {
            //Assign
            Account? account;
            string expectedEmail = "mathias@gmail.com";
            double expectedDiscount = 1;
            int expectedId = 1,
                expectedAmountOfPosts = 0;

            string postQuery = "SELECT COUNT(*) AS 'RowCount' FROM posts Where posts.account_id_fk = " + expectedId.ToString();
            //Act

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = postQuery;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expectedAmountOfPosts += (int)reader["RowCount"];
                        }
                    }
                }
            }

            account = _accountLogic.GetAccountById(aspNetUserId);
            //Assert
            if(account == null)
            {
                Assert.Fail("No account was found");
            }

            Assert.Equal(account.Email, expectedEmail);
            Assert.True(account.Discount == expectedDiscount);
            Assert.True(account.Id == expectedId);
            if(account.Posts.Count != expectedAmountOfPosts)
            {
                Assert.Fail($"Account: {account.Posts.Count}    Expected amount:{expectedAmountOfPosts} ");
            }
        }

        [Fact]

        public void GetAccountByIdWithInvalidIdShouldEmptyAccount()
        {
            //Assign
            Account? account = null;
            string invalidId = "Invalid-Id-123-123-123";
            //Act
            account = _accountLogic.GetAccountById(invalidId);
            //Assert
            Assert.Equal(account.Email, "");
            Assert.True(account.Discount == 0);
            Assert.True(account.Id == -1);
            Assert.True(account.Posts.Count == 0);

        }

        [Fact]
        public void GetAllAccountsShouldReturnAllAccounts()
        {
            //Assign
            int expectedAmountOfAccounts = 0;
            string accountQuery = "SELECT COUNT(*) AS 'RowCount' FROM Accounts";
            List<Account> accounts = new List<Account>();
            //Act
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = accountQuery;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expectedAmountOfAccounts += (int)reader["RowCount"];
                        }
                    }
                }
            }

            accounts = _accountLogic.GetAllAccounts();
            //Assert

            Assert.True (accounts.Count == expectedAmountOfAccounts);
        }

        [Fact]
        public void GetRelatedCurrencyLinesWithValidIdShouldReturnAllRelatedCurrencyLines()
        {
            //Assign
            int id = 1;
            int expectedAmount = 0;
            string currencyLineQuery = "SELECT COUNT(*) AS 'RowCount' FROM CurrencyLines WHERE CurrencyLines.account_id_fk = 1";
            List<CurrencyLine> currencyLines = new List<CurrencyLine>();
            //Act
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = currencyLineQuery;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expectedAmount += (int)reader["RowCount"];
                        }
                    }
                }
            }

            currencyLines = _accountLogic.GetRelatedCurrencyLines(id);
            //Assert

            Assert.True(currencyLines.Count == expectedAmount);
        }

        [Fact]
        public void GetRelatedCurrencyLinesWithNotValidIdShouldReturnAllRelatedCurrencyLines()
        {
            //Assign
            int id = 1;
            int expectedAmount = 0;
            string currencyLineQuery = "SELECT COUNT(*) AS 'RowCount' FROM CurrencyLines WHERE CurrencyLines.account_id_fk = 1";
            List<CurrencyLine> currencyLines = new List<CurrencyLine>();
            //Act
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = currencyLineQuery;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            expectedAmount += (int)reader["RowCount"];
                        }
                    }
                }
            }

            currencyLines = _accountLogic.GetRelatedCurrencyLines(id);
            //Assert

            Assert.True(currencyLines.Count == expectedAmount);
        }
    }
}
