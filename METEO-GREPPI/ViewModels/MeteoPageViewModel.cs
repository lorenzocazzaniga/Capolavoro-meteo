using CommunityToolkit.Mvvm.ComponentModel;
using METEO_GREPPI.Model;

namespace METEO_GREPPI.ViewModels;

public partial class MeteoPageViewModel: ObservableObject
{

    [ObservableProperty]
    Forecast forecast;

    public static DateTime UnixTimeStampToDateTime(long unixTime)
    {
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        DateTime dateTime = unixEpoch.AddSeconds(unixTime);

        return dateTime;
    }
}

