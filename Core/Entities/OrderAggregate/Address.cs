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

    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string LastName { get; set; }
    public string Country { get; set; }
    public string Province { get; set; }
    public string PhoneNumber { get; set; }
}