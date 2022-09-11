namespace Core.Entities.OrderAggregate;

public class Address
{
     
    public Address()
    {
    }

    public Address(string firstName, string lastName, string fullAddress, string remarks)
    {
        FirstName = firstName;
        LastName = lastName;
        FullAddress = fullAddress;
        Remarks = remarks;
    }

    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string FullAddress { get; set; } = null!;
    public string Remarks { get; set; } = null!;
}