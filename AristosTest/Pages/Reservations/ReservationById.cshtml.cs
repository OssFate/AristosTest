using System.Text;
using System.Text.Json;
using AristosTest.Shared.DataModel;
using AristosTest.Shared.Model.Airport;
using AristosTest.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AristosTest.Web.Pages.Reservations
{
    public class ReservationByIdModel : PageModel
    {
        [BindProperty] public Reservation? Reservation { get; set; }
        [BindProperty] public string Message { get; set; }
        [BindProperty] public int ReservationId { get; set; }
        

        private static HttpClient _httpClient = new();
        
        public async Task OnPostAsync()
        {
            try
            {
                var request = new FilterRequest { Id = ReservationId };
                var json = JsonSerializer.Serialize(request);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{GlobalValues.ApiUrl}/getListReservationByFilter", data);

                if (response.IsSuccessStatusCode)
                {
                    var listReservations = JsonSerializer.Deserialize<List<Reservation>>(await response.Content.ReadAsStringAsync(), GlobalValues.SerializerOptions);
                    var result = listReservations?.FirstOrDefault();
                    if(result == null)
                        Message = "No se encontro la Reserva!";
                    Reservation = result;
                }
                else
                {
                    Message = "No se encontro la Reserva!";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
