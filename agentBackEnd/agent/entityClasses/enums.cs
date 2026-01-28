namespace agent.entityClasses
{
    public enum Gender
    {
        Male,
        Female,
        Other,
        PreferNotToSay
    }

    public enum RegistrationStatus
    {
        Pending,
        Approved,
        Rejected,
        Suspended
    }

    public enum EmploymentType
    {
        FullTime,
        PartTime,
        Contract,
        Temporary
    }

    public enum ApplicationStatus
    {
        Applied,
        UnderReview,
        Shortlisted,
        Interviewed,
        Accepted,
        Rejected,
        Withdrawn
    }
}