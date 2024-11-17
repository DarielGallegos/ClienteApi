using ClienteApi.Models;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace ClienteApi;

public partial class ListEmpleados : ContentPage
{
	private static HttpClient client = new() { 
		BaseAddress = new Uri("http://192.168.100.78/")
	};
	private readonly string token;
	private Employee employeeSelected = null;
	private List<Employee> employees = new List<Employee>();
	public ListEmpleados(string token)
	{
		InitializeComponent();
        this.token = token;
		loadData();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
		loadData();
    }
	private async void loadData() {
        try
        {
            getList(token);
        }
        catch (WebException err)
        {
            Console.WriteLine(err.Message);
        }
    }
    private async void getList(string token)
	{
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		using HttpResponseMessage response = await client.GetAsync("empleados/");
		if (response.IsSuccessStatusCode)
		{
			var content = await response.Content.ReadFromJsonAsync<List<Employee>>();
			if (content != null)
			{
				employees = content;
                employeesListView.ItemsSource = employees;
            }
		}
	}

    private void employeesListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		if (e.SelectedItem is Employee selectedEmployee) {
			employeeSelected = selectedEmployee;	
		}
    }
    private void UpdateBtn_Clicked(object sender, EventArgs e)
    {
		if (employeeSelected != null) {
            Navigation.PushAsync(new FormRegisterEmployee(employeeSelected, token));
        }
    }


	private async void DeleteBtn_Clicked(object sender, EventArgs e)
    {
		if (employeeSelected != null) {
			deleteEmployee(token);
        }

    }

	private async void deleteEmployee(string token) {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        using HttpResponseMessage response = await client.DeleteAsync($"empleados/{employeeSelected.Id}");
		if (response.IsSuccessStatusCode)
		{
			var content = await response.Content.ReadFromJsonAsync<ResponseApi>();
			if (content != null)
			{
				loadData();
                await DisplayAlert("Mensaje", content.Message, "OK");
			}
		}
    }

    private void AddBtn_Clicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new FormRegisterEmployee(null, token));
    }
}