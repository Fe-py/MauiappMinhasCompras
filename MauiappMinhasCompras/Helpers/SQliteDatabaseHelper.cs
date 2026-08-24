using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiappMinhasCompras.Models;

namespace MauiappMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        // Cria a conexão com o banco de dados
        readonly SQLiteAsyncConnection _conn;

        // Recebe o caminho onde o banco será armazenado
        public SQLiteDatabaseHelper(string path)
        {
            // Cria a conexão com o banco SQLite
            _conn = new SQLiteAsyncConnection(path);

            // Cria a tabela Produto caso ela ainda não exista
            _conn.CreateTableAsync<Produto>().Wait();
        }

        // Insere um novo produto no banco de dados
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        // Atualiza os dados de um produto existente
        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE Id=?";

            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        // Exclui um produto pelo seu Id
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        // Busca todos os produtos cadastrados
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        // Pesquisa produtos de acordo com a descrição
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE ?";

            return _conn.QueryAsync<Produto>(sql, "%" + q + "%");
        }
    }
}