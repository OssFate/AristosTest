namespace AristosTest.Shared.DataModel;

public class Reservation
{
    public int Id { get; set; }
    public Airport Origin { get; set; }
    public DateTime DepartureTime { get; set; }
    public Airport Destination { get; set; }
    public DateTime ArrivalTime { get; set; }
    public AirLine @AirLine { get; set; }
    public string FlyNumber { get; set; }
    public PassengerType @PassengerType { get; set; }
}
