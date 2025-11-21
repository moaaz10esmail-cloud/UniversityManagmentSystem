namespace UniversityManagementSystem.Core.Enums;

public enum RegistrationStatus
{
    Pending = 1,
    Registered,
    Waitlisted,
    Dropped,
    Rejected,
    Completed
}

public enum RegistrationType
{
    Normal = 1,
    AddDrop,
    LateRegistration,
    Administrative
}
