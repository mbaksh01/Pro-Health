namespace ProHealth.Services.Abstractions;

internal interface IAppointmentService
{
    /// <summary>
    /// Gets all <see cref="Appointment"/>s.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Appointment>> GetAllAsync();

    /// <summary>
    /// Gets a <see cref="Appointment"/> by its
    /// <see cref="Appointment.Id"/>.
    /// </summary>
    /// <param name="id">Id of <see cref="Appointment"/>.</param>
    /// <returns>
    /// The <see cref="Appointment"/>. <see langword="null"/> if it was not
    /// found.
    /// </returns>
    Task<Appointment?> GetByIdAsync(Guid id);

    /// <summary>
    /// Creates a <see cref="Appointment"/>.
    /// </summary>
    /// <param name="appointment">Record to create.</param>
    /// <returns>A <see cref="Task"/> representing this action.</returns>
    Task Create(Appointment appointment);

    /// <summary>
    /// Updates a <see cref="Appointment"/>.
    /// </summary>
    /// <param name="appointment">Record to update.</param>
    /// <returns>A <see cref="Task"/> representing this action.</returns>
    Task Update(Appointment appointment);

    /// <summary>
    /// Deletes a <see cref="Appointment"/>.
    /// </summary>
    /// <param name="id">Id of record to delete.</param>
    /// <returns>A <see cref="Task"/> representing this action.</returns>
    Task Delete(Guid id);
}
