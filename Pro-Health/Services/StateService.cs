namespace ProHealth.Services;

internal class StateService : IStateService
{
    public MedicalRecord? CurrentMedicalRecord { get; private set; }

    public Task SetMedicalRecordAsync(MedicalRecord medicalRecord)
    {
        CurrentMedicalRecord = medicalRecord ?? throw new ArgumentNullException(nameof(medicalRecord));
        
        return Task.CompletedTask;
    }
}
