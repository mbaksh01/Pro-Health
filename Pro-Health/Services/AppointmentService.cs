using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace ProHealth.Services;

internal sealed class AppointmentService : IAppointmentService
{
    List<Appointment> _appointments = new();

    HttpClient _httpClient;

    public AppointmentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public Task Create(Appointment appointment)
    {
        _appointments.Add(appointment);

        return Task.CompletedTask;
    }

    public Task Delete(Guid id)
    {
        var appointment = _appointments.SingleOrDefault(a => a.Id == id);

        if (appointment is null)
        {
            return Task.CompletedTask;
        }

        _appointments.Remove(appointment);

        return Task.CompletedTask;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        if (_appointments.Any())
        {
            return _appointments;
        }

        var response = await _httpClient.GetStringAsync("/data/appointments.json");

        return _appointments = JsonSerializer.Deserialize<List<Appointment>>(response) ?? new();
    }

    public async Task<Appointment?> GetByIdAsync(Guid id)
    {
        if (_appointments.Any())
        {
            return _appointments.SingleOrDefault(a => a.Id == id);
        }

        var response = await _httpClient.GetStringAsync("/data/appointments.json");

        _appointments = JsonSerializer.Deserialize<List<Appointment>>(response) ?? new();

        return _appointments.SingleOrDefault(a => a.Id == id);
    }

    public Task Update(Appointment appointment)
    {
        var oldAppointment  = _appointments.SingleOrDefault(a => a.Id == appointment.Id);

        if (oldAppointment is null)
        {
            return Task.CompletedTask;
        }

        _appointments.Remove(oldAppointment);
        _appointments.Add(appointment);

        return Task.CompletedTask;
    }
}
