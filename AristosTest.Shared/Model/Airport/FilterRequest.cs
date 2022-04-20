namespace AristosTest.Shared.Model.Airport;

public class FilterRequest
{
    public int? Id { get; set; }
    public int? PassengerTypeId { get; set; }
    public int? OriginId { get; set; }
    public int? DestinationId { get; set; }
    public DateTime? DepartureDate { get; set; }
    public DateTime? ArrivalDate { get; set; }
    public int? AirLineId { get; set; }
    public string? FlyNumber { get; set; }
}
