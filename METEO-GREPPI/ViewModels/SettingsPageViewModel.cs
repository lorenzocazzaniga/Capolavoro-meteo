using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Json;

namespace METEO_GREPPI.ViewModels
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        static readonly HttpClient client = new HttpClient();
               
    }
}
