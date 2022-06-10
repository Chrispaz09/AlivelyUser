using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alively.Core.Entities
{
    public class User
    {
        public static User NotFound =
            new User() { Id = -1, FirstName = "Error. ", LastName = "User was not found. " };

        public static User NotValid =
            new User() { Id = -1, FirstName = "Error. ", LastName = "User information is not valid. " };

        [Key]
        public int Id { get; set; }

        public Guid Uuid { get; set; } = Guid.NewGuid();

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public int Age { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
