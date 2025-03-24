using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiComprinhasDaShein.Models;
using SQLite;

namespace MauiComprinhasDaShein.Helpers
{
    /// <summary>
    /// Classe auxiliar para gerenciar operações no banco de dados SQLite de forma assíncrona.
    /// </summary>
    public class SQLiteDatabaseHelper
    {
        /// <summary>
        /// Conexão assíncrona com o banco de dados SQLite.
        /// </summary>
        readonly SQLiteAsyncConnection _conn;

        /// <summary>
        /// Inicializa uma nova instância da classe <see cref="SQLiteDatabaseHelper"/> e cria a tabela Produto se não existir.
        /// </summary>
        /// <param name="rota">Caminho do banco de dados SQLite.</param>
        public SQLiteDatabaseHelper(string rota)
        {
            _conn = new SQLiteAsyncConnection(rota);
            _conn.CreateTableAsync<Produto>().Wait(); // Bloqueia a thread atual até a criação da tabela.
        }

        /// <summary>
        /// Insere um novo produto na tabela Produto.
        /// </summary>
        /// <param name="p">Objeto Produto a ser inserido.</param>
        /// <returns>Retorna o número de linhas afetadas.</returns>
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <param name="p">Objeto Produto contendo os novos valores e o ID do registro a ser atualizado.</param>
        /// <returns>Uma lista contendo os produtos afetados pela atualização.</returns>
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET descricao=?, Quantidade=?, Preco=? WHERE id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.descricao, p.Quantidade, p.Preco, p.id
            );
        }

        /// <summary>
        /// Exclui um produto da tabela com base no ID.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <returns>Retorna o número de linhas afetadas.</returns>
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.id == id);
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados na tabela.
        /// </summary>
        /// <returns>Uma lista contendo todos os produtos.</returns>
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        /// <summary>
        /// Busca produtos com base na descrição.
        /// </summary>
        /// <param name="q">Termo de pesquisa para a descrição do produto.</param>
        /// <returns>Uma lista contendo os produtos que correspondem à busca.</returns>
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE ?";
            return _conn.QueryAsync<Produto>(sql, "%" + q + "%");
        }
    }
}
