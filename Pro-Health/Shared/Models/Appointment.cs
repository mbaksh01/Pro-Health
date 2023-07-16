using System.Text.Json.Serialization;

namespace ProHealth.Shared.Models;

internal sealed class Appointment
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName ("patient")]
    public required Person Patient { get; set; }

    [JsonPropertyName ("dateOfAppointment")]
    public DateOnly DateOfAppointment { get; set; }

    [JsonPropertyName("timeOfAppointment")]
    public TimeOnly TimeOfAppointment { get; set; }

    [JsonPropertyName("reason")]
    public string? Reason { get; set; }

    [JsonPropertyName("otherReason")]
    public string? OtherReason { get; set; }

    [JsonPropertyName("clinic")]
    public string? Clinic { get; set; }

    [JsonPropertyName("seeing")]
    public Person? Seeing { get; set; }

    [JsonPropertyName("notes")]
    public string? Notes { get; set; }

    [JsonIgnore]
    public static readonly Appointment Empty = new()
    {
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
    };
}
