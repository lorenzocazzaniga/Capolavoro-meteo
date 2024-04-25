using System.Net.Http.Json;
using System.Web;
using METEO_GREPPI.Model;
using METEO_GREPPI.ViewModels;

namespace METEO_GREPPI.Views;

public partial class MeteoPage : ContentPage
{
    HttpClient client = new HttpClient();
    public MeteoPage()
    {
        InitializeComponent();      
    }

    public async Task<Location> GetCurrentLocation()
    {
        try
        {          
            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            Location location = await Geolocation.Default.GetLocationAsync(request);

            if (location != null)
                return location;
        }

        catch (Exception ex)
        {
            
        }      
            return null;     
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await DownloadForecast();
    }
    protected override async void OnAppearing()
    {
        if (Preferences.Default.Get("geolocationDefault", false)) //ha attivato la geolocalizzazione
        {
            var posizione = await GetCurrentLocation();
            var list = await client.GetFromJsonAsync<List<ReverseGeocoding>>($"https://api.openweathermap.org/geo/1.0/reverse?lat={posizione.Latitude}&lon={posizione.Longitude}&limit=1&appid=781b31e39be05a35d98c3ee9747e651a");
            if (list.Count != 0)
            {
                entry.Text = list[0].Name;
            }
            else
            {
                entry.Text = Preferences.Default.Get("defaultCity", "Milano");
            }
        }
        else
        {
            entry.Text = Preferences.Default.Get("defaultCity", "Milano");
        }
        
        await DownloadForecast();   
    }

    public async Task DownloadForecast()
    {
        var city = entry.Text;

        var cityEncode = HttpUtility.UrlEncode(city);

        //dal nome della città restituisce le cordinate
        var list = await client.GetFromJsonAsync<List<Model.Geocoding>>($"https://api.openweathermap.org/geo/1.0/direct?q={cityEncode}&limit=1&appid=2a19b25e3d48176a76954da3712c2f72");

        if (list.Count == 0)
        {
            entry.Text = "errore";
            return;
        }

        var lat = list[0].Lat;
        var lon = list[0].Lon;


        //chiamata per ricevere le previsioni
        var forecast = await client.GetFromJsonAsync<Forecast>($"https://api.openweathermap.org/data/3.0/onecall?lat={lat}&lon={lon}&units=metric&appid=2a19b25e3d48176a76954da3712c2f72");
        viewModel.Forecast = forecast; //aggiungo tutti i dati del forecast
    }
}

