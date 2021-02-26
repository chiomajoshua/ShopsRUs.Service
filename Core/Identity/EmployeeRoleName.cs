namespace ShopsRUs.Service.Core.Identity
{
    public class EmployeeRoleName
    {
        public const string AdminRoleName = "Administrators";
        public const string StaffRoleName = "Staffs";

        public static readonly EmployeeRoleName Admin = new EmployeeRoleName(AdminRoleName);
        public static readonly EmployeeRoleName Staff = new EmployeeRoleName(StaffRoleName);

        public readonly string Name;

        private EmployeeRoleName(string name)
        {
            Name = name;
        }

        public static string[] GetAllRoles()
        {
            return new[]
            {
                Admin.Name,
                Staff.Name
            };
        }
    }
}