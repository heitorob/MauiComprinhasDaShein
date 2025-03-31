# MauiComprinhasDaShein

## Introdução
MauiComprinhasDaShein é um aplicativo desenvolvido com .NET MAUI que funciona como um carrinho de compras. Ele permite cadastrar, atualizar e remover produtos, além de calcular automaticamente o total da compra.

## Funcionalidades
- Adicionar produtos ao carrinho
- Atualizar informações dos produtos
- Remover produtos do carrinho
- Exibir a soma total dos valores da compra

## Instalação

### Clone o repositório
```sh
git clone https://github.com/seu-usuario/MauiComprinhasDaShein.git
```
cd MauiComprinhasDaShein

### Restaure os pacotes
```sh
dotnet restore
```

### Execute o projeto
```sh
dotnet build
dotnet run
```

## Capturas de Tela
![Tela Inicial](images/tela_inicial.png)
![Tela do Carrinho](images/tela_carrinho.png)

## Demonstração
![Demonstração](images/demo.gif)

## Estrutura do Código
### Banco de Dados
O aplicativo utiliza SQLite para armazenar os produtos cadastrados.

#### Classe `SQLiteDatabaseHelper`
```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauiComprinhasDaShein.Models;
using SQLite;

namespace MauiComprinhasDaShein.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string rota)
        {
            _conn = new SQLiteAsyncConnection(rota);
            _conn.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "UPDATE Produto SET descricao=?, Quantidade=?, Preco=? WHERE id=?";
            return _conn.QueryAsync<Produto>(sql, p.descricao, p.Quantidade, p.Preco, p.id);
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE ?";
            return _conn.QueryAsync<Produto>(sql, "%" + q + "%");
        }
    }
}
```

### Modelos de Dados

#### Classe `Produto`
```csharp
using System;
using SQLite;

namespace MauiComprinhasDaShein.Models
{
    public class Produto
    {
        private string _descricao;

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        public string descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }
                _descricao = value;
            }
        }

        public double Quantidade { get; set; }

        public double Preco { get; set; }

        public double Total { get => Quantidade * Preco; }
    }
}
```

### Interface do Usuário

#### Tela de Listagem de Produtos (`ListaProdutos.xaml`)
```xml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MauiComprinhasDaShein.Views.ListaProduto"
             Title="Minhas Compras">
    <ContentPage.ToolbarItems>
        <ToolbarItem Text="Adicionar" Clicked="ToolbarItem_Clicked"/>
        <ToolbarItem Text="Somar" Clicked="ToolbarItem_Clicked_1"/>
    </ContentPage.ToolbarItems>

    <StackLayout>
        <SearchBar x:Name="txt_search" Placeholder="Busca de Produtos" TextChanged="txt_search_TextChanged"/>
        <Frame>
            <ListView x:Name="lst_produtos" IsPullToRefreshEnabled="True" Refreshing="lst_produtos_Refreshing" ItemSelected="lst_produtos_ItemSelected">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <ViewCell>
                            <Grid ColumnDefinitions="30, 100, 80, 50, 90">
                                <Label Grid.Column="0" Text="{Binding id}"/>
                                <Label Grid.Column="1" Text="{Binding descricao}"/>
                                <Label Grid.Column="2" Text="{Binding Preco, StringFormat='{}{0:c}'}"/>
                                <Label Grid.Column="3" Text="{Binding Quantidade}"/>
                                <Label Grid.Column="4" Text="{Binding Total, StringFormat='{}{0:c}'}"/>
                            </Grid>
                        </ViewCell>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
        </Frame>
    </StackLayout>
</ContentPage>
```

#### Código-Behind `ListaProdutos.xaml.cs`
```csharp
using System;
using System.Collections.ObjectModel;
using System.Linq;
using MauiComprinhasDaShein.Models;

namespace MauiComprinhasDaShein.Views
{
    public partial class ListaProduto : ContentPage
    {
        ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

        public ListaProduto()
        {
            InitializeComponent();
            lst_produtos.ItemsSource = lista;
        }

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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            double soma = lista.Sum(i => i.Total);
            DisplayAlert("Total dos Produtos", $"O total é {soma:C}", "OK");
        }

        private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string q = e.NewTextValue;
                lista.Clear();
                List<Produto> tmp = await App.Db.Search(q);
                tmp.ForEach(i => lista.Add(i));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private async void lst_produtos_Refreshing(object sender, EventArgs e)
        {
            lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));
            lst_produtos.IsRefreshing = false;
        }

        private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Produto p = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProduto { BindingContext = p });
        }
    }
}
```

## Contribuição
Caso queira contribuir, faça um fork do repositório, crie uma branch e envie um pull request.

## Licença
MIT License
