using SQLite;

namespace MauiappMinhasCompras.Models;

public class Produto
{
    // Cria uma variável privada para armazenar a descrição do produto
    String _descricao;

    // Define o Id como chave primária e gera o valor automaticamente
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Armazena e controla a descrição do produto
    public string Descricao
    {
        // Retorna o valor armazenado na variável _descricao
        get => _descricao;

        // Define um novo valor para a descrição
        set
        {
            // Verifica se a descrição não foi preenchida
            if (value == null)
            {
                // Exibe uma mensagem informando que a descrição deve ser preenchida
                throw new Exception("por favor, preencha a descição ");
            }
            else
            {
                // Armazena o valor informado na variável _descricao
                _descricao = value;
            }
        }
    }

    // Armazena a quantidade do produto
    public int Quantidade { get; set; }

    // Armazena o preço do produto
    public double Preco { get; set; }

    // Calcula automaticamente o valor total do produto
    public double Total { get => Quantidade * Preco; }

    // Armazena a categoria do produto
    public String Categoria { get; set; }

    // Armazena a data em que o produto foi comprado
    public DateTime Data { get; set; }
}