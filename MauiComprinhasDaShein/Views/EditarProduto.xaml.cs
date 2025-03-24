using MauiComprinhasDaShein.Models;

namespace MauiComprinhasDaShein.Views;

public partial class EditarProduto : ContentPage
{
    /// <summary>
    /// Classe da View EditarProduto responsável por manipular a edição e exclusão de um produto.
    /// </summary>
    public EditarProduto()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Método acionado ao clicar no botão de salvar. Atualiza os dados do produto no banco.
    /// </summary>
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Obtém o produto associado ao contexto da página
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
    /// Método acionado ao clicar no botão de apagar. Remove o produto do banco.
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

            // Solicita confirmação do usuário antes da exclusão
            bool resposta = await DisplayAlert("Confirmação", "Deseja realmente apagar este registro?", "Sim", "Não");

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