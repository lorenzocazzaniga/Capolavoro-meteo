namespace METEO_GREPPI.Views;

public partial class SettingsPage : ContentPage
{
    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        Preferences.Default.Set("geolocationDefault", e.Value); //salvo la decisione dell'utente (preferenze)
    }
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        geolocationSwitch.IsToggled = Preferences.Default.Get("geolocationDefault",false); //lo switch della geolocalizzazione all'avvio sarà spento
    }
}