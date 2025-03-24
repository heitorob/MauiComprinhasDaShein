using MauiComprinhasDaShein.Models;

namespace MauiComprinhasDaShein.Views;

public partial class NovoProduto : ContentPage
{
    /// <summary>
    /// Classe responsável pela criação de um novo produto.
    /// </summary>
    public NovoProduto()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Método acionado ao clicar no botão de salvar. Adiciona um novo produto ao banco de dados.
    /// </summary>
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Criação de um novo objeto Produto com os valores informados pelo usuário
            Produto p = new Produto
            {
                descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Insere o produto no banco de dados
            await App.Db.Insert(p);

            // Exibe mensagem de sucesso
            await DisplayAlert("Sucesso!", "Registro Inserido", "OK");

            // Retorna à tela anterior
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Exibe mensagem de erro caso ocorra alguma exceção
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}