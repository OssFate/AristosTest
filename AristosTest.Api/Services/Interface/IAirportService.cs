using AristosTest.Shared.DataModel;
using AristosTest.Shared.Model.Airport;

namespace AristosTest.Api.Services.Interface;

public interface IAirportService
{
    Task<int> AddReservation(Reservation reservation);
    Task<List<Reservation>> GetListReservationByFilter(FilterRequest filter);
    Task<List<Airport>> GetAllAirports();
    Task<List<PassengerType>> GetAllPassengerTypes();
    Task<List<AirLine>> GetAllAirlines();
}
