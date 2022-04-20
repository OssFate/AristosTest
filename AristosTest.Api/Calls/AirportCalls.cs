using AristosTest.Api.Services.Interface;
using AristosTest.Shared.DataModel;
using AristosTest.Shared.Model.Airport;

namespace AristosTest.Api.Calls;

public static class AirportCalls
{

    public static void SetCalls(WebApplication app)
    {
        const string baseApiUrl = "api/airport";
        const string baseApiTag = "Airport";
        
        app.MapPost($"{baseApiUrl}/addReservation", async (Reservation reservation, IAirportService airportService) =>
        {
            return await airportService.AddReservation(reservation);
        }).WithTags(baseApiTag);

        app.MapPost($"{baseApiUrl}/getListReservationByFilter", async (FilterRequest filter, IAirportService airportService) =>
        {
            return await airportService.GetListReservationByFilter(filter);
        }).WithTags(baseApiTag);

        app.MapGet($"{baseApiUrl}/getAllAirports", async (IAirportService airportService) =>
        {
            return await airportService.GetAllAirports();
        }).WithTags(baseApiTag);

        app.MapGet($"{baseApiUrl}/getAllPassengerTypes", async (IAirportService airportService) =>
        {
            return await airportService.GetAllPassengerTypes();
        }).WithTags(baseApiTag);

        app.MapGet($"{baseApiUrl}/getAllAirlines", async (IAirportService airportService) =>
        {
            return await airportService.GetAllAirlines();
        }).WithTags(baseApiTag);

    }

}