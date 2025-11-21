namespace UniversityManagementSystem.Core.Enums;

public enum SemesterType
{
    Fall = 1,
    Spring,
    Summer
}

public enum SemesterStatus
{
    Upcoming = 1,
    RegistrationOpen,
    InProgress,
    Completed,
    Cancelled
}

public enum CourseOfferingStatus
{
    Draft = 1,
    Published,
    Cancelled,
    Full
}

public enum ClassType
{
    Lecture = 1,
    Lab,
    Tutorial,
    Workshop
}
