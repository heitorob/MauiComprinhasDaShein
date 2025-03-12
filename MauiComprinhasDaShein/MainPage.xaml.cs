using MauiComprinhasDaShein.Views;

namespace MauiComprinhasDaShein
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                Navigation.PushAsync(new ListaProduto());
            }
            catch (Exception ex)
            {
                DisplayAlert("OPS!!", ex.Message, "OK");
            }
        }
    }

}
