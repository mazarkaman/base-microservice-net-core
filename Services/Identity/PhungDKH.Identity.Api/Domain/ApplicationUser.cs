namespace PhungDKH.Identity.Api.Domain
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string SecurityNumber { get; set; }

        [RegularExpression(@"(0[1-9]|1[0-2])\/[0-9]{2}", ErrorMessage = "Expiration should match a valid MM/YY value")]
        public string Expiration { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
