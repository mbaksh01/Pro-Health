using System.Text.Json.Serialization;

namespace ProHealth.Shared.Models;

/// <summary>
/// Medical record containing all the required information on a single patient.
/// </summary>
internal sealed class MedicalRecord
{
    /// <summary>
    /// Unique id of this record.
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    /// <summary>
    /// Patient linked to this medical record.
    /// </summary>
    [JsonPropertyName("patient")]
    public required Person Patient { get; set; }

    /// <summary>
    /// Details of next of kin.
    /// </summary>
    [JsonPropertyName("nextOfKin")]
    public required NextOfKin NextOfKin { get; set; }

    /// <summary>
    /// General notes regarding the patient.
    /// </summary>
    [JsonPropertyName("notes")]
    public string Notes { get; set; } = string.Empty;

    [JsonIgnore]
    public static readonly MedicalRecord Empty = new()
    {
        Id = Guid.NewGuid(),
        Patient = new()
        {
            Id = Guid.NewGuid(),
            FamilyName = string.Empty,
            Forename = string.Empty,
            Address = new()
            {
                HouseNameOrNumber = string.Empty,
                Street1 = string.Empty,
                City = string.Empty,
                Postcode = string.Empty,
                Country = string.Empty,
            },
            ContactInfo = new()
            {
                PrimaryContact = string.Empty,
            },
        },
        NextOfKin = new()
        {
            Name = string.Empty,
            ContactInfo = new()
            {
                PrimaryContact = string.Empty,
            },
        }
    };
}

/// <summary>
/// Stores all data needed to represent a person.
/// </summary>
internal sealed class Person
{
    /// <summary>
    /// Unique id of this person.
    /// </summary>
    [JsonPropertyName("id")]
    public required Guid Id { get; set; }

    /// <summary>
    /// Persons family name (surname). 
    /// </summary>
    [JsonPropertyName("familyName")]
    public required string FamilyName { get; set; }

    /// <summary>
    /// Persons forename (first name).
    /// </summary>
    [JsonPropertyName("forename")]
    public required string Forename { get; set; }

    public string FullName => $"{Forename} {FamilyName}";

    /// <summary>
    /// Date and time of when this person was born.
    /// </summary>
    [JsonPropertyName("dateOfBirth")]
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Gender of this person.
    /// </summary>
    [JsonPropertyName("gender")]
    public Gender Gender { get; set; }

    /// <summary>
    /// Address of where this patient lives.
    /// </summary>
    [JsonPropertyName("address")]
    public required Address Address { get; set; }

    /// <summary>
    /// Contact info which can be used to reach the patient.
    /// </summary>
    [JsonPropertyName("contactInfo")]
    public required ContactInfo ContactInfo { get; set; }
}

/// <summary>
/// Represent a persons gender.
/// </summary>
internal enum Gender
{
    Male,
    Female,
}

/// <summary>
/// Stores all data needed to represent an address.
/// </summary>
internal sealed class Address
{
    /// <summary>
    /// Name or number of the address.
    /// </summary>
    [JsonPropertyName("houseNameOrNumber")]
    public required string HouseNameOrNumber { get; set; }

    /// <summary>
    /// First line of the address, i.e. Maple Street.
    /// </summary>
    [JsonPropertyName("street1")]
    public required string Street1 { get; set; }

    /// <summary>
    /// Second line of the address.
    /// </summary>
    [JsonPropertyName("street2")]
    public string Street2 { get; set; } = string.Empty;

    /// <summary>
    /// Address city.
    /// </summary>
    [JsonPropertyName("city")]
    public required string City { get; set; }

    /// <summary>
    /// Address county
    /// </summary>
    [JsonPropertyName("county")]
    public string County { get; set; }

    /// <summary>
    /// Address postcode.
    /// </summary>
    [JsonPropertyName("postcode")]
    public required string Postcode { get; set; }

    /// <summary>
    /// Address country.
    /// </summary>
    [JsonPropertyName("country")]
    public required string Country { get; set; }
}

/// <summary>
/// Stores all the data associated with contact information.
/// </summary>
internal sealed class ContactInfo
{
    /// <summary>
    /// Primary contact info.
    /// </summary>
    [JsonPropertyName("primaryContact")]
    public required string PrimaryContact { get; set; } = string.Empty;

    /// <summary>
    /// Secondary contact info.
    /// </summary>
    [JsonPropertyName("secondaryContact")]
    public string SecondaryContact { get; set; } = string.Empty;
}

/// <summary>
/// Stores all data associated with a patients next of kin.
/// </summary>
internal sealed class NextOfKin
{
    /// <summary>
    /// Name of next of kin.
    /// </summary>
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Relationship to the <see cref="Person"/>.
    /// </summary>
    [JsonPropertyName("relationship")]
    public string Relationship { get; set; } = string.Empty;

    /// <summary>
    /// Info on how to contact next of kin.
    /// </summary>
    [JsonPropertyName("contactInfo")]
    public required ContactInfo ContactInfo { get; set; }
}
