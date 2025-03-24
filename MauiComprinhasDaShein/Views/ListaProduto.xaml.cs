namespace MauiComprinhasDaShein.Views;

using System.Collections.ObjectModel;
using MauiComprinhasDaShein.Models;

public partial class ListaProduto : ContentPage
{
    /// <summary>
    /// Classe responsável pelo gerenciamento da lista de produtos na interface.
    /// </summary>
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();

        // Define a coleção de produtos como fonte de dados para a ListView
        lst_produtos.ItemsSource = lista;
    }

    /// <summary>
    /// Método acionado quando a página aparece na tela. Atualiza a lista de produtos.
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
    /// Método acionado ao clicar no botão de adicionar um novo produto.
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
    /// Método acionado ao clicar no botão de soma. Calcula o total dos produtos na lista.
    /// </summary>
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    /// <summary>
    /// Método acionado quando o usuário digita na barra de pesquisa. Filtra os produtos.
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
    /// Método acionado quando a lista de produtos é atualizada manualmente pelo usuário.
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
    /// Método acionado quando um item da lista é selecionado. Abre a tela de edição do produto.
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