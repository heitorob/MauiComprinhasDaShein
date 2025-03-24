using MauiComprinhasDaShein.Models;

namespace MauiComprinhasDaShein.Views;

public partial class NovoProduto : ContentPage
{
    /// <summary>
    /// Classe respons�vel pela cria��o de um novo produto.
    /// </summary>
    public NovoProduto()
    {
        InitializeComponent();
    }

    /// <summary>
    /// M�todo acionado ao clicar no bot�o de salvar. Adiciona um novo produto ao banco de dados.
    /// </summary>
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Cria��o de um novo objeto Produto com os valores informados pelo usu�rio
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

            // Retorna � tela anterior
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Exibe mensagem de erro caso ocorra alguma exce��o
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}