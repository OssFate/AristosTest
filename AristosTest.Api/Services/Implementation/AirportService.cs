using AristosTest.Api.Data;
using AristosTest.Api.Services.Interface;
using AristosTest.Shared.DataModel;
using AristosTest.Shared.Model.Airport;
using Microsoft.EntityFrameworkCore;

namespace AristosTest.Api.Services.Implementation;

public class AirportService : IAirportService
{
    private readonly AirportContext _airportContext;

    public AirportService(AirportContext airportContext)
    {
        _airportContext = airportContext;
    }

    public async Task<int> AddReservation(Reservation reservation)
    {
        reservation.Id = 0;

        reservation.Origin = await _airportContext.Airports.Where(a => a.Id == reservation.Origin.Id).FirstAsync();
        reservation.Destination = await _airportContext.Airports.Where(a => a.Id == reservation.Destination.Id).FirstAsync();
        reservation.AirLine = await _airportContext.AirLines.Where(a => a.Id == reservation.AirLine.Id).FirstAsync();
        reservation.PassengerType = await _airportContext.PassengerTypes.Where(a => a.Id == reservation.PassengerType.Id).FirstAsync();

        await _airportContext.Reservations.AddAsync(reservation);
        await _airportContext.SaveChangesAsync();

        return reservation.Id;
    }

    public async Task<List<AirLine>> GetAllAirlines()
    {
        var result = await _airportContext.AirLines.ToListAsync();
        return result;
    }

    public async Task<List<Airport>> GetAllAirports()
    {
        var result = await _airportContext.Airports.ToListAsync();
        return result;
    }

    public async Task<List<PassengerType>> GetAllPassengerTypes()
    {
        var result = await _airportContext.PassengerTypes.ToListAsync();
        return result;
    }

    public async Task<List<Reservation>> GetListReservationByFilter(FilterRequest filter)
    {
        var result = await _airportContext.Reservations
                                            .Where(r => (!filter.Id.HasValue || r.Id == filter.Id.Value) &&
                                            (!filter.OriginId.HasValue || r.Origin.Id == filter.OriginId) &&
                                            (!filter.DestinationId.HasValue || r.Destination.Id == filter.DestinationId) &&
                                            (!filter.DepartureDate.HasValue || r.DepartureTime.Date == filter.DepartureDate.Value.Date) &&
                                            (!filter.ArrivalDate.HasValue || r.ArrivalTime.Date == filter.ArrivalDate.Value.Date) &&
                                            (!filter.AirLineId.HasValue || r.AirLine.Id == filter.AirLineId) &&
                                            (filter.FlyNumber == null || r.FlyNumber == filter.FlyNumber) &&
                                            (filter.PassengerTypeId.HasValue || r.PassengerType.Id == filter.PassengerTypeId))
                                            .ToListAsync();

        return result;
    }

}
