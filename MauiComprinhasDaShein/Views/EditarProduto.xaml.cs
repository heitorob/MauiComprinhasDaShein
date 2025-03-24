using MauiComprinhasDaShein.Models;

namespace MauiComprinhasDaShein.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto produto_anexado = BindingContext as Produto;

			Produto p = new Produto
			{
				id = produto_anexado.id,
				descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text)
            };
			await App.Db.Update(p);
			await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
			await Navigation.PopAsync();
		}
		catch (Exception ex)
		{
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

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

            bool resposta = await DisplayAlert("Confirmação", "Deseja realmente apagar este registro?", "Sim", "Não");

            if (!resposta)
                return;

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