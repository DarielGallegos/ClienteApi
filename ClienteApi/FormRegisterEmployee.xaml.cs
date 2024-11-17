using ClienteApi.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
namespace ClienteApi;

public partial class FormRegisterEmployee : ContentPage
{
	private readonly string token;
	private readonly int? id;
	private Employee employee = new Employee();
    private static HttpClient client = new()
    {
        BaseAddress = new Uri("http://192.168.100.78/")
    };
    public FormRegisterEmployee(Employee? e, string token)
	{
		InitializeComponent();
		this.token = token;
        this.id = 0;
        if (e != null) {
			this.employee = e;
			this.id = e.Id;
			loadDataForm(e);
		}
	}


	private void loadDataForm(Employee e) {
		nombreTxt.Text = e.Nombre;
		telefonoTxt.Text = e.Telefono;
        cedulaTxt.Text = e.Cedula;
		direccionTxt.Text = e.Direccion;
		string date = e.Fecha.Split("T")[0];
		string[] dateAlone = date.Split("-");
		int año = Convert.ToInt32(dateAlone[0]);
		int mes = Convert.ToInt32(dateAlone[1]);
		int dia = Convert.ToInt32(dateAlone[2]);
		dateField.Date = new DateTime(año, mes, dia);
    }
    private void RegisterBtn_Clicked(object sender, EventArgs e)
    {
		getData();
		try
		{
			insertAndUpdateRegister();
		}
		catch (WebException err) { 
			Console.WriteLine(err.Message);
        }
    }

	private void getData() {
		employee.Id = id;
		employee.Nombre = nombreTxt.Text;
		employee.Telefono = telefonoTxt.Text;
		employee.Cedula = cedulaTxt.Text;
		employee.Direccion = direccionTxt.Text;
		employee.Fecha = dateField.Date.ToString("yyyy-MM-dd");
    }

	private async void insertAndUpdateRegister() {
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
		HttpResponseMessage response;
        if (id != 0)
		{
            response = await client.PutAsJsonAsync<Employee>("empleados/"+id, employee);
        }
        else { 
			response = await client.PostAsJsonAsync<Employee>("empleados/",employee);
        }
		if (response.IsSuccessStatusCode)
		{
			var content = await response.Content.ReadFromJsonAsync<ResponseApi>();
			if (content != null)
			{
                await DisplayAlert("Mensaje", content.Message, "OK");
                Navigation.PopAsync();
            }
		}
    }

}