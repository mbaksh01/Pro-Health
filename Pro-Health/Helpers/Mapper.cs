namespace ProHealth.Helpers;

internal static class Mapper
{
    public static MedicalRecord MapGeneralFields(this MedicalRecord medicalRecord, FieldValue[] fieldValues)
    {
        medicalRecord.Patient.FamilyName = string.IsNullOrWhiteSpace(fieldValues[0].Value) ? "-" : fieldValues[0].Value!;
        medicalRecord.Patient.Forename = string.IsNullOrWhiteSpace(fieldValues[1].Value) ? "-" : fieldValues[1].Value!;
        medicalRecord.Patient.DateOfBirth = string.IsNullOrWhiteSpace(fieldValues[2].Value) ? null : DateTime.Parse(fieldValues[2].Value!);
        medicalRecord.Patient.Gender = fieldValues[3].Value?.ToLower()?.StartsWith("m") ?? true ? Gender.Male : Gender.Female;

        return medicalRecord;
    }

    public static MedicalRecord MapAddressFields(this MedicalRecord medicalRecord, FieldValue[] fieldValues)
    {
        medicalRecord.Patient.Address.HouseNameOrNumber = string.IsNullOrWhiteSpace(fieldValues[0].Value) ? "-" : fieldValues[0].Value!;
        medicalRecord.Patient.Address.Street1 = string.IsNullOrWhiteSpace(fieldValues[1].Value) ? "-" : fieldValues[1].Value!;
        medicalRecord.Patient.Address.Street2 = string.IsNullOrWhiteSpace(fieldValues[2].Value) ? "-" : fieldValues[2].Value!;
        medicalRecord.Patient.Address.City = string.IsNullOrWhiteSpace(fieldValues[3].Value) ? "-" : fieldValues[3].Value!;
        medicalRecord.Patient.Address.County = string.IsNullOrWhiteSpace(fieldValues[4].Value) ? "-" : fieldValues[4].Value!;
        medicalRecord.Patient.Address.Postcode = string.IsNullOrWhiteSpace(fieldValues[5].Value) ? "-" : fieldValues[5].Value!;
        medicalRecord.Patient.Address.Country = string.IsNullOrWhiteSpace(fieldValues[6].Value) ? "-" : fieldValues[6].Value!;

        return medicalRecord;
    }

    public static MedicalRecord MapContactFields(this MedicalRecord medicalRecord, FieldValue[] fieldValues)
    {
        medicalRecord.Patient.ContactInfo.PrimaryContact = string.IsNullOrWhiteSpace(fieldValues[0].Value) ? "-" : fieldValues[0].Value!;
        medicalRecord.Patient.ContactInfo.SecondaryContact = string.IsNullOrWhiteSpace(fieldValues[1].Value) ? "-" : fieldValues[1].Value!;
        medicalRecord.NextOfKin.Name = string.IsNullOrWhiteSpace(fieldValues[2].Value) ? "-" : fieldValues[2].Value!;
        medicalRecord.NextOfKin.Relationship = string.IsNullOrWhiteSpace(fieldValues[3].Value) ? "-" : fieldValues[3].Value!;
        medicalRecord.NextOfKin.ContactInfo.PrimaryContact = string.IsNullOrWhiteSpace(fieldValues[4].Value) ? "-" : fieldValues[4].Value!;
        medicalRecord.NextOfKin.ContactInfo.PrimaryContact = string.IsNullOrWhiteSpace(fieldValues[5].Value) ? "-" : fieldValues[5].Value!;

        return medicalRecord;
    }

    public static Appointment MapFields(this Appointment appointment, FieldValue[] fieldValues)
    {
        appointment.DateOfAppointment = DateOnly.Parse(fieldValues[1].Value);
        appointment.TimeOfAppointment = TimeOnly.Parse(fieldValues[2].Value);
        appointment.Reason = fieldValues[3].Value;
        appointment.OtherReason = fieldValues[4].Value;
        appointment.Clinic = fieldValues[5].Value;

        return appointment;
    }
}
