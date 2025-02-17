using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiComprinhasDaShein.Models;
using SQLite;

namespace MauiComprinhasDaShein.Helpers
{
    public class SQLiteDatabaseHelper
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
            string sql = "UPDATE Produto SET descricao=?, Quantidade=?, Preco=? WHERE id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.descricao, p.Quantidade, p.Preco, p.id
            );
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + q + "%'";
            return _conn.QueryAsync<Produto>(sql);
        }
    }
}
