namespace ProHealth.Services.Abstractions;

/// <summary>
/// Medical records service used to interact with medical records.
/// </summary>
internal interface IMedicalRecordsService
{
    /// <summary>
    /// Gets all <see cref="MedicalRecord"/>s.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<MedicalRecord>> GetAllAsync();

    /// <summary>
    /// Gets a <see cref="MedicalRecord"/> by its
    /// <see cref="MedicalRecord.Id"/>.
    /// </summary>
    /// <param name="id">Id of <see cref="MedicalRecord"/>.</param>
    /// <returns>
    /// The <see cref="MedicalRecord"/>. <see langword="null"/> if it was not
    /// found.
    /// </returns>
    Task<MedicalRecord?> GetByIdAsync(Guid id);

    Task<MedicalRecord?> GetByNameAsync(string name);

    /// <summary>
    /// Creates a <see cref="MedicalRecord"/>.
    /// </summary>
    /// <param name="medicalRecord">Record to create.</param>
    /// <returns>A <see cref="Task"/> representing this action.</returns>
    Task Create(MedicalRecord medicalRecord);

    /// <summary>
    /// Updates a <see cref="MedicalRecord"/>.
    /// </summary>
    /// <param name="medicalRecord">Record to update.</param>
    /// <returns>A <see cref="Task"/> representing this action.</returns>
    Task Update(MedicalRecord medicalRecord);

    /// <summary>
    /// Deletes a <see cref="MedicalRecord"/>.
    /// </summary>
    /// <param name="id">Id of record to delete.</param>
    /// <returns>A <see cref="Task"/> representing this action.</returns>
    Task Delete(Guid id);
}
