using Microsoft.AspNetCore.Components;

namespace ProHealth.Pages.MedicalRecords;

public partial class Search : ComponentBase
{
    readonly Breadcrumb[] breadcrumbs = new Breadcrumb[]
    {
        new()
        {
            Name = "Medical Records",
            Link = "/medical-records"
        },
        new()
        {
            Name = "Search",
            Link = "/medical-records/search"
        },
    };

    readonly string[] headers = new[]
    {
        "Family Name",
        "Forename",
        "Date of Birth",
        "Postcode",
        "Address"
    };

    string[][] rows = Array.Empty<string[]>();

    [Inject] IMedicalRecordsService MedicalRecordsService { get; set; } = default!;

    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadData();

        await base.OnInitializedAsync();
    }

    async Task NavigateToSelected(int row)
    {
        var record = await MedicalRecordsService.GetByNameAsync($"{rows[row][1]} {rows[row][0]}");

        if (record is null)
        {
            return;
        }

        NavigationManager.NavigateTo($"/medical-records/details/{record.Id}");
    }

    async Task LoadData()
    {
        var medicalRecords = (await MedicalRecordsService.GetAllAsync()).ToList();

        if (!medicalRecords.Any())
        {
            rows = new string[][] { new[] { "-", "-", "-", "-", "-", } };
            return;
        }

        rows = new string[medicalRecords.Count][];

        for (int i = 0; i < medicalRecords.Count;  i++)
        {
            rows[i] = new[]
            { 
                medicalRecords[i].Patient.FamilyName,
                medicalRecords[i].Patient.Forename,
                medicalRecords[i].Patient.DateOfBirth?.ToLongDateString() ?? string.Empty,
                medicalRecords[i].Patient.Address.Postcode,
                $"{medicalRecords[i].Patient.Address.HouseNameOrNumber} {medicalRecords[i].Patient.Address.Street1} {medicalRecords[i].Patient.Address.City}",
            };
        }
    }

    async Task FilterByForename(ChangeEventArgs args)
    {
        if (args.Value is not string input)
        {
            return;
        }

        var medicalRecords = (await MedicalRecordsService.GetAllAsync())
            .Where(t => t.Patient.Forename.StartsWith(input, StringComparison.CurrentCultureIgnoreCase))
            .ToList();

        if (!medicalRecords.Any())
        {
            rows = new string[][] { new[] { "-", "-", "-", "-", "-", } };
            return;
        }

        rows = new string[medicalRecords.Count][];

        for (int i = 0; i < medicalRecords.Count; i++)
        {
            rows[i] = new[]
            {
                medicalRecords[i].Patient.FamilyName,
                medicalRecords[i].Patient.Forename,
                medicalRecords[i].Patient.DateOfBirth?.ToLongDateString() ?? string.Empty,
                medicalRecords[i].Patient.Address.Postcode,
                $"{medicalRecords[i].Patient.Address.HouseNameOrNumber} {medicalRecords[i].Patient.Address.Street1} {medicalRecords[i].Patient.Address.City}",
            };
        }
    }

    async Task FilterByFamilyName(ChangeEventArgs args)
    {
        if (args.Value is not string input)
        {
            return;
        }

        var medicalRecords = (await MedicalRecordsService.GetAllAsync())
            .Where(t => t.Patient.FamilyName.StartsWith(input, StringComparison.CurrentCultureIgnoreCase))
            .ToList();

        if (!medicalRecords.Any())
        {
            rows = new string[][] { new[] { "-", "-", "-", "-", "-", } };
            return;
        }

        rows = new string[medicalRecords.Count][];

        for (int i = 0; i < medicalRecords.Count; i++)
        {
            rows[i] = new[]
            {
                medicalRecords[i].Patient.FamilyName,
                medicalRecords[i].Patient.Forename,
                medicalRecords[i].Patient.DateOfBirth?.ToLongDateString() ?? string.Empty,
                medicalRecords[i].Patient.Address.Postcode,
                $"{medicalRecords[i].Patient.Address.HouseNameOrNumber} {medicalRecords[i].Patient.Address.Street1} {medicalRecords[i].Patient.Address.City}",
            };
        }
    }

    async Task FilterByDateOfBirth(ChangeEventArgs args)
    {
        if (!DateTime.TryParse(args.Value as string, out var input))
        {
            return;
        }

        if (input.Year < 1950)
        {
            return;
        }

        var medicalRecords = (await MedicalRecordsService.GetAllAsync())
            .Where(t => t.Patient.DateOfBirth?.ToShortDateString() == input.ToShortDateString())
            .ToList();

        if (!medicalRecords.Any())
        {
            rows = new string[][] { new[] { "-", "-", "-", "-", "-", } };
            return;
        }

        rows = new string[medicalRecords.Count][];

        for (int i = 0; i < medicalRecords.Count; i++)
        {
            rows[i] = new[]
            {
                medicalRecords[i].Patient.FamilyName,
                medicalRecords[i].Patient.Forename,
                medicalRecords[i].Patient.DateOfBirth?.ToLongDateString() ?? string.Empty,
                medicalRecords[i].Patient.Address.Postcode,
                $"{medicalRecords[i].Patient.Address.HouseNameOrNumber} {medicalRecords[i].Patient.Address.Street1} {medicalRecords[i].Patient.Address.City}",
            };
        }
    }
}
