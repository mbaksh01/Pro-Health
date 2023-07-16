namespace ProHealth.Services.Abstractions;

internal interface IStateService
{
    MedicalRecord? CurrentMedicalRecord { get; }

    Task SetMedicalRecordAsync(MedicalRecord medicalRecord);
}
