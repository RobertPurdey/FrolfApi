namespace Frolf.Api.Models.Users
{
    public class AppUserCreationModel
    {
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Handle { get; set; }
    }
}