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
    }
}
