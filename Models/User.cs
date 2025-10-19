using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WEBKA.Models
{
    public class User : IdentityUser
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}
