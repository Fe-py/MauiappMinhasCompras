using SQLite;

namespace MauiappMinhasCompras.Models;

public class Produto
{
    // Define o Id como chave primária e gera o valor automaticamente
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    // Armazena a descrição do produto
    public string Descricao { get; set; }

    // Armazena a quantidade do produto
    public int Quantidade { get; set; }

    // Armazena o preço do produto
    public double Preco { get; set; }

    public double Total { get => Quantidade * Preco; }
}