using System.Text;
using System.Text.Json;
using AristosTest.Shared.DataModel;
using AristosTest.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AristosTest.Web.Pages.Reservations
{
    public class ReservationAddModel : PageModel
    {
        [BindProperty]
        public Reservation Reservation { get; set; }
        
        [BindProperty]
        public string Message { get; set; }
        
        public SelectList AirPortSelect { get; set; }
        public SelectList AirLineSelect { get; set; }
        public SelectList PassengerTypeSelect { get; set; }

        private static HttpClient _httpClient = new();

        public ReservationAddModel() {}

        public async Task OnGet()
        {
            Message = string.Empty;

            AirPortSelect = new SelectList(await GetAirPorts(), nameof(Airport.Id), nameof(Airport.Name));
            AirLineSelect = new SelectList(await GetAirLines(), nameof(AirLine.Id), nameof(AirLine.Name));
            PassengerTypeSelect = new SelectList(await GetPassengerTypes(), nameof(PassengerType.Id), nameof(PassengerType.SelectText));
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
        
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(Reservation);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var result = await _httpClient.PostAsync($"{GlobalValues.ApiUrl}/addReservation", data);

                if (result.IsSuccessStatusCode)
                {
                    Message = $"Se creo Reserva con id {await result.Content.ReadAsStringAsync()}";
                    return RedirectToPage("ReservationList");
                }
                else
                {
                    Message = await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return Page();
        }
        
    }
}
