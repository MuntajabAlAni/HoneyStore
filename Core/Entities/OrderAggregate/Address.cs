namespace Core.Entities.OrderAggregate;

public class Address
{
    public Address()
    {
    }

    public Address(string country, string province, string phoneNumber)
    {
        Country = country;
        Province = province;
        PhoneNumber = phoneNumber;
    }

    public string Country { get; set; }  = null!;
    public string Province { get; set; }  = null!;
    public string PhoneNumber { get; set; }  = null!;
}