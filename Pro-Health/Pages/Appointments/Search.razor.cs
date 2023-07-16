using Microsoft.AspNetCore.Components;

namespace ProHealth.Pages.Appointments;

public partial class Search
{
    readonly Breadcrumb[] _breadcrumbs =
    {
        new() { Name = "Appointments", Link = "/" },
        new() { Name = "Search", Link = "/appointments/search" }
    };

    readonly string[] _headers =
    {
        "Family Name",
        "Forename",
        "Date Seen",
        "GP / Nurse Seen",
    };

    string[][] _rows = Array.Empty<string[]>();

    DateOnly _startDate = DateOnly.FromDateTime(DateTime.Now);

    DateOnly _endDate = DateOnly.FromDateTime(DateTime.Now);

    [Inject] IAppointmentService AppointmentService { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await LoadAppointments();

        await base.OnInitializedAsync();
    }

    async Task LoadAppointments()
    {
        var appointments = (await AppointmentService.GetAllAsync())
            .OrderBy(a => a.DateOfAppointment)
            .ToList();

        if (!appointments.Any())
        {
            _rows = new[] { new[] { "-", "-", "-", "-", } };
            return;
        }

        _rows = new string[appointments.Count][];
        
        for (int i = 0; i < appointments.Count; i++)
        {
            _rows[i] = new string[]
            { 
                appointments[i].Patient.FamilyName,
                appointments[i].Patient.Forename,
                appointments[i].DateOfAppointment.ToLongDateString(),
                appointments[i].Seeing is null ? "-" : $"{appointments[i].Seeing.Forename} {appointments[i].Seeing.FamilyName}",
            };
        }
    }

    async Task Filter(string date, ChangeEventArgs args)
    {
        if (date == "start")
        {
            _startDate = DateOnly.Parse(args.Value as string);
        }
        else
        {
            _endDate = DateOnly.Parse(args.Value as string);
        }

        var appointments = (await AppointmentService.GetAllAsync()).ToList();

        if (!appointments.Any(a => a.DateOfAppointment >= _startDate && a.DateOfAppointment <= _endDate))
        {
            _rows = new[] { new[] { "-", "-", "-", "-", } };
            return;
        }

        appointments = appointments
            .Where(a => a.DateOfAppointment >= _startDate && a.DateOfAppointment <= _endDate)
            .OrderBy(a => a.DateOfAppointment)
            .ToList();

        _rows = new string[appointments.Count][];

        for (int i = 0; i < appointments.Count; i++)
        {
            _rows[i] = new string[]
            {
                appointments[i].Patient.FamilyName,
                appointments[i].Patient.Forename,
                appointments[i].DateOfAppointment.ToLongDateString(),
                appointments[i].Seeing is null ? "-" : $"{appointments[i].Seeing.Forename} {appointments[i].Seeing.FamilyName}",
            };
        }
    }
}
