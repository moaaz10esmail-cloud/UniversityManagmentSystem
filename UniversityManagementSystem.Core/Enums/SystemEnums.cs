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

public enum StudentStatus
{
    Active = 1,
    Inactive,
    Graduated,
    Suspended,
    Withdrawn
}

public enum AttendanceStatus
{
    Present = 1,
    Absent,
    Late,
    Excused,
    Sick
}

public enum InvoiceStatus
{
    Draft = 1,
    Issued,
    PartiallyPaid,
    Paid,
    Overdue,
    Cancelled
}

public enum PaymentMethod
{
    Cash = 1,
    CreditCard,
    DebitCard,
    BankTransfer,
    Check,
    Online
}

public enum PaymentStatus
{
    Pending = 1,
    Completed,
    Failed,
    Refunded,
    Cancelled
}

public enum BookCategory
{
    Science = 1,
    Engineering,
    Arts,
    Business,
    Medicine,
    Law,
    Literature,
    History,
    Other
}

public enum LoanStatus
{
    Active = 1,
    Returned,
    Overdue,
    Lost
}

public enum StaffPosition
{
    Professor = 1,
    AssociateProfessor,
    AssistantProfessor,
    Lecturer,
    TeachingAssistant,
    Researcher,
    Administrator,
    Librarian,
    ITSupport,
    SecurityGuard,
    Janitor,
    Other
}

public enum EmploymentStatus
{
    Active = 1,
    OnLeave,
    Suspended,
    Terminated,
    Retired
}

public enum LeaveType
{
    Sick = 1,
    Annual,
    Emergency,
    Maternity,
    Paternity,
    Unpaid
}

public enum LeaveStatus
{
    Pending = 1,
    Approved,
    Rejected,
    Cancelled
}
