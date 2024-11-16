using ClienteApi.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ClienteApi;

public partial class ListEmpleados : ContentPage
{
	private readonly HttpClient client;
    public ListEmpleados(string token)
	{
		InitializeComponent();
        client = new HttpClient();
		getList(token);
    }

	private async void getList(string token)
	{
		var url = new Uri("http://192.168.100.78/empleados/");
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		using HttpResponseMessage response = await client.GetAsync(url);
		if (response.IsSuccessStatusCode)
		{
			var content = await response.Content.ReadFromJsonAsync<List<Employee>>();
			if (content != null)
			{
				employeesListView.ItemsSource = content;
            }
		}
	}
}