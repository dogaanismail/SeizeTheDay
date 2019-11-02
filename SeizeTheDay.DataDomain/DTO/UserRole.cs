namespace SeizeTheDay.Entities.EntityClasses.MySQL
{
    public class UserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string IdentityUser_Id { get; set; }
        public string Discriminator { get; set; }

    }
}
