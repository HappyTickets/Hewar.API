namespace Domain.Enums
{
    public enum Permissions
    {
        // roles
        CreateRole = 1,
        UpdateRole = 2,
        DeleteRole = 3,
        ViewRoles = 4,
        AssignUserToRole = 5,
        UnassignUserToRole = 6,

        // companies
        CreateCompany = 50,
        UpdateCompany = 51,
        DeleteCompany = 52,
        ViewCompanies = 53,

        // facilities
        CreateFacility = 100,
        UpdateFacility = 101,
        DeleteFacility = 102,
        ViewFacilities = 103,

        // guards
        CreateGuard = 150,
        UpdateGuard = 151,
        DeleteGuard = 152,
        ViewGuards = 153
    }
}
