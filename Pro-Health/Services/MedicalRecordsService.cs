using System.Text.Json;

namespace ProHealth.Services;

internal class MedicalRecordsService : IMedicalRecordsService
{
    private IEnumerable<MedicalRecord> _records = Enumerable.Empty<MedicalRecord>();

    private readonly HttpClient _httpClient;

    public MedicalRecordsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<MedicalRecord>> GetAllAsync()
    {
        if (_records.Any())
        {
            return _records;
        }

        string response = await _httpClient.GetStringAsync("/data/medical-records.json");

        var records = JsonSerializer.Deserialize<IEnumerable<MedicalRecord>>(response) ?? Enumerable.Empty<MedicalRecord>();

        return _records = records;
    }

    public async Task<MedicalRecord?> GetByIdAsync(Guid id)
    {
        if (!_records.Any())
        {
            string response = await _httpClient.GetStringAsync("/data/medical-records.json");

            var records = JsonSerializer.Deserialize<IEnumerable<MedicalRecord>>(response) ?? Enumerable.Empty<MedicalRecord>();

            _records = records;
        }

        return _records.SingleOrDefault(r => r.Id == id);
    }

    public async Task<MedicalRecord?> GetByNameAsync(string name)
    {
        if (!_records.Any())
        {
            string response = await _httpClient.GetStringAsync("/data/medical-records.json");

            var records = JsonSerializer.Deserialize<IEnumerable<MedicalRecord>>(response) ?? Enumerable.Empty<MedicalRecord>();

            _records = records;
        }

        return _records.SingleOrDefault(r => 
            $"{r.Patient.Forename.ToLower()} {r.Patient.FamilyName.ToLower()}" == name.ToLower()
        );
    }

    public Task Create(MedicalRecord medicalRecord)
    {
        ArgumentNullException.ThrowIfNull(medicalRecord, nameof(medicalRecord));

        var recordsList = _records.ToList();

        recordsList.Add(medicalRecord);

        _records = recordsList;

        return Task.CompletedTask;
    }

    public Task Update(MedicalRecord medicalRecord)
    {
        ArgumentNullException.ThrowIfNull(medicalRecord, nameof(medicalRecord));

        var recordsList = _records.ToList();

        var record = recordsList.SingleOrDefault(r => r.Id == medicalRecord.Id);

        if (record is null)
        {
            return Create(medicalRecord);
        }

        recordsList.Remove(record);
        recordsList.Add(medicalRecord);

        _records = recordsList;

        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        var recordsList = _records.ToList();

        var record = recordsList.SingleOrDefault(r => r.Id == id);

        if (record is null)
        {
            return Task.CompletedTask;
        }

        recordsList.Remove(record);

        _records = recordsList;

        return Task.CompletedTask;
    }
}
