using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Npgsql;
using ExchangeSharp;

namespace TradingBot.Data.Models
{
    public class TestModelRepository
    {
        string connectionString = Constants.DefaultConnection;
        public List<TestModel> GetTestModels()
        {
            List<TestModel> TestModels = new List<TestModel>();
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                TestModels = db.Query<TestModel>("SELECT * FROM testmodel").ToList();
            }
            return TestModels;
        }



        public List<ExchangeTicker> GetExchangeTickers()
        {
            List<ExchangeTicker> TestModels = new List<ExchangeTicker>();
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                TestModels = db.Query<ExchangeTicker>("SELECT name as marketsymbol, * FROM testmodel").ToList();
            }
            return TestModels;
        }


        public int CreateExchangeTicker(ExchangeTicker ticker)
        {
            int resultId = 0;

            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO TestModel (Name) VALUES(@MarketSymbol) returning id;";
                resultId = db.Query<int>(sqlQuery, ticker).FirstOrDefault();
                //TestModel.Id = TestModelId;
            }
            return resultId;
        }



        public TestModel Get(int id)
        {
            TestModel TestModel = null;
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                TestModel = db.Query<TestModel>("SELECT * FROM TestModels WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return TestModel;
        }

        public TestModel Create(TestModel TestModel)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO TestModels (Name, Age) VALUES(@Name, @Age); SELECT CAST(SCOPE_IDENTITY() as int)";
                int? TestModelId = db.Query<int>(sqlQuery, TestModel).FirstOrDefault();
                TestModel.Id = TestModelId;
            }
            return TestModel;
        }

        public void Update(TestModel TestModel)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE TestModels SET Name = @Name, Age = @Age WHERE Id = @Id";
                db.Execute(sqlQuery, TestModel);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM TestModels WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
    }
}
