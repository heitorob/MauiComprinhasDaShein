using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiComprinhasDaShein.Models;
using SQLite;

namespace MauiComprinhasDaShein.Helpers
{
    internal class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string rota)
        {
            _conn = new SQLiteAsyncConnection(rota);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";
        }
    }
}
