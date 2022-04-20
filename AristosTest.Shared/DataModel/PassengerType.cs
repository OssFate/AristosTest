using System.ComponentModel.DataAnnotations.Schema;

namespace AristosTest.Shared.DataModel;

public class PassengerType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    [NotMapped]
    public string SelectText => $"{Name} - {Price}";
}
