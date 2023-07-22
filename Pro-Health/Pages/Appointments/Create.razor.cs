using Microsoft.AspNetCore.Components;
using ProHealth.Helpers;

namespace ProHealth.Pages.Appointments;

public partial class Create
{
    readonly Breadcrumb[] _breadcrumbs =
    {
        new() { Name = "Appointments", Link = "" },
        new() { Name = "Create", Link = "/appointments/create" },
    };

    readonly FieldValue[] _fieldValues =
    {
        new() { Field = "Patient" },
        new() { Field = "Date of Appointment" },
        new() { Field = "Time of Appointment" },
        new() { Field = "Reason" },
        new() { Field = "Other Reason" },
        new() { Field = "Clinic", Value = "Avenue Medical Practice" },
    };

    Appointment _appointment = Appointment.Empty;

    [Inject] IMedicalRecordsService MedicalRecordsService { get; set; } = default!;

    [Inject] IAppointmentService AppointmentService { get; set; } = default!;

    [Inject] NavigationManager NavigationManager { get; set; } = default!;

    [Parameter] public Guid Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var medicalRecord = await MedicalRecordsService.GetByIdAsync(Id);

        if (medicalRecord is null)
        {
            return;
        }

        _fieldValues[0].Value = medicalRecord.Patient.FullName;

        await base.OnInitializedAsync();
    }

    async Task Save()
    {
        var medicalRecord = await MedicalRecordsService.GetByNameAsync(_fieldValues[0].Value);

        medicalRecord ??= await MedicalRecordsService.GetByIdAsync(Id);

        if (medicalRecord is null)
        {
            return;
        }

        _appointment.Patient = medicalRecord.Patient;

        await AppointmentService.Create(_appointment.MapFields(_fieldValues));

        NavigationManager.NavigateTo("/appointments/search");
    }
}
