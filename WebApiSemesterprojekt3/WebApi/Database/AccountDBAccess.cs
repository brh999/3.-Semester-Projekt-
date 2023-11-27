﻿using Dapper;
using System.Data.SqlClient;
using Models;
using Microsoft.AspNetCore.Http;
using Models.DTO;

namespace WebApi.Database
{
    public class AccountDBAccess : IAccountDBAccess
    {
        private string? _connectionString;
        private IConfiguration _configuration { get; set; }

        public AccountDBAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("hildur_prod");
        }

        public bool DeleteAccountById(int id)
        {
            throw new NotImplementedException();
        }

        public Account GetAccountById(int id)
        {
            Account account = null;

            string queryStringAccount = "SELECT email,username,discount FROM AspNetUsers JOIN Accounts ON Accounts.AspNetUsers_id_fk = AspNetUsers.Id WHERE Accounts.id = @id";
            string queryStringWallet = "select * from CurrencyLines where Account_id_fk = @accountId";


            using (SqlConnection conn = new SqlConnection(_connectionString)) {
                conn.Open();
                string email = "",
                        username = "";
                double discount = 0;

                List<CurrencyLine> wallet = new List<CurrencyLine>();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    

                    cmd.CommandText = queryStringAccount;
                    cmd.Parameters.AddWithValue("id", id);
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            email = (string)reader["email"];
                            username = (string)reader["username"];
                            discount = (double)reader["discount"];
                        }
                        
                       
                    }

                    cmd.CommandText = queryStringWallet;
                    cmd.Parameters.AddWithValue("AccountId", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        
                        while(reader.Read())
                        {
                            string currencyType = (string)reader["type"];
                            double amount = (double)reader["amount"];

                            Currency currency = new(currencyType);

                            CurrencyLine line = new CurrencyLine(amount,currency);

                            wallet.Add(line);


                            
                        }
                    }

                    account = new Account(id,discount,username,email,wallet, new List<Post>());

                }
            }


            return account;
        }

        public List<Account> GetAllAccounts()
        {
            List<Account> res = null;
            

            //TODO lav "*" om til individuelle kollonner
            string queryString = "select * from accounts";

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                //TODO add error chekcing mayhaps?
                //res = conn.Query<Account,Post,Account>(queryString, (account, post)=> {account.post = post return account }).ToList();

            }
            return res;
        }

        public bool UpdateAccountById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
