using Microsoft.AspNetCore.Components;
using ProHealth.Helpers;

namespace ProHealth.Pages.MedicalRecords;

public partial class Create
{
    Breadcrumb[] breadcrumbs =
    {
        new() { Name = "Medical Records", Link = "" },
        new() { Name = "Create", Link = "/medical-records/create" },
    };

    FieldValue[] _generalValues = default!;

    FieldValue[] _addressValues = default!;

    FieldValue[] _contactInfoValues = default!;

    [Inject] IMedicalRecordsService MedicalRecordsService { get; set; } = default!;

    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    private MedicalRecord _medicalRecord = default!;

    protected override void OnInitialized()
    {
        _medicalRecord = MedicalRecord.Empty;

        SetGeneralInfo();
        SetAddressInfo();
        SetContactInfo();

        base.OnInitialized();
    }
    
    void SetGeneralInfo()
    {
        _generalValues = new FieldValue[]
        {
            new()
            {
                Field = "Family Name",
                Value = _medicalRecord.Patient.FamilyName
            },
            new()
            {
                Field = "Forename",
                Value = _medicalRecord.Patient.Forename
            },
            new()
            {
                Field = "Date of Birth",
                Value = _medicalRecord.Patient.DateOfBirth?.ToLongDateString(),
            },
            new()
            {
                Field = "Gender",
                Value = _medicalRecord.Patient.Gender.ToString()
            },
        };
    }

    void SetAddressInfo()
    {
        _addressValues = new FieldValue[]
        {
            new()
            {
                Field = "House Name / Number",
                Value = _medicalRecord.Patient.Address.HouseNameOrNumber
            },
            new()
            {
                Field = "Address Line 1",
                Value = _medicalRecord.Patient.Address.Street1
            },
            new()
            {
                Field = "Address Line 2",
                Value = _medicalRecord.Patient.Address.Street2
            },
            new()
            {
                Field = "City",
                Value = _medicalRecord.Patient.Address.City
            },
            new()
            {
                Field = "County / Region",
                Value = _medicalRecord.Patient.Address.Country
            },
            new()
            {
                Field = "Postcode",
                Value = _medicalRecord.Patient.Address.Postcode
            },
            new()
            {
                Field = "Country",
                Value = _medicalRecord.Patient.Address.Country
            },
        };
    }

    void SetContactInfo()
    {
        _contactInfoValues = new FieldValue[]
        {
            new()
            {
                Field = "Primary Contact",
                Value = _medicalRecord.Patient.ContactInfo.PrimaryContact
            },
            new()
            {
                Field = "Secondary Contact",
                Value = _medicalRecord.Patient.ContactInfo.SecondaryContact
            },
            new()
            {
                Field = "Next of Kin Name",
                Value = _medicalRecord.NextOfKin.Name
            },
            new()
            {
                Field = "Next of Kin Relation",
                Value = _medicalRecord.NextOfKin.Relationship
            },
            new()
            {
                Field = "Next of Kin Primary Contact",
                Value = _medicalRecord.NextOfKin.ContactInfo.PrimaryContact
            },
            new()
            {
                Field = "Next of Kin Secondary Contact",
                Value = _medicalRecord.NextOfKin.ContactInfo.PrimaryContact
            },
        };
    }

    async Task Save()
    {
        await MedicalRecordsService.Create(_medicalRecord
            .MapGeneralFields(_generalValues)
            .MapAddressFields(_addressValues)
            .MapContactFields(_contactInfoValues)
        );

        NavigationManager.NavigateTo($"/medical-records/details/{_medicalRecord.Id}");
    }

    void Cancel()
    {
        NavigationManager.NavigateTo("/");
    }
}
