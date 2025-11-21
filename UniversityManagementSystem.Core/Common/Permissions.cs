namespace UniversityManagementSystem.Core.Common;

public static class Permissions
{
    // User Management Permissions
    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";
        public const string Export = "Permissions.Users.Export";
    }

    // Role Management Permissions
    public static class Roles
    {
        public const string View = "Permissions.Roles.View";
        public const string Create = "Permissions.Roles.Create";
        public const string Edit = "Permissions.Roles.Edit";
        public const string Delete = "Permissions.Roles.Delete";
        public const string Assign = "Permissions.Roles.Assign";
    }

    // Academic Management Permissions
    public static class Academic
    {
        public const string View = "Permissions.Academic.View";
        public const string Manage = "Permissions.Academic.Manage";
        public const string Approve = "Permissions.Academic.Approve";
    }

    // Financial Management Permissions
    public static class Financial
    {
        public const string View = "Permissions.Financial.View";
        public const string Manage = "Permissions.Financial.Manage";
        public const string Approve = "Permissions.Financial.Approve";
    }

    // Library Management Permissions
    public static class Library
    {
        public const string View = "Permissions.Library.View";
        public const string Manage = "Permissions.Library.Manage";
    }

    public static List<string> GetAllPermissions()
    {
        return new List<string>
        {
            // Users
            Users.View, Users.Create, Users.Edit, Users.Delete, Users.Export,
            
            // Roles
            Roles.View, Roles.Create, Roles.Edit, Roles.Delete, Roles.Assign,
            
            // Academic
            Academic.View, Academic.Manage, Academic.Approve,
            
            // Financial
            Financial.View, Financial.Manage, Financial.Approve,
            
            // Library
            Library.View, Library.Manage
        };
    }
}
