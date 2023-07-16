namespace ProHealth.Helpers;

internal static class Mapper
{
    public static MedicalRecord MapGeneralFields(this MedicalRecord medicalRecord, FieldValue[] fieldValues)
    {
        medicalRecord.Patient.FamilyName = fieldValues[0].Value ?? "-";
        medicalRecord.Patient.Forename = fieldValues[1].Value ?? "-";
        medicalRecord.Patient.DateOfBirth = fieldValues[2].Value is null ? null : DateTime.Parse(fieldValues[2].Value!);
        medicalRecord.Patient.Gender = fieldValues[3].Value?.ToLower()?.StartsWith("m") ?? true ? Gender.Male : Gender.Female;

        return medicalRecord;
    }

    public static MedicalRecord MapAddressFields(this MedicalRecord medicalRecord, FieldValue[] fieldValues)
    {
        medicalRecord.Patient.Address.HouseNameOrNumber = fieldValues[0].Value ?? "-";
        medicalRecord.Patient.Address.Street1 = fieldValues[1].Value ?? "-";
        medicalRecord.Patient.Address.Street2 = fieldValues[2].Value ?? "-";
        medicalRecord.Patient.Address.City = fieldValues[3].Value ?? "-";
        medicalRecord.Patient.Address.County = fieldValues[4].Value ?? "-";
        medicalRecord.Patient.Address.Postcode = fieldValues[5].Value ?? "-";
        medicalRecord.Patient.Address.County = fieldValues[6].Value ?? "-";

        return medicalRecord;
    }

    public static MedicalRecord MapContactFields(this MedicalRecord medicalRecord, FieldValue[] fieldValues)
    {
        medicalRecord.Patient.ContactInfo.PrimaryContact = fieldValues[0].Value ?? "-";
        medicalRecord.Patient.ContactInfo.SecondaryContact = fieldValues[1].Value ?? "-";
        medicalRecord.NextOfKin.Name = fieldValues[2].Value ?? "-";
        medicalRecord.NextOfKin.Relationship = fieldValues[3].Value ?? "-";
        medicalRecord.NextOfKin.ContactInfo.PrimaryContact = fieldValues[4].Value ?? "-";
        medicalRecord.NextOfKin.ContactInfo.PrimaryContact = fieldValues[5].Value ?? "-";

        return medicalRecord;
    }
}
