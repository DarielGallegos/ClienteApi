using ClienteApi.Models;
using System.Net.Http.Json;
namespace ClienteApi
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient client;
        public MainPage()
        {
            InitializeComponent();
            client = new HttpClient();
        }
        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                login(client, userTxt.Text, passwordTxt.Text);
            }
            catch (HttpRequestException ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private async void login(HttpClient c, string user, string passwd) {
                var baseUrl = "http://192.168.100.78/auth/";
                var uri = new Uri($"{baseUrl}?username={user}&password={passwd}");
            Console.WriteLine($"{baseUrl}?username={user}&password={passwd}");
            using HttpResponseMessage response = await c.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<LoginResponse>();
                Console.WriteLine(content);
                if (content?.Status == true)
                {
                    Console.WriteLine(content.Token);
                    await Navigation.PushAsync(new ListEmpleados(content.Token));
                }
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
 
        }
    }

}
