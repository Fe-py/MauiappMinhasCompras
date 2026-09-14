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

            // Executa o comando SQL para atualizar o produto
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

            // Executa a pesquisa utilizando o texto informado
            return _conn.QueryAsync<Produto>(sql, "%" + q + "%");
        }

        // Pesquisa produtos de acordo com a categoria
        public Task<List<Produto>> SearchCategoria(string categoria)
        {
            // Cria o comando SQL para buscar a categoria informada
            string sql = "SELECT * FROM Produto WHERE Categoria = ?";

            // Executa a pesquisa utilizando a categoria escolhida
            return _conn.QueryAsync<Produto>(sql, categoria);
        }

        // Pesquisa produtos de acordo com uma data específica
        public Task<List<Produto>> SearchData(DateTime data_produto)
        {
            // Cria o comando SQL para buscar produtos pela data
            string sql = "SELECT * FROM Produto WHERE Data = ?";

            // Executa a pesquisa utilizando a data informada
            return _conn.QueryAsync<Produto>(sql, data_produto);
        }

        // Pesquisa produtos dentro de um período de datas
        public Task<List<Produto>> SearchPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            // Cria o comando SQL para buscar produtos entre a data inicial e final
            string sql = "SELECT * FROM Produto WHERE Data >= ? AND Data <= ?";

            // Executa a pesquisa utilizando as duas datas informadas
            return _conn.QueryAsync<Produto>(sql, dataInicial, dataFinal);
        }

    }
}