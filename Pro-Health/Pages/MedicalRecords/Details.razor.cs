using Microsoft.AspNetCore.Components;
using ProHealth.Helpers;

namespace ProHealth.Pages.MedicalRecords;

public partial class Details : ComponentBase
{
    readonly Breadcrumb[] _breadcrumbs = new Breadcrumb[]
    {
        new()
        {
            Name = "Medical Records",
            Link = "",
        },
        new()
        {
            Name = "Details",
            Link = "/medical-records/details",
        },
        new()
        {
            Name = string.Empty,
        },
    };

    FieldValue[] _generalValues = default!;

    FieldValue[] _addressValues = default!;

    FieldValue[] _contactInfoValues = default!;

    readonly string[] _resultsHeaders = new[]
    {
        "Type",
        "Request Reason",
        "ETA",
        "Request Date",
    };

    readonly string[][] _newResults = new string[][]
    {
        new[] { "-", "-", "-", "-" },
    };

    readonly string[][] _pendingResults = new string[][]
    {
        new[] { "Blood", "Blood Sugar Levels", "Tomorrow", "Three days ago" },
        new[] { "Urine", "Kidney Function", "1st July", "One day ago" },
    };

    MedicalRecord _medicalRecord = default!;

    [Parameter] public Guid Id { get; set; }

    [Inject] IMedicalRecordsService MedicalRecordsService { get; set; } = default!;

    [Inject] IStateService StateService { get; set; } = default!;

    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        MedicalRecord? medicalRecord = null;

        if (Id == Guid.Empty)
        {
            medicalRecord = StateService.CurrentMedicalRecord;
        }

        medicalRecord ??= await MedicalRecordsService.GetByIdAsync(Id);
        medicalRecord ??= (await MedicalRecordsService.GetAllAsync()).First();

        await StateService.SetMedicalRecordAsync(medicalRecord);

        _medicalRecord = medicalRecord;

        _breadcrumbs[2].Name = $"{medicalRecord.Patient.Forename} {medicalRecord.Patient.FamilyName}";
        _breadcrumbs[2].Link = $"medical-records/details/{medicalRecord.Id}";

        SetGeneralInfo();
        SetAddressInfo();
        SetContactInfo();

        await base.OnInitializedAsync();
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

    async Task Update()
    {
        await MedicalRecordsService.Update(_medicalRecord
            .MapGeneralFields(_generalValues)
            .MapAddressFields(_addressValues)
            .MapContactFields(_contactInfoValues)
        );

        _breadcrumbs[2].Name = $"{_medicalRecord.Patient.Forename} {_medicalRecord.Patient.FamilyName}";
    }

    void BookAppointment()
    {
        NavigationManager.NavigateTo($"/appointments/create/{_medicalRecord.Id}");
    }
}
