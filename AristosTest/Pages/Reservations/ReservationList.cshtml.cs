using System.Text;
using System.Text.Json;
using AristosTest.Shared.DataModel;
using AristosTest.Shared.Model.Airport;
using AristosTest.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AristosTest.Web.Pages;

public class ReservationListModel : PageModel
{
    [BindProperty] public List<Reservation>? ReserveList { get; set; }
    [BindProperty] public FilterRequest Filter { get; set; }
    [BindProperty] public bool ShowFilter { get; set; }
    [BindProperty] public string Message { get; set; }

    public SelectList AirPortSelect { get; set; }
    public SelectList AirLineSelect { get; set; }
    public SelectList PassengerTypeSelect { get; set; }
    private static HttpClient _httpClient = new();

    public async Task OnGetAsync()
    {
        Filter = new FilterRequest();
        await UpdateData();
    }

    public async Task OnPostAsync()
    {
        await UpdateData();
    }

    public async Task UpdateData()
    {
        try
        {
            var json = JsonSerializer.Serialize(Filter);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{GlobalValues.ApiUrl}/getListReservationByFilter", data);

            if (response.IsSuccessStatusCode)
            {
                var listReservations =
                    JsonSerializer.Deserialize<List<Reservation>>(await response.Content.ReadAsStringAsync(),
                        GlobalValues.SerializerOptions);
                ReserveList = listReservations;
            }
            else
            {
                Message = "Error al consultar lista!";
            }
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }

    private async Task<List<PassengerType>?> GetPassengerTypes()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{GlobalValues.ApiUrl}/getAllPassengerTypes");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<PassengerType>>(result, GlobalValues.SerializerOptions);
                return list;
            }

            Message = await response.Content.ReadAsStringAsync();
            return null;
        }
        catch (Exception ex)
        {
            Message = ex.Message;
            return null;
        }
    }

    private async Task<List<AirLine>?> GetAirLines()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{GlobalValues.ApiUrl}/getAllAirlines");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<AirLine>>(result, GlobalValues.SerializerOptions);
                return list;
            }

            Message = await response.Content.ReadAsStringAsync();
            return null;
        }
        catch (Exception ex)
        {
            Message = ex.Message;
            return null;
        }
    }

    private async Task<List<Airport>?> GetAirPorts()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{GlobalValues.ApiUrl}/getAllAirports");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<Airport>>(result, GlobalValues.SerializerOptions);
                return list;
            }

            Message = await response.Content.ReadAsStringAsync();
            return null;
        }
        catch (Exception ex)
        {
            Message = ex.Message;
            return null;
        }
    }

    public async Task OnPostToggleFilterAsync()
    {
        ShowFilter = !ShowFilter;
        if (ShowFilter)
        {
            AirPortSelect = new SelectList(await GetAirPorts(), nameof(Airport.Id), nameof(Airport.Name));
            AirLineSelect = new SelectList(await GetAirLines(), nameof(AirLine.Id), nameof(AirLine.Name));
            PassengerTypeSelect = new SelectList(await GetPassengerTypes(), nameof(PassengerType.Id), nameof(PassengerType.SelectText));
        }

        await UpdateData();
    }

    public async Task OnPostClearFilter()
    {
        Filter = new FilterRequest();
        await UpdateData();
    }

}
