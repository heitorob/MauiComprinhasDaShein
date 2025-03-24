using MauiComprinhasDaShein.Models;

namespace MauiComprinhasDaShein.Views;

public partial class EditarProduto : ContentPage
{
    /// <summary>
    /// Classe da View EditarProduto respons�vel por manipular a edi��o e exclus�o de um produto.
    /// </summary>
    public EditarProduto()
    {
        InitializeComponent();
    }

    /// <summary>
    /// M�todo acionado ao clicar no bot�o de salvar. Atualiza os dados do produto no banco.
    /// </summary>
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obt�m o produto associado ao contexto da p�gina
            Produto produto_anexado = BindingContext as Produto;

            // Cria um novo objeto Produto com os valores atualizados
            Produto p = new Produto
            {
                id = produto_anexado.id,
                descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text)
            };

            // Atualiza o produto no banco de dados
            await App.Db.Update(p);
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    /// <summary>
    /// M�todo acionado ao clicar no bot�o de apagar. Remove o produto do banco.
    /// </summary>
    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try
        {
            Produto p = BindingContext as Produto;

            if (p == null)
            {
                await DisplayAlert("Erro", "Nenhum registro selecionado.", "OK");
                return;
            }

            // Solicita confirma��o do usu�rio antes da exclus�o
            bool resposta = await DisplayAlert("Confirma��o", "Deseja realmente apagar este registro?", "Sim", "N�o");

            if (!resposta)
                return;

            // Exclui o produto do banco de dados
            await App.Db.Delete(p.id);
            await DisplayAlert("Sucesso!", "Registro Apagado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}