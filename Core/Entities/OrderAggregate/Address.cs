namespace Core.Entities.OrderAggregate;

public class Address
{
    public Address()
    {
    }

    public Address(string firstName, string secondName, string lastName, string country, string province,
        string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Country = country;
        Province = province;
        PhoneNumber = phoneNumber;
        SecondName = secondName;
    }

    public string FirstName { get; set; }  = null!;
    public string SecondName { get; set; }  = null!;
    public string LastName { get; set; }  = null!;
    public string Country { get; set; }  = null!;
    public string Province { get; set; }  = null!;
    public string PhoneNumber { get; set; }  = null!;
}