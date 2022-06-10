namespace Alivley.Api.DTOs
{
    public class UserDTO
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public int Age { get; set; }
    }
}
