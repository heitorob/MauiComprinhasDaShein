namespace MauiComprinhasDaShein.Views;

using System.Collections.ObjectModel;
using MauiComprinhasDaShein.Models;

public partial class ListaProduto : ContentPage
{
    /// <summary>
    /// Classe respons�vel pelo gerenciamento da lista de produtos na interface.
    /// </summary>
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        // Define a cole��o de produtos como fonte de dados para a ListView
        lst_produtos.ItemsSource = lista;
    }

    /// <summary>
    /// M�todo acionado quando a p�gina aparece na tela. Atualiza a lista de produtos.
    /// </summary>
    protected async override void OnAppearing()
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    /// <summary>
    /// M�todo acionado ao clicar no bot�o de adicionar um novo produto.
    /// </summary>
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    /// <summary>
    /// M�todo acionado ao clicar no bot�o de soma. Calcula o total dos produtos na lista.
    /// </summary>
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total � {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    /// <summary>
    /// M�todo acionado quando o usu�rio digita na barra de pesquisa. Filtra os produtos.
    /// </summary>
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;

            lst_produtos.IsRefreshing = true;

            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    /// <summary>
    /// M�todo acionado quando a lista de produtos � atualizada manualmente pelo usu�rio.
    /// </summary>
    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        try
        {
            lista.Clear();

            List<Produto> tmp = await App.Db.GetAll();

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
        finally
        {
            lst_produtos.IsRefreshing = false;
        }
    }

    /// <summary>
    /// M�todo acionado quando um item da lista � selecionado. Abre a tela de edi��o do produto.
    /// </summary>
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Produto p = e.SelectedItem as Produto;

            Navigation.PushAsync(new Views.EditarProduto
            {
                BindingContext = p,
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}